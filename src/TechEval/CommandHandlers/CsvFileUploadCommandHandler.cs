using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechEval.CommandHandlers.Validators;
using TechEval.Commands;
using TechEval.Commands.Results;
using TechEval.Core;
using TechEval.DataContext;

namespace TechEval.CommandHandlers
{
    public class CsvFileUploadCommandHandler : FileUploadBase<CsvFileUploadCommand>,
        ICommandHandler<CsvFileUploadCommand, FileUploadCommandResult>
    {

        public CsvFileUploadCommandHandler(Db db, ILogger<CsvFileUploadCommand> logger) : base(db, logger)
        {
            Validator = new TransactionCsvValidator();
        }
        override public async Task<CommandResult<FileUploadCommandResult>> Execute(CsvFileUploadCommand command)
        {
            return await base.Execute(command);
        }


        protected override IEnumerable<TransactionModel> LoadFile(Stream file)
        {
            List<string> lines = new List<string>();
            try
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load file");
                throw;
            }

            foreach (var el in lines)
            {
                yield return GetTransaction(el); ;
            }
        }

        private TransactionModel GetTransaction(string el)
        {
            try
            {
                var reg = new Regex("\"([^\"]*)\"", RegexOptions.ECMAScript);
                var rec = reg.Matches(el);
                return new TransactionModel
                {
                    TransactionId = rec[0].Groups[1].Value,
                    Amount = rec[1].Groups[1].Value,
                    CurrencyCode = rec[2].Groups[1].Value,
                    TransactionDate = rec[3].Groups[1].Value,
                    Status = rec[4].Groups[1].Value,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to parse csv file. Unknown format.");
                throw new FormatUnknownException("Failed to parse csv file. Unknown format.", ex);
            }


        }
    }
}

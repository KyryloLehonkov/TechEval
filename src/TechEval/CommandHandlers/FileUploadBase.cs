using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechEval.CommandHandlers.Validators;
using TechEval.Commands;
using TechEval.Commands.Results;
using TechEval.Core;
using TechEval.DataContext;

namespace TechEval.CommandHandlers
{
    public abstract class FileUploadBase<T>
        where T : FileUploadCommandBase
    {
        protected readonly Db _db;
        protected readonly ILogger<T> _logger;

        protected List<ImportLog> FailedRecords = new List<ImportLog>();
        protected List<TransactionModel> SuccessRecords = new List<TransactionModel>();

        protected TransactionValidatorBase Validator = null;

        private IDictionary<int, string> _map = new Dictionary<int, string> {
                { 0, "A" },
                { 1, "R" },
                { 2, "D" }
            };

        public FileUploadBase(Db db, ILogger<T> logger)
        {
            _db = db;
            _logger = logger;
        }
        virtual public Task<CommandResult<FileUploadCommandResult>> Execute(T command)
        {
            var loadedRecords = LoadFile(command.UploadedFile);

            ValidateTransaction(loadedRecords);

            var isSuccess = !FailedRecords.Any();
            if (isSuccess)
            {
                var trs = MapToTransactions(SuccessRecords);
                _db.Transactions.AddRange(trs);
            }
            else
            {
                _db.ImportLogs.AddRange(FailedRecords);
            }

            _db.SaveChangesAsync();

            return Task.FromResult(new CommandResult<FileUploadCommandResult>
            {
                Value = new FileUploadCommandResult
                {
                    IsOk = isSuccess,
                    FailedRecords = FailedRecords
                }
            });
        }

        private void ValidateTransaction(IEnumerable<TransactionModel> loadedRecords)
        {
            foreach (var tr in loadedRecords)
            {
                var validationResults = Validator.Validate(tr);

                if (validationResults.IsValid) SuccessRecords.Add(tr);
                else
                {
                    FailedRecords.Add(new ImportLog
                    {
                        RecordSource = tr.TransactionId,
                        Reason = validationResults.ToString()
                    });
                }
            }
        }

        protected IEnumerable<Transaction> MapToTransactions(IEnumerable<TransactionModel> loadedRecords)
        {

            var mappedTr = loadedRecords.Select(c =>
                new Transaction
                {
                    Amount = decimal.Parse(c.Amount),
                    CurrencyCode = c.CurrencyCode,
                    TransactionDate = Validator.DateParse(c.TransactionDate),
                    TransationId = c.TransactionId,
                    Status = _map[Validator.GetStatusId(c.Status)]

                }
            );
            return mappedTr;
        }



        virtual protected IEnumerable<TransactionModel> LoadFile(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}

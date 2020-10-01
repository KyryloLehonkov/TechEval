using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TechEval.CommandHandlers.Validators;
using TechEval.Commands;
using TechEval.Commands.Results;
using TechEval.Core;
using TechEval.DataContext;

namespace TechEval.CommandHandlers
{
    public class XmlFileUploadCommandHandler : FileUploadBase<XmlFileUploadCommand>,
        ICommandHandler<XmlFileUploadCommand, FileUploadCommandResult>
    {

        public XmlFileUploadCommandHandler(Db db, ILogger<XmlFileUploadCommand> logger) : base(db, logger)
        {
            Validator = new TransactionXmlValidator();
        }
        override public async Task<CommandResult<FileUploadCommandResult>> Execute(XmlFileUploadCommand command)
        {
            return await base.Execute(command);
        }


        protected override IEnumerable<TransactionModel> LoadFile(Stream file)
        {
            XDocument doc;
            try
            {
                using (var reader = XmlReader.Create(file))
                {
                    doc = XDocument.Load(reader);

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load file");
                throw new FormatUnknownException("Failed to load file. Format unknown", ex);
            }

            foreach (var el in doc.Root.Elements("Transaction"))
            {

                yield return GetTransaction(el); ;
            }
        }

        private TransactionModel GetTransaction(XElement el)
        {
            return new TransactionModel
            {
                TransactionId = el.Attribute("id")?.Value,
                Amount = el.Element("PaymentDetails")?.Element("Amount").Value,
                CurrencyCode = el.Element("PaymentDetails")?.Element("CurrencyCode").Value,
                TransactionDate = el.Element("TransactionDate")?.Value,
                Status = el.Element("Status")?.Value
            };


        }
    }
}

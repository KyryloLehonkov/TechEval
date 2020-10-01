using FluentValidation;
using System;
using System.Globalization;

namespace TechEval.CommandHandlers.Validators
{
    public class TransactionCsvValidator : TransactionValidatorBase
    {
        public enum AllowedCsvStatuses
        {
            Approved,
            Failed,
            Finished
        }
        public TransactionCsvValidator() : base()
        {

        }

        override protected void RuleForStatus()
        {
            RuleFor(c => c.Status).NotEmpty().IsEnumName(typeof(AllowedCsvStatuses), false);
        }

        override protected bool BeValidDateTime(string transactionDate)
        {
            return DateTime.TryParseExact(transactionDate, "dd/MM/yyyy hh:mm:ss", null, DateTimeStyles.AssumeUniversal, out _);
        }

        public override DateTime DateParse(string stDate)
        {
            return DateTime.ParseExact(stDate, "dd/MM/yyyy hh:mm:ss", null, DateTimeStyles.AssumeUniversal);
        }
        override public int GetStatusId(string status)
        {
            return (int)Enum.Parse(typeof(AllowedCsvStatuses), status, true);
        }
    }


}

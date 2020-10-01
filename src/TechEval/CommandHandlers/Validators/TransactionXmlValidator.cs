using FluentValidation;
using System;

namespace TechEval.CommandHandlers.Validators
{
    public class TransactionXmlValidator : TransactionValidatorBase
    {
        public enum AllowedXmlStatuses
        {
            Approved,
            Rejected,
            Done
        }
        public TransactionXmlValidator():base()
        {
           
        }

        override protected void RuleForStatus()
        {
            RuleFor(c => c.Status).NotEmpty().IsEnumName(typeof(AllowedXmlStatuses), false);

        }

        override protected bool BeValidDateTime(string transactionDate)
        {
            return DateTime.TryParse(transactionDate, out _);
        }
       

        override public int GetStatusId(string status)
        {
            return (int)Enum.Parse(typeof(AllowedXmlStatuses), status, true);
        }
    }

   
}

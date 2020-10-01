using FluentValidation;
using System;

namespace TechEval.CommandHandlers.Validators
{
    public class TransactionValidatorBase : AbstractValidator<TransactionModel>
    {
      
        public TransactionValidatorBase()
        {
            RuleFor(c => c.TransactionId).NotEmpty().MaximumLength(50);
            RuleFor(c => c.TransactionDate).NotEmpty().Must(BeValidDateTime);
            RuleFor(c => c.Amount).NotEmpty().Must(BeValidDecimal);
            RuleFor(c => c.CurrencyCode).NotEmpty().Length(3);

            RuleForStatus();
        }

        virtual protected void RuleForStatus()
        {
            throw new NotImplementedException();
        }

        virtual protected bool BeValidDateTime(string transactionDate)
        {
            throw new NotImplementedException();
        }
        virtual protected bool BeValidDecimal(string amount)
        {
            return decimal.TryParse(amount, out _);
        }

        virtual public int GetStatusId(string status)
        {
            throw new NotImplementedException();
        }

        virtual public DateTime DateParse(string stDate)
        {
            return DateTime.Parse(stDate);
        }
    }

    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }



    }
}

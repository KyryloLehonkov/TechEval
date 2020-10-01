using System;

namespace TechEval.Core
{
    public class DomainException :Exception
    {
        public DomainException(string message, Exception ex) :base (message, ex)
        {

        }
    }

    public class FormatUnknownException : DomainException
    {
        public FormatUnknownException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}

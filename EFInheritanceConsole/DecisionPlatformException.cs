using System;

namespace EFInheritanceConsole
{
    public class DecisionPlatformException : ApplicationException
    {
        public DecisionPlatformException()
        {
        }

        public DecisionPlatformException(string message)
            : base(message)
        {
        }

        public DecisionPlatformException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
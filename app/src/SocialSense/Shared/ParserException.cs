using System;

namespace SocialSense.Shared
{
    public class ParserException : ApplicationException
    {
        public ParserException() { }
        public ParserException(string message) : base(message) { }
    }
}

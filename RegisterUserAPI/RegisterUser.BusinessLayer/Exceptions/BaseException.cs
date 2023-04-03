using System;

namespace RegisterUser.BusinessLayer.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string ErrorMessageResourceKey => this.GetType().Name.Replace("Exception", "");
        public string ErrorType { get; set; }

        protected BaseException()
        {
        }

        protected BaseException(string errorType)
        {
            ErrorType = errorType;
        }
    }
}

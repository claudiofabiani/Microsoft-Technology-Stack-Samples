using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exception
{
    public class BusinessLogicValidationException
       : BaseException
    {

        #region Properties

        /// <summary>
        /// Get or set the validation errors messages
        /// </summary>
        public IEnumerable<string> ValidationErrors { get; }

        #endregion

        #region constructors

        public BusinessLogicValidationException()
        {
        }

        public BusinessLogicValidationException(string message)
            : base(message)
        {
        }


        public BusinessLogicValidationException(string message, IEnumerable<string> validationErrors)
            : base(string.Join(Environment.NewLine, validationErrors))
        {
            this.ValidationErrors = validationErrors;
        }

        #endregion
    }


    public class BusinessLogicValidationException<TExceptionCode>
     : BusinessLogicValidationException where TExceptionCode : struct
    {

        #region Properties

        public TExceptionCode ExceptionCode { get; }

        #endregion

        #region constructors

        public BusinessLogicValidationException()
            : base()
        {
            ExceptionCode = default(TExceptionCode);
            Data.Add("ExceptionCode", ExceptionCode);
        }

        public BusinessLogicValidationException(TExceptionCode exceptionCode, string message)
            : base(message)
        {
            ExceptionCode = exceptionCode;
            Data.Add("ExceptionCode", ExceptionCode);
        }


        public BusinessLogicValidationException(TExceptionCode exceptionCode, IEnumerable<string> validationErrors)
            : base(string.Join(Environment.NewLine, validationErrors), validationErrors)
        {
            ExceptionCode = exceptionCode;
            Data.Add("ExceptionCode", ExceptionCode);

        }


        #endregion
    }
}

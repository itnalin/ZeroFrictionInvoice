using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Domain.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        private readonly string _errorCode;

        public string ErrorCode
        {
            get { return _errorCode; }
        }

        public BusinessException(string errorCode, string message) : base(message)
        {
            _errorCode = errorCode;
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            _errorCode = info.GetString("ErrorCode");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("ErrorCode", _errorCode);
            }
        }
    }
}

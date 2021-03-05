using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Exception
{
    public class BaseException: System.Exception, ISerializable
    {
        public BaseException()
            : base()
        {
            // Add implementation (if required)
        }

        public BaseException(string message)
            : base(message)
        {
            // Add implementation (if required)
        }

        public BaseException(string message, System.Exception inner)
            : base(message, inner)
        {
            // Add implementation (if required)
        }

        public BaseException(string message, System.Exception inner, IDictionary data)
            : base(message, inner)
        {
            if (data != null)
            {
                foreach (var key in data.Keys)
                {
                    this.Data.Add(key, data[key]);
                }
            }
        }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation (if required)
        }
    }
}

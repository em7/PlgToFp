using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlpToFp.Core
{

    [Serializable]
    public class FlightPlanConvertException : Exception, ISerializable
    {
        public FlightPlanConvertException() { }
        public FlightPlanConvertException(string message) : base(message) { }
        public FlightPlanConvertException(string message, Exception inner) : base(message, inner) { }

        protected FlightPlanConvertException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}

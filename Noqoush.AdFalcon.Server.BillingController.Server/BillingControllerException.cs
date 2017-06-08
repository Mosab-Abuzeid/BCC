using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noqoush.AdFalcon.Server.BillingController.Server
{
    [Serializable]
    public class BillingControllerException : Exception
    {
        public BillingControllerException() { }
        public BillingControllerException(string message) : base(message) { }
        public BillingControllerException(string message, Exception inner) : base(message, inner) { }
        protected BillingControllerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

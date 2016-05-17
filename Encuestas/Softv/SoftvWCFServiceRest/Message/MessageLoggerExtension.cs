using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace SoftvWCFService
{
    public class MessageLoggerExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new MessageInspector();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(MessageInspector);
            }
        }
    }
}
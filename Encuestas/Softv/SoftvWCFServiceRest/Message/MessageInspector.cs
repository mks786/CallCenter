using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Diagnostics.Eventing;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Configuration;

namespace SoftvWCFService
{
    public class MessageInspector : IDispatchMessageInspector, IServiceBehavior
    {
        #region IDispatchMessageInspector
        List<String> lstInvaliModules;
        List<String> lstInvaliAction;
        public MessageInspector()
        {
            lstInvaliModules = ConfigurationManager.AppSettings["NoRegisterInBitacoraModules"].Split(',').ToList();
            lstInvaliAction = ConfigurationManager.AppSettings["NoRegisterInBitacoraStartWith"].Split(',').ToList();
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel,
            InstanceContext instanceContext)
        {
            List<String> lstUriAction = request.Headers.Action.ToString().Split('/').ToList();
            String Action = lstUriAction.Last().ToUpper();
            String Module = lstUriAction[lstUriAction.Count() - 2].Remove(0, 1).ToUpper();
            String RemoteIp = lstUriAction[lstUriAction.Count() - 2].Remove(0, 1).ToUpper();
            String UserName = lstUriAction[lstUriAction.Count() - 2].Remove(0, 1).ToUpper();
            if (!lstInvaliModules.Contains(Module) && !(lstInvaliAction.Where(x => (Action.StartsWith(x) || lstInvaliAction.Contains(Action))).Any()))
            {
                XElement xe = XElement.Parse(RemoveXmlns(request.ToString()).InnerXml);
                xe = xe.XPathSelectElement("//Body/" + lstUriAction.Last());
                XElement xeUser = xe.XPathSelectElement("//BaseIdUser");
                XElement xeIp = xe.XPathSelectElement("//BaseRemoteIp");
                int BaseIdUser = int.Parse(xeUser.Value.ToString());
                String BaseRemoteIp = xeIp.Value.ToString();
                xeUser.Remove();
                xeIp.Remove();
                //Bitacora.Add(new BitacoraEntity() { Accion = Action, Modulo = Module, Fecha = DateTime.Now, Descripcion = xe.ToString(), IdUsuario = BaseIdUser, IP = BaseRemoteIp });
            }
            return null;
        }

        public static XmlDocument RemoveXmlns(String xml)
        {
            XDocument d = XDocument.Parse(xml);
            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            d.Root.Descendants().Attributes().Where(x => x.Name.Namespace != "").Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(d.CreateReader());

            return xmlDocument;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        #endregion

        #region IServiceBehavior

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new MessageInspector());
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftvMVC
{
    public class TrackUserIP
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Debug.WriteLine("Inside OnActionExecuting");
            string userIP = filterContext.HttpContext.Request.UserHostAddress;
            LogIP(filterContext.HttpContext.Request.Url.PathAndQuery, userIP, "Attempted");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Debug.WriteLine("Inside OnActionExecuted");
            string userIP = filterContext.HttpContext.Request.UserHostAddress;
            LogIP(filterContext.HttpContext.Request.Url.PathAndQuery, userIP, "Completed");
        }

        private void LogIP(string url, string ip, string msg)
        {
            Debug.WriteLine(msg + " : " + url + "[" + ip + "] on " + DateTime.Now.ToString());
        }

    }
}
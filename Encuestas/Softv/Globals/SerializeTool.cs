using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Globals
{
    public static class SerializeTool
    {
        public static string Serialize<T>(T value)
        {
            Type objType = value.GetType();
            XElement xe = new XElement(objType.Name);
            foreach (var prop in objType.GetProperties())
            {
                if (prop.GetValue(value, null) != null)
                {
                    xe.Add(new XAttribute(prop.Name, prop.GetValue(value, null).ToString()));
                }
            }
            return xe.ToString();
        }







        public static string SerializeList<T>(List<T> value, String RootName = "root")
        {
            Type objType = null;
            XElement xe = new XElement(RootName);
            XElement xei = null;
            if (value != null)
                foreach (var i in value)
                {
                    objType = i.GetType();
                    xei = new XElement(objType.Name);
                    foreach (var prop in objType.GetProperties())
                    {
                        if (prop.GetValue(i, null) != null)
                        {
                            xei.Add(new XAttribute(prop.Name, prop.GetValue(i, null).ToString()));
                        }
                    }
                    xe.Add(xei);
                }
            return xe.ToString();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace RESTClient
{
    /*This static class holds function to perform HTTP request*/
    public static class RestManager
    {
        /*Performs a get request on a given uri*/
        public static HttpWebResponse GetResponse(string uri)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(uri));

            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();

                return null;
            }

            return res;
        }

        /*Retrieve an Xml document from a given uri*/
        public static XmlDocument GetXmlResponse(string uri)
        {
            XmlDocument doc = new XmlDocument();

            HttpWebResponse res = GetResponse(uri);

            if (res != null)
                doc.Load(res.GetResponseStream());
            else
            {
                return null;
            }

            res.Close();

            return doc;
        }

        /*Performs a POST request on a given uri with the given xml*/
        public static HttpWebResponse PostXmlRequest(string uri, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            request.Method = "Post";
            request.ContentType = "application/xml";
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(postData);

            doc.Save(request.GetRequestStream());

            HttpWebResponse res;
            
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();

                return null;
            }

            return res;
        }

        /*Performs a PUT request on a given uri with the given putData*/
        public static HttpWebResponse PutXmlRequest(string uri, string putData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            request.Method = "Put";
            request.ContentType = "application/xml";
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(putData);

            doc.Save(request.GetRequestStream());

            HttpWebResponse res;

            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();

                return null;
            }

            return res;
        }

        /*Performs a Delete request on a given uri*/
        public static void DeleteRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            request.Method = "DELETE";

            Console.WriteLine(uri);

            HttpWebResponse res;

            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();
                return;
            }

            res.Close();
        }
    }
}

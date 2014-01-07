using System;
using System.Text;
using System.Xml;
using System.Net;

using Shared.Services;

namespace Services {
    /*This static class holds function to perform HTTP request*/
    public class RestManager : IRestService
    {
        /*Performs a get request on a given uri*/
        public HttpWebResponse GetResponse(string uri) {
            return Request(uri, "GET", null);
        }

        /*Retrieve an Xml document from a given uri*/
        public XmlDocument GetXmlResponse(string uri) {
            XmlDocument doc = new XmlDocument();

            HttpWebResponse res = GetResponse(uri);

            if (res != null) {
                doc.Load(res.GetResponseStream());
                res.Close();
            } else
                doc = null;

            return doc;
        }

        /*Performs a POST request on a given uri with the given xml*/
        public HttpWebResponse PostXmlRequest(string uri, string postData) {
            return XmlRequest(uri, "POST", postData);
        }

        /*Performs a PUT request on a given uri with the given putData*/
        public HttpWebResponse PutXmlRequest(string uri, string putData) {
            return XmlRequest(uri, "PUT", putData);
        }

        /*Performs a Delete request on a given uri*/
        public void DeleteRequest(string uri) {
            Request(uri, "DELETE", null);
        }

        protected virtual HttpWebResponse Request(string uri, string method, string contentType) {
            HttpWebRequest request = CreateRequestObject(uri, method, contentType);
            return GetResponse(request);
        }

        protected virtual HttpWebResponse XmlRequest(string uri, string method, string data) {
            HttpWebRequest request = CreateRequestObject(uri, method, "application/xml");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            doc.Save(request.GetRequestStream());

            return GetResponse(request);
        }

        private HttpWebResponse GetResponse(HttpWebRequest request) {
            HttpWebResponse res = null;

            try {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx) {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();
                res = null;
            }

            return res;
        }

        private HttpWebRequest CreateRequestObject(string uri, string method, string contentType) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            if (method != null)
              request.Method = method;
            if (contentType != null)
              request.ContentType = contentType;
            return request;
        }
    }
}

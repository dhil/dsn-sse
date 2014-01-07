using System;
using System.Xml;
using System.Net;

namespace Shared.Services {
    public interface IRestService {
        HttpWebResponse GetResponse(string uri);
        XmlDocument GetXmlResponse(string uri);
        HttpWebResponse PutXmlRequest(string uri, string putData);
        HttpWebResponse PostXmlRequest(string uri, string postData);
        void DeleteRequest(string uri);
    }
}


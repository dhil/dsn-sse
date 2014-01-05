using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RESTClient
{
    public static class TwittManager
    {

        /*Retrieve a twitt user from a given screen name, see https://dev.twitter.com/docs/api/1/get/users/show*/
        public static TwittUser GetTwittUser(string screenName)
        {
            string uri = "https://api.twitter.com/1/users/show.xml?screen_name=" + screenName;
            XmlDocument doc;
            doc = RestManager.GetXmlResponse(uri);
            if (doc == null)
                return null;

            XmlNode user = doc.SelectSingleNode("user");
            TwittUser tu = new TwittUser(user["id"].InnerText, user["name"].InnerText, user["screen_name"].InnerText,
                    user["location"].InnerText, user["description"].InnerText);
            return tu;
        }
    }
}

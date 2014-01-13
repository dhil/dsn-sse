using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace RESTClient
{
    /*This class is the data model for a twitt user*/
    /*Serializable enable to serialize this object*/
    [Serializable()]
    public class TwittStatus
    {
        /*Fields are declared public for conveniance*/
        public string id;
        public string userId;
        public string text;
        public DateTime createdAt;

        /*Empty constructor required to serialize*/
        public TwittStatus() { }

        /*Basic contructor*/
        public TwittStatus(string id, string userId, string text, DateTime createdAt)
        {
            this.id = id;
            this.text = text;
            this.userId = userId;
            this.createdAt = createdAt;
        }

        /*Print the object in clear text*/
        public void printOut()
        {
            Console.WriteLine("Id: " + this.id);
            Console.WriteLine("UserId: " + this.userId);
            Console.WriteLine("Text: " + this.text);
            Console.WriteLine("CreatedAt: " + this.createdAt);
        }

        /*Serialize the object to an xml string*/
        public string toXmlString()
        {
            XmlSerializer xs = new XmlSerializer(this.GetType());

            using (StringWriter writer = new StringWriter())
            {
                xs.Serialize(writer, this);

                return writer.ToString();
            }

        }
    }
}

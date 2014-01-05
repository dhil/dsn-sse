using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Models
{
    /*This class is the data model for a twitt user*/
    /*Serializable enable to serialize this object*/
    [Serializable()]
    public class TwitterUser
    {
        /*Fields are declared public for conveniance*/
        public string id;
        public string name;
        public string screenName;
        public string location;
        public string description;

        /*Empty constructor required to serialize*/
        public TwitterUser() { }

        /*Basic contructor*/
        public TwitterUser(string id, string name, string screenName, string location, string description)
        {
            this.id = id;
            this.name = name;
            this.screenName = screenName;
            this.location = location;
            this.description = description;
        }

        /*Print the object in clear text*/
        public void printOut()
        {
            Console.WriteLine("Id: " + this.id);
            Console.WriteLine("Name: " + this.name);
            Console.WriteLine("Screen Name: " + this.screenName);
            Console.WriteLine("Location: " + this.location);
            Console.WriteLine("Description: " + this.description);
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

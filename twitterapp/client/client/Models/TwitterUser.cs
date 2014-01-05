using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Shared.Models;

namespace Models
{
    /*This class is the data model for a twitt user*/
    /*Serializable enable to serialize this object*/
    [Serializable()]
    public class TwitterUser : ITwitterUser
    {
        #region ITwitterUser implementation
        public string Id {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string ScreenName {
            get; set;
        }

        public string Location {
            get; set;
        }

        public string Description {
            get; set;
        }
        #endregion

        /*Empty constructor required to serialize*/
        public TwitterUser() { }

        /*Basic contructor*/
        public TwitterUser(string id, string name, string screenName, string location, string description)
        {
            this.Id = id;
            this.Name = name;
            this.ScreenName = screenName;
            this.Location = location;
            this.Description = description;
        }

        /*Print the object in clear text*/
        public override string ToString() {
            return string.Format("{0,11}: {1}\n{2,11}: {3}\n{4,11}: {5}\n{6,11}: {7}\n{8,11}: {9}", 
                                 "Id", this.Id, 
                                 "Name", this.Name, 
                                 "ScreenName", this.ScreenName, 
                                 "Location", this.Location, 
                                 "Description", this.Description
                                 );
        }

        /*Serialize the object to an xml string*/
        public string ToXmlString() {
            XmlSerializer xs = new XmlSerializer(this.GetType());

            using (StringWriter writer = new StringWriter()) {
                xs.Serialize(writer, this);

                return writer.ToString();
            }
        }
    }
}

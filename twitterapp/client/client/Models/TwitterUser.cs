using System;

using Shared.Models;

namespace Models {
    public class TwitterUser : SelfXmlSerializableObject, ITwitterUser {
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

        /* Generate pretty printable string representation of the object */
        public override string ToString() {
            return string.Format("{0,11}: {1}\n{2,11}: {3}\n{4,11}: {5}\n{6,11}: {7}\n{8,11}: {9}", 
                                 "Id", this.Id, 
                                 "Name", this.Name, 
                                 "ScreenName", this.ScreenName, 
                                 "Location", this.Location, 
                                 "Description", this.Description
                                 );
        }
    }
}

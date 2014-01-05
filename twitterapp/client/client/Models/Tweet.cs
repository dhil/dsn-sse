using System;
using System.Xml.Serialization;

using Shared.Models;

namespace Models {
    public class TwitterStatus : SelfXmlSerializableObject, ITweet {
        #region Constructors
        /* Empty constructor */
        public TwitterStatus() {
        }
        #endregion

        #region ITweet implementation
        [XmlElement("UserId", typeof(string))]
        public ITwitterUser Author {
            get; set;
        }

        public string Id {
            get; set;
        }

        public string Text {
            get; set;
        }

        public DateTime Date {
            get; set; 
        }
        #endregion
    }
}


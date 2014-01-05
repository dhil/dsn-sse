using System;
using System.Xml.Serialization;

using Shared.Models;

namespace Models {
    public class Tweet : SelfXmlSerializableObject, ITweet {
        #region Constructors
        /* Empty constructor */
        public Tweet() {
        }

        public Tweet(string id, ITwitterUser author, string text, DateTime createdAt) {
            Id = id;
            Author = author;
            Text = text;
            CreatedAt = createdAt;
        }
        #endregion

        #region ITweet implementation
        [XmlIgnore]
        public ITwitterUser Author {
            get; set;
        }

        public string UserId {
            get { return Author.Id; }
            set {}
        }

        public string Id {
            get; set;
        }

        public string Text {
            get; set;
        }

        public DateTime CreatedAt {
            get; set; 
        }
        #endregion
    }
}


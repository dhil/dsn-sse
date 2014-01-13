using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RESTClient {
    [Serializable()]
    [XmlRoot("TwittStatuses")]
    public class TwittStatuses {
        public TwittStatuses() {
        }

        [XmlElement("TwittStatus")]
        public List<TwittStatus> Items { get; set; }
    }
}


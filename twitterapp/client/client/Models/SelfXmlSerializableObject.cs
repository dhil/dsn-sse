using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Shared.Models;

namespace Models {
    /*This class is the data model for a twitt user*/
    /*Serializable enable to serialize this object*/
    [Serializable()]
    public abstract class SelfXmlSerializableObject : Shared.Models.IXmlSerializable {
        #region Implementation of IXmlSerializable
        public virtual string ToXmlString() {
            XmlSerializer xs = new XmlSerializer(this.GetType());

            using (MemoryStream ms = new MemoryStream()) {
                using (XmlWriter writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 })) {
                    xs.Serialize(writer, this); // Serialize me!
                    ms.Position = 0; // rewind stream
                    using (StreamReader sr = new StreamReader(ms)) {
                        return sr.ReadToEnd(); // Generate string representation of the stream contents
                    }
                }
            }
        }
        #endregion
    }
}


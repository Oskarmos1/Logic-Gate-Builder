using System.Collections.Generic;
using System.Xml.Serialization;

namespace Logic_Gate_Builder.Logic_Gate_Classes
{
    [XmlRoot("Gate")]
    public class GateData
    {
        [XmlElement("GateType")]
        public string gateType { get; set; }
        
        [XmlElement("GateName")]
        public string gateName { get; set; }

        [XmlElement("Minterms")]
        public string minterms { get; set; }

        [XmlArray("Inputs")]
        [XmlArrayItem("Input")]
        public List<string> inputs { get; set; }

        public GateData() {
            inputs = new List<string>();

        }
    }
}

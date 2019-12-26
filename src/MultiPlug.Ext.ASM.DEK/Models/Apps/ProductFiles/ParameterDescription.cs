
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles
{
    [Serializable]
    [XmlRoot("ParameterDefaults")]
    public class Defaults
    {
        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public List<ParameterDescription> Parameters { get; set; }
    }

    [Serializable]
    public class ParameterDescription
    {
        public enum Types
        {
            Unknown = -1,
            NumericDouble = 0,
            String = 7,
            NumericShort = 8
        }

        public Types Type { get; set; }

        public short Id { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public string Size { get; set; }
        [XmlIgnore]
        public string StringValue { get; set; }
        [XmlIgnore]
        public double DoubleValue { get; set; }
        [XmlIgnore]
        public ushort ShortValue { get; set; }

        public override string ToString()
        {
            switch( Type )
            {
                case Types.NumericDouble:
                    return string.Format("{0:0.00}", DoubleValue);
                case Types.NumericShort:
                    return ShortValue.ToString();
                case Types.String:
                    return StringValue;
                case Types.Unknown:
                    return "";
            }

            return "";
        }
    }
}

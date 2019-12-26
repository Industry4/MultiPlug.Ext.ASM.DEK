using System.Collections.Generic;
using System.Xml.Serialization;

namespace MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles.DEKResources
{
    [XmlRoot(ElementName = "Text")]
    public class Textb
    {
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "TextResources")]
    public class TextResources
    {
        [XmlElement(ElementName = "Text")]
        public List<Textb> Text { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "DateTime")]
        public string DateTime { get; set; }
        [XmlAttribute(AttributeName = "Language")]
        public string Language { get; set; }
        [XmlAttribute(AttributeName = "EnglishName")]
        public string EnglishName { get; set; }
        [XmlAttribute(AttributeName = "NativeName")]
        public string NativeName { get; set; }
    }

    [XmlRoot(ElementName = "Resources")]
    public class Resources
    {
        [XmlElement(ElementName = "TextResources")]
        public TextResources TextResources { get; set; }
    }

}

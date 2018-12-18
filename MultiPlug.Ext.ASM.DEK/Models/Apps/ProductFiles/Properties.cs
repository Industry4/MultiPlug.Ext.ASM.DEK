using System.Runtime.Serialization;

namespace MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles
{
    public class Properties
    {
        [DataMember]
        public string ProductFilesPath { get; set; } = "E:\\product\\";
        [DataMember]
        public bool Enabled { get; set; } = true;
    }
}

using System.Runtime.Serialization;

namespace MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles
{
    public class Properties
    {
        [DataMember]
        public string ProductFilesPath { get; set; } = "E:\\product\\";

        [DataMember]
        public string DEKResourcesFolderPath { get; set; } = "C:\\Program Files\\DEK\\Printer\\Resources";

        [DataMember]
        public bool Enabled { get; set; } = true;
    }
}

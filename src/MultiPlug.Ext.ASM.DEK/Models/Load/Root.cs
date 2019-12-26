using System.Runtime.Serialization;

namespace MultiPlug.Ext.ASM.DEK.Models.Load
{
    public class Root
    {
        [DataMember]
        public Models.Apps.ProductFiles.Properties ProductFilesApp { get; set; }
    }
}

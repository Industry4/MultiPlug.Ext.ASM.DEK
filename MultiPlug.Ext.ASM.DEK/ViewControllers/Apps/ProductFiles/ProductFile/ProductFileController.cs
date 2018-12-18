using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Http;
using MultiPlug.Ext.ASM.DEK.Properties;
using MultiPlug.Base.Attribute;
using MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles.ProductFile
{
    [Route("productfile")]
    class ProductFileController : Controller
    {
        public Response Get(int index)
        {
            var myFiles = Directory.GetFiles(Core.Instance.ProductFilesApp.ProductFilesPath, "*.pr1*");

            if( myFiles.Length == 0 || index < 0 || index > myFiles.Length)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            var Parameters = new List<ParameterDescription>();

            using (BinaryReader b = new BinaryReader(File.Open(myFiles[index], FileMode.Open)))
            {
                int Position = 0;

                long FileLength = b.BaseStream.Length;

                while (Position < FileLength)
                {
                    short ParamId = b.ReadInt16();
                    Position += sizeof(short);

                    short DataSize = b.ReadInt16();
                    Position += sizeof(short);

                    byte[] Data = b.ReadBytes(DataSize);
                    Position += DataSize;

                    if (DataSize == 8)  // Guess it will be a double.
                    {
                        Parameters.Add(new ParameterDescription { Id = ParamId.ToString(), IdAsShort = ParamId, Size = DataSize.ToString(), AsString = string.Format("{0:0.00}", BitConverter.ToDouble(Data, 0)) });
                    }
                    else if (DataSize == 2)
                    {
                        // TODO
                        BitConverter.ToInt16(Data, 0);
                    }
                    else // TODO
                    {
                        Parameters.Add(new ParameterDescription { Id = ParamId.ToString(), IdAsShort = ParamId, Size = DataSize.ToString(), AsString = Encoding.ASCII.GetString(Data) });
                    }
                }
            }

            return new Response
            {
                Model = new Models.Apps.ProductFiles.ProductFile { Parameters = Parameters.OrderBy( p => p.IdAsShort).ToList() },
                ModelType = typeof(Models.Apps.ProductFiles.ProductFile),
                Template = new KeyValuePair<string, string>("GetProductFile", Resources.ProductFile)
            };
        }
    }
}

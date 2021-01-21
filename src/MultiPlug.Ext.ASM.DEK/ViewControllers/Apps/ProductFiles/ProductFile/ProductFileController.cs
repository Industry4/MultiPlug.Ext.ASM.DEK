using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Http;
using MultiPlug.Base.Attribute;
using MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles.ProductFile
{
    [Route("productfile")]
    public class ProductFileController : ProductFilesApp
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

            bool NewParameterFound = false;

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

                    ParameterDescription Description = null;

                    if( Core.Instance.ParameterDescriptions.TryGetValue(ParamId, out Description) )
                    {
                        var pd = new ParameterDescription { Type = Description.Type, Id = ParamId, Size = DataSize.ToString(), Name = Description.Name };

                        switch ( Description.Type)
                        {
                            case ParameterDescription.Types.NumericShort:
                                pd.ShortValue = BitConverter.ToUInt16(Data, 0);
                                break;
                            case ParameterDescription.Types.NumericDouble:
                                pd.DoubleValue = BitConverter.ToDouble(Data, 0);
                                break;
                            case ParameterDescription.Types.String:
                                pd.StringValue = Encoding.ASCII.GetString(Data);
                                break;
                        }

                        Parameters.Add(pd);
                    }
                    else // Lets Guess what the parameter might be until the config file has been updated via the Setting page.
                    {
                        NewParameterFound = true;

                        if (DataSize == 2)
                        {
                            Parameters.Add( new ParameterDescription { Type = ParameterDescription.Types.NumericShort, Id = ParamId, Size = DataSize.ToString(), ShortValue = BitConverter.ToUInt16(Data, 0) } );
                            Core.Instance.ParameterDescriptions.Add(ParamId, new ParameterDescription { Type = ParameterDescription.Types.NumericShort, Id = ParamId });
                        }
                        else if (DataSize == 8)
                        {
                            Parameters.Add(new ParameterDescription { Type = ParameterDescription.Types.NumericDouble, Id = ParamId, Size = DataSize.ToString(), DoubleValue = BitConverter.ToDouble(Data, 0) });
                            Core.Instance.ParameterDescriptions.Add(ParamId, new ParameterDescription { Type = ParameterDescription.Types.NumericDouble, Id = ParamId });
                        }
                        else
                        {
                            Parameters.Add(new ParameterDescription { Type = ParameterDescription.Types.String, Id = ParamId, Size = DataSize.ToString(), StringValue = Encoding.ASCII.GetString(Data) });
                            Core.Instance.ParameterDescriptions.Add(ParamId, new ParameterDescription { Type = ParameterDescription.Types.String, Id = ParamId });
                        }
                    }
                }
            }

            if (NewParameterFound)
            {
                Core.Instance.SaveDefaultsFile();
            }

            return new Response
            {
                Model = new Models.Apps.ProductFiles.ProductFile { Parameters = Parameters.OrderBy( p => p.Id).ToList() },
                Template = "GetProductFile"
            };
        }
    }
}

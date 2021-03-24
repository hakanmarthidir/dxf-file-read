using System;
using System.IO;
using netDxf;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using netDxf.Header;

namespace AutoCadDfx
{
    public static class DxfManager
    {

        private static bool IsValid(string dxfFile)
        {
            DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(dxfFile);
            if (dxfVersion < DxfVersion.AutoCad2000)
                return false;

            return true;
        }

        public static DxfDocument LoadDxfDocument(string dfxPath)
        {
            if (string.IsNullOrEmpty(dfxPath))
                throw new ArgumentNullException("filepath or layername is empty");

            if (!File.Exists(dfxPath))
                throw new FileNotFoundException("DFX file not found");

            if (IsValid(dfxPath))
                return DxfDocument.Load(dfxPath);

            throw new FormatException("Dxf version is not compatible");
        }

        public static IList<DxfPolylineModel> ReadLwPolylinesByLayerName(DxfDocument dxfDocument, string layerName)
        {

            if (dxfDocument == null)
                throw new NullReferenceException("dxfdocument is null");

            if (string.IsNullOrEmpty(layerName))
                throw new ArgumentNullException("filepath or layername is empty");

            var lwPolylines = dxfDocument.LwPolylines.Where(x => x.Layer.Name == layerName);

            IList<DxfPolylineModel> myPolylineModels = new List<DxfPolylineModel>();

            foreach (var p in lwPolylines)
            {
                DxfPolylineModel model = new DxfPolylineModel
                {
                    PolylineName = p.Handle,
                    PolylineVertexes = p.Vertexes,
                    Color = new DxfLayerColor()
                    {
                        R = p.Layer.Color.R,
                        G = p.Layer.Color.G,
                        B = p.Layer.Color.B
                    },
                    LayerName = p.Layer.Handle,
                    LineType = (DxfLineType)Enum.Parse(typeof(DxfLineType), p.Layer.Linetype.Name, true),
                    LineWeight = p.Layer.Lineweight
                };

                myPolylineModels.Add(model);
            };

            return myPolylineModels;
        }

        public static string ConvertPolylineToJson(IList<DxfPolylineModel> myPolylineModels)
        {
            var jsonModel = JsonConvert.SerializeObject(myPolylineModels);
            return jsonModel;
        }
    }
}


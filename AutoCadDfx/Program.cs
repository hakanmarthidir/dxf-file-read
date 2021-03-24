using System;
using netDxf;
using netDxf.Header;

namespace AutoCadDfx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello AutoCad!");

            string dxfFile = "architectural_example-imperial.dxf";

            var document = DxfManager.LoadDxfDocument(dxfFile);
            var myPolylineModels = DxfManager.ReadLwPolylinesByLayerName(document, "Windows");
            var json = DxfManager.ConvertPolylineToJson(myPolylineModels);

            Console.WriteLine(json);

            foreach (var item in myPolylineModels)
            {
                Console.WriteLine($"Polyline => {item.PolylineName}");
                Console.WriteLine($"Color => R:{item.Color.R} G: {item.Color.G} B:{item.Color.B}");
                Console.WriteLine($"LayerName => {item.LayerName}");
                Console.WriteLine($"LineType => {item.LineType}");
                Console.WriteLine($"LineWeight => {item.LineWeight}");
                Console.WriteLine($"Vertexes ----------- =>");

                foreach (var v in item.PolylineVertexes)
                {
                    Console.WriteLine($"{v.Position}");
                }
            }
        }
    }
}


using netDxf;
using netDxf.Entities;
using System.Collections.Generic;

namespace AutoCadDfx
{
    public class DxfPolylineModel
    {
        public string PolylineName { get; set; }
        public IEnumerable<LwPolylineVertex> PolylineVertexes { get; set; }
        public string LayerName { get; set; }
        public DxfLayerColor Color { get; set; }
        public Lineweight LineWeight { get; set; }
        public DxfLineType LineType { get; set; }
    }
}


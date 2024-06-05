using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.Model.dto
{
    public class WaterSampleDTO
    {
        public double ph { get; set; }
        public double Hardness { get; set; }
        public double Solids { get; set; }
        public double Chloramines { get; set; }
        public double Sulfate { get; set; }
        public double Conductivity { get; set; }
        public double Organic_carbon { get; set; }
        public double Trihalomethanes { get; set; }
        public double Turbidity { get; set; }

        public WaterSampleDTO(double ph, double hardness, double solids, double chloramines, double sulfate, double conductivity, double organic_carbon, double trihalomethanes, double turbidity)
        {
            this.ph = ph;
            Hardness = hardness;
            Solids = solids;
            Chloramines = chloramines;
            Sulfate = sulfate;
            Conductivity = conductivity;
            Organic_carbon = organic_carbon;
            Trihalomethanes = trihalomethanes;
            Turbidity = turbidity;
        }
    }
}

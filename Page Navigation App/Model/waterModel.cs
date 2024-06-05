using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.Model
{
    class waterModel
    {
        public ObjectId _id { get; set; }
        public double ph { get; set; }
        public double Hardness { get; set; }
        public double Solids { get; set; }
        public double Chloramines { get; set; }
        public double Sulfate { get; set; }
        public double Conductivity { get; set; }
        public double Organic_carbon { get; set; }
        public double Trihalomethanes { get; set; }
        public double Turbidity { get; set; }

    }
}

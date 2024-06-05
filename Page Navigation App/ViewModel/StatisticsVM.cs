using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using Page_Navigation_App.Model;
using Page_Navigation_App.Utilities;
using SkiaSharp;

namespace Page_Navigation_App.ViewModel
{
    class StatisticsVM : ViewModelBase
    {
        public StatisticsVM(){
        var outer = 0;
        var data = new[] { 236, 431, 254 };

        // you can convert any array, list or IEnumerable<T> to a pie series collection:
        Series = data.AsPieSeries((value, series) =>
        {
           
            series.OuterRadiusOffset = outer;
            outer += 50;

            series.DataLabelsPaint = new SolidColorPaint(SKColors.White)
        {
            SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
            };
            
                series.ToolTipLabelFormatter =
                point =>
                {
                    var pv = point.Coordinate.PrimaryValue;
                    var sv = point.StackedValue!;

                    var a = $"{pv}/{sv.Total}{Environment.NewLine}{sv.Share:P2}";
                    return a;
                };

                     series.DataLabelsFormatter =
                point =>
                {
                    var pv = point.Coordinate.PrimaryValue;
                    var sv = point.StackedValue!;
                    

                    var a = $"{pv}/{sv.Total}{Environment.NewLine}{sv.Share:P2}";
                    return a;
                };
        });
    }

    public IEnumerable<ISeries> Series { get; set; }
}
}

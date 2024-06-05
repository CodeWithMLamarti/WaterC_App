using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Archive.xaml
    /// </summary>
    public partial class Archive : UserControl
    {
        public Archive()
        {
            InitializeComponent();
            InitializeAsync();
            //this.fieldCarousel.ItemsSource = ArchiveHelpers.LoadDataAsync();
        }

        private async void InitializeAsync()
        {
            var waterSamples = await ArchiveHelpers.LoadDataAsync();
            if (waterSamples != null)
            {
                this.fieldCarousel.ItemsSource = waterSamples;
            }
            else
            {
                // Handle error or show appropriate message
            }
        }
    }
}

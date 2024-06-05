using Page_Navigation_App.Model.dto;
using Page_Navigation_App.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Page_Navigation_App.ViewModel
{
    class ArchiveVM : ViewModelBase

    {
        /*
        private ObservableCollection<WaterSampleDTO> waterSamples;
        public ObservableCollection<WaterSampleDTO> WaterSamples { get { return waterSamples; } set { waterSamples = value; OnPropertyChanged(); } }

        public ArchiveVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            WaterSamples = await ArchiveHelpers.LoadDataAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Page_Navigation_App.Utilities;
using System.Windows.Input;

namespace Page_Navigation_App.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand TestCommand { get; set; }
        public ICommand ArchiveCommand { get; set; }
        public ICommand StatisticsCommand { get; set; }
        public ICommand AboutCommand { get; set; }


        private void Home(object obj) => CurrentView = new HomeVM();
        private void Test(object obj) => CurrentView = new TestVM();
        private void Archive(object obj) => CurrentView = new ArchiveVM();
        private void Statistics(object obj) => CurrentView = new StatisticsVM();
        private void About(object obj) => CurrentView = new AboutVM();


        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            TestCommand = new RelayCommand(Test);
            ArchiveCommand = new RelayCommand(Archive);
            StatisticsCommand = new RelayCommand(Statistics);
            AboutCommand = new RelayCommand(About);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}

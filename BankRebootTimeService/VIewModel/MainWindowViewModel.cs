using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BankRebootTimeService.VIewModel
{
    class MainWindowViewModel: ViewModelBase
    {
        private Page _currentpage { get; set; }
        public Page CurrentPage { get { return _currentpage; } set { _currentpage = value; OnPropertyChanged("CurrentPage"); } }
        public MainWindowViewModel()
        {
            FrameOpacity = 1;
            CurrentPage = new View.WelcomePage();
            SlowOpacity(new View.WorkPage());
            // CurrentPage = new View.WorkPage();
        }
        private double _frameOpacity { get; set; }
        public double FrameOpacity { get { return _frameOpacity; } set { _frameOpacity = value; OnPropertyChanged("FrameOpacity"); } }
        public async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
            });
        }
    }

}

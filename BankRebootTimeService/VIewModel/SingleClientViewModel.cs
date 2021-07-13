using BankRebootTimeService.DataBase;
using BankRebootTimeService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace BankRebootTimeService.VIewModel
{
    public class SingleClientViewModel: ViewModelBase
    {
        public SingleClientViewModel()
        {

        }
        public Client CurrentClient { get; set; }
        public SingleClientViewModel(Model.Client cl)
        {
            CurrentClient = cl;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += timer_Tick;
            TimeString = Time.ToString("T");
            if (Model.ConfigClass.getInstance().IsTimerGo)
            Timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Time = Time.AddSeconds(-1);
            TimeString = Time.ToString("T");
            if(Time.Second ==0 && Time.Minute == 0)
            {
                ToTake = ToTake + TheSalary;
                TakeS = false;
                ToPay += Tax;
                CountOfNotPaydTimes++;
                Time = new DateTime(2000, 1, 1, 0, 25, 0);
                
            }
            if(CountOfNotPaydTimes >= 2) { ColorStatus = Brushes.Red; }
            if(CountOfNotPaydTimes < 2) { ColorStatus = Brushes.White; }
            if(ToPay < 0) { ColorStatus = Brushes.Green; }
            using(var db = new DataBaseContext())
            {
                try
                {
                    db.Clients.Attach(CurrentClient);
                    db.Entry(CurrentClient).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch { Timer.Stop(); }
            }
        }
        //public bool IsTakeSalary { get { return CurrentClient.IsTakeTheSalary; } set { CurrentClient.IsTakeTheSalary = value; } }
        public decimal ToTake { get { return CurrentClient.NeedToTakeMoney; } set { CurrentClient.NeedToTakeMoney = value; OnPropertyChanged("ToTake"); } }
        public string Firstname { get { return CurrentClient.Firstname; } set { CurrentClient.Firstname = value; OnPropertyChanged("Firstname"); } }
        public string Secondname { get { return CurrentClient.Secondname;  } set { CurrentClient.Secondname = value; OnPropertyChanged("Secondname"); } }
        public string Post { get { return CurrentClient.Post; } set { CurrentClient.Post = value; OnPropertyChanged("Post"); } }
        public decimal TheSalary { get { return CurrentClient.TheSalary; } set { CurrentClient.TheSalary = value; OnPropertyChanged("TheSalary"); } }
        public decimal Tax { get { return CurrentClient.Tax; } set { CurrentClient.Tax = value; OnPropertyChanged("Tax"); } }
        public string PasNumber { get { return CurrentClient.Id; } set { CurrentClient.Id = value; OnPropertyChanged("PasNumber"); } }
      //  private string takes { get; set; }
        private Brush _ColorStatus { get; set; }
        public Brush ColorStatus { get { return _ColorStatus; } set { _ColorStatus = value; OnPropertyChanged("ColorStatus"); } }
        public bool TakeS { get { return CurrentClient.IsTakeTheSalary; } set { CurrentClient.IsTakeTheSalary = value;  OnPropertyChanged("TakeS"); } }
        public decimal ToPay { get { return CurrentClient.NeedPayTax; } set { CurrentClient.NeedPayTax = value; OnPropertyChanged("ToPay"); } }
        public int CountOfNotPaydTimes { get { return CurrentClient.CountOfNotPaydTimes; } set { CurrentClient.CountOfNotPaydTimes = value; OnPropertyChanged("CountOfNotPaydTimes"); } }
        public DispatcherTimer Timer { get; set; }
        private string timestring { get; set; }
        public bool UseAFalse { get { return CurrentClient.UseAFalse; } set { CurrentClient.UseAFalse = value; OnPropertyChanged("UseAFalse"); } }
        public string TimeString { get { return timestring; } set {timestring = value; OnPropertyChanged("TimeString"); } }
        public DateTime Time { get { return CurrentClient.TimeToChange; } set { CurrentClient.TimeToChange = value; OnPropertyChanged("Time"); } }
    }
}

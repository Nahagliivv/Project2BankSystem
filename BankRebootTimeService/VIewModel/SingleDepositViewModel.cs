using BankRebootTimeService.DataBase;
using BankRebootTimeService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BankRebootTimeService.VIewModel
{
    class SingleDepositViewModel:ViewModelBase
    {
        public Deposit CurrentDeposit { get; set; }
        public SingleDepositViewModel(Deposit dep)
        {
            CurrentDeposit = dep;
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
            if (Time.Second == 0 && Time.Minute == 0)
            {
                Time = new DateTime(2000, 1, 1, 0, CurrentDeposit.CycleMinuts, 0);
                MoneyToBack = MoneyToBack + (MoneyToBack * (Procent / 100));
            }
            using (var db = new DataBaseContext())
            {
                try
                {
                    db.Deposits.Attach(CurrentDeposit);
                    db.Entry(CurrentDeposit).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch { Timer.Stop(); }
            }
        }

        public string PasNum { get { return CurrentDeposit.ClientId; } }
        public decimal MoneyToBack { get { return CurrentDeposit.UserBackMoney; } set { CurrentDeposit.UserBackMoney = value; OnPropertyChanged("MoneyToBack"); } }
        public decimal Procent { get { return CurrentDeposit.Procent; } }
        private string timestring { get; set; }
        public string TimeString { get { return timestring; } set { timestring = value; OnPropertyChanged("TimeString"); } }
        public DateTime Time { get { return CurrentDeposit.TimeToEnd; } set { CurrentDeposit.TimeToEnd = value; OnPropertyChanged("Time"); } }
        public DispatcherTimer Timer { get; set; }
    }
}

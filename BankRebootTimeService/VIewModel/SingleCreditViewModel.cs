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
    class SingleCreditViewModel:ViewModelBase
    {
        public Credit CurrentCredit { get; set; }
        public SingleCreditViewModel(Credit credit)
        {
            CurrentCredit = credit;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += timer_Tick;
            TimeString = Time.ToString("T");
            if (ConfigClass.getInstance().IsTimerGo && !CurrentCredit.IsTimeOut)
            {
                Timer.Start();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Time = Time.AddSeconds(-1);
            TimeString = Time.ToString("T");
            if (Time.Second == 0 && Time.Minute == 0)
            {
                IsTimeOut = true;
                Timer.Stop();
            }
            using (var db = new DataBaseContext())
            {
                try
                {
                    db.Credits.Attach(CurrentCredit);
                    db.Entry(CurrentCredit).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch { Timer.Stop(); }
            }
        }

        public string PasNum { get { return CurrentCredit.ClientId; } }
        public decimal Money { get { return CurrentCredit.UserTakeMoney; } }
        
        public bool IsTimeOut { get { return CurrentCredit.IsTimeOut; } set { CurrentCredit.IsTimeOut = value; OnPropertyChanged("IsTimeOut"); } }
        public decimal MoneyToBack { get { return CurrentCredit.UserBackMoney; } }
        private string timestring { get; set; }
        public string TimeString { get { return timestring; } set { timestring = value; OnPropertyChanged("TimeString"); } }
        public DateTime Time { get { return CurrentCredit.TimeToEnd; } set { CurrentCredit.TimeToEnd = value; OnPropertyChanged("Time"); } }
        public DispatcherTimer Timer { get; set; }

    }
}

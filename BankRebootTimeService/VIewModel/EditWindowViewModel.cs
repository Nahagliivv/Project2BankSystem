using BankRebootTimeService.DataBase;
using BankRebootTimeService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BankRebootTimeService.VIewModel
{
    class EditWindowViewModel: ViewModelBase
    {
        public SingleClientViewModel CurrentClient { get; set; }
        private Window CurrentWnd { get; set; }
        public EditWindowViewModel(SingleClientViewModel cl, Window current )
        {
            CurrentClient = cl;
            CurrentWnd = current;
            PasNumber = CurrentClient.PasNumber;
            _Firstname = CurrentClient.Firstname;
            _Secondname = CurrentClient.Secondname;
            _ToTake =  CurrentClient.ToTake ;
            _Post = CurrentClient.Post ;
            _TheSalary =CurrentClient.TheSalary;
            _Tax = CurrentClient.Tax ;
            _TakeS = CurrentClient.TakeS;
           _ToPay = CurrentClient.ToPay ;
           _CountOfNotPaydTimes = CurrentClient.CountOfNotPaydTimes ;
            _UseAFalse = CurrentClient.UseAFalse;
        }
        bool _UseAFalse { get; set; }
        public bool UseAFalse { get { return _UseAFalse; } set { _UseAFalse = value;  } }
        decimal _ToTake { get; set; }
        public decimal ToTake { get { return _ToTake; } set { _ToTake = value; } }
        string _Firstname { get; set; }
        public string Firstname { get { return _Firstname; } set { _Firstname = value; } }
        string _Secondname { get; set; }
        public string Secondname { get { return _Secondname; } set { _Secondname = value; } }
        string _Post { get; set; }
        public string Post { get { return _Post; } set { _Post = value; } }
        decimal _TheSalary{ get; set; }
        public decimal TheSalary { get { return _TheSalary; } set { _TheSalary = value; } }
        decimal _Tax { get; set; }
        public decimal Tax { get { return _Tax; } set { _Tax = value; } }
        bool _TakeS { get; set; }
        public bool TakeS { get { return _TakeS; } set { _TakeS = value; } }
        decimal _ToPay { get; set; }
        public decimal ToPay { get { return _ToPay; } set { _ToPay = value; } }
        int _CountOfNotPaydTimes { get; set; }
        public int CountOfNotPaydTimes { get { return _CountOfNotPaydTimes; } set { _CountOfNotPaydTimes = value; } }
        private string _PasNumber { get; set; }
        public string PasNumber { get { return _PasNumber; } set { _PasNumber = value; } }
        public ICommand SaveChanges
        {
            get
            {
                return new DelegateCommand(() => SvChngs());
            }
        }
        void SvChngs()
        {

            CurrentClient.Firstname = _Firstname;
            CurrentClient.Secondname = _Secondname;
            CurrentClient.ToTake = _ToTake;
            CurrentClient.Post = _Post;
            CurrentClient.TheSalary = _TheSalary;
            CurrentClient.Tax = _Tax;
            CurrentClient.TakeS = _TakeS;
            CurrentClient.ToPay = _ToPay;
            CurrentClient.CountOfNotPaydTimes = _CountOfNotPaydTimes;
            CurrentClient.UseAFalse = _UseAFalse;
            using (var db = new DataBaseContext())
            {
                db.Clients.Attach(CurrentClient.CurrentClient);
                db.Entry(CurrentClient.CurrentClient).State = EntityState.Modified;
                db.SaveChanges();
            }
            CurrentWnd.Close();
        }
    }
}

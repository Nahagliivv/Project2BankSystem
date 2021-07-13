using BankRebootTimeService.DataBase;
using BankRebootTimeService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace BankRebootTimeService.VIewModel
{
    class WorkPageVIewModel : ViewModelBase
    {
        public SingleClientViewModel FocusClient { get; set; }
        public ObservableCollection<SingleClientViewModel> Clients { get; set; }//отображаемый лист
        public ObservableCollection<SingleClientViewModel> HelpListClients { get; set; }//основной лист
        public WorkPageVIewModel()
        {
            Clients = new ObservableCollection<SingleClientViewModel>();
            HelpListClients = new ObservableCollection<SingleClientViewModel>();
            AllCreditHelpList = new ObservableCollection<SingleCreditViewModel>();
            AllCreditsList = new ObservableCollection<SingleCreditViewModel>();
            AllDepositsHelpList = new ObservableCollection<SingleDepositViewModel>();
            AllDepositsList = new ObservableCollection<SingleDepositViewModel>();
            using (var db = new DataBaseContext())
            {
                foreach (var x in db.Clients)
                {
                    var client = new SingleClientViewModel(x);
                    HelpListClients.Add(client);
                    Clients.Add(client);
                }
                foreach (var x in db.Credits)
                {
                    var credit = new SingleCreditViewModel(x);
                    AllCreditHelpList.Add(credit);
                    AllCreditsList.Add(credit);
                }
                foreach (var x in db.Deposits)
                {
                    var deposit = new SingleDepositViewModel(x);
                    AllDepositsHelpList.Add(deposit);
                    AllDepositsList.Add(deposit);
                }
            }
            CheckTimerStatus();
        }
        public string SearchText { get; set; } = "";
        string _Firstname { get; set; }
        public string Firstname { get { return _Firstname; } set { _Firstname = value; OnPropertyChanged("Firstname"); } }
        public string PassNumber { get { return _PassNumber; } set { _PassNumber = value; OnPropertyChanged("PassNumber"); } }
        private string _PassNumber { get; set; } = "";
        string _Secondname { get; set; }
        public string Secondname { get { return _Secondname; } set { _Secondname = value; OnPropertyChanged("Secondname"); } }
        string _Post { get; set; }
        public string Post { get { return _Post; } set { _Post = value; OnPropertyChanged("Post"); } }
        decimal _TheSalary { get; set; }
        public decimal TheSalary { get { return _TheSalary; } set { _TheSalary = value; OnPropertyChanged("TheSalary"); } }
        decimal _Addmoney { get; set; } = 0;
        public decimal Addmoney { get { return _Addmoney; } set { _Addmoney = value; OnPropertyChanged("Addmoney"); } }
        int _CountAddMoney { get; set; } = 0;
        public int CountAddMoney { get { return _CountAddMoney; } set { _CountAddMoney = value; OnPropertyChanged("CountAddMoney"); } }
        public bool IsGoInJail { get; set; }
        public ICommand Search
        {
            get
            {
                return new DelegateCommand(() => Srch());
            }
        }
        void Srch()
        {
            var reg = new Regex(SearchText.ToLower());
            Clients.Clear();
            foreach (var x in HelpListClients)
            {
                if (reg.IsMatch(x.CurrentClient.ToString().ToLower()))
                {
                    if (x.CountOfNotPaydTimes >= 2 && IsGoInJail == true)
                        Clients.Add(x);
                    if (IsGoInJail == false)
                        Clients.Add(x);
                }
            }
        }
        public ICommand AddClient
        {
            get
            {
                return new DelegateCommand(() => AddCl());
            }
        }
        void AddCl()
        {
            using (var db = new DataBaseContext())
            {
                if (PassNumber == "")
                {
                    MessageBox.Show("Поле с номером паспорта не может быть пустым...");
                    return;
                }
                var checkexist = db.Clients.Count(x => x.Id == PassNumber);
                if (checkexist != 0)
                {
                    MessageBox.Show("Клиент с таким номером паспорат уже есть в бд");
                    return;
                }
                //var count = db.Clients.Count(x => x.Firstname == Firstname && x.Secondname == Secondname);
                //if (count != 0)
                //{
                //    MessageBox.Show("Клиент с таким именем уже существует...");
                //    return;
                //}
                decimal taxx;
                if (TheSalary == 0)
                {
                    taxx = 1000;
                }
                else { taxx = TheSalary / 10; }
                var newcl = new Model.Client() { Id = PassNumber, Firstname = Firstname, Secondname = Secondname, Post = Post, TheSalary = TheSalary, Tax = taxx, IsTakeTheSalary = true };
                db.Clients.Add(newcl);
                db.SaveChanges();
                var clbox = new SingleClientViewModel(newcl);
                Clients.Add(clbox);
                HelpListClients.Add(clbox);
            }
            Firstname = "";
            Secondname = "";
            Post = "";
            TheSalary = 0;
        }
        public ICommand TakeTheSalary
        {
            get
            {
                return new DelegateCommand(() => TakeSalary());
            }
        }
        void TakeSalary()
        {
            if (FocusClient == null) return;
            var accept = MessageBox.Show("Вы действительно хотите забрать всю ЗП?", "Забрать зарплату", MessageBoxButton.YesNo);
            if (accept != MessageBoxResult.Yes)
            {
                return;
            }
            FocusClient.TakeS = true;
            FocusClient.ToTake = 0;
            using (var db = new DataBaseContext())
            {
                db.Clients.Attach(FocusClient.CurrentClient);
                db.Entry(FocusClient.CurrentClient).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public ICommand AddMoney
        {
            get
            {
                return new DelegateCommand(() => AddM());
            }
        }
        void AddM()
        {
            if (FocusClient == null) return;
            FocusClient.ToPay -= Addmoney;
            FocusClient.CountOfNotPaydTimes -= CountAddMoney;
            Addmoney = 0;
            CountAddMoney = 0;
            using (var db = new DataBaseContext())
            {
                db.Clients.Attach(FocusClient.CurrentClient);
                db.Entry(FocusClient.CurrentClient).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        public ICommand Edit
        {
            get
            {
                return new DelegateCommand(() => Ed());
            }
        }
        public ICommand Delete
        {
            get
            {
                return new DelegateCommand(() => Del());
            }
        }
        void Del()
        {
            if (FocusClient == null) return;
            var accept = MessageBox.Show("Вы действительно хотите удалить клиента, а также все данные, которые с ним связаны(вклады, кредиты) из базы данных?", "Удаление клиента", MessageBoxButton.YesNo);
            if (accept != MessageBoxResult.Yes)
            {
                return;
            }
            using (var db = new DataBaseContext())
            {
                foreach (var x in db.Credits)
                {
                    if (x.ClientId == FocusClient.CurrentClient.Id)
                    {
                        db.Credits.Remove(x);
                    }
                }
                foreach (var x in db.Deposits)
                {
                    if (x.ClientId == FocusClient.CurrentClient.Id)
                    {
                        db.Deposits.Remove(x);
                    }
                }
                db.SaveChanges();
                db.Clients.Remove(db.Clients.Where(x => x.Id == FocusClient.CurrentClient.Id).FirstOrDefault());
                db.SaveChanges();
                foreach (var x in AllDepositsList)
                { if (x.CurrentDeposit.ClientId == FocusClient.CurrentClient.Id)
                    { AllDepositsList.Remove(x); break; } }
                foreach (var x in AllDepositsHelpList)
                {
                    if (x.CurrentDeposit.ClientId == FocusClient.CurrentClient.Id)
                    { AllDepositsHelpList.Remove(x); break; }
                }
                foreach (var x in AllCreditHelpList)
                {
                    if (x.CurrentCredit.ClientId == FocusClient.CurrentClient.Id)
                    { AllCreditHelpList.Remove(x); break; }
                }
                foreach (var x in AllCreditsList)
                {
                    if (x.CurrentCredit.ClientId == FocusClient.CurrentClient.Id)
                    { AllCreditsList.Remove(x); break; }
                }
                HelpListClients.Remove(FocusClient);
                Clients.Remove(FocusClient);

            }
        }
        void Ed()
        {
            if (FocusClient == null) return;
            var ed = new View.EditWindow(FocusClient);
            ed.ShowDialog();
        }
        //////////////////////////////////////////////////////////////////////////Приколы с таймерами
        private string _TimerStatus { get; set; }
        public string TimerStatus { get { return _TimerStatus; } set { _TimerStatus = value; OnPropertyChanged("TimerStatus"); } }
        private Brush _TimerStatusColor { get; set; }
        public Brush TimerStatusColor { get { return _TimerStatusColor; } set { _TimerStatusColor = value; OnPropertyChanged("TimerStatusColor"); } }
        void CheckTimerStatus()
        {
            if (Model.ConfigClass.getInstance().IsTimerGo)
            {
                TimerStatusColor = Brushes.Green;
                TimerStatus = "Время возобновило свой ход.";
            }
            if (!Model.ConfigClass.getInstance().IsTimerGo)
            {
                TimerStatusColor = Brushes.Red;
                TimerStatus = "Время остановилось.";
            }
        }
        public ICommand PlayTimers
        {
            get
            {
                return new DelegateCommand(() => PlayTime());
            }
        }
        void PlayTime()
        {
            var accept = MessageBox.Show("Таймеры будут работать до следующей остановки их вручную.", "Запуск таймеров", MessageBoxButton.YesNo);
            if (accept != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var x in HelpListClients)
            {
                x.Timer.Start();
            }
            foreach (var x in AllCreditHelpList)
            {
                if (!x.IsTimeOut)
                    x.Timer.Start();
            }
            foreach (var x in AllDepositsHelpList)
            {
                x.Timer.Start();
            }
            Model.ConfigClass.SetTimerStatus(true);
            CheckTimerStatus();
        }
        public ICommand TimerStop
        {
            get
            {
                return new DelegateCommand(() => StopTime());
            }
        }
        void StopTime()
        {
            var accept = MessageBox.Show("Таймеры будут не работать до следующего запуска вручную.", "Остановка таймеров", MessageBoxButton.YesNo);
            if (accept != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var x in HelpListClients)
            {
                x.Timer.Stop();
            }
            foreach (var x in AllCreditHelpList)
            {
                x.Timer.Stop();
            }
            foreach (var x in AllDepositsHelpList)
            {
                x.Timer.Stop();
            }
            Model.ConfigClass.SetTimerStatus(false);
            CheckTimerStatus();
        }
        ////////////Кредиты и вклады создание
        public bool IsCredit { get; set; } = true;
        public bool IsDeposit { get; set; }
        public string PasNum { get; set; }
        public decimal Money { get; set; }
        public decimal Procent { get; set; } = 15;
        public int GiveTime { get; set; } = 15;
        public ObservableCollection<SingleCreditViewModel> AllCreditsList { get; set; }
        public ObservableCollection<SingleCreditViewModel> AllCreditHelpList { get; set; }
        public ObservableCollection<SingleDepositViewModel> AllDepositsList { get; set; }
        public ObservableCollection<SingleDepositViewModel> AllDepositsHelpList { get; set; }
        public SingleCreditViewModel FocusCredit { get; set; }
        public SingleDepositViewModel FocusDeposit { get; set; }
        public ICommand CreateCreditOrDeposit
        {
            get
            {
                return new DelegateCommand(() => CreateCrOrDep());
            }
        }
        void CreateCrOrDep()
        {
            using (var db = new DataBaseContext())
            {
                var existclient = db.Clients.Where(x => x.Id == PasNum).FirstOrDefault();
                if (existclient == null)
                {
                    MessageBox.Show("Клиента с таким номером паспорта не существует"); return;
                }
                if (!IsCredit && !IsDeposit) { return; }
                if (Procent < 0 || Procent > 100) { MessageBox.Show("Процент выходит за допустимый диапазон(0-100)"); return; }
                if (Money < 1) { MessageBox.Show("Кол-во денег не может быть меньше единицы"); return; }
                if (GiveTime < 1 || GiveTime > 60) { MessageBox.Show("Время выходит за допустимый диапазон 1-60"); return; }


                if (IsCredit)
                {
                    var isexistcr = db.Credits.Where(x => x.ClientId == PasNum).FirstOrDefault();
                    if (isexistcr != null)
                    {
                        MessageBox.Show("У пользователя с таком номером паспорта уже существует кредит"); return;
                    }
                    decimal MoneyToBack = Money + (Money * (Procent / 100));
                    var AddedCredit = new Credit() { ClientId = PasNum, IsTimeOut = false, UserTakeMoney = Money, UserBackMoney = MoneyToBack, TimeToEnd = new DateTime(2000, 1, 1, 0, GiveTime, 0)
                    };
                    db.Credits.Add(AddedCredit);
                    db.SaveChanges();
                    var AddedCredtiViewModel = new SingleCreditViewModel(AddedCredit);
                    AllCreditHelpList.Add(AddedCredtiViewModel);
                    AllCreditsList.Add(AddedCredtiViewModel);
                }
                if (IsDeposit)
                {
                    var isexistdep = db.Deposits.Where(x => x.ClientId == PasNum).FirstOrDefault();
                    if (isexistdep != null)
                    {
                        MessageBox.Show("У пользователя с таком номером паспорта уже существует вклад"); return;
                    }
                    var AddedDeposit = new Deposit() { Procent = Procent, CycleMinuts = GiveTime, ClientId = PasNum, UserBackMoney = Money, TimeToEnd = new DateTime(2000, 1, 1, 0, GiveTime, 0)
                    };
                    db.Deposits.Add(AddedDeposit);
                    db.SaveChanges();
                    var AddedDepositiViewModel = new SingleDepositViewModel(AddedDeposit);
                    AllDepositsHelpList.Add(AddedDepositiViewModel);
                    AllDepositsList.Add(AddedDepositiViewModel);
                }
            }
        }
        ///Управление кредитами
        public string SearchCreditString { get; set; } = "";
        public bool IsTimeOutCredit { get; set; }
        public ICommand DeleteCredit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DelCred();
                });
            }
        }
        void DelCred()
        {
            if (FocusCredit == null) return;
            var accept = MessageBox.Show("Кредит будет удалён", "Удаление записи кредита", MessageBoxButton.YesNo);
            if (accept != MessageBoxResult.Yes)
            {
                return;
            }
            using (var db = new DataBaseContext())
            {
                db.Credits.Remove(db.Credits.Where(x => x.ClientId == FocusCredit.CurrentCredit.ClientId).FirstOrDefault());
                db.SaveChanges();
                AllCreditHelpList.Remove(FocusCredit);
                AllCreditsList.Remove(FocusCredit);
            }
        }
        public ICommand SearchCredit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SearchCrdt();
                });
            }
        }
        void SearchCrdt()
        {
            var reg = new Regex(SearchCreditString.ToLower());
            AllCreditsList.Clear();
            foreach (var x in AllCreditHelpList)
            {
                if (reg.IsMatch(x.CurrentCredit.ToString().ToLower()))
                {
                    if (x.IsTimeOut && IsTimeOutCredit)
                        AllCreditsList.Add(x);
                    if (IsTimeOutCredit == false)
                        AllCreditsList.Add(x);
                }
            }
        }
        ////управление вкладами
        public string SearchDepositString { get; set; } = "";
        decimal takemon { get; set; }
        public decimal TakedMoney { get { return takemon; } set { takemon = value; OnPropertyChanged("TakedMoney"); } }
        public ICommand SearchDeposit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SearchDep();
                });
            }
        }
        public ICommand DeleteDeposit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DelDep();
                });
            }
        }
        void DelDep()
        {
            if (FocusDeposit == null) return;
            var accept = MessageBox.Show("Вклад будет удалён", "Удаление записи депозита", MessageBoxButton.YesNo);
            if (accept != MessageBoxResult.Yes)
            {
                return;
            }
            using (var db = new DataBaseContext())
            {
                db.Deposits.Remove(db.Deposits.Where(x => x.ClientId == FocusDeposit.CurrentDeposit.ClientId).FirstOrDefault());
                db.SaveChanges();
                AllDepositsHelpList.Remove(FocusDeposit);
                AllDepositsList.Remove(FocusDeposit);
            }
        }
        void SearchDep()
        {
            var reg = new Regex(SearchDepositString.ToLower());
            AllDepositsList.Clear();
            foreach (var x in AllDepositsHelpList)
            {
                if (reg.IsMatch(x.CurrentDeposit.ToString().ToLower()))
                {
                        AllDepositsList.Add(x);
                }
            }
        }
        public ICommand TakeMoneyFromDep
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    TakeMFromD();
                });
            }
        }
        void TakeMFromD()
        {
            if (FocusDeposit == null) return;
            FocusDeposit.MoneyToBack -= TakedMoney;
            TakedMoney = 0;
            using (var db = new DataBaseContext())
            {
                db.Deposits.Attach(FocusDeposit.CurrentDeposit);
                db.Entry(FocusDeposit.CurrentDeposit).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        ///Кнопка для всего
        public ICommand PaySalAndTax
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PaySAndTax();
                });
            }
        }
        void PaySAndTax()
        {
            if (FocusClient == null) return;
            FocusClient.ToPay = 0; 
            FocusClient.CountOfNotPaydTimes = 0;
            FocusClient.TakeS = true;
            FocusClient.ToTake = 0;
            using (var db = new DataBaseContext())
            {
                db.Clients.Attach(FocusClient.CurrentClient);
                db.Entry(FocusClient.CurrentClient).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        ///выключение и сворачивание

        public ICommand ShDw
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ShutDown();
                });
            }
        }
        void ShutDown()
        {
            App.Current.Shutdown();
        }
        public ICommand Min
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Minimize();
                });
            }
        }
        void Minimize()
        {
            App.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
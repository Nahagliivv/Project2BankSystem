using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRebootTimeService.Model
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        public string Firstname { get; set; } = "";
        public string Secondname { get; set; } = "";
        public string Post { get; set; } = "";//должность 
        public decimal TheSalary { get; set; } //зп
        private decimal tax { get; set; }
        public decimal Tax { get { return tax; } set { if (TheSalary == 0) { tax = 1000; } else tax = TheSalary / 10; } } //налог
        public decimal NeedPayTax { get; set; } //налог к оплате
        public decimal NeedToTakeMoney { get; set; }//необходимо забрать
        public int CountOfNotPaydTimes { get; set; }//кол-во раз неуплаты налога
        public bool IsTakeTheSalary { get; set; }//была ли получены последняя зп
        public bool UseAFalse { get; set; } = false;
        public virtual Credit Credit { get; set; }
        public virtual Deposit Deposit { get; set; }
        public DateTime TimeToChange { get; set; }
        public Client()
        {
            Tax = 0;
            TimeToChange = new DateTime(2000, 1, 1, 0, 25, 0);
        }
        public override string ToString()
        {
            return $"{Id} {Firstname} {Secondname} {Post} {TheSalary} {Tax} {NeedPayTax} {IsTakeTheSalary}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRebootTimeService.Model
{
    public class Deposit
    {
        [Key]
       [ForeignKey("Client")]
        public string ClientId { get; set; }
        public decimal UserBackMoney { get; set; }
        public decimal Procent { get; set; }
       public virtual Client Client { get; set; }
        public DateTime TimeToEnd { get; set; }
        public int CycleMinuts { get; set; }
        public Deposit()
        {
            
        }
        public override string ToString()
        {
            return $"{ClientId} {UserBackMoney} {Procent} {TimeToEnd}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRebootTimeService.Model
{
    public class Credit
    {
        [Key]
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public decimal UserTakeMoney { get; set; }
        public decimal UserBackMoney { get; set; }
        public int Procent { get; set; }
        public virtual Client Client { get; set; }
        public DateTime TimeToEnd { get; set; }
        public bool IsTimeOut { get; set; }

        public Credit()
        {
            
        }
        public override string ToString()
        {
            return $"{ClientId} {UserTakeMoney} {UserBackMoney} {Procent} {TimeToEnd}";
        }
    }
}

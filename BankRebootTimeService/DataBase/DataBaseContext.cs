using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRebootTimeService.DataBase
{
    class DataBaseContext: DbContext
    {
        public DataBaseContext()
           : base("SQLConnection")
        { }
        public DbSet<Model.Client> Clients { get; set; }
        public DbSet<Model.Credit> Credits { get; set; }
        public DbSet<Model.Deposit> Deposits { get; set; }
        
    }
}

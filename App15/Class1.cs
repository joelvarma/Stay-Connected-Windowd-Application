using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace App15
{
    [Table("users1")]
    class user
    {
        public string u_name { get; set; }
        public string passwd { get; set; }
        public string email { get; set; }
        
   }
}

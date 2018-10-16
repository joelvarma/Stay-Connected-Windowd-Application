using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15
{
    [Table("detail")]
    class detail
    {

        public string id { get; set; }
        public string email { get; set; }

        public string latitude { get; set; }
        public string username { get; set; }

        public string longitude { get; set; }
    }    
}

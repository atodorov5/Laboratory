using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory
{
    public class Test
    {
        public int idTest { get; set; }
        public DateTime testDate { get; set; }
        public string refnumber { get; set; }
        public string pin { get; set; }
        public string p_name { get; set; }
        public string p_surname { get; set; }
        public string p_lastname { get; set; }
        public string p_email { get; set; }


        public string getFullName()
        {
            return p_name + " " + p_surname + " " + p_lastname;
        }

    }
}

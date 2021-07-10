using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public Test() { }
        public Test(int id,string name, string surname, string lastname,string email)
        {
            idTest = id;
            p_name = name;
            p_surname = surname;
            p_lastname = lastname;
            p_email = email;
        }

        public string getFullName()
        {
            return p_name + " " + p_surname + " " + p_lastname;
        }

        public bool CheckEmail()
        {
            
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match match = regex.Match(p_email);
            if (match.Success)
                return true;
            else                
                return false;
        }

   
    }
}

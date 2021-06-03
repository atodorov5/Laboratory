using Laboratory.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Laboratory.Administrator
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        public admin()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            using (var conn = DBConfig.Connection)
            {

                String salt = Password.CreateSalt(10);
                String hashedpass = Password.GenerateSHA256Hash("am", salt);
               
                conn.Open();

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO user(name, surname,lastname,username,hashedPassWrd,salt,ClinicBranch_idClinicBranch,Roles_idRoles) VALUES(@name, @surname,@lastname,@username,@hashedPassWrd,@salt,@ClinicBranch_idClinicBranch,@Roles_idRoles)";
                comm.Parameters.AddWithValue("@name", "Angel");
                comm.Parameters.AddWithValue("@surname", "Angelov");
                comm.Parameters.AddWithValue("@lastname", "Mihailov");
                comm.Parameters.AddWithValue("@username", "am");
                comm.Parameters.AddWithValue("@hashedPassWrd", hashedpass);
                comm.Parameters.AddWithValue("@salt", salt);
                comm.Parameters.AddWithValue("@ClinicBranch_idClinicBranch", 1);
                comm.Parameters.AddWithValue("@Roles_idRoles", 2);



                //comm.ExecuteNonQuery();



                MessageBox.Show("Успешно добавен лаборант!");
            }
            
        }
    }
}

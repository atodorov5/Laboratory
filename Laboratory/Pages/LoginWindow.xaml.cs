using Laboratory.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {

            using (var conn = DBConfig.Connection)
            {
                string salt = "", hashedpass = "";
                int roleID = -1, userid = -1;
                try
                {
                    if (usernameTB.Text != "")
                    {


                        conn.Open();

                        var cmd = conn.CreateCommand();
                        cmd.CommandText = "select idUser,Roles_idRoles,hashedPassWrd,salt from lab_user where username = @username";
                        cmd.Parameters.AddWithValue("@username", usernameTB.Text);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // here could be problems if database value is null
                            hashedpass = reader["hashedPassWrd"].ToString();
                            salt = reader["salt"].ToString();
                            roleID = (int)reader["Roles_idRoles"];
                            userid = (int)reader["idUser"];
                        }



                        conn.Close();

                        if (Password_salt.GenerateSHA256Hash(passwordTB.Password.ToString(), salt) == hashedpass)
                        {
                            //GlobalInfo.CurrentUser = new UserInfo((int)cmd.Parameters["o_id"].Value);   //DA SE PROVERI
                            Properties.Settings.Default.user = usernameTB.Text;
                            Properties.Settings.Default.roleID = roleID;
                            Properties.Settings.Default.userID = userid;
                            Properties.Settings.Default.Save();
                            UserWindow userW = new UserWindow();
                            userW.Show();
                            this.Close();

                        }
                        else
                        {
                            loginError.Content = "Грешно потребителско име или парола!";
                        }

                    }
                    else
                        loginError.Content = "Грешно потребителско име или парола!";


                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Грешка " + ex);
                }

            }



        }

        private void login2(object sender, RoutedEventArgs e)
        {


            string salt = "", hashedpass = "";
            int? roleID = -1, userid = -1 , labID=-1;


            if (usernameTB.Text != "")
            {

                Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
                retrieve_UsersTableAdapter.login_procedure(usernameTB.Text,out hashedpass, out salt,out roleID, out userid, out labID);


                if (Password_salt.GenerateSHA256Hash(passwordTB.Password.ToString(), salt) == hashedpass)
                {
                    //GlobalInfo.CurrentUser = new UserInfo((int)cmd.Parameters["o_id"].Value);   //DA SE PROVERI
                    Properties.Settings.Default.user = usernameTB.Text;
                    Properties.Settings.Default.roleID = (int)roleID;
                    Properties.Settings.Default.userID = (int)userid;
                    Properties.Settings.Default.labID = (int)labID;
                    Properties.Settings.Default.Save();
                    UserWindow userW = new UserWindow();
                    userW.Show();
                    this.Close();

                }
                else
                {
                    loginError.Content = "Грешно потребителско име или парола!";
                }

            }
            else
                loginError.Content = "Грешно потребителско име или парола!";








        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var conn = DBConfig.Connection)
            {

                String salt = Password_salt.CreateSalt(10);
                String hashedpass = Password_salt.GenerateSHA256Hash("az", salt);

                conn.Open();

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO lab_user(name, surname,lastname,username,user_pin,user_address,user_phone,hashedPassWrd,salt,ClinicBranch_idClinicBranch,Roles_idRoles) VALUES(@name, @surname,@lastname,@username,@user_pin,@user_address,@user_phone,@hashedPassWrd,@salt,@ClinicBranch_idClinicBranch,@Roles_idRoles)";
                comm.Parameters.AddWithValue("@name", "Milen");
                comm.Parameters.AddWithValue("@surname", "Angelov");
                comm.Parameters.AddWithValue("@lastname", "Mihailov");
                comm.Parameters.AddWithValue("@username", "az");
                comm.Parameters.AddWithValue("@user_pin", "9802068725");
                comm.Parameters.AddWithValue("@user_address", "aasdasd");
                comm.Parameters.AddWithValue("@user_phone", "089222");

                comm.Parameters.AddWithValue("@hashedPassWrd", hashedpass);
                comm.Parameters.AddWithValue("@salt", salt);
                comm.Parameters.AddWithValue("@ClinicBranch_idClinicBranch", 1);
                comm.Parameters.AddWithValue("@Roles_idRoles", 1);



                comm.ExecuteNonQuery();



                MessageBox.Show("Успешно добавен лаборант!");
            }

        }
    }
}

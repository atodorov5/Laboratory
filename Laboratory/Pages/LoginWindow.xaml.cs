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
          /* UserWindow userW = new UserWindow();
            userW.Show();
            this.Close();

            */
            
            using (var conn = DBConfig.Connection)
            {
                string salt = "", hashedpass = "";
                int roleID=-1,userid=-1;
                try
                {
                    if (usernameTB.Text != "")
                    {


                        conn.Open();
                        
                       var cmd = conn.CreateCommand();
                       cmd.CommandText = "select idUser,Roles_idRoles,hashedPassWrd,salt from user where username = @username";
                       cmd.Parameters.AddWithValue("@username", usernameTB.Text);
                       var reader = cmd.ExecuteReader();
                     while (reader.Read())
                            {
                                // here could be problems if database value is null
                                hashedpass = reader["hashedPassWrd"].ToString();
                                salt = reader["salt"].ToString();
                                roleID =(int) reader["Roles_idRoles"];
                                 userid =(int) reader["idUser"];
                            }
                        


                        conn.Close();

                        if (Password.GenerateSHA256Hash(passwordTB.Password.ToString(), salt) == hashedpass)
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
    }
}

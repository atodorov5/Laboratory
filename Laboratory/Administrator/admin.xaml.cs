using Laboratory.Data;
using Laboratory.Windows;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            load_data();
            
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
                comm.Parameters.AddWithValue("@name", "Milen");
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

        private void load_data()
        {
            using (var connection = DBConfig.Connection)
            {
                string sql = "SELECT * FROM clinicbranch";

                
                MySqlCommand cmdSel = new MySqlCommand(sql, connection);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                da.Fill(dt);
                labsDG.DataContext = dt;
            }

            using (var connection = DBConfig.Connection)
            {
                string sql = "SELECT * FROM testtype";


                MySqlCommand cmdSel = new MySqlCommand(sql, connection);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                da.Fill(dt);
                dt.Columns["idTestType"].ColumnName = "№";
                testDG.DataContext = dt;
            }
        }

        private void add_testtype(object sender, RoutedEventArgs e)
        {
            var test = new Type_Window();
            test.ShowDialog();
            load_data();
        }

        private void edit_testtype(object sender, RoutedEventArgs e)
        {
            DataRowView row = testDG.SelectedItem as DataRowView;
            if (row != null)
            {
                var test = new Edittype_Window(row);
                test.ShowDialog();
                load_data();
            }
            else
                MessageBox.Show("Изберете тест!");
        }

        private void delete_testtype(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = testDG.SelectedItem as DataRowView;
            
            if (rowview != null)
            {
                using (var conn = DBConfig.Connection)
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("delete_testtype", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("p_testTypeID", MySqlDbType.Int32));
                        cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters[0].Value = (int)rowview.Row[0];
                        if (cmd.ExecuteNonQuery() > 0)
                            MessageBox.Show("Успешно премахнат тест ");
                        else
                            MessageBox.Show("Тестът не може да бъде премахнат");

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Грешка " + ex);
                    }

                    load_data();
                }
            }
            else
                MessageBox.Show("Изберете тест");
        
    }
    }
}

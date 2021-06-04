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
using System.Windows.Shapes;

namespace Laboratory.Windows
{
    /// <summary>
    /// Interaction logic for Type_Window.xaml
    /// </summary>
    public partial class Type_Window : Window
    {
        public Type_Window()
        {
            InitializeComponent();
        }

        private void add_type(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(test_nameTB.Text) || String.IsNullOrEmpty(test_minTB.Text) || String.IsNullOrEmpty(test_maxTB.Text) || String.IsNullOrEmpty(test_valueTB.Text) || String.IsNullOrEmpty(test_priceTB.Text))
                MessageBox.Show("Въведете данни!");
            else
            {
                using (var conn = DBConfig.Connection)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("add_testtype", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new MySqlParameter("p_name", MySqlDbType.VarChar));
                    cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.Add(new MySqlParameter("p_minV", MySqlDbType.Double));
                    cmd.Parameters[1].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.Add(new MySqlParameter("p_maxV", MySqlDbType.Double));
                    cmd.Parameters[2].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.Add(new MySqlParameter("p_unit", MySqlDbType.VarChar));
                    cmd.Parameters[3].Direction = System.Data.ParameterDirection.Input;
                    cmd.Parameters.Add(new MySqlParameter("p_price", MySqlDbType.Double));
                    cmd.Parameters[4].Direction = System.Data.ParameterDirection.Input;

                    cmd.Parameters[0].Value = test_nameTB.Text;
                    cmd.Parameters[1].Value = Convert.ToDouble(test_minTB.Text);
                    cmd.Parameters[2].Value = Convert.ToDouble(test_maxTB.Text);
                    cmd.Parameters[3].Value = test_valueTB.Text;
                    cmd.Parameters[4].Value = Convert.ToDouble(test_priceTB.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Успешно!");
                }
                this.Close();
            }
        }
    }
}

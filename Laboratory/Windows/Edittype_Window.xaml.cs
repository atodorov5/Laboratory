using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Edittype_Window.xaml
    /// </summary>
    public partial class Edittype_Window : Window
    {
        DataRowView row;
        public Edittype_Window(DataRowView drv)
        {
            InitializeComponent();
            this.row = drv;
           
                test_nameTB.Text = drv.Row.ItemArray[1].ToString();
                test_maxTB.Text = drv.Row.ItemArray[3].ToString();
                test_minTB.Text = drv.Row.ItemArray[2].ToString();
                test_valueTB.Text = drv.Row.ItemArray[4].ToString();
                test_priceTB.Text = drv.Row.ItemArray[5].ToString();
            
        }

        private void save_testtype(object sender, RoutedEventArgs e)
        {
            
            if (row != null)
            {
                using (var conn = DBConfig.Connection)
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("edit_testype", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("p_testTypeID", MySqlDbType.Int32));
                        cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_name", MySqlDbType.VarChar));
                        cmd.Parameters[1].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_minValue", MySqlDbType.VarChar));
                        cmd.Parameters[2].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_maxValue", MySqlDbType.VarChar));
                        cmd.Parameters[3].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_unit", MySqlDbType.VarChar));
                        cmd.Parameters[4].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_price", MySqlDbType.VarChar));
                        cmd.Parameters[5].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters[0].Value = (int)row[0];
                        if (!string.IsNullOrWhiteSpace(test_nameTB.Text))
                            cmd.Parameters[1].Value = test_nameTB.Text;
                        if (!string.IsNullOrWhiteSpace(test_minTB.Text))
                            cmd.Parameters[2].Value = test_minTB.Text;
                        if (!string.IsNullOrWhiteSpace(test_nameTB.Text))
                            cmd.Parameters[3].Value = test_maxTB.Text;
                        if (!string.IsNullOrWhiteSpace(test_valueTB.Text))
                            cmd.Parameters[4].Value = test_valueTB.Text;
                        if (!string.IsNullOrWhiteSpace(test_priceTB.Text))
                            cmd.Parameters[5].Value = test_priceTB.Text;

                        cmd.ExecuteNonQuery();
                        DialogResult = true;
                        this.Close();
                      

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Грешка " + ex);
                    }

                }
            }
            else
                MessageBox.Show("Изберете клиника!");
        
    }
    }
}

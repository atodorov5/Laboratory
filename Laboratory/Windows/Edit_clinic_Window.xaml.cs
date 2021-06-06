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
    /// Interaction logic for Edit_clinic_Window.xaml
    /// </summary>
    public partial class Edit_clinic_Window : Window
    {

        DataRowView row;
        public Edit_clinic_Window(DataRowView drv)
        {
            InitializeComponent();
            row = drv;
            
            lab_townTB.Text = drv.Row.ItemArray[1].ToString();
            lab_phoneTB.Text = drv.Row.ItemArray[2].ToString();
            lab_addressTB.Text = drv.Row.ItemArray[3].ToString();            
            lab_rczTB.Text = drv.Row.ItemArray[4].ToString(); 
            
        }

     

        private void save_clinic(object sender, RoutedEventArgs e)
        {


            if (row != null)
            {
                using (var conn = DBConfig.Connection)
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("edit_clinic", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("p_clinicID", MySqlDbType.VarChar));
                        cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_town", MySqlDbType.VarChar));
                        cmd.Parameters[1].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_address", MySqlDbType.VarChar));
                        cmd.Parameters[2].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_phone", MySqlDbType.VarChar));
                        cmd.Parameters[3].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters.Add(new MySqlParameter("p_license", MySqlDbType.VarChar));
                        cmd.Parameters[4].Direction = System.Data.ParameterDirection.Input;


                        cmd.Parameters[0].Value = (int)row[0];
                        if (!string.IsNullOrWhiteSpace(lab_townTB.Text))
                            cmd.Parameters[1].Value = lab_townTB.Text;
                        if (!string.IsNullOrWhiteSpace(lab_addressTB.Text))
                            cmd.Parameters[2].Value = lab_addressTB.Text;
                        if (!string.IsNullOrWhiteSpace(lab_phoneTB.Text))
                            cmd.Parameters[3].Value = lab_phoneTB.Text;
                        if (!string.IsNullOrWhiteSpace(lab_rczTB.Text))
                            cmd.Parameters[4].Value = lab_rczTB.Text;

                        cmd.ExecuteNonQuery();
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

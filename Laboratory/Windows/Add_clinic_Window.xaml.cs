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
    /// Interaction logic for Add_clinic_Window.xaml
    /// </summary>
    public partial class Add_clinic_Window : Window
    {
        public Add_clinic_Window()
        {
            InitializeComponent();
        }

        private void add_clinic(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(lab_townTB.Text) || String.IsNullOrEmpty(lab_addressTB.Text) || String.IsNullOrEmpty(lab_phoneTB.Text) || String.IsNullOrEmpty(lab_rczTB.Text))
                MessageBox.Show("Въведете данни!");
            else
            {
                using (var conn = DBConfig.Connection)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("add_clinic", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_town", lab_townTB.Text);
                    cmd.Parameters.AddWithValue("p_address", lab_addressTB.Text);
                    cmd.Parameters.AddWithValue("p_phone", lab_phoneTB.Text);
                    cmd.Parameters.AddWithValue("p_license", lab_rczTB.Text);

                 
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Успешно!");
                }
                this.Close();
            }
        }
    }
}

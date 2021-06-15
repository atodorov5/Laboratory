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

            
        }

        private void save_testtype(object sender, RoutedEventArgs e)
        {
            
            if (row != null)
            {
               

            }
            else
                MessageBox.Show("Изберете клиника!");
        
    }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table type_group. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter laboratorydbDataSettype_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
            laboratorydbDataSettype_groupTableAdapter.Fill(laboratorydbDataSet.type_group);
            System.Windows.Data.CollectionViewSource type_groupViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("type_groupViewSource")));
            type_groupViewSource.View.MoveCurrentToFirst();



            test_nameTB.Text = row[1].ToString();
            test_valueTB.Text = row[2].ToString();
            test_priceTB.Text = row[3].ToString();
            type_groupComboBox.SelectedValue = (int)row[5];
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table type_group. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter laboratorydbDataSettype_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter();
            laboratorydbDataSettype_groupTableAdapter.edit_testype((int)row[0],test_nameTB.Text,test_valueTB.Text,test_priceTB.Text,(int)type_groupComboBox.SelectedValue);

            this.Close();
        
        }
    }
}

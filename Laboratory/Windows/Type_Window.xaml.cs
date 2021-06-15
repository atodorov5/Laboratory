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
                int? testtypeID;
                

                Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter testTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter();
                testTypeTableAdapter.add_testtype(test_nameTB.Text, test_valueTB.Text,Convert.ToDouble( test_priceTB.Text),(int) type_groupComboBox.SelectedValue, out testtypeID);


                Laboratory.laboratorydbDataSetTableAdapters.QueriesTableAdapter queryTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.QueriesTableAdapter();
                if ((bool)gender_checkBox.IsChecked)
                {
                    queryTableAdapter.add_ref_value("0", Convert.ToDouble(test_minTB.Text), Convert.ToDouble(test_maxTB.Text), testtypeID);
                    queryTableAdapter.add_ref_value("1", Convert.ToDouble(test_min2TB.Text), Convert.ToDouble(test_max2TB.Text), testtypeID);
                }
                else
                {
                    queryTableAdapter.add_ref_value("x", Convert.ToDouble(test_minTB.Text), Convert.ToDouble(test_maxTB.Text), testtypeID);
                }



                    MessageBox.Show("Успешно!");
                 }



                DialogResult = true;
                this.Close();
            }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table type_group. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter laboratorydbDataSettype_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
            laboratorydbDataSettype_groupTableAdapter.Fill(laboratorydbDataSet.type_group);
            System.Windows.Data.CollectionViewSource type_groupViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("type_groupViewSource")));
            type_groupViewSource.View.MoveCurrentToFirst();
        }
    }
}

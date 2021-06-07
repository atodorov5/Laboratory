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
    /// Interaction logic for Edit_user_Window.xaml
    /// </summary>
    public partial class Edit_user_Window : Window
    {
        DataRowView row;
        public Edit_user_Window(DataRowView rowview)
        {
            InitializeComponent();
            row = rowview;

            firstnameTB.Text = row.Row.ItemArray[1].ToString();
            surnameTB.Text = row.Row.ItemArray[2].ToString();
            lastnameTB.Text = row.Row.ItemArray[3].ToString();
            
            
            pinTB.Text = row.Row.ItemArray[4].ToString();
            addressTB.Text = row.Row.ItemArray[5].ToString();
            phoneTB.Text = row.Row.ItemArray[6].ToString();

            /*labCB.SelectedItem = row.Row.ItemArray[11].ToString();
            rolesComboBox.SelectedItem = row.Row.ItemArray[7].ToString();*/

            /* var comboBoxItem = rolesComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == row.Row.ItemArray[7].ToString());
             int index = rolesComboBox.SelectedIndex = rolesComboBox.Items.IndexOf(comboBoxItem);

             rolesComboBox.SelectedIndex = index;

             var comboBoxItemLab = labCB.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == row.Row.ItemArray[11].ToString());
             int index2 = labCB.SelectedIndex = labCB.Items.IndexOf(comboBoxItemLab);

             labCB.SelectedIndex = 2;*/

            




            MessageBox.Show(row.Row.ItemArray[11].ToString()+row.Row.ItemArray[7].ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table roles. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.rolesTableAdapter laboratorydbDataSetrolesTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.rolesTableAdapter();
            laboratorydbDataSetrolesTableAdapter.Fill(laboratorydbDataSet.roles);
            System.Windows.Data.CollectionViewSource rolesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rolesViewSource")));
            rolesViewSource.View.MoveCurrentToFirst();

            // Load data into the table clinicbranch. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter laboratorydbDataSetclinicbranchTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter();
            laboratorydbDataSetclinicbranchTableAdapter.Fill(laboratorydbDataSet.clinicbranch);
            System.Windows.Data.CollectionViewSource clinicbranchViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clinicbranchViewSource")));
            clinicbranchViewSource.View.MoveCurrentToFirst();

            

        }

        private void save_user(object sender, RoutedEventArgs e)
        {
            rolesComboBox.SelectedItem = row.Row.ItemArray[7].ToString();
            
            
        }
    }
}

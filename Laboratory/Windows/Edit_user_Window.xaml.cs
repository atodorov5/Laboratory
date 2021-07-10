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

            firstnameTB.Text = row.Row.ItemArray[1].ToString();
            surnameTB.Text = row.Row.ItemArray[2].ToString();
            lastnameTB.Text = row.Row.ItemArray[3].ToString();
            pinTB.Text = row.Row.ItemArray[4].ToString();
            addressTB.Text = row.Row.ItemArray[5].ToString();
            phoneTB.Text = row.Row.ItemArray[6].ToString();
            labCB.SelectedValue = (int)row.Row.ItemArray[9];
            rolesComboBox.SelectedValue = (int)row.Row.ItemArray[10];

            if (Convert.ToBoolean(row.Row.ItemArray[8]))
                status_user.IsChecked = false;
            else
                status_user.IsChecked = true;
        }

        private void save_user(object sender, RoutedEventArgs e)
        {

            laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter USERTableAdapter = new laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            bool stat = true;
            if ((bool)status_user.IsChecked)
                stat = false;
            USERTableAdapter.update_users((int)row.Row.ItemArray[0], pinTB.Text,firstnameTB.Text, surnameTB.Text, lastnameTB.Text, addressTB.Text, phoneTB.Text,(int) labCB.SelectedValue,(int) rolesComboBox.SelectedValue, stat);

            Close();
        }
    }
}

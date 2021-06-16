using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Laboratory.Pages;
using MySql.Data.MySqlClient;

namespace Laboratory.Pages
{
    /// <summary>
    /// Interaction logic for Fill_test_Page.xaml
    /// </summary>
    public partial class Fill_test_Page : Page
    {
       
        public Fill_test_Page()
        {
            InitializeComponent();
           

        }
        private bool CustomFilter(object obj)
        {
            if (string.IsNullOrEmpty(text_filterTB.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(text_filterTB.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void txtData_TextChanged(object sender, TextChangedEventArgs e)
        {


        }




        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table clinicbranch. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_testTableAdapter testTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_testTableAdapter();
            testTableAdapter.FillPendingTests(laboratorydbDataSet.retrieve_test);
            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("retrieve_testViewSource")));
            testViewSource.View.MoveCurrentToFirst();
           
            
        }
        private void newtab_test_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Test item = (Test)retrieve_testListView.SelectedItem;
            TabItem newTabItem = new TabItem
            {
                Header = "ТЕСТ-Реф.№" + item.idTest,
                Name = "Test"
            };
            

            Grid g = new Grid();
            Frame f = new Frame();
            f.Content = new Test_values_Page(item, tabctrl, retrieve_testListView);
            g.Children.Add(f);
            newTabItem.Content = g;
            
            tabctrl.Items.Add(newTabItem);



            
            Dispatcher.BeginInvoke(new Action(() => tabctrl.SelectedItem = newTabItem));
        }



        private void Button_Close(object sender, RoutedEventArgs e)
        {
            TabItem tab = (TabItem)tabctrl.SelectedItem;
            tab.Template = null;
            tabctrl.Items.Remove(tab);
            tabctrl.Items.Refresh();

        }

    }
}

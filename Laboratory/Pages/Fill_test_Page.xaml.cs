using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Laboratory.Windows;
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



        TestCollection col3;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(col3 == null)
                col3 = new TestCollection();
            retrieve_testListView.ItemsSource = col3.fillColection();


            CollectionViewSource.GetDefaultView(retrieve_testListView.ItemsSource).Filter = UserFilter;

        }

        private void Filter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(retrieve_testListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("idTest", ListSortDirection.Ascending));


            CollectionViewSource.GetDefaultView(retrieve_testListView.ItemsSource).Refresh();
           
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(text_filterTB.Text))
                return true;

            var test1 = (Test)item;

            return test1.idTest.ToString().StartsWith(text_filterTB.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void newtab_test_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Test item = (Test)retrieve_testListView.SelectedItem;
            TabItem newTabItem = new TabItem
            {
                Header = "ТЕСТ-Реф.№" + item.idTest,
                Name = "Test" + item.idTest
            };
                      
            if (tabctrl.Items.OfType<TabItem>().SingleOrDefault(n => n.Name == "Test" + item.idTest) == null)
            {

                Grid g = new Grid();
                Frame f = new Frame();
                f.Content = new Test_values_Page(item, tabctrl, retrieve_testListView);
                g.Children.Add(f);
                newTabItem.Content = g;
            
                tabctrl.Items.Add(newTabItem);

                Dispatcher.BeginInvoke(new Action(() => tabctrl.SelectedItem = newTabItem));
            }
            else
                tabctrl.SelectedItem = tabctrl.Items.OfType<TabItem>().SingleOrDefault(n => n.Name == "Test" + item.idTest);            

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

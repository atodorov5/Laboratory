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
    /// Interaction logic for TestGroupWindow.xaml
    /// </summary>
    public partial class TestGroupWindow : Window
    {
        DataRowView row;
        public TestGroupWindow()
        {
            InitializeComponent();
        }

        public TestGroupWindow(DataRowView groupRow)
        {
            InitializeComponent();
            row = groupRow;
            group_name.Text = groupRow[1].ToString();
        }

        private void Button_AddGroup(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            if (content == "Добави") {
                laboratorydbDataSetTableAdapters.type_groupTableAdapter type_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
                type_groupTableAdapter.insert_group(group_name.Text);
            }
            else
            {
                laboratorydbDataSetTableAdapters.type_groupTableAdapter type_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
                type_groupTableAdapter.edit_group((int)row[0],group_name.Text);
            }
            this.Close();
        }
    }
}

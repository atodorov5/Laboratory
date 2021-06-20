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
    /// Interaction logic for Ref_Values_Window.xaml
    /// </summary>
    public partial class Ref_Values_Window : Window
    {
        DataRowView row;
        public Ref_Values_Window(DataRowView r)
        {
            InitializeComponent();
            this.row = r;
            titleLabel.Content = "Реф. стойности за " + row[5].ToString();
            if (row[1].ToString() == "1")
                genderLb.Content = "Жени:";
            else if (row[1].ToString() == "0")
                genderLb.Content = "Мъже:";

            allMin.Text = row[2].ToString();
            allMax.Text = row[3].ToString();
        }

        private void Button_save(object sender, RoutedEventArgs e)
        {
            
            laboratorydbDataSetTableAdapters.ref_valueTableAdapter ref_valTableAdapter = new laboratorydbDataSetTableAdapters.ref_valueTableAdapter();
            ref_valTableAdapter.edit_ref_value((int)row[0],Convert.ToDouble( allMin.Text), Convert.ToDouble(allMax.Text));
            this.Close();
        }
    }
}

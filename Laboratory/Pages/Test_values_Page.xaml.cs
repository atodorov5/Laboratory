using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratory.Pages
{
    /// <summary>
    /// Interaction logic for Test_values_Page.xaml
    /// </summary>
    public partial class Test_values_Page : Page
    {
        Test test;
        TabControl tabctrl;
        ListView list;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Test_values_Page(Test row,TabControl tabctrl, ListView ls)
        {
            InitializeComponent();
            this.test = row;
            name_label.Content = row.getFullName();
            pin_label.Content = row.pin;
            select_result_byTestIDListView.UnselectAll();
            ref_numbTV.Content = row.refnumber;
            this.tabctrl = tabctrl;
            this.list = ls;
            log4net.Config.XmlConfigurator.Configure();

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table clinicbranch. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter resultTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter();
            resultTableAdapter.Fill(laboratorydbDataSet.select_result_byTestID, test.idTest, 0);
            CollectionViewSource resultViewSource = (CollectionViewSource)this.FindResource("select_result_byTestIDViewSource");
            resultViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Save_Results(object sender, RoutedEventArgs e)
        {
            int count = select_result_byTestIDListView.Items.Count;


            laboratorydbDataSetTableAdapters.QueriesTableAdapter queryTableAdapter = new laboratorydbDataSetTableAdapters.QueriesTableAdapter();
            

            for (int i = 0; i < count; i++)
            {
                DataRowView item = (DataRowView)select_result_byTestIDListView.Items[i];

                queryTableAdapter.enter_test_result(item[0].ToString(), (int)item[10],(int)item[18]);
            }

            queryTableAdapter.set_test_status(test.idTest);

            log.Info(Properties.Settings.Default.user + " обработи тест -" + test.idTest);


            MessageBox.Show("Изследването е обработено!");           
            TestCollection col3 = new TestCollection();
            list.ItemsSource = col3.fillColection();

            TabItem tab = (TabItem)tabctrl.SelectedItem;
            tab.Template = null;
            tabctrl.Items.Remove(tab);
            tabctrl.Items.Refresh();

            sendEmail();




        }

        private void sendEmail()
        {
            var fromAddress = new MailAddress("laboratory_sintex@mail.bg", "Лаборатория Синтех");
            var toAddress = new MailAddress(test.p_email, test.getFullName());
            const string fromPassword = "lab123";
            const string subject = "Вашето изследвани е обработено!";
            string body = "Здравейте, " + test.getFullName() + " изследване №" + test.idTest + " e обработено";

            var smtp = new SmtpClient
            {
                Host = "smtp.mail.bg",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}

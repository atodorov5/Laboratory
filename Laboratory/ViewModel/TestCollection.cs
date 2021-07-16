using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Laboratory
{
    class TestCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                CollectionViewSource.GetDefaultView(Collection).Refresh();
                RaisePropertyChanged("Filter");
            }
        }
        public  ObservableCollection<Test> Collection { get; set; }

 
        public ObservableCollection<Test> fillColection()
        {
            Collection = new ObservableCollection<Test>();
            CollectionViewSource.GetDefaultView(Collection).Filter = new Predicate<object>(Contains);

            using (var conn = DBConfig.Connection)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("retrieve_pending_tests", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        var obj = new Test()
                        {
                            idTest = (int)dr["idTest"],
                            refnumber = (string)dr["refNumber"],
                            testDate = (DateTime)dr["testDate"],
                            pin = (string)dr["pin"],
                            p_name = (string)dr["p_name"],
                            p_surname = (string)dr["p_surname"],
                            p_lastname = (string)dr["p_lastname"],
                            p_email = (string)dr["email"],
                        };
                        Collection.Add(obj);
                    }

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Грешка " + ex);
                }
             
            }
            return Collection;
        }
        public TestCollection()
        {
            Collection = new ObservableCollection<Test>();
            CollectionViewSource.GetDefaultView(Collection).Filter = new Predicate<object>(Contains);

            using (var conn = DBConfig.Connection)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("retrieve_pending_tests", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        
                            var obj = new Test()
                        {
                            idTest = (int)dr["idTest"],
                            refnumber = (string)dr["refNumber"],
                            testDate = (DateTime)dr["testDate"],
                            pin = (string)dr["pin"],
                            p_name = (string)dr["p_name"],
                            p_surname= (string)dr["p_surname"],
                            p_lastname = (string)dr["p_lastname"],
                            p_email = (string)dr["email"],
                            };
                        Collection.Add(obj);
                    }
 
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Грешка " + ex);
                }

            }


        }

        private bool Contains(object o)
        {
            if (Filter != null)
            {
                Test test = (Test)o;
                return test.idTest.ToString().Contains(Filter);
            }
            return true;
        }
       
        private void RaisePropertyChanged(string t)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(t));
        }
    }
}

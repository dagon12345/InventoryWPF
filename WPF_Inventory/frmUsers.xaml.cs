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
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;
using System.Data;

namespace WPF_Inventory
{
    /// <summary>
    /// Interaction logic for frmUsers.xaml
    /// </summary>
    public partial class frmUsers : Window
    {

        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;

        MySqlDataAdapter da;
        DataTable dt;

        BackgroundWorker _bgWorker;
        bool _iNeedToCloseAfterBgWorker;


        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ////completed here
            lblnetstatus.Text = "Successfully connected to SQL Server.";
            lblnetstatus.Foreground = Brushes.SeaGreen;
           // btnadd.IsEnabled = true;
           // btnupdate.IsEnabled = true;
           // btndelete.IsEnabled = true;
            // btn_rc.Enabled = true;
            if (_iNeedToCloseAfterBgWorker)
                Close();
        }


        public void display()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                da = new MySqlDataAdapter("Select NameOfStaff,Section,Division,Piece,TypeOfICTEquipment,Type,YearAcquired FROM db_inventory  WHERE NameOfStaff = '"+ lblname.Text +"' ORDER BY id DESC", con);
                dt = new DataTable();
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                //this.datagrid.Columns[0].Visibility = Visibility.Hidden;


            });

        }


        public void statusbar()
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from db_statusbar ";
            //  cmd.Parameters.AddWithValue("Name", string.Format("%{0}%", txtsearch.Text));
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {

                    lbluserid.Text = dr["UserID"].ToString();
                    lblusertype.Text = dr["Usertype"].ToString();
                    lblname.Text = dr["Name"].ToString();

                });
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Access lbl_internet here


            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your UI-related code here
                lblnetstatus.Text = "Detecting SQL Connection please wait...";
                lblnetstatus.Foreground = Brushes.Yellow;
            });


            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your UI-related code here
              // btnadd.IsEnabled = false;
              // btndelete.IsEnabled = false;
              // btnupdate.IsEnabled = false;
            });



            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your UI-related code here
                // panel.IsEnabled = false;
            });



            // Do long lasting work above is the before process before final
            Thread.Sleep(1000);



            try
            {


                con = new MySqlConnection(cs.DBcon);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                statusbar();

                display();
             



            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Your UI-related code here
                    lblnetstatus.Text = "Can't connect to SQL host.";
                    lblnetstatus.Foreground = Brushes.Crimson;
                });

                MessageBox.Show(ex.Message);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Your UI-related code here
                    Application.Current.Shutdown();
                });

            }
        }



        public frmUsers()
        {
            InitializeComponent();


            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _bgWorker.RunWorkerAsync();

        }

        private void txtsearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
           
            // this.datagrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
         
           
        }

        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {

            if (txtsearch.Text == "")
            {
                MessageBox.Show("Please input what type of equipment.","Input",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select NameOfStaff,Section,Division,Piece,TypeOfICTEquipment,Type,YearAcquired FROM db_inventory WHERE Type LIKE '" + txtsearch.Text + "%' AND NameofStaff = '" + lblname.Text + "'";
                //  cmd.Parameters.AddWithValue("Name", string.Format("%{0}%", txtsearch.Text));
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
            }
            
        }

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            display();
        }
    }
}

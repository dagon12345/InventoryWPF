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
using System.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Threading;

namespace WPF_Inventory
{
    /// <summary>
    /// Interaction logic for ActivityLogs.xaml
    /// </summary>
    /// 
    public partial class ActivityLogs : Window
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;



        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;

        BackgroundWorker _bgWorker;
        bool _iNeedToCloseAfterBgWorker;
        public ActivityLogs()
        {
            InitializeComponent();

            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _bgWorker.RunWorkerAsync();



        }
        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ////completed here
            lblnetstatus.Text = "Successfully connected to SQL Server.";
            lblnetstatus.Foreground = Brushes.SeaGreen;
           // btninsert.IsEnabled = true;
           // btndelete.IsEnabled = true;
            // btn_rc.Enabled = true;
            if (_iNeedToCloseAfterBgWorker)
                Close();
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
              //  btninsert.IsEnabled = false;
              //  btndelete.IsEnabled = false;
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
                display();
                statusbar();



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
        public void display()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {

            da = new MySqlDataAdapter("Select * from db_activitylogs ORDER BY id DESC", con);
            dt = new DataTable();
            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            this.datagrid.Columns[0].Visibility = Visibility.Hidden;
                //datagrid.Columns[1].DefaultCellST = "yyyy-MM-dd hh:mm:ss";
                // datagrid.Columns[]
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



                    txtstatus1.Text = dr["UserID"].ToString();
                    txtusertypestatus1.Text = dr["Usertype"].ToString();

                });





            }
        }
        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
          
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fromdatepicker.Text == "")
            {
                MessageBox.Show("Please fill the from start date before retrieving");
                fromdatepicker.Focus();
            }
            else if (todatepicker.Text == "")
            {
                MessageBox.Show("Please fill the from start date before retrieving");
                todatepicker.Focus();
            }
            else
            {
 

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from db_activitylogs WHERE Date BETWEEN  @dtfrom AND @dtto ORDER BY id DESC ";
                cmd.Parameters.Add("@dtfrom", MySqlDbType.Date).Value = fromdatepicker.SelectedDate.Value;
                cmd.Parameters.Add("@dtto", MySqlDbType.Date).Value = todatepicker.SelectedDate.Value;
          
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                this.datagrid.Columns[0].Visibility = Visibility.Hidden;
            
            }
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            display();
        }
    }
}

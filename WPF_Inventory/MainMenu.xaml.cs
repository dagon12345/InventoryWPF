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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {


        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;


        BackgroundWorker _bgWorker;
        bool _iNeedToCloseAfterBgWorker;



        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ////completed here
            lblnetstatus.Text = "Successfully connected to SQL Server.";
            lblnetstatus.Foreground = Brushes.SeaGreen ;
            panel.IsEnabled = true;
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
                panel.IsEnabled = false;
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


        public MainMenu()
        {
            InitializeComponent();



            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _bgWorker.RunWorkerAsync();




        }




        private void frm_mainmenu_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();


        }


        private void logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("You are about to logout, Continue?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Hide();
                Login log = new Login();
                log.Show();

            }
        }

        private void activitylogs_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogs al = new ActivityLogs();
            al.Show();
        }

        private void txtstatus1_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void frm_mainmenu_Loaded(object sender, RoutedEventArgs e)
        {
           


        }

        public void statusbar()
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select UserID,Usertype FROM db_statusbar";
            //  cmd.Parameters.AddWithValue("Name", string.Format("%{0}%", txtsearch.Text));
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Your UI-related code here
                    txtstatus1.Text = dr["UserID"].ToString();
                    txtusertypestatus1.Text = dr["Usertype"].ToString();
                });


             
            }
        }

        private void inventory_Click(object sender, RoutedEventArgs e)
        {
            frm_inventory fi = new frm_inventory();
            fi.Show();
        }

        private void information_Click(object sender, RoutedEventArgs e)
        {
            frmUsers fu = new frmUsers();
            fu.Show();
        }
    }
}

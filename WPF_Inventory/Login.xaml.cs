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
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace WPF_Inventory
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;

        string utype = "";

        BackgroundWorker _bgWorker;
        bool _iNeedToCloseAfterBgWorker;


        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ////completed here
            btnlogin.IsEnabled = true;
            lblnetstatus.Content = "Successfully connected to SQL Server.";
            lblnetstatus.Foreground = Brushes.SeaGreen;
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
                lblnetstatus.Content = "Detecting Internet Connection please wait...";
                lblnetstatus.Foreground = Brushes.DarkBlue;
            });

            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your UI-related code here
                btnlogin.IsEnabled = false;
            });




            // Do long lasting work above is the before process before final
            Thread.Sleep(1000);

            ///// INTERNET IS UP OR DOWN CODE
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    //MessageBox.Show("Internet Up");



                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Your UI-related code here
                        lblnetstatus.Content = "Checking for Updates....";
                        lblnetstatus.Foreground = Brushes.SeaGreen;

                    });

                    WebClient webClient = new WebClient();
                    // var client = new WebClient();

                    if (!webClient.DownloadString("https://www.dropbox.com/s/cwts7oep596v51n/Update.txt?dl=1").Contains("1.0.0"))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            lblnetstatus.Content = "Update available!...";
                        });

                        if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                if (File.Exists(@".\QueueInstaller.msi")) { File.Delete(@".\QueueInstaller.msi"); }
                                webClient.DownloadFile("https://www.dropbox.com/s/ewfnnfa2yn8442k/InventoryInstaller.zip?dl=1", @"QueueInstaller.zip");
                                string zipPath = @".\QueueInstaller.zip";
                                string extractPath = @".\";
                                ZipFile.ExtractToDirectory(zipPath, extractPath);

                                Process process = new Process();
                                process.StartInfo.FileName = "msiexec";
                                process.StartInfo.Arguments = String.Format("/i QueueInstaller.msi");

                                this.Close();

                                process.Start();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                    }


                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Your UI-related code here
                        lblnetstatus.Content = "Internet is up.";
                        lblnetstatus.Foreground = Brushes.SeaGreen;
                    });
                }
            }

            catch
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Your UI-related code here
                    lblnetstatus.Content = "No internet connection.";
                    lblnetstatus.Foreground = Brushes.Crimson;
                });
                // MessageBox.Show("Internet Down");

                //lblnetstatus
            }




            Application.Current.Dispatcher.Invoke(() =>
            {
                // Your UI-related code here
                lblnetstatus.Content = "Detecting SQL Connection.";
                lblnetstatus.Foreground = Brushes.DarkBlue;
            });



            try
            {
             

                con = new MySqlConnection(cs.DBcon);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

            }
            catch(Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Your UI-related code here
                    lblnetstatus.Content = "Can't connect to SQL host.";
                    lblnetstatus.Foreground = Brushes.Crimson;
                });

                MessageBox.Show("Can't connect to sql host please turn on SQL Database.");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Your UI-related code here
                    Application.Current.Shutdown();
                });
               
            }

        





        }



    
    public Login()
        {
            InitializeComponent();

            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _bgWorker.RunWorkerAsync();




            txtusername.Focus();

            lbluserid.Visibility = Visibility.Hidden;
            lbluseridlabel.Visibility = Visibility.Hidden;


            lblusertype.Visibility = Visibility.Hidden;
            lblusertypetext.Visibility = Visibility.Hidden;


            lblname.Visibility = Visibility.Hidden;
            txtname.Visibility = Visibility.Hidden;



        


        }



        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtusername.Text == "" || txtpassword.Password == "")
            {
                MessageBox.Show("Fill The Fields", "Fill", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else 
            {
                MySqlCommand cmd0 = con.CreateCommand();
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "SELECT * FROM db_register WHERE Username like'" + txtusername.Text + "'";
                cmd0.ExecuteNonQuery();
                DataTable dt0 = new DataTable();
                MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0);
                da0.Fill(dt0);
                foreach (DataRow dr in dt0.Rows)
                {
                    lblusertype.Text = dr["UserType"].ToString();
                    lbluserid.Text = dr["OfficeID"].ToString();
                    txtname.Text = dr["Name"].ToString();
                }

       

                int i = 0;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from db_register where Username='" + txtusername.Text + "' and BINARY Password='" + txtpassword.Password + "' and Usertype='" + lblusertype.Text + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (dt.Rows.Count > 0)
                {
                    utype = dt.Rows[0][3].ToString();
                    if (utype == "Administrator")
                    {


                        MySqlCommand cmd2 = con.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "update db_statusbar SET UserID=@UserID, Usertype=@Usertype, name=@name";
                        cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
                        cmd2.Parameters.AddWithValue("@Usertype", lblusertype.Text);
                        cmd2.Parameters.AddWithValue("@name", txtname.Text);
                        cmd2.ExecuteNonQuery();


                        this.Hide();


                        /// ACTIVITY LOGS

                        MySqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                        cmd3.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                        cmd3.Parameters.AddWithValue("@UserID", lbluserid.Text);
                        cmd3.Parameters.AddWithValue("@Activity", "has logged in to the system with the USERNAME " + txtusername.Text + " and USERTYPE " + lblusertype.Text);
                        cmd3.ExecuteNonQuery();

                        MainMenu mm = new MainMenu();
                  
                        mm.Show();


                    }
                    else if (utype == "User")
                    {
                        MySqlCommand cmd2 = con.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "update db_statusbar SET UserID=@UserID, Usertype=@Usertype, name=@name";
                        cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
                        cmd2.Parameters.AddWithValue("@Usertype", lblusertype.Text);
                        cmd2.Parameters.AddWithValue("@name", txtname.Text);
                        cmd2.ExecuteNonQuery();
                        this.Hide();

                        /// ACTIVITY LOGS

                        MySqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                        cmd3.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                        cmd3.Parameters.AddWithValue("@UserID", lbluserid.Text);
                        cmd3.Parameters.AddWithValue("@Activity", "has logged in to the system with the USERNAME " + txtusername.Text + " and USERTYPE " + lblusertype.Text);
                        cmd3.ExecuteNonQuery();


                        MainMenu mm = new MainMenu();
                   
                        mm.inventory.IsEnabled = false;
                        mm.activitylogs.IsEnabled = false;
                        mm.registration.IsEnabled = false;
            
                        mm.Show();


                    }


                
                       
                    }
                else
                {
                    MessageBox.Show("Invalid password entered.");
                }



            }
        }

        private void frm_login_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            
        }
    }
}

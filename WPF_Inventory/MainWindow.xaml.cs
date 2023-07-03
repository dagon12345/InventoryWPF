using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using MySql.Data.MySqlClient;


namespace WPF_Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //MySqlConnection con = new MySqlConnection("server=localhost; port=3306; userid=root; database=inventory");

        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;



        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;

        BackgroundWorker _bgWorker;
        bool _iNeedToCloseAfterBgWorker;





        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ////completed here
            lblnetstatus.Text = "Successfully connected to SQL Server.";
            lblnetstatus.Foreground = Brushes.SeaGreen;
            btninsert.IsEnabled = true;
            btndelete.IsEnabled = true;
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
                btninsert.IsEnabled = false;
                btndelete.IsEnabled = false;
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




        public MainWindow()
        {
            InitializeComponent();
            lbl_id.Visibility = Visibility.Hidden;

            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _bgWorker.RunWorkerAsync();


        }

        public void display()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
               
          

            da = new MySqlDataAdapter("Select * from db_register ORDER BY id DESC", con);
            dt = new DataTable();
            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            this.datagrid.Columns[0].Visibility = Visibility.Hidden;

            });

        }

        public void clear()
        {
            txtsearch.Clear();
            lbl_id.Text = "ID";
            btninsert.Content = "Register";
            cmbusertype.Text = "";
            txtusername.Clear();
            txtpassword.Clear();
            txtname.Clear();
            txtofficeid.Clear();
            txtofficeid.Focus();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            btninsert.Content = "Register";
            txtofficeid.Focus();
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            if (btninsert.Content == "Register")
            {
                if (txtofficeid.Text == "" || txtname.Text == "" || cmbusertype.Text == "" || txtusername.Text == "" || txtpassword.Text == "")
                {
                    MessageBox.Show("Please Fill All the Fields!");
                }

                else
                {
                    int i = 0;
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from db_register where Username='" + txtusername.Text + "' OR OfficeID='" + txtofficeid.Text + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 0)
                    {
                        if (MessageBox.Show("Are you sure you want to register this data?", "Register", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MySqlCommand cmd1 = con.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = "insert into db_register (OfficeID,Name,Usertype,Username,Password)values (@OfficeID,@Name,@Usertype,@Username,@Password)";
                            cmd1.Parameters.AddWithValue("@OfficeID", txtofficeid.Text);
                            cmd1.Parameters.AddWithValue("@Name", txtname.Text);
                            cmd1.Parameters.AddWithValue("@Usertype", cmbusertype.Text);
                            cmd1.Parameters.AddWithValue("@Username", txtusername.Text);
                            cmd1.Parameters.AddWithValue("@Password", txtpassword.Text);
                            cmd1.ExecuteNonQuery();




                            //// ACTIVITY LOGS HERE
                            ///
                            DateTime theDate = DateTime.Now;
                            theDate.ToString("yyyy-MM-dd hh:mm:ss");

                            MySqlCommand cmd2 = con.CreateCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                            cmd2.Parameters.AddWithValue("@Date", DateTime.Now);
                            cmd2.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                            cmd2.Parameters.AddWithValue("@UserID", txtstatus1.Text);
                            cmd2.Parameters.AddWithValue("@Activity", "has REGISTERED new user with the usertype " + cmbusertype.Text + " and a username of " + txtusername.Text);
                            cmd2.ExecuteNonQuery();


                            clear();

                            display();
                            MessageBox.Show(" Registered Successfully!.");


                        }
                    }
                    else
                    {
                        MessageBox.Show("Name Already Exist Please Enter Another.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

            }


            if(btninsert.Content == "Update")
            {
                if (txtofficeid.Text == "" || txtname.Text == "" || cmbusertype.Text == "" || txtusername.Text == "" || txtpassword.Text == "")
                {
                    MessageBox.Show("Can't Update While Empty!");
                }

                else
                {

                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd = new MySqlCommand("update db_register set OfficeID=@OfficeID, Name=@Name, Usertype = @Usertype ,Username = @Username,Password =@Password WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", lbl_id.Text);
                    cmd.Parameters.AddWithValue("@OfficeID", txtofficeid.Text);
                    cmd.Parameters.AddWithValue("@Name", txtname.Text);
                    cmd.Parameters.AddWithValue("@Usertype", cmbusertype.Text);
                    cmd.Parameters.AddWithValue("@Username", txtusername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtpassword.Text);
                    cmd.ExecuteNonQuery();






                    ///LOGS
                    DateTime theDate = DateTime.Now;
                    theDate.ToString("yyyy-MM-dd hh:mm:ss");

                    MySqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                    cmd2.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd2.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                    cmd2.Parameters.AddWithValue("@UserID", txtstatus1.Text);
                    cmd2.Parameters.AddWithValue("@Activity", "has UPDATED user with the usertype " + cmbusertype.Text + " and a username of " + txtusername.Text);
                    cmd2.ExecuteNonQuery();

                    display();
                    clear();




                    // display();
                    MessageBox.Show("Successfully Updated");

                }
            }
        }
       
        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {

          
        }


        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            display();

            clear();
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (cmbusertype.Text == "" || txtusername.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Retrieve some data before deleting");
            }
            else if (MessageBox.Show("Are you sure you want to delete this data?","Delete",MessageBoxButton.YesNo,MessageBoxImage.Warning)== MessageBoxResult.Yes )
            {






                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd = new MySqlCommand("DELETE FROM db_register WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", lbl_id.Text);
                cmd.ExecuteNonQuery();


                //LOGS
                DateTime theDate = DateTime.Now;
                theDate.ToString("yyyy-MM-dd hh:mm:ss");

                MySqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                cmd2.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd2.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                cmd2.Parameters.AddWithValue("@UserID", txtstatus1.Text);
                cmd2.Parameters.AddWithValue("@Activity", "has DELETED user with the usertype " + cmbusertype.Text + " and a username of " + txtusername.Text);
                cmd2.ExecuteNonQuery();

                display();
                clear();




                // display();
                MessageBox.Show("Data successfully deleted.");

            }
            
        }
      
        private void btnretrieve_Click(object sender, RoutedEventArgs e)
        {
            if (txtsearch.Text == "")
            {
                MessageBox.Show("Please fill the searchbar before retrieving");
                txtsearch.Focus();
            }
            else

            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from db_register where OfficeID = '" + txtsearch.Text + " ' OR Username = '" + txtsearch.Text + " '";
              //  cmd.Parameters.AddWithValue("Name", string.Format("%{0}%", txtsearch.Text));
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                this.datagrid.Columns[0].Visibility = Visibility.Hidden;
                foreach (DataRow dr in dt.Rows)
                {
                    lbl_id.Text = dr["id"].ToString();
                    txtofficeid.Text = dr["OfficeID"].ToString();
                    txtname.Text = dr["Name"].ToString();
                    cmbusertype.Text = dr["Usertype"].ToString();
                    txtusername.Text = dr["Username"].ToString();
                    txtpassword.Text = dr["Password"].ToString();

                }


                btninsert.Content = "Update";
            }

        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
           
                /*
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd = new MySqlCommand("select * from register where id LIKE '" + txtsearch.Text + "%' OR Username LIKE '" + txtsearch.Text + "%'", con);
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    datagrid.ItemsSource = dt.DefaultView;
                  */
                    //dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
               
            
        }

        private void datagrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

    }
}

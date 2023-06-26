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

     

        public Login()
        {
            InitializeComponent();

            con = new MySqlConnection(cs.DBcon);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();


            txtusername.Focus();

            lbluserid.Visibility = Visibility.Hidden;
            lbluseridlabel.Visibility = Visibility.Hidden;


            lblusertype.Visibility = Visibility.Hidden;
            lblusertypetext.Visibility = Visibility.Hidden;



    

            ///// INTERNET IS UP OR DOWN CODE
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    //MessageBox.Show("Internet Up");
                    lblnetstatus.Content = "Internet is up.";
                }
            }
            catch
            {
                // MessageBox.Show("Internet Down");
                lblnetstatus.Content = "No internet connection.";
               //lblnetstatus
            }


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
                        cmd2.CommandText = "update db_statusbar SET UserID=@UserID, Usertype=@Usertype";
                        cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
                        cmd2.Parameters.AddWithValue("@Usertype", lblusertype.Text);
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
                        cmd2.CommandText = "update db_statusbar SET UserID=@UserID, Usertype=@Usertype";
                        cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
                        cmd2.Parameters.AddWithValue("@Usertype", lblusertype.Text);
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
            Application.Current.Shutdown();
        }
    }
}

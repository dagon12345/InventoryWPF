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

namespace WPF_Inventory
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {


        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;





        public MainMenu()
        {
            InitializeComponent();
     
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
            con = new MySqlConnection(cs.DBcon);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            statusbar();


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
                txtstatus1.Text = dr["UserID"].ToString();
                txtusertypestatus1.Text = dr["Usertype"].ToString();
            }
        }
    }
}

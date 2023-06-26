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


        public ActivityLogs()
        {
            InitializeComponent();



        }

        public void display()
        {

            da = new MySqlDataAdapter("Select * from db_activitylogs ORDER BY id DESC", con);
            dt = new DataTable();
            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            this.datagrid.Columns[0].Visibility = Visibility.Hidden;
            //datagrid.Columns[1].DefaultCellST = "yyyy-MM-dd hh:mm:ss";
            // datagrid.Columns[]

        }
      

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            con = new MySqlConnection(cs.DBcon);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            display();
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

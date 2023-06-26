using System;
using System.Collections.Generic;
using System.Data;
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

namespace WPF_Inventory
{
    /// <summary>
    /// Interaction logic for frm_inventory.xaml
    /// </summary>
    public partial class frm_inventory : Window
    {

        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;

        MySqlDataAdapter da;
        DataTable dt;


        public void display()
        {

            da = new MySqlDataAdapter("Select NameofStaff,Section,Division,Piece,TypeOfICTEquipment,Type,YearAcquired FROM db_inventory ORDER BY id DESC", con);
            dt = new DataTable();
            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            //this.datagrid.Columns[0].Visibility = Visibility.Hidden;

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
                txtstatus1.Text = dr["UserID"].ToString();
                txtusertypestatus1.Text = dr["Usertype"].ToString();


            }
        }

        public frm_inventory()
        {
            InitializeComponent();

            con = new MySqlConnection(cs.DBcon);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            display();
            statusbar();

            txtnameofstaff.Focus();


        }
        public void clear()
        {
            txtnameofstaff.Clear();
            txtsection.Clear();
            txtdivision.Clear();
            txtpiece.Clear();
            txttypeofict.Clear();
            cmbtype.Text = "";
            datetimepicker.Text = "";
            txtnameofstaff.Focus();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (txtnameofstaff.Text == "" || txtsection.Text == "" || txtdivision.Text == "" || txtpiece.Text == "" || txttypeofict.Text == "" || cmbtype.Text == "" || datetimepicker.Text == "")
            {
                MessageBox.Show("Please Fill All the Fields!");
            }

            else
            {
                /*
                int i = 0;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from db_inventory where Username='" + txtusername.Text + "' OR OfficeID='" + txtofficeid.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {*/
                if (MessageBox.Show("Are you sure you want to register this data?", "Register", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MySqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "insert into db_inventory (NameOfStaff,Section,Division,Piece,TypeOfICTEquipment,Type,YearAcquired)values (@NameOfStaff,@Section,@Division,@Piece,@TypeOfICTEquipment,@Type,@YearAcquired)";
                    cmd1.Parameters.AddWithValue("@NameOfStaff", txtnameofstaff.Text);
                    cmd1.Parameters.AddWithValue("@Section", txtsection.Text);
                    cmd1.Parameters.AddWithValue("@Division", txtdivision.Text);
                    cmd1.Parameters.AddWithValue("@Piece", txtpiece.Text);
                    cmd1.Parameters.AddWithValue("@TypeOfICTEquipment", txttypeofict.Text);
                    cmd1.Parameters.AddWithValue("@Type", cmbtype.Text);
                    cmd1.Parameters.AddWithValue("@YearAcquired", datetimepicker.Text);
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
                    cmd2.Parameters.AddWithValue("@Activity", " has added data into the system. Number of Pieces: " + txtpiece.Text + " Type of ICT Equipment: " + txttypeofict.Text + " Hardware Type: " + cmbtype.Text  );
                    cmd2.ExecuteNonQuery();


                    clear();

                    display();
                    MessageBox.Show(" Registered Successfully!.");


                }
                /*
            }
            else
            {
                MessageBox.Show("Name Already Exist Please Enter Another.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }  */

            }

        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}

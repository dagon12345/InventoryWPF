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
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;

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

        BackgroundWorker _bgWorker;
        bool _iNeedToCloseAfterBgWorker;


        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ////completed here
            lblnetstatus.Text = "Successfully connected to SQL Server.";
            lblnetstatus.Foreground = Brushes.SeaGreen;
            btnadd.IsEnabled = true;
            btnupdate.IsEnabled = true;
            btndelete.IsEnabled = true;
            // btn_rc.Enabled = true;
            if (_iNeedToCloseAfterBgWorker)
                Close();
        }


        public void display()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                da = new MySqlDataAdapter("Select id,NameofStaff,Section,Division,Piece,TypeOfICTEquipment,Type,YearAcquired FROM db_inventory ORDER BY id DESC", con);
                dt = new DataTable();
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                //this.datagrid.Columns[0].Visibility = Visibility.Hidden;


            });

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
                btnadd.IsEnabled = false;
                btndelete.IsEnabled = false;
                btnupdate.IsEnabled = false;
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


        private DispatcherTimer timer;

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

                });
            }
        }

        public frm_inventory()
        {
            InitializeComponent();




            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _bgWorker.RunWorkerAsync();



            txtnameofstaff.Focus();

            if (txtnameofstaff.Text == "")
            {
                listBoxSuggestions.Visibility = Visibility.Hidden;
            }
            else
            {
                listBoxSuggestions.Visibility = Visibility.Visible;
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();


            timerTextBlock.Visibility = Visibility.Hidden;


        }
        private void Timer_Tick(object sender, EventArgs e)
        {



          

            timerTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");

            if (txtnameofstaff.Text == "")
            {
                listBoxSuggestions.Visibility = Visibility.Hidden;
            }

           else  if(lblmatch.Content == "User Detected.")
            {
                listBoxSuggestions.Visibility = Visibility.Hidden;
            }

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
            lblmatch.Content = "----------";
            display();
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
                    cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
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
            if (MessageBox.Show("Are you sure you want to clear all the fields?", "Clear", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                clear();
            }


        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtnameofstaff.Text == "" || txtsection.Text == "" || txtdivision.Text == "" || txtpiece.Text == "" || txttypeofict.Text == "" || cmbtype.Text == "" || datetimepicker.Text == "")
            {
                MessageBox.Show("Please select data you want to update","Warning",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
            else
            {

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd = new MySqlCommand("update db_inventory set NameOfStaff=@NameOfStaff, Section=@Section, Division=@Division ,Piece=@Piece,TypeOfICTEquipment=@TypeOfICTEquipment,Type=@Type,YearAcquired=@YearAcquired WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", txtid.Text);
                cmd.Parameters.AddWithValue("@NameOfStaff", txtnameofstaff.Text);
                cmd.Parameters.AddWithValue("@Section", txtsection.Text);
                cmd.Parameters.AddWithValue("@Division", txtdivision.Text);
                cmd.Parameters.AddWithValue("@Piece", txtpiece.Text);
                cmd.Parameters.AddWithValue("@TypeOfICTEquipment", txttypeofict.Text);
                cmd.Parameters.AddWithValue("@Type", cmbtype.Text);
                cmd.Parameters.AddWithValue("@YearAcquired", datetimepicker.Text);
                cmd.ExecuteNonQuery();






                ///LOGS
                DateTime theDate = DateTime.Now;
                theDate.ToString("yyyy-MM-dd hh:mm:ss");

                MySqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                cmd2.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd2.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
                cmd2.Parameters.AddWithValue("@Activity", "has UPDATED INVENTORY with the usertype " + lblusertype.Text + " and with the username of " + lbluserid.Text);
                cmd2.ExecuteNonQuery();

                display();
                clear();




                // display();
                MessageBox.Show("Successfully Updated");

            }
            
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)datagrid.SelectedItem;
                string id = selectedRow["id"].ToString(); // Replace "YourColumnName" with the actual column name
                string nameofstaff = selectedRow["NameOfStaff"].ToString(); // Replace "YourColumnName" with the actual column name
                string section = selectedRow["Section"].ToString(); // Replace "YourColumnName" with the actual column name
                string division = selectedRow["Division"].ToString(); // Replace "YourColumnName" with the actual column name
                string Piece = selectedRow["Piece"].ToString(); // Replace "YourColumnName" with the actual column name
                string typeofict = selectedRow["TypeOfICTEquipment"].ToString(); // Replace "YourColumnName" with the actual column name
                string type = selectedRow["Type"].ToString(); // Replace "YourColumnName" with the actual column name
                string yearacquired = selectedRow["YearAcquired"].ToString(); // Replace "YourColumnName" with the actual column name

                txtid.Text = id;
                txtnameofstaff.Text = nameofstaff;
                txtsection.Text = section;
                txtdivision.Text = division;
                txtpiece.Text = Piece;
                txttypeofict.Text = typeofict;
                cmbtype.Text = type;
                datetimepicker.Text = yearacquired;
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtnameofstaff.Text == "" || txtsection.Text == "" || txtdivision.Text == "" || txtpiece.Text == "" || txttypeofict.Text == "" || cmbtype.Text == "" || datetimepicker.Text == "")
            {
                MessageBox.Show("Retrieve some data before deleting","Retrieve",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else if (MessageBox.Show("Are you sure you want to delete this data?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {






                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd = new MySqlCommand("DELETE FROM db_inventory WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", txtid.Text);
                cmd.ExecuteNonQuery();


                //LOGS
                DateTime theDate = DateTime.Now;
                theDate.ToString("yyyy-MM-dd hh:mm:ss");

                MySqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into db_activitylogs (Date,Time,UserID,Activity)values (@Date,@Time,@UserID,@Activity)";
                cmd2.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd2.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                cmd2.Parameters.AddWithValue("@UserID", lbluserid.Text);
                cmd2.Parameters.AddWithValue("@Activity", "has DELETED data in Inventory with the usertype " + lblusertype.Text + " and a username of " + lbluserid.Text);
                cmd2.ExecuteNonQuery();

                display();
                clear();




                // display();
                MessageBox.Show("Data successfully deleted.");

            }
        }

        private void ShowSuggestions(List<string> suggestions)
        {
            // Clear previous suggestions
            listBoxSuggestions.Items.Clear();

            // Add new suggestions to the ListBox
            foreach (string suggestion in suggestions)
            {
                listBoxSuggestions.Items.Add(suggestion);
            }
        }

        private void listBoxSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxSuggestions.SelectedItem != null)
            {
                string selectedSuggestion = listBoxSuggestions.SelectedItem.ToString();
                txtnameofstaff.Text = selectedSuggestion;
                listBoxSuggestions.Visibility = Visibility.Hidden;
            }
        }

        private List<string> GetSuggestionsFromDatabase(string inputText)
        {
            List<string> suggestions = new List<string>();

            // Establish a connection to your SQL database
            using (MySqlConnection connection = new MySqlConnection(cs.DBcon))
            {
                connection.Open();

                // Create a SQL command to query the database
                MySqlCommand command = new MySqlCommand("SELECT Name FROM db_register WHERE Name LIKE @inputText", connection);
                command.Parameters.AddWithValue("@inputText", "%" + inputText + "%");

                // Execute the SQL command and retrieve the suggestions
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string suggestion = reader.GetString(0);
                        suggestions.Add(suggestion);
                    }
                }
                listBoxSuggestions.Visibility = Visibility.Visible;
            }

            return suggestions;
        }

        private void txtnameofstaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = txtnameofstaff.Text;

            // Perform database query based on the input text
            List<string> suggestions = GetSuggestionsFromDatabase(inputText);

            // Display the suggestions
            ShowSuggestions(suggestions);



            int i = 0;
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Name from db_register where Name='" + txtnameofstaff.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());


            if (i == 0)
            {

                lblmatch.Content = "User Not Existed.";
                lblmatch.Foreground = Brushes.Crimson;

            }
            else
            {


                lblmatch.Content = "User Detected.";
                lblmatch.Foreground = Brushes.SeaGreen;
            }


        }

        private void txtnameofstaff_MouseLeave(object sender, MouseEventArgs e)
        {
          
        }

        private void btnrfresh_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void txtsearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
           
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from db_inventory where NameofStaff LIKE '" + txtsearch.Text + "%' OR Section LIKE '" + txtsearch.Text + " %' OR Division LIKE '" + txtsearch.Text + "%'";
                //  cmd.Parameters.AddWithValue("Name", string.Format("%{0}%", txtsearch.Text));
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
               // this.datagrid.Columns[0].Visibility = Visibility.Hidden;
            
            
        }

        private void txtnameofstaff_SelectionChanged(object sender, RoutedEventArgs e)
        {
          
        }
    }
}

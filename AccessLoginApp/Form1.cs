using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AccessLoginApp
{
    public partial class Form1 : Form
    {
        // Create global OleDbConnection connection instance and 
        // Access only inside the Form1
        private OleDbConnection connection = new OleDbConnection();

        // Constructor 
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Programmer\Documents\EmployeeInfo.accdb;
                                            Persist Security Info=False;";
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Close any open Connection
                connection.Close();
               // Whenever we want to connection we just write
               // Connection open And Connection Close method.
                connection.Open();
                lblCheckConnection.Text = "Connection succeffuly";
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            connection.Open();

            OleDbCommand command = new OleDbCommand();

            // We attach command to the connection 
            command.Connection = connection;

            // Create a query
            command.CommandText = "select * from EmployeeData where Username = '" + txtUserName.Text+ "' and Password = '" + txtPassword.Text+"'";

            // Excute the query reader the data and assign to reader
            OleDbDataReader reader = command.ExecuteReader();
            // variables
            int count = 0;
            while (reader.Read())
            {
                count = count + 1;
            }
            if (count == 1)
            {
                MessageBox.Show("Username And Password is correct");

                connection.Close();
                // Release all resoucre used by the component
                connection.Dispose();
                
                EmployeeFile formOpen = new EmployeeFile();
                formOpen.ShowDialog();
                // Close form1
                this.Close();
            }
           else if (count > 1)
            {
                MessageBox.Show("Duplicate Username And Password ");
            }
            else
            {
                MessageBox.Show("Username And Password is not correct");
            }

            connection.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

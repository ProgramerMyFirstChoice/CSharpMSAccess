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
    public partial class EmployeeFile : Form
    {
        // Create global OleDbConnection connection instance and 
        // Access only inside the Form1
        private OleDbConnection connection = new OleDbConnection();

        public EmployeeFile()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Programmer\Documents\EmployeeInfo.accdb;
                                            Persist Security Info=False;";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                // Create a query
                command.CommandText = "Insert into EmployeeData (FName,LName, Pay) " +
                    "values('"+txtFName.Text+"', '"+txtLName.Text+"', '"+txtPay.Text+"')";

                // Excute non query
                command.ExecuteNonQuery();
                MessageBox.Show("Data saved");
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {                
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "Update EmployeeData set FName = '" + txtFName.Text + "' ,LName = '" + txtLName.Text + "' ,Pay = '" + txtPay.Text + "' where EmployeeID = " + txtEmployeeID.Text + "";

                MessageBox.Show(query);
                // Create a query
                command.CommandText = query;

                // Excute non query
                command.ExecuteNonQuery();
                MessageBox.Show("Data edited Succeful");
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "Delete from EmployeeData where EmployeeID = " + txtEmployeeID.Text + "";

                MessageBox.Show(query);
                // Create a query
                command.CommandText = query;

                // Excute non query for insert delete a data
                command.ExecuteNonQuery();
                MessageBox.Show("Data deleted Succeful");
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void EmployeeFile_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "Select * from EmployeeData";
                // We can also write the query as follows
                // sring query = "Select FName, LName from EmployeeData";

                // Create a query
                command.CommandText = query;


                // Excute the query reader the data and assign to reader
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboAddString.Items.Add(reader["FName"].ToString());
                    listBoxData.Items.Add(reader["FName"].ToString());
                    // We can write the statemnt using index
                    // comboAddString.Items.Add(reader[0].ToString() + " " + reader[1].ToString());

                }

                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }

        // Display Database value in tetxtBox if select combobox 
        private void comboAddString_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "Select * from EmployeeData where FName = '"+comboAddString.Text+ "'";
                

                // Create a query
                command.CommandText = query;


                // Excute the query reader the data and assign to reader
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    //string employeeID = reader["EmployeeID"].ToString();
                    txtEmployeeID.Text = reader["EmployeeID"].ToString();
                    txtFName.Text = reader["FName"].ToString();
                    txtLName.Text = reader["LName"].ToString();
                    txtPay.Text = reader["Pay"].ToString();
                }

                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }

        // Link listbox with database and diplay the value in textbox when selected and clicked
        private void listBoxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "Select * from EmployeeData where FName = '" + listBoxData.Text + "'";


                // Create a query
                command.CommandText = query;


                // Excute the query reader the data and assign to reader
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    //string employeeID = reader["EmployeeID"].ToString();
                    txtEmployeeID.Text = reader["EmployeeID"].ToString();
                    txtFName.Text = reader["FName"].ToString();
                    txtLName.Text = reader["LName"].ToString();
                    txtPay.Text = reader["Pay"].ToString();
                }

                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }

        private void btnLoadTable_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "select EmployeeID, FName, LName, Pay from EmployeeData";

                // Create a query
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();

                // We attach command to the connection 
                command.Connection = connection;

                string query = "select * from EmployeeData";

                // Create a query
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chart1.Series["Pay"].Points.AddXY(reader["FName"].ToString(), reader["Pay"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error  " + ex);
            }
        }
        // Display data from datagridview in textbox
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            //    txtEmployeeID.Text = row.Cells[0].Value.ToString();
            //    txtFName.Text = row.Cells[1].Value.ToString();
            //    txtLName.Text = row.Cells[2].Value.ToString();
            //    txtPay.Text = row.Cells[3].Value.ToString();

            //}

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                txtEmployeeID.Text = row.Cells[0].Value.ToString();
                txtFName.Text = row.Cells[1].Value.ToString();
                txtLName.Text = row.Cells[2].Value.ToString();
                txtPay.Text = row.Cells[3].Value.ToString();
            }
                

            }
        }
    
}

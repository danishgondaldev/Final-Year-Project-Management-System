using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MidFinalProject
{
    public partial class AddAStudent : Form
    {
        public AddAStudent()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (RegNoTxt.Text == "" || FirstNameTxt.Text == "" || EmailTxt.Text == "")
            {
                MessageBox.Show("Registration Number / First Name / Email fields can not be left empty !");
            }
            else
            {
                int gender = 0;
                var co = Configuration.getInstance().getConnection();
                SqlCommand cm = new SqlCommand("Insert into Person (FirstName, LastName, Contact, Email, DateOfBirth, Gender) values (@FirstName, @LastName, @Contact, @Email, @DateOfBirth, @Gender)", co);
                cm.Parameters.AddWithValue("@FirstName", FirstNameTxt.Text);
                cm.Parameters.AddWithValue("@LastName", LastNameTxt.Text);
                cm.Parameters.AddWithValue("@Contact", LastNameTxt.Text);
                cm.Parameters.AddWithValue("@Email", EmailTxt.Text);
                cm.Parameters.AddWithValue("@DateOfBirth", DOB.Value);
                if (MaleBtn.Checked)
                {
                    gender = 1;
                }
                else if (FemaleBtn.Checked)
                {
                    gender = 2;
                }
                cm.Parameters.AddWithValue("@Gender", gender);
                cm.ExecuteNonQuery();




                int Id = 0;
                // Define the connection string
                string connectionString = @"Data Source=(local);Initial Catalog=ProjectA;Integrated Security=True";

                // Define the SQL query to retrieve data from the database
                string query = "SELECT Id FROM Person";

                // Create a new SqlConnection object
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a new SqlCommand object to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            // Open the database connection
                            connection.Open();

                            // Execute the query and retrieve the data
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Retrieve the data from each column in the current row
                                    Id = reader.GetInt32(0);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }







               
                var c = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Student (Id, RegistrationNo) values (@ID, @RegistrationNo)", c);
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@RegistrationNo", RegNoTxt.Text);
                cmd.ExecuteNonQuery();

      
                MessageBox.Show("Successfully saved");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}

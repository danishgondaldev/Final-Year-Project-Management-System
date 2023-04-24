using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidFinalProject
{
    public partial class UpdateAStudentMain : Form
    {
        int Id = 0;
        public UpdateAStudentMain()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

            private void UpdateBtn_Click(object sender, EventArgs e)
        {

                // Define the connection string
                string connectionString1 = @"Data Source=(local);Initial Catalog=ProjectA;Integrated Security=True";

                // Define the SQL query to retrieve data from the database
                string query1 = "SELECT * FROM Person WHERE Id = " + Id;

                // Create a new SqlConnection object
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    // Create a new SqlCommand object to execute the query
                    using (SqlCommand command1 = new SqlCommand(query1, connection1))
                    {
                        try
                        {
                            // Open the database connection
                            connection1.Open();

                            // Execute the query and retrieve the data
                            using (SqlDataReader reader1 = command1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    // Retrieve the data from each column in the current row
                                    FirstNameTxt.Text = reader1.GetString(1);
                                    LastNameTxt.Text = reader1.GetString(2);
                                    ContactTxt.Text = reader1.GetString(3);
                                    EmailTxt.Text = reader1.GetString(4);
                                    DOB.Value = reader1.GetDateTime(5);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

        private void ContactTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }


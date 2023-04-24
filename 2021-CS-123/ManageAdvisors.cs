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
    public partial class ManageAdvisors : Form
    {
        public ManageAdvisors()
        {
            InitializeComponent();
        }

        private void updateAdvisorBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT  A.Id AS Id, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender, (SELECT Value FROM Lookup AS L WHERE Id = A.Designation) AS Designation,  A.Salary AS Salary  FROM Advisor AS A JOIN Person AS P ON A.id = P.id JOIN Lookup AS L ON P.Gender = L.Id;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                editDataUGrid.DataSource = dt;
                updateAdvisorPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void viewAdvisorBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT  A.Id AS Id, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender, (SELECT Value FROM Lookup AS L WHERE Id = A.Designation) AS Designation,  A.Salary AS Salary  FROM Advisor AS A JOIN Person AS P ON A.id = P.id JOIN Lookup AS L ON P.Gender = L.Id;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewAdvisorsGridView.DataSource = dt;
                viewAdvisorsPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (designationComboBox.Text == "" || FirstNameTxt.Text == "" || EmailTxt.Text == "")
            {
                MessageBox.Show("Designation / First Name / Email fields can not be left empty !");
            }
            else
            {
                try
                {
                    string email = null;
                    var co = Configuration.getInstance().getConnection();
                    SqlCommand sc = new SqlCommand("SELECT Email FROM Person WHERE Email = @Email", co);
                    sc.Parameters.AddWithValue("@Email", EmailTxt.Text);

                    SqlDataReader reader1;
                    reader1 = sc.ExecuteReader();

                    while (reader1.Read())
                    {
                        email = reader1["Email"].ToString();
                    }
                    reader1.Close();

                    if (email != null)
                    {
                        MessageBox.Show("Person with this email id already exists!");
                    }
                    else
                    {
                        SqlCommand cm = new SqlCommand("Insert into Person (FirstName, LastName, Contact, Email, DateOfBirth, Gender) values (@FirstName, @LastName, @Contact, @Email, @DateOfBirth, @Gender)", co);
                        cm.Parameters.AddWithValue("@FirstName", FirstNameTxt.Text);
                        cm.Parameters.AddWithValue("@LastName", LastNameTxt.Text);
                        cm.Parameters.AddWithValue("@Contact", ContactTxt.Text);
                        cm.Parameters.AddWithValue("@Email", EmailTxt.Text);
                        cm.Parameters.AddWithValue("@DateOfBirth", DOB.Value);
                        cm.Parameters.AddWithValue("@Gender", int.Parse(genderComboBox.SelectedValue.ToString()));
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
                        if (salaryTxt.Text != "")
                        {
                            SqlCommand cmd = new SqlCommand("Insert into Advisor (Id, Designation , Salary) values (@ID, (SELECT Id FROM Lookup WHERE Value = @Designation), @Salary)", c);
                            cmd.Parameters.AddWithValue("@ID", Id);
                            cmd.Parameters.AddWithValue("@Designation", designationComboBox.Text.ToString());
                            cmd.Parameters.AddWithValue("@Salary", int.Parse(salaryTxt.Text.ToString()));
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("Insert into Advisor (Id, Designation) values (@ID, (SELECT Id FROM Lookup WHERE Value = @Designation))", c);
                            cmd.Parameters.AddWithValue("@ID", Id);
                            cmd.Parameters.AddWithValue("@Designation", designationComboBox.Text.ToString());
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Successfully saved");
                        clearBtn.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            FirstNameTxt.Text = "";
            LastNameTxt.Text = "";
            ContactTxt.Text = "";
            EmailTxt.Text = "";
            genderComboBox.Text = "";
            designationComboBox.Text = "";
            salaryTxt.Text = "";
        }

        private void searchAdvBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT  A.Id AS Id, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender, (SELECT Value FROM Lookup AS L WHERE Id = A.Designation) AS Designation,  A.Salary AS Salary FROM Advisor AS A JOIN Person AS P ON A.id = P.id JOIN Lookup AS L ON P.Gender = L.Id WHERE FirstName = @FirstName", con);
                cmd.Parameters.AddWithValue("@FirstName", nameSearchTxt.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewAdvisorsGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editDataUGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    DataGridViewRow row = this.editDataUGrid.Rows[e.RowIndex];
                    string id = row.Cells["Id"].Value.ToString();
                    var con = Configuration.getInstance().getConnection();

                    if (firstNameUTxt.Text != "" && firstNameUTxt.Text != row.Cells["FirstName"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @Name WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@Name", firstNameUTxt.Text);
                        cmd.ExecuteNonQuery();
                    }

                    if (lastNameUTxt.Text != "" && lastNameUTxt.Text != row.Cells["LastName"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Person SET LastName = @LastName WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@LastName", lastNameUTxt.Text);
                        cmd.ExecuteNonQuery();
                    }

                    if (contactUTxt.Text != "" && contactUTxt.Text != row.Cells["Contact"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Person SET Contact = @Contact WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@Contact", contactUTxt.Text);
                        cmd.ExecuteNonQuery();
                    }


                    if (emailUTxt.Text != "" && emailUTxt.Text != row.Cells["Email"].Value.ToString())
                    {
                        string email = null;
                        SqlCommand sc = new SqlCommand("SELECT Email FROM Person WHERE Email = @Email", con);
                        sc.Parameters.AddWithValue("@Email", emailUTxt.Text);

                        SqlDataReader reader1;
                        reader1 = sc.ExecuteReader();

                        while (reader1.Read())
                        {
                            email = reader1["Email"].ToString();
                        }
                        reader1.Close();
                        if (email != null)
                        {
                            MessageBox.Show("This email id already exists in system!");
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Person SET Email = @Email WHERE Id = " + id, con);
                            cmd.Parameters.AddWithValue("@Email", emailUTxt.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (dateUPicker.ToString() != row.Cells["DateOfBirth"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Person SET DateOfBirth = @DateOfBirth WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateUPicker.Value);
                        cmd.ExecuteNonQuery();
                    }

                    if (genderUBox.Text != "" && genderUBox.Text != row.Cells["Gender"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Person SET Gender = @Gender WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@Gender", int.Parse(genderUBox.SelectedValue.ToString()));
                        cmd.ExecuteNonQuery();
                    }

                    if (designationUBox.Text != "" && designationUBox.Text != row.Cells["Designation"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Advisor SET Designation = @Designation WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@Designation", int.Parse(designationUBox.SelectedValue.ToString()));
                        cmd.ExecuteNonQuery();
                    }
                    if (salaryUBox.Text != "" && salaryUBox.Text != row.Cells["Salary"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Advisor SET Salary = @Salary WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@Salary", salaryUBox.Text);
                        cmd.ExecuteNonQuery();
                    }
                    clearUBtn.PerformClick();
                }
                var co = Configuration.getInstance().getConnection();
                SqlCommand cma = new SqlCommand("SELECT  A.Id AS Id, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender, (SELECT Value FROM Lookup AS L WHERE Id = A.Designation) AS Designation,  A.Salary AS Salary FROM Advisor AS A JOIN Person AS P ON A.id = P.id JOIN Lookup AS L ON P.Gender = L.Id", co);
                SqlDataAdapter da = new SqlDataAdapter(cma);
                DataTable dt = new DataTable();
                da.Fill(dt);
                editDataUGrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void goBackAddAButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm f = new MainMenuForm();
            f.Show();
        }

        private void goBackViewABtn_Click(object sender, EventArgs e)
        {
            addAnAdvisorPanel.BringToFront();
        }

        private void goBackUpdateABtn_Click(object sender, EventArgs e)
        {
            addAnAdvisorPanel.BringToFront();
        }

        private void ManageAdvisors_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            fillAAdvGenBox();
            fillAAdvDesBox();
            fillUAdvGenBox();
            fillUAdvDesBox();
        }

        private void fillAAdvGenBox()
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cmde = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category = 'GENDER'", co);
                SqlDataAdapter da = new SqlDataAdapter(cmde);
                DataSet ds = new DataSet();
                da.Fill(ds);
                genderComboBox.Items.Clear();
                genderComboBox.DataSource = ds.Tables[0];
                genderComboBox.DisplayMember = "Value";
                genderComboBox.ValueMember = "Id";
                genderComboBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillAAdvDesBox()
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cmde = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category = 'DESIGNATION'", co);
                SqlDataAdapter da = new SqlDataAdapter(cmde);
                DataSet ds = new DataSet();
                da.Fill(ds);
                designationComboBox.Items.Clear();
                designationComboBox.DataSource = ds.Tables[0];
                designationComboBox.DisplayMember = "Value";
                designationComboBox.ValueMember = "Id";
                designationComboBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillUAdvGenBox()
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cmde = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category = 'GENDER'", co);
                SqlDataAdapter da = new SqlDataAdapter(cmde);
                DataSet ds = new DataSet();
                da.Fill(ds);
                genderUBox.Items.Clear();
                genderUBox.DataSource = ds.Tables[0];
                genderUBox.DisplayMember = "Value";
                genderUBox.ValueMember = "Id";
                genderUBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillUAdvDesBox()
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cmde = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category = 'DESIGNATION'", co);
                SqlDataAdapter da = new SqlDataAdapter(cmde);
                DataSet ds = new DataSet();
                da.Fill(ds);
                designationUBox.Items.Clear();
                designationUBox.DataSource = ds.Tables[0];
                designationUBox.DisplayMember = "Value";
                designationUBox.ValueMember = "Id";
                designationUBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearUBtn_Click(object sender, EventArgs e)
        {
            firstNameUTxt.Text = "";
            lastNameUTxt.Text = "";
            contactUTxt.Text = "";
            emailUTxt.Text = "";
            genderUBox.Text = "";
            designationUBox.Text = "";
            salaryUBox.Text = "";
        }
    }
}

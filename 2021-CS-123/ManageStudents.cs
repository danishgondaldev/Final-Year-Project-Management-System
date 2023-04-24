using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MidFinalProject
{
    public partial class ManageStudents : Form
    {
        public ManageStudents()
        {
            InitializeComponent();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (RegNoTxt.Text == "" || FirstNameTxt.Text == "" || EmailTxt.Text == "")
            {
                MessageBox.Show("Registration Number / First Name / Email fields can not be left empty !");
            }
            else
            {
                try
                {
                    string registrationNo = null;
                    string email = null;
                    var co = Configuration.getInstance().getConnection();
                    SqlCommand sc = new SqlCommand("SELECT RegistrationNo FROM Student WHERE RegistrationNo = @RegistrationNo", co);
                    sc.Parameters.AddWithValue("@RegistrationNo", RegNoTxt.Text);

                    SqlDataReader reader1;
                    reader1 = sc.ExecuteReader();

                    while (reader1.Read())
                    {
                        registrationNo = reader1["RegistrationNo"].ToString();
                    }
                    reader1.Close();

                    SqlCommand se = new SqlCommand("SELECT Email FROM Person WHERE Email = @Email", co);
                    se.Parameters.AddWithValue("@Email", EmailTxt.Text);

                    SqlDataReader reader2;
                    reader2 = se.ExecuteReader();

                    while (reader2.Read())
                    {
                        email = reader2["Email"].ToString();
                    }
                    reader2.Close();


                    if (registrationNo != null || email != null)
                    {
                        MessageBox.Show("Student with this registration number / email already exists! ");
                    }
                    else
                    {
                        SqlCommand cm = new SqlCommand("Insert into Person (FirstName, LastName, Contact, Email, DateOfBirth, Gender) values (@FirstName, @LastName, @Contact, @Email, @DateOfBirth, @Gender)", co);
                        cm.Parameters.AddWithValue("@FirstName", FirstNameTxt.Text);
                        cm.Parameters.AddWithValue("@LastName", LastNameTxt.Text);
                        cm.Parameters.AddWithValue("@Contact", ContactTxt.Text);
                        cm.Parameters.AddWithValue("@Email", EmailTxt.Text);
                        cm.Parameters.AddWithValue("@DateOfBirth", DOB.Value);
                        cm.Parameters.AddWithValue("@Gender", int.Parse(genderBox.SelectedValue.ToString()));
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
                        clearBtn.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }
        }
        private void updateAStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT  S.Id AS Id, RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender FROM Student AS S JOIN Person AS P ON S.id = P.id JOIN Lookup AS L ON P.Gender = L.Id;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                editDataUGrid.DataSource = dt;
                updatePanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void viewStudentsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT  S.Id AS Id, RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender FROM Student AS S JOIN Person AS P ON S.id = P.id JOIN Lookup AS L ON P.Gender = L.Id;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewStudentsGridView.DataSource = dt;
                viewStudentPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void createGroupsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [ProjectA].[dbo].[Group]", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                groupDataGridView.DataSource = dt;
                createGroupPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void studentsGroupsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("(SELECT GroupId, StudentId, RegistrationNo, (FirstName + ' ' + LastName) AS Name, Value AS Status FROM GroupStudent JOIN Lookup AS L ON L.Id = Status JOIN Student AS S ON StudentId = S.Id JOIN Person AS P ON StudentId = P.Id) ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                StuGroupsDataGridView.DataSource = dt;
                Fillcombobox();
                studentsToGroupsPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void goBackAddBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm f = new MainMenuForm();
            f.Show();
        }

        private void goBackManageBtn_Click(object sender, EventArgs e)
        {
            addAStudentPnl.BringToFront();
        }

        private void goBackStudentsBtn_Click(object sender, EventArgs e)
        {
            removeStuGrpData();
            addAStudentPnl.BringToFront();
        }

        private void removeStuGrpData()
        {
            selectGrpIDComboBox.Items.Clear();
            selectStuRegAComboBox.Items.Clear();
            selectStuRegRComboBox.Items.Clear();
        }
        private void goBackCreateBtn_Click(object sender, EventArgs e)
        {
            addAStudentPnl.BringToFront();
        }

        private void goBackUpdateBtn_Click(object sender, EventArgs e)
        {
            addAStudentPnl.BringToFront();
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

                    if (regNoUText.Text != "" && regNoUText.Text != row.Cells["RegistrationNo"].Value.ToString())
                    {
                        string regNo = null;
                        SqlCommand sc = new SqlCommand("SELECT RegistrationNo FROM Student WHERE RegistrationNo = @RegistrationNo", con);
                        sc.Parameters.AddWithValue("@RegistrationNo", regNoUText.Text);

                        SqlDataReader reader1;
                        reader1 = sc.ExecuteReader();

                        while (reader1.Read())
                        {
                            regNo = reader1["RegistrationNo"].ToString();
                        }
                        reader1.Close();
                        if (regNo != null)
                        {
                            MessageBox.Show("This registration number already exists in system!");
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Student SET RegistrationNo = @RegistrationNo WHERE Id = " + id, con);
                            cmd.Parameters.AddWithValue("@RegistrationNo", regNoUText.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    clearUBtn.PerformClick();
                }
                var co = Configuration.getInstance().getConnection();
                SqlCommand cma = new SqlCommand("SELECT  S.Id AS Id, RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender FROM Student AS S JOIN Person AS P ON S.id = P.id JOIN Lookup AS L ON P.Gender = L.Id;", co);
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

        private void clearUBtn_Click(object sender, EventArgs e)
        {
            regNoUText.Text = "";
            firstNameUTxt.Text = "";
            lastNameUTxt.Text = "";
            contactUTxt.Text = "";
            emailUTxt.Text = "";
            genderUBox.Text = "";
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            RegNoTxt.Text = "";
            FirstNameTxt.Text = "";
            LastNameTxt.Text = "";
            ContactTxt.Text = "";
            EmailTxt.Text = "";
            genderBox.Text = "";
        }

        private void searchStuBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT  S.Id AS Id, RegistrationNo, FirstName, LastName, Contact, Email, DateOfBirth, Value AS Gender FROM Student AS S JOIN Person AS P ON S.id = P.id JOIN Lookup AS L ON P.Gender = L.Id WHERE RegistrationNo = @RegistrationNo", con);
                cmd.Parameters.AddWithValue("@RegistrationNo", regNoSearchTxt.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewStudentsGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void createGroupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var c = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO [ProjectA].[dbo].[Group] (Created_On) VALUES (@Created_On)", c);
                cmd.Parameters.AddWithValue("@Created_On", createGroupDateBox.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");

                SqlCommand cma = new SqlCommand("SELECT  * FROM [ProjectA].[dbo].[Group]", c);
                SqlDataAdapter da = new SqlDataAdapter(cma);
                DataTable dt = new DataTable();
                da.Fill(dt);
                groupDataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        void Fillcombobox()
        {
            try
            {
                var conn = Configuration.getInstance().getConnection();
                SqlCommand sc = new SqlCommand("SELECT Id FROM [Group] AS G WHERE 4 > (SELECT COUNT(*) FROM GroupStudent WHERE GroupId = G.Id AND Status = (SELECT Id FROM Lookup WHERE Value = 'Active'))", conn);
                SqlDataReader reader;
                reader = sc.ExecuteReader();

                while (reader.Read())
                {
                    selectGrpIDComboBox.Items.Add(reader["Id"].ToString());
                }

                reader.Close();

                SqlCommand sce = new SqlCommand("SELECT RegistrationNo FROM Student WHERE Id <> ALL (SELECT StudentId FROM GroupStudent WHERE Status = (SELECT Id FROM Lookup WHERE Value = 'Active'))", conn);
                SqlDataReader reader1;

                reader1 = sce.ExecuteReader();

                while (reader1.Read())
                {
                    selectStuRegAComboBox.Items.Add(reader1["RegistrationNo"].ToString());
                }

                reader1.Close();
                SqlCommand s = new SqlCommand("SELECT RegistrationNo FROM Student WHERE Id = ANY (SELECT StudentId FROM GroupStudent WHERE Status = (SELECT Id FROM Lookup WHERE Value = 'Active'))", conn);
                SqlDataReader reader2;

                reader2 = s.ExecuteReader();

                while (reader2.Read())
                {
                    selectStuRegRComboBox.Items.Add(reader2["RegistrationNo"].ToString());
                }

                reader2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addStuGroupsBtn_Click(object sender, EventArgs e)
        {
            try
            { 
                DateTime currentDate = DateTime.Now;
                var c = Configuration.getInstance().getConnection();

                int status = 0;
                SqlCommand sc = new SqlCommand("SELECT Id FROM Lookup WHERE Value = 'Active'", c);
                SqlDataReader reader;
                reader = sc.ExecuteReader();

                while (reader.Read())
                {
                    status = int.Parse(reader["Id"].ToString());
                }

                reader.Close();

                SqlCommand cmd = new SqlCommand("Insert into GroupStudent (GroupId, StudentId, Status, AssignmentDate) values (@GroupId, (SELECT Id FROM Student WHERE RegistrationNo = @RegistrationNo), @Status, @AssignmentDate)", c);
                cmd.Parameters.AddWithValue("@GroupId", int.Parse(selectGrpIDComboBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@AssignmentDate", currentDate);
                cmd.Parameters.AddWithValue("@RegistrationNo", selectStuRegAComboBox.Text.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");

                selectStuRegAComboBox.Text = "";
                selectGrpIDComboBox.Text = "";
                selectStuRegRComboBox.Text = "";
                selectStuRegAComboBox.Items.Clear();
                selectGrpIDComboBox.Items.Clear();
                selectStuRegRComboBox.Items.Clear();
                SqlCommand cm = new SqlCommand("(SELECT GroupId, StudentId, RegistrationNo, (FirstName + ' ' + LastName) AS Name, Value AS Status FROM GroupStudent JOIN Lookup AS L ON L.Id = Status JOIN Student AS S ON StudentId = S.Id JOIN Person AS P ON StudentId = P.Id) ", c);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                StuGroupsDataGridView.DataSource = dt;
                Fillcombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void removeStuGroupsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectStuRegRComboBox.Text.ToString() == "")
                {
                    MessageBox.Show("Registration Number field can not be left empty! ");
                }
                else
                {
                    var c = Configuration.getInstance().getConnection();

                    int status = 0;
                    SqlCommand sc = new SqlCommand("SELECT Id FROM Lookup WHERE Value = 'InActive'", c);
                    SqlDataReader reader;
                    reader = sc.ExecuteReader();

                    while (reader.Read())
                    {
                        status = int.Parse(reader["Id"].ToString());
                    }
                    reader.Close();

                    SqlCommand cmd = new SqlCommand("Update GroupStudent SET Status = @Status WHERE StudentId = (SELECT Id FROM Student WHERE RegistrationNo = @RegistrationNo)", c);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@RegistrationNo", selectStuRegRComboBox.Text.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Removed!");

                    selectStuRegAComboBox.Text = "";
                    selectGrpIDComboBox.Text = "";
                    selectStuRegRComboBox.Text = "";
                    selectStuRegRComboBox.Items.Clear();
                    selectStuRegAComboBox.Items.Clear();
                    selectGrpIDComboBox.Items.Clear();
                    SqlCommand cm = new SqlCommand("(SELECT GroupId, StudentId, RegistrationNo, (FirstName + ' ' + LastName) AS Name, Value AS Status FROM GroupStudent JOIN Lookup AS L ON L.Id = Status JOIN Student AS S ON StudentId = S.Id JOIN Person AS P ON StudentId = P.Id) ", c);
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    StuGroupsDataGridView.DataSource = dt;
                    Fillcombobox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ManageStudents_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            fillAStuGenBox();
            fillUStuGenBox();
        }

        private void fillAStuGenBox()
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cmde = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category = 'GENDER'", co);
                SqlDataAdapter da = new SqlDataAdapter(cmde);
                DataSet ds = new DataSet();
                da.Fill(ds);
                genderBox.Items.Clear();
                genderBox.DataSource = ds.Tables[0];
                genderBox.DisplayMember = "Value";
                genderBox.ValueMember = "Id";
                genderBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillUStuGenBox()
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
    }
}

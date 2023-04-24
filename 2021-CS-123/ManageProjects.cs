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
    public partial class ManageProjects : Form
    {
        
        public ManageProjects()
        {
            InitializeComponent();
        }

        private void updateProjectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id, Title, Description FROM Project;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                editDataUGrid.DataSource = dt;
                updateProjectPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void viewProjectsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title, Description FROM Project;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewProjectsGridView.DataSource = dt;
                viewProjectsPanel.BringToFront();
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
            addAProjectPanel.BringToFront();
        }

        private void goBackUpdateABtn_Click(object sender, EventArgs e)
        {
            addAProjectPanel.BringToFront();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameTxt.Text == "")
                {
                    MessageBox.Show("Title can not be left empty!");
                }
                else
                {
                    var c = Configuration.getInstance().getConnection();

                    string title = null;
                    SqlCommand sc = new SqlCommand("SELECT Title FROM Project WHERE Title = @Title", c);
                    sc.Parameters.AddWithValue("@Title", nameTxt.Text);

                    SqlDataReader reader1;
                    reader1 = sc.ExecuteReader();

                    while (reader1.Read())
                    {
                        title = reader1["Title"].ToString();
                    }
                    reader1.Close();
                    if (title != null)
                    {
                        MessageBox.Show("Project with this title already exists in system!");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Project (Title, Description) values (@Title, @Description)", c);
                        cmd.Parameters.AddWithValue("@Title", nameTxt.Text);
                        cmd.Parameters.AddWithValue("@Description", descriptionTxt.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully saved");
                        clearBtn.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            nameTxt.Text = "";
            descriptionTxt.Text = "";
        }

        private void searchProjectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Title, Description FROM Project WHERE Title = @Title", con);
                cmd.Parameters.AddWithValue("@Title", titleSearchTxt.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewProjectsGridView.DataSource = dt;
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

                    if (titleUTxt.Text != "" && titleUTxt.Text != row.Cells["Title"].Value.ToString())
                    {
                        string title = null;
                        SqlCommand sc = new SqlCommand("SELECT Title FROM Project WHERE Title = @Title", con);
                        sc.Parameters.AddWithValue("@Title", titleUTxt.Text);

                        SqlDataReader reader1;
                        reader1 = sc.ExecuteReader();

                        while (reader1.Read())
                        {
                            title = reader1["Title"].ToString();
                        }
                        reader1.Close();
                        if (title != null)
                        {
                            MessageBox.Show("Project with this title already exists in system!");
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Project SET Title = @Title WHERE Id = " + id, con);
                            cmd.Parameters.AddWithValue("@Title", titleUTxt.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (descriptionUTxt.Text != "" && descriptionUTxt.Text != row.Cells["Description"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Project SET Description = @Description WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@Description", descriptionUTxt.Text);
                        cmd.ExecuteNonQuery();
                    }
                    clearUBtn.PerformClick();

                }
                var co = Configuration.getInstance().getConnection();
                SqlCommand cma = new SqlCommand("SELECT Id, Title, Description FROM Project", co);
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
            titleUTxt.Text = "";
            descriptionUTxt.Text = "";
        }

        private void Fillcombobox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand sce = new SqlCommand("SELECT Id AS ProjectId FROM Project AS P WHERE 3 > (SELECT COUNT(*) FROM ProjectAdvisor WHERE ProjectId = P.Id)", con);
                SqlDataReader reader1;

                reader1 = sce.ExecuteReader();

                while (reader1.Read())
                {
                    selectProAComboBox.Items.Add(reader1["ProjectId"].ToString());
                }
                reader1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void allocateAdvisorBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT ProjectId, P.Title AS ProjectTitle, AdvisorId, L.Value AS AdvisorRole FROM ProjectAdvisor AS PA JOIN Project AS P ON P.Id = ProjectId JOIN Lookup AS L ON L.Id = PA.AdvisorRole;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                allocateAdvDataGridView.DataSource = dt;
                Fillcombobox();
                allocateAdvisorPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addProABtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                DateTime currentDate = DateTime.Now;
                SqlCommand cmd = new SqlCommand("Insert into ProjectAdvisor (AdvisorId, ProjectId, AdvisorRole, AssignmentDate) values (@AdvisorId, @ProjectId, (SELECT Id FROM Lookup WHERE Value = @AdvisorRole), @AssignmentDate)", con);
                cmd.Parameters.AddWithValue("@AdvisorId", int.Parse(selectAdvAComboBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@ProjectId", selectProAComboBox.Text);
                cmd.Parameters.AddWithValue("@AssignmentDate", currentDate);
                cmd.Parameters.AddWithValue("@AdvisorRole", selectAdvRComboBox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");

                clearAlloPBtn.PerformClick();
                removeDataAlloForm();
                SqlCommand cm = new SqlCommand("SELECT ProjectId, P.Title AS ProjectTitle, AdvisorId, L.Value AS AdvisorRole FROM ProjectAdvisor JOIN Project AS P ON P.Id = ProjectId JOIN Lookup AS L ON L.Id = AdvisorRole", con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                allocateAdvDataGridView.DataSource = dt;
                Fillcombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void clearAlloPBtn_Click(object sender, EventArgs e)
        {
            selectAdvAComboBox.Text = "";
            selectProAComboBox.Text = "";
            selectAdvRComboBox.Text = "";
        }

        private void removeDataAlloForm ()
        {
            selectProAComboBox.DataSource = null;
            selectAdvAComboBox.DataSource = null;
            selectAdvRComboBox.DataSource = null;
            selectProAComboBox.Items.Clear();
            selectAdvAComboBox.Items.Clear();
            selectAdvRComboBox.Items.Clear();
        }

        private void fillAlloProjectComboBox()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                SqlCommand sc = new SqlCommand("SELECT Id AS ProjectId FROM Project WHERE Id <> ALL (SELECT ProjectId FROM GroupProject)", con);
                SqlDataReader reader;

                reader = sc.ExecuteReader();

                while (reader.Read())
                {
                    selectPPComboBox.Items.Add(reader["ProjectId"].ToString());
                }
                reader.Close();
               
                SqlCommand sce = new SqlCommand("SELECT Id AS GroupId FROM [Group] WHERE Id <> ALL (SELECT GroupId FROM GroupProject)", con);
                SqlDataReader reader1;

                reader1 = sce.ExecuteReader();

                while (reader1.Read())
                {
                    selectGPComboBox.Items.Add(reader1["GroupId"].ToString());
                }
                reader1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void allocateProjectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(" SELECT ProjectId, Title,  GroupId FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                allocateProDataGridView.DataSource = dt;
                fillAlloProjectComboBox();
                allocateProjectPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void allocateProGrpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into GroupProject (ProjectId, GroupId, AssignmentDate) values (@ProjectId, @GroupId, @AssignmentDate)", con);
                cmd.Parameters.AddWithValue("@GroupId", int.Parse(selectGPComboBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@ProjectId", selectPPComboBox.Text);
                cmd.Parameters.AddWithValue("@AssignmentDate", currentDate);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");

                clearAlloProGrpBtn.PerformClick();
                removeDataAlloProGrpForm();
                SqlCommand cm = new SqlCommand(" SELECT ProjectId, Title,  GroupId FROM GroupProject AS GP JOIN Project AS P ON P.Id = GP.ProjectId", con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                allocateProDataGridView.DataSource = dt;
                fillAlloProjectComboBox();
      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearAlloProGrpBtn_Click(object sender, EventArgs e)
        {
            selectGPComboBox.Text = "";
            selectPPComboBox.Text = "";
        }
        private void removeDataAlloProGrpForm()
        {
            selectGPComboBox.Items.Clear();
            selectPPComboBox.Items.Clear();
        }

        private void goBackAPBtn_Click(object sender, EventArgs e)
        {
            removeDataAlloProGrpForm();
            addAProjectPanel.BringToFront();
        }

        private void goBackAPAdvBtn_Click(object sender, EventArgs e)
        {
            removeDataAlloForm();
            addAProjectPanel.BringToFront();
        }

        private void selectProAComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int proId = int.Parse(selectProAComboBox.Text.ToString());
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmde = new SqlCommand("SELECT Id, Value FROM Lookup WHERE Category = 'ADVISOR_ROLE' AND Id <> ALL (SELECT AdvisorRole FROM ProjectAdvisor WHERE ProjectId = " + proId + ")", con);
                SqlDataAdapter das = new SqlDataAdapter(cmde);
                DataSet dse = new DataSet();
                das.Fill(dse);
                selectAdvRComboBox.DataSource = dse.Tables[0];
                selectAdvRComboBox.DisplayMember = "Value";
                selectAdvRComboBox.ValueMember = "Id";
                selectAdvRComboBox.Text = "";

                int prId = int.Parse(selectProAComboBox.Text.ToString());
                var co = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Advisor WHERE Id <> ALL (SELECT AdvisorId FROM ProjectAdvisor WHERE ProjectId = " + prId + ")", co);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectAdvAComboBox.DataSource = ds.Tables[0];
                selectAdvAComboBox.DisplayMember = "Id";
                selectAdvAComboBox.ValueMember = "Id";
                selectAdvAComboBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManageProjects_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}

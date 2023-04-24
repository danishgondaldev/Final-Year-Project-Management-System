using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidFinalProject
{
    public partial class ManageEvaluations : Form
    {
        public ManageEvaluations()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (evaANameTxt.Text == "" || evaTotalANum.Value == 0 || evaWeightANum.Value == 0)
                {
                    MessageBox.Show("Please select values for all fields !");
                }
                else
                {
                    string name = null;
                    var c = Configuration.getInstance().getConnection();
                    SqlCommand sc = new SqlCommand("SELECT Name FROM Evaluation WHERE Name = @Name", c);
                    sc.Parameters.AddWithValue("@Name", evaANameTxt.Text);

                    SqlDataReader reader1;
                    reader1 = sc.ExecuteReader();

                    while (reader1.Read())
                    {
                        name = reader1["Name"].ToString();
                    }
                    reader1.Close();
                    if (name != null)
                    {
                        MessageBox.Show("Evaluation with this name already exists in system!");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Evaluation (Name, TotalMarks, TotalWeightage) values (@Name, @TotalMarks, @TotalWeightage)", c);
                        cmd.Parameters.AddWithValue("@Name", evaANameTxt.Text);
                        cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(evaTotalANum.Text));
                        cmd.Parameters.AddWithValue("@TotalWeightage", int.Parse(evaWeightANum.Text));
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

        private void editDataUGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    DataGridViewRow row = this.editDataUGrid.Rows[e.RowIndex];
                    string id = row.Cells["Id"].Value.ToString();
                    var con = Configuration.getInstance().getConnection();

                    if (evaUNameTxt.Text != "" && evaUNameTxt.Text != row.Cells["Name"].Value.ToString())
                    {
                        string name = null;
                        SqlCommand sc = new SqlCommand("SELECT Name FROM Evaluation WHERE Name = @Name", con);
                        sc.Parameters.AddWithValue("@Name", evaUNameTxt.Text);

                        SqlDataReader reader1;
                        reader1 = sc.ExecuteReader();

                        while (reader1.Read())
                        {
                            name = reader1["Name"].ToString();
                        }
                        reader1.Close();
                        if (name != null)
                        {
                            MessageBox.Show("Evaluation with this name already exists in system!");
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET Name = @Name WHERE Id = " + id, con);
                            cmd.Parameters.AddWithValue("@Name", evaUNameTxt.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (evaTotalUNum.Value != 0 && evaTotalUNum.Value.ToString() != row.Cells["TotalMarks"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET TotalMarks = @TotalMarks WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@TotalMarks", evaTotalUNum.Value);
                        cmd.ExecuteNonQuery();
                    }

                    if (evaluateWeightUNum.Value != 0 && evaluateWeightUNum.Value.ToString() != row.Cells["TotalWeightage"].Value.ToString())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Evaluation SET TotalWeightage = @TotalWeightage WHERE Id = " + id, con);
                        cmd.Parameters.AddWithValue("@TotalWeightage", evaluateWeightUNum.Value);
                        cmd.ExecuteNonQuery();
                    }
                    clearUBtn.PerformClick();

                }
                var co = Configuration.getInstance().getConnection();
                SqlCommand cma = new SqlCommand("SELECT * FROM Evaluation", co);
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

        private void updateEvaluationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cma = new SqlCommand("SELECT * FROM Evaluation", co);
                SqlDataAdapter da = new SqlDataAdapter(cma);
                DataTable dt = new DataTable();
                da.Fill(dt);
                editDataUGrid.DataSource = dt;
                updateEvaluationPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void viewEvaluationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var co = Configuration.getInstance().getConnection();
                SqlCommand cma = new SqlCommand("SELECT * FROM Evaluation", co);
                SqlDataAdapter da = new SqlDataAdapter(cma);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewEvaGridView.DataSource = dt;
                viewEvaluationsPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchEvaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Evaluation WHERE Name = @Name", con);
                cmd.Parameters.AddWithValue("@Name", nameSearchTxt.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                viewEvaGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void markEvaluationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT EvaluationId, Name, GroupId, ObtainedMarks FROM GroupEvaluation JOIN Evaluation AS E ON E.Id = EvaluationId", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                markEvaDataGridView.DataSource = dt;
                fillMarkEvaComboBox();
                markEvaPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void fillMarkEvaComboBox()
        {
            var conn = Configuration.getInstance().getConnection();
            try
            {
                SqlCommand sce = new SqlCommand("SELECT Id FROM [Group]", conn);
                SqlDataReader reader1;

                reader1 = sce.ExecuteReader();

                while (reader1.Read())
                {
                    selectGEComboBox.Items.Add(reader1["Id"].ToString());
                }
                reader1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void markEvaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                var c = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into GroupEvaluation (GroupId, EvaluationId, ObtainedMarks, EvaluationDate) values (@GroupId, @EvaluationId, @ObtainedMarks, @EvaluationDate)", c);
                cmd.Parameters.AddWithValue("@GroupId", int.Parse(selectGEComboBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@EvaluationId", int.Parse(selectEEComboBox.Text.ToString()));
                cmd.Parameters.AddWithValue("@ObtainedMarks", selectObtENum.Value);
                cmd.Parameters.AddWithValue("@EvaluationDate", selectEvaDateBox.Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");

                clearMarkEvaBtn.PerformClick();
                removeDataAlloProGrpForm();

                SqlCommand cm = new SqlCommand("SELECT EvaluationId, Name, GroupId, ObtainedMarks FROM GroupEvaluation JOIN Evaluation AS E ON E.Id = EvaluationId", c);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                markEvaDataGridView.DataSource = dt;
                fillMarkEvaComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void removeDataAlloProGrpForm()
        {
            selectGEComboBox.Items.Clear();
            selectEEComboBox.Items.Clear();
        }
        private void clearMarkEvaBtn_Click_1(object sender, EventArgs e)
        {
            selectObtENum.Value = 0;
            selectGEComboBox.Text = "";
            selectEEComboBox.Text = "";
        }

        private void goBackAddAButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm f2 = new MainMenuForm();
            f2.Show();
        }

        private void goBackUEvaBtn_Click(object sender, EventArgs e)
        {
            addEvaPanel.BringToFront();
        }

        private void goBackVEBtn_Click(object sender, EventArgs e)
        {
            addEvaPanel.BringToFront();
        }

        private void goBackMEBtn_Click(object sender, EventArgs e)
        {
            removeDataAlloProGrpForm();
            addEvaPanel.BringToFront();
        }

        private void clearUBtn_Click(object sender, EventArgs e)
        {
            evaUNameTxt.Text = "";
            evaTotalUNum.Value = 0;
            evaluateWeightUNum.Value = 0;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            evaANameTxt.Text = "";
            evaTotalANum.Value = 0;
            evaWeightANum.Value = 0;
        }

        private void selectEEComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var c = Configuration.getInstance().getConnection();
                SqlCommand sce = new SqlCommand("SELECT TotalMarks FROM Evaluation WHERE Id = " + int.Parse(selectEEComboBox.Text.ToString()) + "", c);
                SqlDataReader reader1;

                reader1 = sce.ExecuteReader();
                while (reader1.Read())
                {
                    selectObtENum.Maximum = int.Parse(reader1["TotalMarks"].ToString());
                }
                reader1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selectObtENum_ValueChanged(object sender, EventArgs e)
        {
            if (selectEEComboBox.Text == "")
            {
                MessageBox.Show("Please choose Evaluation ID first! ");
            }
        }

        private void selectGEComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var c = Configuration.getInstance().getConnection();
                SqlCommand sc = new SqlCommand("SELECT Id FROM Evaluation WHERE Id <> ALL (SELECT EvaluationId FROM GroupEvaluation WHERE GroupId = " + int.Parse(selectGEComboBox.Text.ToString()) + ")", c);
                SqlDataReader reader;

                reader = sc.ExecuteReader();

                while (reader.Read())
                {
                    selectEEComboBox.Items.Add(reader["Id"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManageEvaluations_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ProjectAdvStudentsReport.pdf";
                Document DC = new Document();
                FileStream FS = File.Create(path);
                PdfWriter.GetInstance(DC, FS);
                DC.Open();
                DC.Add(new Paragraph(""));

                var c = Configuration.getInstance().getConnection();
                SqlCommand sc = new SqlCommand("SELECT P.Id AS Id, P.Title AS Title, (PE.FirstName + ' ' + PE.LastName) AS AdvisorName, L.Value AS AdvisorRole, ST.RegistrationNo AS RegistrationNumber, PR.FirstName + ' ' + PR.LastName AS StudentName FROM Project P FULL OUTER JOIN ProjectAdvisor PA ON P.Id = PA.ProjectId FULL OUTER JOIN GroupProject GP ON GP.ProjectId = P.Id JOIN GroupStudent AS GS ON GS.GroupId = GP.GroupId LEFT JOIN Person AS PE ON PE.Id = PA.AdvisorId LEFT JOIN Lookup AS L ON L.Id = PA.AdvisorRole LEFT JOIN Student AS ST ON ST.Id = GS.StudentId LEFT JOIN Person AS PR ON PR.Id = GS.StudentId;", c);
                SqlDataReader reader;

                reader = sc.ExecuteReader();

                DC.Add(new Paragraph("ID    TITLE   ADVISOR NAME    ADVISOR ROLE     REG. NO.   STUDENT NAME"));
                while (reader.Read())
                {
                    string id = reader["Id"].ToString();
                    string title = reader["Title"].ToString();
                    string adv = reader["AdvisorName"].ToString();
                    string advr = reader["AdvisorRole"].ToString();
                    string reg = reader["RegistrationNumber"].ToString();
                    string stun = reader["StudentName"].ToString();
                    DC.Add(new Paragraph(id + "     " + title + "      " + adv + "               " + advr + "                   " + reg + "                               " + stun));
                }

                reader.Close();

                DC.Close();
                MessageBox.Show("Report has been generated successfully! Please check DESKTOP folder.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void mrkShtRepBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ProjectEvaluationsReport.pdf";
                Document DC = new Document();
                FileStream FS = File.Create(path);
                PdfWriter.GetInstance(DC, FS);
                DC.Open();
                DC.Add(new Paragraph(""));

                var c = Configuration.getInstance().getConnection();
                SqlCommand sc = new SqlCommand("SELECT P.Id AS [Project ID], P.Title AS [Project Title], GE.GroupId, E.Name AS [Name], GE.ObtainedMarks AS [ObtainedMarks],  S.RegistrationNo AS [Registration No.] FROM Project AS P JOIN GroupProject AS GP ON GP.ProjectId = P.Id JOIN GroupStudent AS GS ON GS.GroupId = GP.GroupId JOIN Student AS S ON S.Id = GS.StudentId JOIN GroupEvaluation AS GE ON GE.GroupId = GS.GroupId JOIN Evaluation AS E ON E.Id = GE.EvaluationId", c);
                SqlDataReader reader;

                reader = sc.ExecuteReader();

                DC.Add(new Paragraph("REG. NO    ID   PROJECT TITLE    GROUP ID      EVALUATION NAME    OBT MARKS"));
                while (reader.Read())
                {
                    string reg = reader["Registration No."].ToString();
                    string id = reader["Project ID"].ToString();
                    string title = reader["Project Title"].ToString();
                    string grpid = reader["GroupId"].ToString();
                    string name = reader["Name"].ToString();
                    string marks = reader["ObtainedMarks"].ToString();
                    DC.Add(new Paragraph(reg + "     " + id + "      " + title + "               " + grpid + "                   " + name + "                               " + marks));
                }

                reader.Close();
                DC.Close();
                MessageBox.Show("Report has been generated successfully! Please check DESKTOP folder.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stuNotGrpRepBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/StudentsNotInGroupsReport.pdf";
                Document DC = new Document();
                FileStream FS = File.Create(path);
                PdfWriter.GetInstance(DC, FS);
                DC.Open();
                DC.Add(new Paragraph(""));

                var c = Configuration.getInstance().getConnection();
                SqlCommand sc = new SqlCommand(" SELECT RegistrationNo, (FirstName + ' ' + LastName) AS Name  FROM Student AS S JOIN Person AS P ON S.Id = P.Id WHERE S.Id <> ALL (SELECT StudentId FROM GroupStudent WHERE Status = (SELECT Id FROM Lookup WHERE Value = 'Active')) ORDER BY RegistrationNo\r\n", c);
                SqlDataReader reader;

                reader = sc.ExecuteReader();

                DC.Add(new Paragraph("REG. NO                           FULL NAME"));
                while (reader.Read())
                {
                    string reg = reader["RegistrationNo"].ToString();
                    string name = reader["Name"].ToString();
                    DC.Add(new Paragraph(reg + "                      " + name));
                }

                reader.Close();
                DC.Close();
                MessageBox.Show("Report has been generated successfully! Please check DESKTOP folder.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grpLessPerRepBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/GroupsLessScoreReport.pdf";
                Document DC = new Document();
                FileStream FS = File.Create(path);
                PdfWriter.GetInstance(DC, FS);
                DC.Open();
                DC.Add(new Paragraph(""));

                var c = Configuration.getInstance().getConnection();
                SqlCommand sc = new SqlCommand(" SELECT Id, Name, GroupId, TotalMarks, ObtainedMarks FROM Evaluation AS E JOIN GroupEvaluation AS GE ON GE.EvaluationId = E.Id WHERE ((GE.ObtainedMarks) / e.TotalMarks) * 100 < 60 ORDER BY E.Id, GE.GroupId", c);
                SqlDataReader reader;

                reader = sc.ExecuteReader();

                DC.Add(new Paragraph("EVA. ID            NAME           GROUP ID            TOTAL MARKS             OBT. MARKS"));
                while (reader.Read())
                {
                    string evaid = reader["Id"].ToString();
                    string name = reader["Name"].ToString();
                    string grpid = reader["GroupId"].ToString();
                    string total = reader["TotalMarks"].ToString();
                    string obt = reader["ObtainedMarks"].ToString();    
                    DC.Add(new Paragraph(evaid + "                   " + name + "                 " + grpid + "                           " + total + "                            " + obt));
                }

                reader.Close();
                DC.Close();
                MessageBox.Show("Report has been generated successfully! Please check DESKTOP folder.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void goBackGenRepBtn_Click(object sender, EventArgs e)
        {
            addEvaPanel.BringToFront();
        }

        private void generateReportsBtn_Click(object sender, EventArgs e)
        {
            genReportsPanel.BringToFront();
        }
    }
}

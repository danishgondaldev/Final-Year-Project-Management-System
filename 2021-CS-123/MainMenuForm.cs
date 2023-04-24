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
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void mngAdvisorsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAdvisors f2 = new ManageAdvisors();
            f2.Show();
        }

        private void mngStudentsBtn_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ManageStudents f2 = new ManageStudents();
            f2.Show();
        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f2 = new Form1();
            f2.Show();
        }

        private void mngProjectsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageProjects f2 = new ManageProjects();
            f2.Show();
        }

        private void mngEvaluationsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageEvaluations f2 = new ManageEvaluations();
            f2.Show();
        }
    }
}

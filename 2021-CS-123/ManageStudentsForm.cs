using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidFinalProject
{
    public partial class ManageStudentsForm : Form
    {
        public ManageStudentsForm()
        {
            InitializeComponent();
        }

        private void addStudentBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageStudents f1 = new ManageStudents();
            f1.Show();
        }

        private void updateStudentBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateAStudentMain f2 = new UpdateAStudentMain();
            f2.Show();
        }
    }
}

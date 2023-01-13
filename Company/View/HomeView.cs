using System;
using System.Windows.Forms;

namespace Company.View
{
    public partial class HomeView : Form
    {
        public HomeView()
        {
            InitializeComponent();
            IsMdiContainer = true;

        }

        private void department_Click(object sender, EventArgs e)
        {
            Form _department = new DepartmentView();
            _department.MdiParent = this;
            _department.Dock = DockStyle.Fill;
            _department.WindowState = FormWindowState.Maximized;
            _department.Show();
        }

        private void employee_Click(object sender, EventArgs e)
        {
            Form _employee = new EmployeeView();
            _employee.MdiParent = this;
            _employee.Dock = DockStyle.Fill;
            _employee.WindowState = FormWindowState.Maximized;
            _employee.Show();
        }

        private void salary_Click(object sender, EventArgs e)
        {
            Form _salary = new SalaryView();
            _salary.MdiParent = this;
            _salary.Dock = DockStyle.Fill;
            _salary.WindowState = FormWindowState.Maximized;
            _salary.Show();
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form _attendance = new AttendanceView();
            _attendance.MdiParent = this;
            _attendance.Show();
        }

        private void treeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form _treeview = new DepartmentTreeView();
            _treeview.MdiParent = this;
            _treeview.Dock = DockStyle.Fill;
            _treeview.WindowState = FormWindowState.Maximized;
            _treeview.Show();
        }
    }
}

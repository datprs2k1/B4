using Company.Controller;
using Company.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Company.View
{
    public partial class AttendanceView : Form
    {
        AttendanceController controller = new AttendanceController();
        EmployeeController employeeController = new EmployeeController();
        public AttendanceView()
        {
            InitializeComponent();
            load(dtpDate.Value);
        }

        public void load(DateTime date)
        {

            lblDate.Text = date.ToString("dd-MM-yyyy");

            var data = employeeController.getAll()
                .GroupJoin(
                controller.getAll().Where(x => x.created_at.Day == date.Day && x.created_at.Month == date.Month && x.created_at.Year == date.Year),
                e => e.id,
                a => a.employee_id,
                (e, a) => new { e, a }
                )
                .SelectMany(x => x.a.DefaultIfEmpty(), (employee, attendance) => new
                {
                    id = attendance != null ? attendance.id : -1,
                    name = employee.e.name,
                    status = attendance != null ? attendance.status : 0,
                    created_at = attendance != null ? attendance.created_at.ToString("dd-MM-yyyy") : date.ToString("dd-MM-yyyy"),
                    employee_id = employee.e.id
                }).ToList();
            ;

            var table = new DataTable();

            table.Columns.Add("id", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("status", typeof(int));
            table.Columns.Add("created_at", typeof(String));
            table.Columns.Add("employee_id", typeof(int));

            foreach (var entity in data)
            {
                var row = table.NewRow();
                row["id"] = entity.id;
                row["name"] = entity.name;
                row["status"] = entity.status;
                row["created_at"] = entity.created_at;
                row["employee_id"] = entity.employee_id;

                table.Rows.Add(row);
            }
            list.AutoGenerateColumns = false;
            list.DataSource = table;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in list.Rows)
            {
                if (Convert.ToInt32(row.Cells[0].Value) == -1)
                {
                    Attendance a = new Attendance();
                    a.employee_id = Convert.ToInt32(row.Cells[4].Value);
                    a.status = Convert.ToInt32(row.Cells[2].Value);
                    a.created_at = dtpDate.Value;

                    try
                    {
                        Boolean result = controller.add(a);
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
                else
                {

                    Attendance a = new Attendance();
                    a.employee_id = Convert.ToInt32(row.Cells[4].Value);
                    a.status = Convert.ToInt32(row.Cells[2].Value);
                    a.created_at = dtpDate.Value;
                    try
                    {
                        Boolean result = controller.update(Convert.ToInt32(row.Cells[0].Value), a);
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }

            load(dtpDate.Value);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Today.Day != dtpDate.Value.Day)
            {
                load(dtpDate.Value);
                btnSave.Enabled = false;
            }
            else
            {
                load(dtpDate.Value);
                btnSave.Enabled = true;
            }
        }

    }
}

using Company.Controller;
using Company.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.View
{
    public partial class EmployeeView : Form
    {
        string status = "reset";
        int id;
        EmployeeController controller = new EmployeeController();
        DepartmentController departmentController= new DepartmentController();
        SalaryController salaryController = new SalaryController();
        public EmployeeView()
        {
            InitializeComponent();

            setStatus(status);

            load();
            getData();

        }

        public void getData()
        {
            list.AutoGenerateColumns = false;
            list.DataSource = controller.getAll().Select(x => new
            {
                id = x.id,
                name = x.name,
                dob = x.dob,
                address = x.address,
                genderdisplay = x.gender == 1 ? "Male" : "Female",
                startdate = x.startdate,
                department = x.Department.name,
                salary = x.Salary.name,
            }).ToList();
        }

        public void load()
        {
            var departments = departmentController.getAll();

            if (departments != null)
            {
                cboDepartment.DisplayMember = "name";
                cboDepartment.ValueMember = "id";
                cboDepartment.DataSource = departments;
            }

            var salaries = salaryController.getAll();

            if (salaries != null)
            {
                cboSalary.DisplayMember = "name";
                cboSalary.ValueMember = "id";
                cboSalary.DataSource = salaries;
            }

            cboGender.DisplayMember = "name";
            cboGender.ValueMember = "id";

            var gender = new[]
            {
                new
                {
                    name = "Male",
                    id = 1
                },
                new
                {
                    name = "Female",
                    id = 2
                }
            };

            cboGender.DataSource = gender;

        }

        public void setStatus(string status)
        {
            switch (status)
            {
                case "reset":
                    txtName.Enabled = false;
                    txtAddress.Enabled = false;
                    cboGender.Enabled = false;
                    cboSalary.Enabled = false;
                    cboDepartment.Enabled = false;
                    dtpStartDate.Enabled = false;
                    dtpDOB.Enabled = false;
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnReset.Enabled = true;
                    break;
                case "add":
                    txtName.Enabled = true;
                    txtAddress.Enabled = true;
                    cboGender.Enabled = true;
                    cboSalary.Enabled = true;
                    cboDepartment.Enabled = true;
                    dtpStartDate.Enabled = true;
                    dtpDOB.Enabled = true;
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    btnReset.Enabled = true;
                    break;
                case "edit":
                    txtName.Enabled = true;
                    txtAddress.Enabled = true;
                    cboGender.Enabled = true;
                    cboSalary.Enabled = true;
                    cboDepartment.Enabled = true;
                    dtpStartDate.Enabled = true;
                    dtpDOB.Enabled = true;
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    btnReset.Enabled = true;
                    break;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            status = "add";
            setStatus(status);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            status = "edit";
            setStatus(status);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (status == "add")
            {
                if (String.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Name is required.");
                    return;
                }

                if (String.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Address is required.");
                    return;
                }                

                Employee employee = new Employee();
                employee.name= txtName.Text;
                employee.address= txtAddress.Text;
                employee.dob = dtpDOB.Value;
                employee.gender = Convert.ToByte(cboGender.SelectedValue.ToString());
                employee.startdate = dtpStartDate.Value;
                employee.salary_id = Convert.ToInt32(cboSalary.SelectedValue.ToString());
                employee.department_id = Convert.ToInt32(cboDepartment.SelectedValue.ToString());


                Boolean result = controller.add(employee);

                if (result == true)
                {
                    MessageBox.Show("Success");
                    getData();
                    load();
                    setStatus("reset");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
            else if (status == "edit")
            {
                if (String.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Name is required.");
                    return;
                }

                if (String.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Address is required.");
                    return;
                }

                Employee employee = new Employee();
                employee.name = txtName.Text;
                employee.address = txtAddress.Text;
                employee.dob = dtpDOB.Value;
                employee.gender = Convert.ToByte(cboGender.SelectedValue.ToString());
                employee.startdate = dtpStartDate.Value;
                employee.salary_id = Convert.ToInt32(cboSalary.SelectedValue.ToString());
                employee.department_id = Convert.ToInt32(cboDepartment.SelectedValue.ToString());


                Boolean result = controller.update(id, employee);

                if (result == true)
                {
                    MessageBox.Show("Success");
                    getData();
                    load();
                    setStatus("reset");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            DataGridViewRow row = list.Rows[index];

            if (row != null)
            {
                id = Convert.ToInt32(row.Cells[0].Value.ToString());

                Employee a = controller.find(id);

                txtName.Text = a.name;
                txtAddress.Text = a.address;
                cboGender.SelectedValue = Convert.ToInt32(a.gender);
                cboDepartment.SelectedValue = a.department_id;
                cboSalary.SelectedValue= a.salary_id;
                dtpDOB.Value = a.dob;
                dtpStartDate.Value = a.startdate;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Boolean result = controller.delete(id);

                if (result == true)
                {
                    MessageBox.Show("Success");
                    getData();
                    load();
                    setStatus("reset");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            setStatus("reset");
            load();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Keyword is required.");
                return;
            }

            var result = controller.find(Convert.ToInt32(txtSearch.Text));

            if(result == null)
            {
                list.DataSource= null;
                list.Refresh();
                return;
            }

            var a = new[]
            {
                new
                    {

                        id = result.id,
                        name = result.name,
                        dob = result.dob,
                        address = result.address,
                        genderdisplay = result.gender == 1 ? "Male" : "Female",
                        startdate = result.startdate,
                        department = result.Department.name,
                        salary = result.Salary.name,
                    }
            };

            list.DataSource = a;

            list.Refresh();
        }
    }
}

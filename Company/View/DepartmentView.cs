using Company.Controller;
using Company.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Company.View
{
    public partial class DepartmentView : Form
    {
        string status = "reset";
        int id;
        DepartmentController controller = new DepartmentController();
        public DepartmentView()
        {
            InitializeComponent();

            setStatus("reset");

            load();
            getData();

        }

        public void getData()
        {
            list.AutoGenerateColumns = false;
            list.DataSource = controller.getAll().Select(x => new
            {
                id = x.id,
                code = x.code,
                name = x.name,
                description = x.description,
                parent = x.parentDepartment?.name
            }).ToList();
        }

        public void load()
        {
            var parent = controller.getAll().Where(x => x.parent == null).ToList();

            Department department = new Department();
            department.id = 0;
            department.name = "Select Department";

            parent.Insert(0, department);

            if (parent != null)
            {
                cboParent.DisplayMember = "name";
                cboParent.ValueMember = "id";
                cboParent.DataSource = parent;
            }

        }

        public void setStatus(string status)
        {
            switch (status)
            {
                case "reset":
                    txtCode.Enabled = false;
                    txtName.Enabled = false;
                    txtDescription.Enabled = false;
                    cboParent.Enabled = false;
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnReset.Enabled = true;
                    break;
                case "add":
                    txtCode.Enabled = true;
                    txtName.Enabled = true;
                    txtDescription.Enabled = true;
                    cboParent.Enabled = true;
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    btnReset.Enabled = true;
                    break;
                case "edit":
                    txtCode.Enabled = true;
                    txtName.Enabled = true;
                    txtDescription.Enabled = true;
                    cboParent.Enabled = true;
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
                if (String.IsNullOrEmpty(txtCode.Text))
                {
                    MessageBox.Show("Code is required");
                    return;
                }
                if (String.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Name is required.");
                    return;
                }

                if (String.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageBox.Show("Description is required.");
                    return;
                }

                Department department = new Department();
                department.code = txtCode.Text;
                department.name = txtName.Text;
                department.description = txtDescription.Text;
                if (Convert.ToInt32(cboParent.SelectedValue.ToString()) > 0)
                {
                    department.parent = Convert.ToInt32(cboParent.SelectedValue.ToString());
                }


                Boolean result = controller.add(department);

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
                if (String.IsNullOrEmpty(txtCode.Text))
                {
                    MessageBox.Show("Code is required");
                    return;
                }
                if (String.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Name is required.");
                    return;
                }

                if (String.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageBox.Show("Description is required.");
                    return;
                }

                Department department = new Department();
                department.code = txtCode.Text;
                department.name = txtName.Text;
                department.description = txtDescription.Text;
                if (Convert.ToInt32(cboParent.SelectedValue.ToString()) > 0)
                {
                    department.parent = Convert.ToInt32(cboParent.SelectedValue.ToString());
                }


                Boolean result = controller.update(department);

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

                Department a = controller.find(id);

                txtCode.Text = a.code;
                txtName.Text = a.name;
                txtDescription.Text = a.description;


                cboParent.SelectedItem = cboParent.FindString(a.parentDepartment?.name);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var department = controller.find(id);
                var employeeCount = department.Employees.Count();

                if (employeeCount > 0)
                {
                    MessageBox.Show("The department is not empty");
                    return;
                }

                var departmentCount = department.childDepartments.Count();

                if (departmentCount > 0)
                {
                    MessageBox.Show("The department has a sub department");
                    return;
                }
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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Keyword is required.");
                return;
            }

            var result = controller.find(Convert.ToInt32(txtSearch.Text));

            if (result == null)
            {
                list.DataSource = null;
                list.Refresh();
                return;
            }

            var a = new[]
            {
                new
                    {

                        id = result.id,
                        code = result.code,
                        name = result.name,
                        description = result.description,
                        parent = result.parentDepartment?.name
                    }
            };

            list.DataSource = a;

            list.Refresh();
        }
    }
}

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
    public partial class SalaryView : Form
    {
        string status = "reset";
        int id;
        SalaryController controller = new SalaryController();
        public SalaryView()
        {
            InitializeComponent();

            setStatus(status);
            getData();

        }

        public void getData()
        {
            list.AutoGenerateColumns = false;
            list.DataSource = controller.getAll().ToList();
        }


        public void setStatus(string status)
        {
            switch (status)
            {
                case "reset":
                    txtName.Enabled = false;
                    txtCoefficient.Enabled = false;
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnReset.Enabled = false;
                    break;
                case "add":
                    txtName.Enabled = true;
                    txtCoefficient.Enabled = true;
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    btnReset.Enabled = true;
                    break;
                case "edit":
                    txtName.Enabled = true;
                    txtCoefficient.Enabled = true;
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

                if (String.IsNullOrEmpty(txtCoefficient.Text))
                {
                    MessageBox.Show("Coefficient is required.");
                    return;
                }

                Salary salary = new Salary();
                salary.name = txtName.Text;

                salary.coefficient = Convert.ToDecimal(txtCoefficient.Text);

               
                Boolean result = controller.add(salary);

                if (result == true)
                {
                    MessageBox.Show("Success");
                    getData();
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

                if (String.IsNullOrEmpty(txtCoefficient.Text))
                {
                    MessageBox.Show("Coefficient is required.");
                    return;
                }

                Salary salary = new Salary();

                Boolean result = controller.update(id, salary);

                if (result == true)
                {
                    MessageBox.Show("Success");
                    getData();
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

                Salary a = controller.find(id);

                txtName.Text = a.name;
                txtCoefficient.Text = a.coefficient.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var salary = controller.find(id);

                var employeeCount = salary.Employees.Count();

                if(employeeCount > 0)
                {
                    MessageBox.Show("Salary is used");
                    return;
                }
                Boolean result = controller.delete(id);

                if (result == true)
                {
                    MessageBox.Show("Success");
                    getData();
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
            if(String.IsNullOrEmpty(txtSearch.Text))
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

            list.DataSource = result;

            list.Refresh();
        }
    }
}

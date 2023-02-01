using Company.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Company.View
{
    public partial class DepartmentTreeView : Form
    {
        DepartmentController controller = new DepartmentController();
        int idDepartment;
        public DepartmentTreeView()
        {
            InitializeComponent();

            var listRoot = controller.getAll().Where(x => x.parent == null).ToList();

            listRoot.ForEach(x =>
            {
                TreeNode treeRoot = new TreeNode();
                treeRoot.Text = x.name;
                treeRoot.Tag = x.id;
                treeRoot.ExpandAll();

                treeView1.Nodes.Add(treeRoot);

                foreach (TreeNode t in GetChildNode(x.id))
                {
                    treeRoot.Nodes.Add(t);
                }
            });
        }

        public List<TreeNode> GetChildNode(int parent)
        {
            List<TreeNode> list = new List<TreeNode>();

            var child = controller.getAll().Where(x => x.parent == parent).ToList();

            if (child.Count() > 0)
            {
                child.ForEach(x =>
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = x.name;
                    childNode.Tag = x.id;
                    childNode.ExpandAll();

                    foreach (TreeNode c in GetChildNode(x.id))
                    {
                        TreeNode a = new TreeNode();
                        a.Text = c.Text;
                        a.Tag = c.Tag;
                        a.ExpandAll();
                        childNode.Nodes.Add(a);
                    }

                    list.Add(childNode);
                });
            }
            return list;

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo info = treeView1.HitTest(treeView1.PointToClient(Cursor.Position));
            idDepartment = Convert.ToInt32(info.Node.Tag);
            loadData(Convert.ToInt32(info.Node.Tag), dtpDate.Value);
        }

        public void loadData(int id, DateTime date)
        {
            var data = controller.find(id).Employees.Select(x => new
            {
                id = x.id,
                name = x.name,
                work = x.Attendances.Where(a => a.created_at.Month == dtpDate.Value.Month && a.created_at.Year == dtpDate.Value.Year && a.status == 1).Count(),
                salary = x.Salary.coefficient * x.Attendances.Where(e => e.created_at.Month == date.Month && e.created_at.Year == date.Year && e.status == 1).Count(),
            }).ToList();

            list.AutoGenerateColumns = false;
            list.DataSource = data;
            list.Refresh();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            loadData(idDepartment, dtpDate.Value);
        }
    }
}

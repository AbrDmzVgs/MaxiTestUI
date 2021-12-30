using MaxiService.Models;
using System;
using MaxiService;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaxiTest
{
    public partial class frmEmployees : Form
    {
        private readonly ServiceClientApi serviceClient = new ServiceClientApi();
        public frmEmployees()
        {
            InitializeComponent();
        }

        private void btnAsignBeneficiaries_Click(object sender, EventArgs e)
        {
            frmBeneficiaries frm = new frmBeneficiaries();
            frm.ShowDialog();
        }

        private void btnNewEmployee_Click(object sender, EventArgs e)
        {
            frmEmployee frm = new frmEmployee();
            frm.Text = "New Employee";
            frm.ShowDialog();

            LoadEmployees();

        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            if (grdEmployees.CurrentRow != null)
            {
                frmEmployee frm = new frmEmployee();
                frm.Text = "Edit Employee";
                frm.EmployeeId = int.Parse(grdEmployees.CurrentRow.Cells["EmployeeId"].Value.ToString()); ;
                frm.ShowDialog();
                LoadEmployees();
            }
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        public async void LoadEmployees()
        {
            var result = await serviceClient.GetEmployees(new Employee {Id = 0 });
            grdEmployees.DataSource = result;

        }
    }
}

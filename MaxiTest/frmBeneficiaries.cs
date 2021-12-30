using MaxiService;
using MaxiService.Models;
using System;
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
    public partial class frmBeneficiaries : Form, IMaxiTest
    {
        private readonly ServiceClientApi serviceClient = new ServiceClientApi();
        List<Beneficiary> lstBeneficiary = new List<Beneficiary>();
        public frmBeneficiaries()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmBeneficiary frm = new frmBeneficiary();
            frm.contract = this;
            frm.ShowDialog();
        }

        public void GetBeneficiary(Beneficiary beneficiary)
        {
            grdBeneficiaries.DataSource = null;
            grdBeneficiaries.Refresh();
            lstBeneficiary.Add(beneficiary);
            grdBeneficiaries.DataSource = lstBeneficiary;
            grdBeneficiaries.Refresh();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int sumpct = 0;
            foreach(Beneficiary beneficiary in lstBeneficiary)
            {
                sumpct += beneficiary.ParticipationPercentage;
            }

            if(sumpct == 100)
            {
                var result = await serviceClient.CreateBeneficiary(lstBeneficiary);

                if (result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else 
            {
                MessageBox.Show("The sum of the participation percentages must be equal to 100", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

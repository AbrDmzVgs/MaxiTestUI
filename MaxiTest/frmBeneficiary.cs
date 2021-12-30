using System;
using MaxiService;
using System.Windows.Forms;
using System.Threading.Tasks;
using MaxiService.Models;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace MaxiTest
{
    public partial class frmBeneficiary : Form
    {
        public int BeneficiaryId;
        private readonly ServiceClientApi serviceClient = new ServiceClientApi();

        public IMaxiTest contract { get; set; }
        public frmBeneficiary()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (validateFields())
            {
                Beneficiary beneficiary = new Beneficiary();
                beneficiary.Name = textBox1.Text;
                beneficiary.LastName = textBox2.Text;
                beneficiary.DoB = dateTimePicker1.Value;
                beneficiary.Curp = textBox4.Text;
                beneficiary.Ssn = textBox5.Text;
                beneficiary.Phone = textBox6.Text;
                beneficiary.Nationality = textBox7.Text;
                beneficiary.ParticipationPercentage = Convert.ToInt32(textBox8.Text);


                contract.GetBeneficiary(beneficiary);

                Close();
                //contract.GetBeneficiary(beneficiary);
                //if (BeneficiaryId == 0)
                //{
                //    Beneficiary beneficiary = new Beneficiary();
                //    beneficiary.Name = textBox1.Text;
                //    beneficiary.LastName = textBox2.Text;
                //    beneficiary.DoB = dateTimePicker1.Value;
                //    beneficiary.Curp = textBox4.Text;
                //    beneficiary.Ssn = textBox5.Text;
                //    beneficiary.Phone = textBox6.Text;
                //    beneficiary.Nationality = textBox7.Text;

                //    var result = await serviceClient.CreateBeneficiary(beneficiary);

                //    if (result.IsSuccess)
                //    {
                //        MessageBox.Show(result.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        Close();
                //    }
                //    else
                //    {
                //        MessageBox.Show(result.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //}
                //else
                //{
                //    Beneficiary beneficiary = new Beneficiary();
                //    beneficiary.Id = BeneficiaryId;
                //    beneficiary.Name = textBox1.Text;
                //    beneficiary.LastName = textBox2.Text;
                //    beneficiary.DoB = dateTimePicker1.Value;
                //    beneficiary.Curp = textBox4.Text;
                //    beneficiary.Ssn = textBox5.Text;
                //    beneficiary.Phone = textBox6.Text;
                //    beneficiary.Nationality = textBox7.Text;

                //    var result = await serviceClient.UpdateBeneficiary(beneficiary);

                //    if (result.IsSuccess)
                //    {
                //        MessageBox.Show(result.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        Close();
                //    }
                //    else
                //    {
                //        MessageBox.Show(result.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //}

            }
            else
                MessageBox.Show("Incomplete information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private bool validateFields()
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) &&
                !string.IsNullOrEmpty(textBox2.Text.Trim()) &&
                !string.IsNullOrEmpty(textBox4.Text.Trim()) &&
                !string.IsNullOrEmpty(textBox5.Text.Trim()) &&
                !string.IsNullOrEmpty(textBox6.Text.Trim()) &&
                !string.IsNullOrEmpty(textBox7.Text.Trim()) &&
                Validar(textBox6.Text))
                return true;
            else
                return false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
            txt.Select(txt.Text.Length, 0);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
            txt.Select(txt.Text.Length, 0);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
            txt.Select(txt.Text.Length, 0);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
            txt.Select(txt.Text.Length, 0);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
            txt.Select(txt.Text.Length, 0);
        }
        public static bool Validar(string telefono)
        {
            Regex exreg = new Regex("^\\d{10}$");
            return exreg.IsMatch(telefono);
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            var resp = Validar(textBox6.Text);

            if (!resp)
            {
                MessageBox.Show("the phone number format must be 10 digits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox6.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.AddYears(18) > DateTime.Today)
            {
                MessageBox.Show("The Beneficiary must be over 18 years old", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker1.Focus();
            }
        }

        private async void frmBeneficiary_Load(object sender, EventArgs e)
        {
            if (BeneficiaryId > 0)
            {
                var Beneficiary = await serviceClient.GetBeneficiaries(new Beneficiary { Id = BeneficiaryId });

               if(Beneficiary.ToList().Count > 0)
                {
                    textBox1.Text = Beneficiary.ToList()[0].Name;
                    textBox2.Text = Beneficiary.ToList()[0].LastName;
                    textBox4.Text = Beneficiary.ToList()[0].Curp;
                    textBox5.Text = Beneficiary.ToList()[0].Ssn;
                    textBox6.Text = Beneficiary.ToList()[0].Phone;
                    textBox7.Text = Beneficiary.ToList()[0].Nationality;
                    dateTimePicker1.Value = Beneficiary.ToList()[0].DoB;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

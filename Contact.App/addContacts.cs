using Contact.App.Repository;
using Contact.App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Contact.App
{
    public partial class addContacts : Form
    {
        public int contactId = 0;
        IContactRepository repository;
        public addContacts()
        {
            InitializeComponent();
            repository = new ContactRepository();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void addContacts_Load(object sender, EventArgs e)
        {
            if (contactId == 0)
            {
                this.Text = "Add Contact";
            }
            else
            {
                this.Text = "Edit Contact";
                DataTable dt = repository.SelectById(contactId);
                txtContactName.Text = dt.Rows[0][1].ToString();
                txtContactFamily.Text = dt.Rows[0][2].ToString();
                txtMobile.Text = dt.Rows[0][3].ToString();
                txtEmail.Text = dt.Rows[0][4].ToString();
                txtAge.Text = dt.Rows[0][5].ToString();
                txtAddress.Text = dt.Rows[0][6].ToString();
                btnAdd.Text = "Edit";
            }
        }
        bool ValidateInputs()
        {
            if (txtContactName.Text == "")
            {
                MessageBox.Show("please enter your name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobile.Text == "")
            {
                MessageBox.Show("please enter your mobile", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;
                return false;
            }
            if (txtContactFamily.Text == "")
            {
                MessageBox.Show("please enter your family", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;
                return false;
            }
            if (txtAge.Value == 0)
            {
                MessageBox.Show("please enter your age", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("please enter your email", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;
                return false;
            }
            return true;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                bool isSuccess;
                if (contactId == 0)
                {
                    isSuccess = repository.Insert(txtContactName.Text, txtContactFamily.Text, txtMobile.Text, txtEmail.Text, (int)txtAge.Value, txtAddress.Text);
                }
                else
                {
                    isSuccess = repository.Update(contactId, txtContactFamily.Text, txtContactFamily.Text, txtMobile.Text, txtEmail.Text, (int)txtAge.Value, txtAddress.Text);
                }

                if (isSuccess == true)
                {
                    MessageBox.Show("Done", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Faile", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}


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

namespace Contact.App
{
    public partial class Form1 : Form
    {
        IContactRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new ContactRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }


        void BindGrid()
        {
            dgvContacts.AutoGenerateColumns = false;
            dgvContacts.DataSource = repository.SelectAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addContacts frmAddContacts = new addContacts();
            frmAddContacts.ShowDialog();
            if (frmAddContacts.DialogResult == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow != null)
            {
                string name = dgvContacts.CurrentRow.Cells[1].Value.ToString();
                string family = dgvContacts.CurrentRow.Cells[2].Value.ToString();
                string fullName = name + " " + family;
                if (MessageBox.Show($"want to delete {fullName}", "warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int contactID = int.Parse(dgvContacts.CurrentRow.Cells[0].Value.ToString());
                    repository.Delete(contactID);
                    BindGrid();
                }

            }
            else
            {
                MessageBox.Show("please select contact");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow != null)
            {
                int contactId = int.Parse(dgvContacts.CurrentRow.Cells[0].Value.ToString());
                addContacts frm = new addContacts();
                frm.contactId = contactId;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvContacts.DataSource = repository.Search(txtSearch.Text);
        }
    }
}

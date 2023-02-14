using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        public frmMemberManagement()
        {
            InitializeComponent();
            //if (!mail.Equals("admin@fstore.com") && !pass.Equals("admin@@"))
            //{
            //    txtID.Enabled = false;
            //    txtSearch.Enabled = false;
            //    btnDelete.Enabled = false;
            //    btnNew.Enabled = false;
            //    btnSort.Enabled = false;
            //    cboCity.Enabled = false;
            //    cboCountry.Enabled = false;
            //}
            Load();
            DataGridViewButtonColumn dataGridViewButton = new DataGridViewButtonColumn();
            dataGridViewButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewButton.Text = "Update";
            dataGridViewButton.HeaderText = "Action";
            dataGridViewButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(dataGridViewButton);
        }

        public IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;

        public void Load()
        {
            source = new BindingSource();
            source.DataSource = memberRepository.GetMembers();
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].ReadOnly = true;
            cboCity.DataSource = memberRepository.GetCity();
            cboCountry.DataSource = memberRepository.GetCountry();           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmAddMember frmAddMember = new frmAddMember();
            frmAddMember.Show();
            frmAddMember.FormClosed += new FormClosedEventHandler(frmAddMember_FormClosed);
        }

        private void frmAddMember_FormClosed(object sender, FormClosedEventArgs e)
        {
            Load();
        }

        string id;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                memberRepository.DeleteMember(int.Parse(id));
                MessageBox.Show("Delete successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell is DataGridViewButtonCell)
            {
                string name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string email = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string pass = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string city = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                string country = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                Member member = new Member
                {
                    MemberID = int.Parse(id),
                    MemberName = name,
                    Email = email,
                    Password = pass,
                    City = city,
                    Country = country
                };
                memberRepository.UpdateMember(int.Parse(id), member);
                MessageBox.Show("Update member successful");
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = memberRepository.SortMembers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToString();
            if (search.Length == 0 || search.Equals(""))
            {
                dataGridView1.DataSource = memberRepository.GetMembers();
            }
            else
            {
                dataGridView1.DataSource = memberRepository.Search(search);
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (txtID.Text.Length == 0)
            {
                dataGridView1.DataSource = memberRepository.GetMembers();
            }
            if (Regex.IsMatch(txtID.Text, "^[0-9]"))
            {
                int sid = int.Parse(txtID.Text);
                dataGridView1.DataSource = memberRepository.SearchbyID(sid);
            }
            else
            {
                MessageBox.Show("Number only", "Warning");
            }
        }

        private void cboCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string city = cboCity.Text;
            string country = cboCountry.Text;
            dataGridView1.DataSource = memberRepository.Filter(city, country);
        }

        private void cboCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string city = cboCity.Text;
            string country = cboCountry.Text;
            dataGridView1.DataSource = memberRepository.Filter(city, country);
        }
    }
}


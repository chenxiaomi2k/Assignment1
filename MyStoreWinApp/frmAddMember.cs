using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmAddMember : Form
    {
        public frmAddMember()
        {
            InitializeComponent();
        }

        public IMemberRepository memberRepository = new MemberRepository();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtMemberID.Text.ToString();
            string name = txtName.Text.ToString();
            string email = txtEmail.Text.ToString();
            string pass = txtPassword.Text.ToString();
            string city = txtCity.Text.ToString();
            string country = txtCountry.Text.ToString();
            if (id.ToString().Length == 0 || name.Length == 0 ||
                email.Length == 0 || pass.Length == 0 || city.Length == 0 || country.Length == 0)
            {
                MessageBox.Show("Enter missing field", "Warning");
            }
            else
            {
                try
                {
                    Member m = new Member
                    {
                        MemberID = int.Parse(id),
                        MemberName = name,
                        Email = email,
                        Password = pass,
                        City = city,
                        Country = country
                    };
                    memberRepository.CreateMember(id, name, email, m);
                    Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}

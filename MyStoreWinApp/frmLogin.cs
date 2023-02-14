using BusinessObject;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public IMemberRepository mem = new MemberRepository();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            Member m = mem.Login(email, pass);
            if (m != null)
            {
                this.Hide();
                frmMemberManagement frmMemberManagement = new frmMemberManagement();
                frmMemberManagement.FormClosed += (s, args) => this.Close();
                frmMemberManagement.Show();
            }
            else
            {
                MessageBox.Show("Incorrect email or password!", "Fail to login");
            }
        }
    }
}
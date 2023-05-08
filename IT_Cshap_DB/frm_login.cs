using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IT_Cshap_DB
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if(txt_username.Text == "")
            {
                MessageBox.Show("ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "ແຈ້ງເຕືອນ");
                txt_username.Text = "";
                txt_password.Text = "";
                txt_username.Select();
            }else if(txt_password.Text == "")
            {
                MessageBox.Show("ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "ແຈ້ງເຕືອນ");
                txt_username.Text = "";
                txt_password.Text = "";
                txt_username.Select();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4JF77HQ\SQLEXPRESS;Initial Catalog=Project2023;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_login WHERE user_name = @user_name and user_password = @user_password", conn);
                    cmd.Parameters.AddWithValue("@user_name", txt_username.Text);
                    cmd.Parameters.AddWithValue("@password_name", txt_password.Text);
                }
                catch { }
            }
        }
    }
}

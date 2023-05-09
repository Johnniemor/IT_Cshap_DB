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

        string strcon = @"Data Source=DESKTOP-4JF77HQ\SQLEXPRESS;Initial Catalog=Project2023;Integrated Security=True";

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
                    SqlConnection conn = new SqlConnection(strcon);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_login WHERE user_name = @user_name and user_password = @user_password", conn);
                    cmd.Parameters.AddWithValue("@user_name", txt_username.Text);
                    cmd.Parameters.AddWithValue("@user_password", txt_password.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if(dt.Rows.Count > 0)
                    {
                        MessageBox.Show("ຍິນດີຕ້ອນຮັບເຂົ້າສູ່ລະບົບ" , "ສຳເລັດ");
                        frm_menu Menu = new frm_menu();
                        Menu.Show();
                        this.Hide();
                        txt_username.Text = "";
                        txt_password.Text = "";
                        txt_username.Select();
                    }
                    else
                    {
                        MessageBox.Show("ຊື່ຜູ້ໃຊ້ ແລະ ລະຫັດຜ່ານບໍ່ຖືກຕ້ອງ", "ແຈ້ງເຕືອນ");
                        txt_username.Text = "";
                        txt_password.Text = "";
                        txt_username.Select();
                    }
                }
                catch(Exception ex) {
                    MessageBox.Show(" " + ex);
                }
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_username.Clear();
            txt_password.Clear();
        }
    }
}

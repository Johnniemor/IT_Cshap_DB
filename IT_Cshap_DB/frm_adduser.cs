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
    public partial class frm_adduser : Form
    {
        public frm_adduser()
        {
            InitializeComponent();
            Auto_id();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4JF77HQ\SQLEXPRESS;Initial Catalog=Project2023;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        string sql;

        public void Auto_id()
        {
            try
            {
                sql = "SELECT MAX(user_id) FROM tbl_login";
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                var maxid = cmd.ExecuteScalar() as string;
                if (maxid == null)
                {
                    txt_id.Text = "U-000001";
                }
                else
                {
                    int intval = int.Parse(maxid.Substring(2, 6));
                    intval++;
                    txt_id.Text = string.Format("U-{0:000000}", intval);
                }
                conn.Close();
            }catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }

        private void frm_adduser_Load(object sender, EventArgs e)
        {

        }
    }
}

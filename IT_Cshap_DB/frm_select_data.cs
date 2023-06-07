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
    public partial class frm_select_data : Form
    {
        public frm_select_data()
        {
            InitializeComponent();
            User_ID();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-4JF77HQ\SQLEXPRESS;Initial Catalog=Project2023;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string sql;

        public void User_ID()
        {
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sql = "SELECT * FROM tbl_login";
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmb_id.DataSource = dt;
                cmb_id.DisplayMember = "user_id";
                cmb_id.ValueMember = "user_id";
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmb_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            sql = "SELECT * FROM tbl_login WHERE user_id = + '" + cmb_id.Text + "'";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = (string)dr["user_name"].ToString();
                txt_name.Text = name;
                string pass = (string)dr["user_password"].ToString();
                txt_pass.Text = pass;
                string status = (string)dr["user_status"].ToString();
                txt_status.Text = status;
            }
            conn.Close();
        }
    }
}

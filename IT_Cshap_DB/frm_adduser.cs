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

        private void btn_save_Click(object sender, EventArgs e)
        {

            sql = "INSERT INTO tbl_login (user_id , user_name , user_password , user_status) " +
                "Values (@user_id , @user_name , @user_password , @user_status)";
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user_id" , txt_id.Text);
            cmd.Parameters.AddWithValue("@user_name" , txt_name.Text);
            cmd.Parameters.AddWithValue("@user_password", txt_pass.Text);
            cmd.Parameters.AddWithValue("@user_status" , cmb_status.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("ບັນທຶກຂໍ້ມູນສຳເລັດ" , "ສຳເລັດ");
            conn.Close();
            Auto_id();
            txt_name.Clear();
            txt_pass.Clear();
            cmb_status.Text = "";
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            conn.Open();
            sql = "SELECT user_id as 'id' , user_name as 'name' , user_password as 'password' , user_status as 'status' FROM tbl_login ORDER BY user_id ASC";
            cmd = new SqlCommand(sql, conn);
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView1.CurrentRow.Index != -1)
            {
                txt_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txt_pass.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cmb_status.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
        }
    }
}

﻿using System;
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
        DataTable dt;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }

        public void header_table(string e)
        {
            dataGridView1.Columns[0].HeaderText = "ລະຫັດຜູ້ໃຊ້";
            dataGridView1.Columns[1].HeaderText = "ຊື່ຜູ້ໃຊ້";
            dataGridView1.Columns[2].HeaderText = "ລະຫັດຜ່ານ";
            dataGridView1.Columns[3].HeaderText = "ສະຖານະ";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            conn.Open();
            sql = "INSERT INTO tbl_login (user_id , user_name , user_password , user_status) Values (@user_id , @user_name , @user_password , @user_status)";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user_id" , txt_id.Text);
            cmd.Parameters.AddWithValue("@user_name" , txt_name.Text);
            cmd.Parameters.AddWithValue("@user_password", txt_pass.Text);
            cmd.Parameters.AddWithValue("@user_status", cmb_status.SelectedItem);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ບັນທຶກຂໍ້ມູນສຳເລັດ" , "ສຳເລັດ");
            conn.Close();
            header_table("");
            Auto_id();
            txt_name.Clear();
            txt_pass.Clear();
            cmb_status.Text = "";
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            conn.Open();
            sql = "SELECT user_id , user_name , user_password , user_status FROM tbl_login ORDER BY user_id";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            header_table("");
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນ ຫຼື ບໍ່?", "ຄຳເຕືອນ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                conn.Open();
                sql = "UPDATE tbl_login SET user_name = @user_name , user_password = @user_password , user_status = @user_status WHERE user_id = '" + txt_id.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user_id", txt_id.Text);
                cmd.Parameters.AddWithValue("@user_name", txt_name.Text);
                cmd.Parameters.AddWithValue("@user_password", txt_pass.Text);
                cmd.Parameters.AddWithValue("@user_status", cmb_status.SelectedItem);
                cmd.ExecuteNonQuery();
                MessageBox.Show("ແກ້ໄຂຂໍ້ມູນສຳເລັດ", "ສຳເລັດ");
                conn.Close();
                header_table("");
                Auto_id();
                dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }     
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ທ່ານຕ້ອງການລົບຂໍ້ມູນ ຫຼື ບໍ່?", "ຄຳເຕືອນ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                sql = "DELETE FROM tbl_login WHERE user_id = '" + txt_id.Text + "'";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                check_dgv();
                conn.Close();
                Auto_id();
                txt_name.Clear();
                txt_pass.Clear();
                cmb_status.Text = "";
                DataTable dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void check_dgv()
        {
            int i = dataGridView1.RowCount;
            if (i > 0)
            {
                MessageBox.Show("ລົບຂໍ້ມູນສຳເລັດ", "ສຳເລັດ");
                txt_name.Clear();
                txt_name.Clear();
                txt_pass.Clear();
                cmb_status.SelectedItem = null;
                txt_name.Select();
            }
            else
            {
                MessageBox.Show("ບໍ່ມີຂໍ້ມູນໃຫ້ທ່ານລົບ", "ຄຳເຕືອນ");
                txt_name.Clear();
                txt_pass.Clear();
                cmb_status.SelectedItem = null;
                txt_name.Select();
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            string searchValue = txt_search.Text.Trim();
            string sqlstr = "";

            if (searchValue != "")
            {
                sqlstr = " WHERE user_name LIKE @user_name OR user_password LIKE @user_pwd";
            }
            string sql = "SELECT * FROM tbl_login " + sqlstr;
            cmd.Parameters.AddWithValue("@user_name", "%" + searchValue + "%");
            cmd.Parameters.AddWithValue("@user_pwd", "%" + searchValue + "%");
            cmd.ExecuteNonQuery();
            conn.Close();
            da = new SqlDataAdapter(cmd);
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_id.Clear();
            txt_name.Clear();
            txt_pass.Clear();
            txt_search.Clear();
            cmb_status.SelectedItem = null;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            header_table("");
        }
    }
}

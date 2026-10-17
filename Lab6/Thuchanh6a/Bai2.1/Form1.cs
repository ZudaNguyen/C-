using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai2._1
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private TextBox txtMaMH, txtTenMH;
        private Button btnThem, btnXoa, btnSua;

        public Form1()
        {
            VeGiaoDien();
            TuDongTaoDatabase();
        }

        private void TuDongTaoDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(masterConn))
                {
                    conn.Open();
                    string sqlCreateDB = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QLSinhVien') BEGIN CREATE DATABASE QLSinhVien; END";
                    using (SqlCommand cmd = new SqlCommand(sqlCreateDB, conn)) { cmd.ExecuteNonQuery(); }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlCreateTable = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MonHoc' and xtype='U') BEGIN CREATE TABLE MonHoc (MaMH NVARCHAR(20) PRIMARY KEY, TenMH NVARCHAR(100) NOT NULL); END";
                    using (SqlCommand cmd = new SqlCommand(sqlCreateTable, conn)) { cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo CSDL: " + ex.Message);
            }
        }

        private void VeGiaoDien()
        {
            this.Text = "Quản lý môn học";
            this.Size = new Size(360, 220);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblMaMH = new Label { Text = "Mã mh", Location = new Point(30, 25), AutoSize = true };
            txtMaMH = new TextBox { Location = new Point(100, 22), Width = 200 };

            Label lblTenMH = new Label { Text = "Tên mh", Location = new Point(30, 65), AutoSize = true };
            txtTenMH = new TextBox { Location = new Point(100, 62), Width = 200 };

            btnThem = new Button { Text = "Thêm", Location = new Point(35, 115), Size = new Size(75, 30) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(130, 115), Size = new Size(75, 30) };
            btnSua = new Button { Text = "Sửa", Location = new Point(225, 115), Size = new Size(75, 30) };

            this.Controls.AddRange(new Control[] { lblMaMH, txtMaMH, lblTenMH, txtTenMH, btnThem, btnXoa, btnSua });

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMH.Text) || string.IsNullOrEmpty(txtTenMH.Text))
            {
                MessageBox.Show("Không được để trống!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO MonHoc (MaMH, TenMH) VALUES (@MaMH, @TenMH)", conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenMH", txtTenMH.Text.Trim());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                    txtMaMH.Clear(); txtTenMH.Clear();
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Number == 2627 ? "Trùng mã môn học!" : "Lỗi: " + ex.Message); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMH.Text) || !string.IsNullOrEmpty(txtTenMH.Text))
            {
                MessageBox.Show("Chỉ nhập Mã môn học, để trống Tên môn học!");
                txtTenMH.Clear();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM MonHoc WHERE MaMH = @MaMH", conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text.Trim());
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Xóa thành công!" : "Không tìm thấy mã này!");
                    txtMaMH.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMH.Text) || string.IsNullOrEmpty(txtTenMH.Text))
            {
                MessageBox.Show("Không được để trống!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("UPDATE MonHoc SET TenMH = @TenMH WHERE MaMH = @MaMH", conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenMH", txtTenMH.Text.Trim());
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Sửa thành công!" : "Không tìm thấy mã này!");
                    txtMaMH.Clear(); txtTenMH.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}
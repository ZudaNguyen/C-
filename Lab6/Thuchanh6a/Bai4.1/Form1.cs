/*
* BÀI TẬP VỀ NHÀ - BÀI 1: QUẢN LÝ LỚP
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
*
* Yêu cầu chức năng:
* - Thêm: Mã khoa, Mã lớp, Tên lớp không được để trống -> Lưu vào bảng Lop.
* - Xóa: Mã lớp không được để trống. Mã khoa, Tên lớp không nhập dữ liệu -> Xóa dòng[cite: 12].
* - Sửa: Mã lớp không để trống. Mã khoa, Tên lớp phải có ít nhất 1 trường có dữ liệu -> Cập nhật các trường được nhập[cite: 12].
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai4._1
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private TextBox txtMaKhoa, txtMaLop, txtTenLop;
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

                // Tự động tạo bảng Lop
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlCreateTable = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Lop' and xtype='U') 
                        BEGIN 
                            CREATE TABLE Lop (
                                MaLop NVARCHAR(20) PRIMARY KEY, 
                                MaKhoa NVARCHAR(20),
                                TenLop NVARCHAR(100)
                            ); 
                        END";
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
            this.Text = "frmLop";
            this.Size = new Size(360, 220);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblMaKhoa = new Label { Text = "Mã khoa", Location = new Point(30, 25), AutoSize = true };
            txtMaKhoa = new TextBox { Location = new Point(100, 22), Width = 200 };

            Label lblMaLop = new Label { Text = "Mã lớp", Location = new Point(30, 60), AutoSize = true };
            txtMaLop = new TextBox { Location = new Point(100, 57), Width = 200 };

            Label lblTenLop = new Label { Text = "Tên lớp", Location = new Point(30, 95), AutoSize = true };
            txtTenLop = new TextBox { Location = new Point(100, 92), Width = 200 };

            btnThem = new Button { Text = "Thêm", Location = new Point(35, 135), Size = new Size(75, 30) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(130, 135), Size = new Size(75, 30) };
            btnSua = new Button { Text = "Sửa", Location = new Point(225, 135), Size = new Size(75, 30) };

            this.Controls.AddRange(new Control[] { lblMaKhoa, txtMaKhoa, lblMaLop, txtMaLop, lblTenLop, txtTenLop, btnThem, btnXoa, btnSua });

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
        }

        // 1. Chức năng THÊM
        private void BtnThem_Click(object sender, EventArgs e)
        {
            // Mã khoa, Mã lớp, Tên lớp không được để trống[cite: 12]
            if (string.IsNullOrEmpty(txtMaKhoa.Text) || string.IsNullOrEmpty(txtMaLop.Text) || string.IsNullOrEmpty(txtTenLop.Text))
            {
                MessageBox.Show("Mã khoa, Mã lớp, Tên lớp không được để trống!", "Cảnh báo");
                return;
            }

            string query = "INSERT INTO Lop (MaLop, MaKhoa, TenLop) VALUES (@MaLop, @MaKhoa, @TenLop)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaKhoa", txtMaKhoa.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm lớp thành công!");
                    LamMoi();
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Number == 2627 ? "Trùng mã lớp!" : "Lỗi: " + ex.Message); }
        }

        // 2. Chức năng XÓA
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            // Mã lớp không được để trống[cite: 12]
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Mã lớp không được để trống khi thực hiện xóa!", "Cảnh báo");
                return;
            }

            // Mã khoa, Tên lớp không nhập dữ liệu[cite: 12]
            if (!string.IsNullOrEmpty(txtMaKhoa.Text) || !string.IsNullOrEmpty(txtTenLop.Text))
            {
                MessageBox.Show("Mã khoa và Tên lớp không được nhập dữ liệu khi xóa!", "Cảnh báo");
                txtMaKhoa.Clear();
                txtTenLop.Clear();
                return;
            }

            string query = "DELETE FROM Lop WHERE MaLop = @MaLop";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Xóa lớp thành công!" : "Không tìm thấy lớp có mã tương ứng!");
                    LamMoi();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // 3. Chức năng SỬA
        private void BtnSua_Click(object sender, EventArgs e)
        {
            // Mã lớp không để trống[cite: 12]
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Mã lớp không được để trống!", "Cảnh báo");
                return;
            }

            bool hasMaKhoa = !string.IsNullOrEmpty(txtMaKhoa.Text);
            bool hasTenLop = !string.IsNullOrEmpty(txtTenLop.Text);

            // Mã khoa, Tên lớp phải có ít nhất 1 trường có dữ liệu[cite: 12]
            if (!hasMaKhoa && !hasTenLop)
            {
                MessageBox.Show("Mã khoa hoặc Tên lớp phải có ít nhất một trường có dữ liệu để thực hiện sửa!", "Cảnh báo");
                return;
            }

            // Tạo câu truy vấn động dựa vào trường được nhập[cite: 12]
            List<string> updateFields = new List<string>();
            if (hasMaKhoa) updateFields.Add("MaKhoa = @MaKhoa");
            if (hasTenLop) updateFields.Add("TenLop = @TenLop");

            string query = "UPDATE Lop SET " + string.Join(", ", updateFields) + " WHERE MaLop = @MaLop";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                    if (hasMaKhoa) cmd.Parameters.AddWithValue("@MaKhoa", txtMaKhoa.Text.Trim());
                    if (hasTenLop) cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text.Trim());

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Cập nhật thông tin lớp thành công!" : "Không tìm thấy mã lớp này!");
                    LamMoi();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LamMoi()
        {
            txtMaKhoa.Clear();
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtMaKhoa.Focus();
        }
    }
}
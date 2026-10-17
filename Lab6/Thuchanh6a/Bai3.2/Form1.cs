/*
* BÀI TẬP NÂNG CAO - BÀI 2: QUẢN LÝ ĐIỂM
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
*
* Yêu cầu chức năng:
* - Thêm: Nhập Mã sv, Mã mh, Điểm (không được để trống) -> Lưu vào bảng Diem[cite: 11].
* - Xóa: Nhập Mã sv, Mã mh (không trống). Điểm không nhập dữ liệu -> Xóa dòng tương ứng[cite: 11].
* - Sửa: Mã sv, Mã mh không trống. Điểm phải nhập dữ liệu VÀ CHỈ ĐƯỢC NHẬP SỐ -> Cập nhật Điểm[cite: 11].
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai3._2 // Đặt tên có dấu gạch dưới để tránh lỗi CS0115 như bài trước
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private TextBox txtMaSV, txtMaMH, txtDiem;
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

                // Tự động tạo bảng Diem. Sử dụng Khóa chính kép (MaSV, MaMH) để mỗi sinh viên chỉ có 1 điểm cho 1 môn
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlCreateTable = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Diem' and xtype='U') 
                        BEGIN 
                            CREATE TABLE Diem (
                                MaSV NVARCHAR(20), 
                                MaMH NVARCHAR(20),
                                Diem FLOAT,
                                PRIMARY KEY (MaSV, MaMH)
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
            this.Text = "Quản lý Điểm";
            this.Size = new Size(360, 240);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblMaSV = new Label { Text = "Mã sv", Location = new Point(30, 25), AutoSize = true };
            txtMaSV = new TextBox { Location = new Point(100, 22), Width = 200 };

            Label lblMaMH = new Label { Text = "Mã mh", Location = new Point(30, 60), AutoSize = true };
            txtMaMH = new TextBox { Location = new Point(100, 57), Width = 200 };

            Label lblDiem = new Label { Text = "Điểm", Location = new Point(30, 95), AutoSize = true };
            txtDiem = new TextBox { Location = new Point(100, 92), Width = 200 };

            btnThem = new Button { Text = "Thêm", Location = new Point(35, 140), Size = new Size(75, 30) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(130, 140), Size = new Size(75, 30) };
            btnSua = new Button { Text = "Sửa", Location = new Point(225, 140), Size = new Size(75, 30) };

            this.Controls.AddRange(new Control[] { lblMaSV, txtMaSV, lblMaMH, txtMaMH, lblDiem, txtDiem, btnThem, btnXoa, btnSua });

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
        }

        // 1. Chức năng THÊM
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtMaMH.Text) || string.IsNullOrEmpty(txtDiem.Text))
            {
                MessageBox.Show("Mã môn học, Mã sinh viên, Điểm không được để trống!", "Cảnh báo");
                return;
            }

            // Kiểm tra điểm phải là số hợp lệ[cite: 11]
            if (!float.TryParse(txtDiem.Text.Trim(), out float diem))
            {
                MessageBox.Show("Điểm chỉ được nhập số hợp lệ!", "Cảnh báo");
                return;
            }

            string query = "INSERT INTO Diem (MaSV, MaMH, Diem) VALUES (@MaSV, @MaMH, @Diem)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text.Trim());
                    cmd.Parameters.AddWithValue("@Diem", diem);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm điểm thành công!");
                    LamMoi();
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Number == 2627 ? "Sinh viên này đã có điểm môn học này rồi!" : "Lỗi: " + ex.Message); }
        }

        // 2. Chức năng XÓA
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtMaMH.Text))
            {
                MessageBox.Show("Mã sinh viên và Mã môn học không được để trống!", "Cảnh báo");
                return;
            }
            if (!string.IsNullOrEmpty(txtDiem.Text))
            {
                MessageBox.Show("Điểm không được nhập dữ liệu khi thao tác Xóa!", "Cảnh báo");
                txtDiem.Clear();
                return;
            }

            string query = "DELETE FROM Diem WHERE MaSV = @MaSV AND MaMH = @MaMH";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text.Trim());

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Xóa thành công!" : "Không tìm thấy dữ liệu điểm của sinh viên này!");
                    LamMoi();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // 3. Chức năng SỬA
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtMaMH.Text) || string.IsNullOrEmpty(txtDiem.Text))
            {
                MessageBox.Show("Mã sinh viên, Mã môn học, Điểm không được để trống!", "Cảnh báo");
                return;
            }

            // Kiểm tra điểm phải là số hợp lệ[cite: 11]
            if (!float.TryParse(txtDiem.Text.Trim(), out float diem))
            {
                MessageBox.Show("Điểm chỉ được nhập số!", "Cảnh báo");
                return;
            }

            string query = "UPDATE Diem SET Diem = @Diem WHERE MaSV = @MaSV AND MaMH = @MaMH";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text.Trim());
                    cmd.Parameters.AddWithValue("@Diem", diem);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Cập nhật điểm thành công!" : "Không tìm thấy điểm của sinh viên/môn học này để sửa!");
                    LamMoi();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LamMoi()
        {
            txtMaSV.Clear();
            txtMaMH.Clear();
            txtDiem.Clear();
            txtMaSV.Focus();
        }
    }
}
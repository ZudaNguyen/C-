using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai3._1
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private TextBox txtMaLop, txtMaSV, txtHoTen;
        private MaskedTextBox txtNgaySinh;
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
                    string sqlCreateTable = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SinhVien' and xtype='U') 
                        BEGIN 
                            CREATE TABLE SinhVien (
                                MaSV NVARCHAR(20) PRIMARY KEY, 
                                MaLop NVARCHAR(20),
                                HoTen NVARCHAR(100),
                                NgaySinh DATE
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
            this.Text = "Quản lý sinh viên";
            this.Size = new Size(380, 270);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblMaLop = new Label { Text = "Mã lớp", Location = new Point(30, 25), AutoSize = true };
            txtMaLop = new TextBox { Location = new Point(100, 22), Width = 220 };

            Label lblMaSV = new Label { Text = "Mã sv", Location = new Point(30, 60), AutoSize = true };
            txtMaSV = new TextBox { Location = new Point(100, 57), Width = 220 };

            Label lblHoTen = new Label { Text = "Họ tên", Location = new Point(30, 95), AutoSize = true };
            txtHoTen = new TextBox { Location = new Point(100, 92), Width = 220 };

            Label lblNgaySinh = new Label { Text = "Ngày sinh", Location = new Point(30, 130), AutoSize = true };
            txtNgaySinh = new MaskedTextBox { Location = new Point(100, 127), Width = 220, Mask = "00/00/0000" };

            btnThem = new Button { Text = "Thêm", Location = new Point(45, 175), Size = new Size(75, 30) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(140, 175), Size = new Size(75, 30) };
            btnSua = new Button { Text = "Sửa", Location = new Point(235, 175), Size = new Size(75, 30) };

            this.Controls.AddRange(new Control[] {
                lblMaLop, txtMaLop, lblMaSV, txtMaSV, lblHoTen, txtHoTen, lblNgaySinh, txtNgaySinh,
                btnThem, btnXoa, btnSua
            });

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
        }

        private bool IsNgaySinhEmpty()
        {
            return txtNgaySinh.Text.Replace("/", "").Trim().Length == 0;
        }

        private DateTime? ParseNgaySinh()
        {
            if (DateTime.TryParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime date))
                return date;
            return null;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text) || string.IsNullOrEmpty(txtMaSV.Text) ||
                string.IsNullOrEmpty(txtHoTen.Text) || !txtNgaySinh.MaskCompleted)
            {
                MessageBox.Show("Mã lớp, Mã sinh viên, Tên sinh viên, Ngày sinh không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? ngaySinh = ParseNgaySinh();
            if (ngaySinh == null)
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập đúng định dạng dd/MM/yyyy.");
                return;
            }

            string query = "INSERT INTO SinhVien (MaSV, MaLop, HoTen, NgaySinh) VALUES (@MaSV, @MaLop, @HoTen, @NgaySinh)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên thành công!");
                    LamMoi();
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Number == 2627 ? "Trùng mã sinh viên!" : "Lỗi: " + ex.Message); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Mã sinh viên và Tên sinh viên không được để trống để thực hiện xóa!", "Cảnh báo");
                return;
            }

            if (!string.IsNullOrEmpty(txtMaLop.Text) || !IsNgaySinhEmpty())
            {
                MessageBox.Show("Mã lớp và Ngày sinh không được nhập dữ liệu khi xóa!", "Cảnh báo");
                txtMaLop.Clear();
                txtNgaySinh.Clear();
                return;
            }

            string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV AND HoTen = @HoTen";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Xóa sinh viên thành công!" : "Không tìm thấy sinh viên có thông tin khớp với Mã và Tên vừa nhập!");
                    LamMoi();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "Cảnh báo");
                return;
            }

            bool hasMaLop = !string.IsNullOrEmpty(txtMaLop.Text);
            bool hasHoTen = !string.IsNullOrEmpty(txtHoTen.Text);
            bool hasNgaySinh = txtNgaySinh.MaskCompleted;

            if (!hasMaLop && !hasHoTen && !hasNgaySinh)
            {
                MessageBox.Show("Mã lớp, Tên sinh viên, Ngày sinh phải có ít nhất một trường có dữ liệu để sửa!", "Cảnh báo");
                return;
            }

            List<string> updateFields = new List<string>();
            if (hasMaLop) updateFields.Add("MaLop = @MaLop");
            if (hasHoTen) updateFields.Add("HoTen = @HoTen");
            if (hasNgaySinh) updateFields.Add("NgaySinh = @NgaySinh");

            string query = "UPDATE SinhVien SET " + string.Join(", ", updateFields) + " WHERE MaSV = @MaSV";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    if (hasMaLop) cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                    if (hasHoTen) cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());

                    if (hasNgaySinh)
                    {
                        DateTime? ngaySinh = ParseNgaySinh();
                        if (ngaySinh == null)
                        {
                            MessageBox.Show("Ngày sinh không hợp lệ.");
                            return;
                        }
                        cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Value);
                    }

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Cập nhật thông tin thành công!" : "Không tìm thấy mã sinh viên này!");
                    LamMoi();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LamMoi()
        {
            txtMaLop.Clear();
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtNgaySinh.Clear();
            txtMaLop.Focus();
        }
    }
}
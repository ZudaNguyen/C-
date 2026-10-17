/*
* THỰC HÀNH 6B - BÀI 2.2: CẬP NHẬT DỮ LIỆU TRÊN BẢNG ĐIỂM (COMBOBOX)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
*
* Yêu cầu chức năng:
* - Hiển thị mã sinh viên, mã môn học lên combobox.
* - Nhập đủ thông tin, Điểm nằm trong khoảng (0-10)[cite: 13].
* - Thêm: Mã sv, mã mh phải tồn tại (Khóa ngoại), không trùng khóa chính (thông báo nếu trùng)[cite: 13].
* - Xóa/Sửa: Kiểm tra khóa chính/khóa ngoại khi thao tác[cite: 13].
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai2._2
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private ComboBox cboSinhVien, cboMonHoc;
        private TextBox txtDiem;
        private Button btnThem, btnXoa, btnSua;

        public Form1()
        {
            VeGiaoDien();
            TuDongTaoDatabase();
            LoadComboBoxes(); // Hiển thị mã SV và mã MH lên combobox[cite: 13]
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
                    // Đảm bảo có bảng MonHoc và SinhVien với dữ liệu mẫu để đưa lên ComboBox
                    string sqlSetup = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MonHoc' and xtype='U') 
                        BEGIN CREATE TABLE MonHoc (MaMH NVARCHAR(20) PRIMARY KEY, TenMH NVARCHAR(100)); END;
                        
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SinhVien' and xtype='U') 
                        BEGIN CREATE TABLE SinhVien (MaSV NVARCHAR(20) PRIMARY KEY, HoTen NVARCHAR(100)); END;

                        -- Chèn dữ liệu mẫu nếu bảng trống
                        IF NOT EXISTS (SELECT 1 FROM MonHoc)
                        BEGIN 
                            INSERT INTO MonHoc VALUES ('CS01', 'C#'), ('WEB01', 'Web Design');
                        END
                        IF NOT EXISTS (SELECT 1 FROM SinhVien)
                        BEGIN 
                            INSERT INTO SinhVien VALUES ('SV01', 'Nguyen Van A'), ('SV02', 'Tran Thi B');
                        END

                        -- Tạo bảng Điểm với ràng buộc Khóa chính kép và Khóa ngoại[cite: 13]
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Diem' and xtype='U') 
                        BEGIN 
                            CREATE TABLE Diem (
                                MaSV NVARCHAR(20) FOREIGN KEY REFERENCES SinhVien(MaSV), 
                                MaMH NVARCHAR(20) FOREIGN KEY REFERENCES MonHoc(MaMH),
                                Diem FLOAT,
                                PRIMARY KEY (MaSV, MaMH)
                            ); 
                        END";
                    using (SqlCommand cmd = new SqlCommand(sqlSetup, conn)) { cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khởi tạo CSDL: " + ex.Message); }
        }

        private void LoadComboBoxes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Tải danh sách Sinh viên[cite: 13]
                    using (SqlCommand cmd = new SqlCommand("SELECT MaSV FROM SinhVien", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) cboSinhVien.Items.Add(reader["MaSV"].ToString());
                    }

                    // Tải danh sách Môn học[cite: 13]
                    using (SqlCommand cmd = new SqlCommand("SELECT MaMH FROM MonHoc", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) cboMonHoc.Items.Add(reader["MaMH"].ToString());
                    }

                    if (cboSinhVien.Items.Count > 0) cboSinhVien.SelectedIndex = 0;
                    if (cboMonHoc.Items.Count > 0) cboMonHoc.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void VeGiaoDien()
        {
            this.Text = "frmDiem_combo";
            this.Size = new Size(360, 240);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Label lblSinhVien = new Label { Text = "Sinh viên", Location = new Point(30, 25), AutoSize = true };
            cboSinhVien = new ComboBox { Location = new Point(100, 22), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblMonHoc = new Label { Text = "Môn học", Location = new Point(30, 60), AutoSize = true };
            cboMonHoc = new ComboBox { Location = new Point(100, 57), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblDiem = new Label { Text = "Điểm", Location = new Point(30, 95), AutoSize = true };
            txtDiem = new TextBox { Location = new Point(100, 92), Width = 200 };

            btnThem = new Button { Text = "Thêm", Location = new Point(35, 140), Size = new Size(75, 30) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(130, 140), Size = new Size(75, 30) };
            btnSua = new Button { Text = "Sửa", Location = new Point(225, 140), Size = new Size(75, 30) };

            this.Controls.AddRange(new Control[] { lblSinhVien, cboSinhVien, lblMonHoc, cboMonHoc, lblDiem, txtDiem, btnThem, btnXoa, btnSua });

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
        }

        // Validate dữ liệu: Tất cả phải nhập và Điểm từ 0-10[cite: 13]
        private bool ValidateInput(out float diem)
        {
            diem = -1;
            if (cboSinhVien.SelectedItem == null || cboMonHoc.SelectedItem == null || string.IsNullOrEmpty(txtDiem.Text))
            {
                MessageBox.Show("Tất cả thông tin phải được nhập dữ liệu!", "Cảnh báo");
                return false;
            }
            if (!float.TryParse(txtDiem.Text.Trim(), out diem) || diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm phải là số và nằm trong khoảng (0-10)!", "Cảnh báo");
                return false;
            }
            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out float diem)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Diem (MaSV, MaMH, Diem) VALUES (@MaSV, @MaMH, @Diem)", conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", cboSinhVien.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MaMH", cboMonHoc.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Diem", diem);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm điểm thành công!");
                }
            }
            catch (SqlException ex)
            {
                // Kiểm tra trùng Khóa chính và Khóa ngoại[cite: 13]
                if (ex.Number == 2627) MessageBox.Show("Sinh viên này đã có điểm môn học này! (Trùng khóa chính)", "Lỗi");
                else if (ex.Number == 547) MessageBox.Show("Mã SV hoặc Mã MH không tồn tại trong CSDL! (Lỗi khóa ngoại)", "Lỗi");
                else MessageBox.Show("Lỗi CSDL: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (cboSinhVien.SelectedItem == null || cboMonHoc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Sinh viên và Môn học cần xóa!", "Cảnh báo");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Diem WHERE MaSV = @MaSV AND MaMH = @MaMH", conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", cboSinhVien.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MaMH", cboMonHoc.SelectedItem.ToString());

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) MessageBox.Show("Xóa dữ liệu điểm thành công!");
                    else MessageBox.Show("Không tìm thấy dữ liệu điểm này! (Lỗi kiểm tra khóa chính)", "Lỗi");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out float diem)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("UPDATE Diem SET Diem = @Diem WHERE MaSV = @MaSV AND MaMH = @MaMH", conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", cboSinhVien.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MaMH", cboMonHoc.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Diem", diem);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) MessageBox.Show("Cập nhật điểm thành công!");
                    else MessageBox.Show("Không tìm thấy dữ liệu điểm để cập nhật! (Lỗi kiểm tra khóa chính)", "Lỗi");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi CSDL: " + ex.Message); }
        }
    }
}
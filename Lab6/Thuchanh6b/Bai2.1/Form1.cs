/*
* THỰC HÀNH 6B - BÀI 2.1: THIẾT KẾ FORM SINH VIÊN (COMBOBOX)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
*
* Yêu cầu chức năng:
* - Hiển thị mã lớp trên comboBox.
* - Thêm: Mã sv không trùng, nhập đủ dữ liệu, tuổi từ 17 trở lên (tính theo năm).
* - Xóa/Sửa: Kiểm tra khóa chính, khóa ngoại khi thao tác[cite: 12].
*/

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai2._1 // Namespace tương thích để không lỗi Designer
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private ComboBox cboLop;
        private TextBox txtMaSV, txtHoTen;
        private MaskedTextBox txtNgaySinh;
        private Button btnThem, btnXoa, btnSua;

        public Form1()
        {
            VeGiaoDien();
            TuDongTaoDatabase();
            LoadComboBoxLop(); // Tải danh sách lớp vào ComboBox
        }

        private void TuDongTaoDatabase()
        {
            try
            {
                // 1. Tạo DB nếu chưa có
                using (SqlConnection conn = new SqlConnection(masterConn))
                {
                    conn.Open();
                    string sqlCreateDB = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QLSinhVien') BEGIN CREATE DATABASE QLSinhVien; END";
                    using (SqlCommand cmd = new SqlCommand(sqlCreateDB, conn)) { cmd.ExecuteNonQuery(); }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // 2. Tạo bảng Lop và thêm dữ liệu mẫu (để ComboBox có cái hiển thị)
                    string sqlTaoLop = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Lop' and xtype='U') 
                        BEGIN 
                            CREATE TABLE Lop (
                                MaLop NVARCHAR(20) PRIMARY KEY, 
                                MaKhoa NVARCHAR(20),
                                TenLop NVARCHAR(100)
                            );
                            INSERT INTO Lop (MaLop, MaKhoa, TenLop) VALUES ('04CDNTH', 'CNTT', 'CĐ Tin Học 04');
                            INSERT INTO Lop (MaLop, MaKhoa, TenLop) VALUES ('05CDNTH', 'CNTT', 'CĐ Tin Học 05');
                        END";
                    using (SqlCommand cmd = new SqlCommand(sqlTaoLop, conn)) { cmd.ExecuteNonQuery(); }

                    // 3. Tạo bảng SinhVien CÓ RÀNG BUỘC KHÓA NGOẠI (Foreign Key) tham chiếu tới bảng Lop
                    string sqlTaoSV = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SinhVien' and xtype='U') 
                        BEGIN 
                            CREATE TABLE SinhVien (
                                MaSV NVARCHAR(20) PRIMARY KEY, 
                                MaLop NVARCHAR(20) FOREIGN KEY REFERENCES Lop(MaLop),
                                HoTen NVARCHAR(100),
                                NgaySinh DATE
                            ); 
                        END";
                    using (SqlCommand cmd = new SqlCommand(sqlTaoSV, conn)) { cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo CSDL: " + ex.Message);
            }
        }

        private void LoadComboBoxLop()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MaLop FROM Lop", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboLop.Items.Add(reader["MaLop"].ToString());
                    }
                    if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0; // Chọn sẵn dòng đầu tiên
                }
            }
            catch { }
        }

        private void VeGiaoDien()
        {
            this.Text = "frmSinhVien_Combo";
            this.Size = new Size(360, 260);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblLop = new Label { Text = "Lớp", Location = new Point(30, 25), AutoSize = true };
            cboLop = new ComboBox { Location = new Point(100, 22), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblMaSV = new Label { Text = "Mã sv", Location = new Point(30, 60), AutoSize = true };
            txtMaSV = new TextBox { Location = new Point(100, 57), Width = 200 };

            Label lblHoTen = new Label { Text = "Họ tên", Location = new Point(30, 95), AutoSize = true };
            txtHoTen = new TextBox { Location = new Point(100, 92), Width = 200 };

            Label lblNgaySinh = new Label { Text = "Ngày sinh", Location = new Point(30, 130), AutoSize = true };
            txtNgaySinh = new MaskedTextBox { Location = new Point(100, 127), Width = 200, Mask = "00/00/0000" };

            btnThem = new Button { Text = "Thêm", Location = new Point(35, 175), Size = new Size(75, 30) };
            btnXoa = new Button { Text = "Xóa", Location = new Point(130, 175), Size = new Size(75, 30) };
            btnSua = new Button { Text = "Sửa", Location = new Point(225, 175), Size = new Size(75, 30) };

            this.Controls.AddRange(new Control[] { lblLop, cboLop, lblMaSV, txtMaSV, lblHoTen, txtHoTen, lblNgaySinh, txtNgaySinh, btnThem, btnXoa, btnSua });

            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
        }

        private DateTime? ParseNgaySinh()
        {
            if (DateTime.TryParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime date)) return date;
            return null;
        }

        // --- 1. NÚT THÊM ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra: Tất cả thông tin phải được nhập dữ liệu[cite: 12]
            if (cboLop.SelectedItem == null || string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHoTen.Text) || !txtNgaySinh.MaskCompleted)
            {
                MessageBox.Show("Tất cả thông tin phải được nhập dữ liệu!", "Cảnh báo");
                return;
            }

            DateTime? ngaySinh = ParseNgaySinh();
            if (ngaySinh == null)
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập định dạng dd/MM/yyyy.", "Lỗi");
                return;
            }

            // Kiểm tra: Tuổi từ 17 trở lên (chỉ tính theo năm sinh)[cite: 12]
            if (DateTime.Now.Year - ngaySinh.Value.Year < 17)
            {
                MessageBox.Show("Sinh viên phải có tuổi từ 17 trở lên!", "Không hợp lệ");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO SinhVien (MaSV, MaLop, HoTen, NgaySinh) VALUES (@MaSV, @MaLop, @HoTen, @NgaySinh)", conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaLop", cboLop.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên thành công!");
                }
            }
            catch (SqlException ex)
            {
                // Kiểm tra ràng buộc khóa chính/khóa ngoại[cite: 12]
                if (ex.Number == 2627) MessageBox.Show("Mã sinh viên này đã tồn tại! (Vi phạm Khóa chính)", "Lỗi Khóa chính");
                else if (ex.Number == 547) MessageBox.Show("Lớp này không tồn tại trong hệ thống! (Vi phạm Khóa ngoại)", "Lỗi Khóa ngoại");
                else MessageBox.Show("Lỗi CSDL: " + ex.Message);
            }
        }

        // --- 2. NÚT XÓA ---
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sinh viên cần xóa!", "Cảnh báo");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM SinhVien WHERE MaSV = @MaSV", conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) MessageBox.Show("Xóa sinh viên thành công!");
                    else MessageBox.Show("Không tìm thấy Mã sinh viên này để xóa! (Lỗi kiểm tra Khóa chính)", "Lỗi");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) MessageBox.Show("Không thể xóa sinh viên này vì đang có dữ liệu điểm liên quan! (Vi phạm Khóa ngoại)", "Lỗi Khóa ngoại");
                else MessageBox.Show("Lỗi CSDL: " + ex.Message);
            }
        }

        // --- 3. NÚT SỬA ---
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (cboLop.SelectedItem == null || string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHoTen.Text) || !txtNgaySinh.MaskCompleted)
            {
                MessageBox.Show("Vui lòng nhập đủ Mã SV và các thông tin cần sửa!", "Cảnh báo");
                return;
            }

            DateTime? ngaySinh = ParseNgaySinh();
            if (ngaySinh == null) { MessageBox.Show("Ngày sinh không hợp lệ.", "Lỗi"); return; }
            if (DateTime.Now.Year - ngaySinh.Value.Year < 17) { MessageBox.Show("Tuổi không được nhỏ hơn 17!", "Lỗi"); return; }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("UPDATE SinhVien SET MaLop = @MaLop, HoTen = @HoTen, NgaySinh = @NgaySinh WHERE MaSV = @MaSV", conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaLop", cboLop.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Value);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) MessageBox.Show("Cập nhật thông tin thành công!");
                    else MessageBox.Show("Không tìm thấy Mã sinh viên này để cập nhật! (Lỗi kiểm tra Khóa chính)", "Lỗi");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) MessageBox.Show("Lớp được chọn không tồn tại! (Vi phạm Khóa ngoại)", "Lỗi Khóa ngoại");
                else MessageBox.Show("Lỗi CSDL: " + ex.Message);
            }
        }
    }
}
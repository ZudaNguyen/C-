/*
* BÀI TẬP NÂNG CAO - BÀI 1: HIỂN THỊ DANH SÁCH LỚP TRÊN LISTVIEW
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
*
* Yêu cầu chức năng:
* - Khi nhấn button "Hiển thị dữ liệu": thông tin lớp học trong bảng Lop sẽ được lấy ra và hiển thị lên ListView.
* - Khi nhấn button "Thoát": thực hiện chức năng đóng form.
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai3._1
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private Button btnHienThi, btnThoat;
        private ListView lsvLop;

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
                    // Tạo bảng Lop và tự động chèn dữ liệu mẫu y hệt như hình yêu cầu của đề bài[cite: 13]
                    string sqlSetup = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Lop' and xtype='U') 
                        BEGIN 
                            CREATE TABLE Lop (
                                MaLop NVARCHAR(20) PRIMARY KEY, 
                                MaKhoa NVARCHAR(20),
                                TenLop NVARCHAR(100)
                            );
                        END

                        IF NOT EXISTS (SELECT 1 FROM Lop)
                        BEGIN 
                            INSERT INTO Lop (MaLop, MaKhoa, TenLop) VALUES 
                            ('05DHKT2', 'KT', N'Kế toán ngân hàng khóa 05'),
                            ('05DHTH1', 'CNTT', N'Công nghệ phần mềm khóa 05'),
                            ('05DHTH2', 'CNTT', N'Lập trình mạng khóa 05'),
                            ('06DHHH1', 'HH', N'Hóa phân tích khóa 06'),
                            ('06DHKT1', 'KT', N'Thuế doanh nghiệp khóa 06'),
                            ('06DHTH1', 'CNTT', N'Công nghệ phần mềm khóa 06');
                        END";
                    using (SqlCommand cmd = new SqlCommand(sqlSetup, conn)) { cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khởi tạo CSDL: " + ex.Message); }
        }

        private void VeGiaoDien()
        {
            this.Text = "Danh sách Lớp"; // Tiêu đề form[cite: 13]
            this.Size = new Size(650, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            btnHienThi = new Button { Text = "Hiển thị dữ liệu", Location = new Point(30, 20), Size = new Size(100, 30) };

            // Cấu hình ListView giống với giao diện đề bài: Có các dòng kẻ (GridLines), chế độ xem chi tiết (Details)[cite: 13]
            lsvLop = new ListView
            {
                Location = new Point(30, 60),
                Size = new Size(570, 230),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };

            // Khởi tạo 4 cột: STT, Mã lớp, Tên lớp, Mã khoa[cite: 13]
            lsvLop.Columns.Add("STT", 50);
            lsvLop.Columns.Add("Mã lớp", 100);
            lsvLop.Columns.Add("Tên lớp", 250);
            lsvLop.Columns.Add("Mã khoa", 100);

            btnThoat = new Button { Text = "Thoát", Location = new Point(500, 310), Size = new Size(100, 30) };

            this.Controls.AddRange(new Control[] { btnHienThi, lsvLop, btnThoat });

            // Gắn sự kiện cho các nút bấm[cite: 13]
            btnHienThi.Click += BtnHienThi_Click;
            btnThoat.Click += BtnThoat_Click;
        }

        // Chức năng: Lấy thông tin lớp học trong bảng Lop hiển thị lên[cite: 13]
        private void BtnHienThi_Click(object sender, EventArgs e)
        {
            lsvLop.Items.Clear(); // Xóa dữ liệu cũ trước khi tải mới để không bị trùng lặp
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MaLop, TenLop, MaKhoa FROM Lop", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int stt = 1;
                        while (reader.Read())
                        {
                            // Tạo một dòng (Item) cho ListView, cột đầu tiên là STT[cite: 13]
                            ListViewItem item = new ListViewItem(stt.ToString());

                            // Thêm các cột tiếp theo (SubItems)[cite: 13]
                            item.SubItems.Add(reader["MaLop"].ToString());
                            item.SubItems.Add(reader["TenLop"].ToString());
                            item.SubItems.Add(reader["MaKhoa"].ToString());

                            lsvLop.Items.Add(item);
                            stt++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy xuất dữ liệu: " + ex.Message);
            }
        }

        // Chức năng: Đóng form[cite: 13]
        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
/*
* BÀI TẬP VỀ NHÀ - BÀI 1: QUẢN LÝ CỬA HÀNG (QLCuaHang)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
*
* Chức năng hoàn thiện theo đề:
* - CSDL: SANPHAM, TONKHO, HOADON, CHITIET_HOADON (MaSP và MaHD tự động tăng).
* - Form chính (Menu): Có nút Tạo tồn kho, Thêm SP, Bán hàng.
* - Tạo tồn kho: Lấy SL cuối ngày trước làm SL Đầu ngày nay. SL Nhập/Bán = 0.
* - Thêm SP: Thêm vào SANPHAM (lấy tự động MaSP), chèn vào TONKHO ngày hiện tại bằng 0[cite: 15].
* - Bán hàng: Khóa các nút ban đầu. Nhấn Tạo HĐ mới sẽ sinh MaHD[cite: 15]. Kiểm tra tồn kho trước khi bán và Update lại số lượng[cite: 15].
*/

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai4._1
{
    // =========================================================
    // 1. FORM MENU CHÍNH
    // =========================================================
    public partial class Form1 : Form
    {
        public static readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        public static readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLCuaHang;Integrated Security=True;TrustServerCertificate=True;";

        private Button btnTaoTonKho, btnThemSP, btnBanHang;

        public Form1()
        {
            TuDongTaoDatabase();
            VeGiaoDienMenu();
        }

        private void TuDongTaoDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(masterConn))
                {
                    conn.Open();
                    string sqlCreateDB = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QLCuaHang') BEGIN CREATE DATABASE QLCuaHang; END";
                    using (SqlCommand cmd = new SqlCommand(sqlCreateDB, conn)) { cmd.ExecuteNonQuery(); }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Tạo 4 bảng với IDENTITY(1,1) cho MaSP và MaHD[cite: 15]
                    string sqlSetup = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SANPHAM' and xtype='U') 
                        BEGIN CREATE TABLE SANPHAM (MaSP INT IDENTITY(1,1) PRIMARY KEY, TenSP NVARCHAR(100)); END;

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='HOADON' and xtype='U') 
                        BEGIN CREATE TABLE HOADON (MaHD INT IDENTITY(1,1) PRIMARY KEY, NgayTao DATE, TinhTrangThanhToan INT); END;

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TONKHO' and xtype='U') 
                        BEGIN CREATE TABLE TONKHO (MaSP INT FOREIGN KEY REFERENCES SANPHAM(MaSP), NgayTon DATE, SLDau INT, SLNhap INT, SLBan INT, PRIMARY KEY (MaSP, NgayTon)); END;

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CHITIET_HOADON' and xtype='U') 
                        BEGIN CREATE TABLE CHITIET_HOADON (MaHD INT FOREIGN KEY REFERENCES HOADON(MaHD), MaSP INT FOREIGN KEY REFERENCES SANPHAM(MaSP), SoLuong INT, PRIMARY KEY (MaHD, MaSP)); END;
                    ";
                    using (SqlCommand cmd = new SqlCommand(sqlSetup, conn)) { cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khởi tạo DB: " + ex.Message); }
        }

        private void VeGiaoDienMenu()
        {
            this.Text = "Menu Quản Lý Cửa Hàng";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnTaoTonKho = new Button { Text = "Tạo tồn kho (Đầu ngày)", Location = new Point(50, 30), Size = new Size(230, 40) };
            btnThemSP = new Button { Text = "Thêm sản phẩm mới", Location = new Point(50, 80), Size = new Size(230, 40) };
            btnBanHang = new Button { Text = "Bán hàng", Location = new Point(50, 130), Size = new Size(230, 40) };

            this.Controls.AddRange(new Control[] { btnTaoTonKho, btnThemSP, btnBanHang });

            btnTaoTonKho.Click += BtnTaoTonKho_Click;
            btnThemSP.Click += (s, e) => new FormThemSP().ShowDialog();
            btnBanHang.Click += (s, e) => new FormBanHang().ShowDialog();
        }

        // Chức năng: Tạo dữ liệu tồn kho đầu ngày mới[cite: 15]
        private void BtnTaoTonKho_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Lấy tất cả MaSP, tính SLDau mới = SLDau (cũ) + SLNhap (cũ) - SLBan (cũ). Sau đó Insert cho ngày hôm nay[cite: 15]
                    string sql = @"
                        INSERT INTO TONKHO (MaSP, NgayTon, SLDau, SLNhap, SLBan)
                        SELECT MaSP, CAST(GETDATE() AS DATE), 
                               ISNULL((SELECT TOP 1 (SLDau + SLNhap - SLBan) FROM TONKHO tk2 WHERE tk2.MaSP = SANPHAM.MaSP ORDER BY NgayTon DESC), 0), 
                               0, 0
                        FROM SANPHAM
                        WHERE NOT EXISTS (SELECT 1 FROM TONKHO WHERE MaSP = SANPHAM.MaSP AND NgayTon = CAST(GETDATE() AS DATE))
                    ";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(rows > 0 ? $"Đã tạo tồn kho đầu ngày cho {rows} sản phẩm!" : "Tồn kho hôm nay đã được tạo trước đó rồi!");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }

    // =========================================================
    // 2. FORM THÊM SẢN PHẨM MỚI
    // =========================================================
    public class FormThemSP : Form
    {
        private TextBox txtTenSP;
        private Button btnThem, btnThoat;

        public FormThemSP()
        {
            this.Text = "Thêm sản phẩm mới";
            this.Size = new Size(350, 180);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTen = new Label { Text = "Tên sản phẩm:", Location = new Point(20, 35), AutoSize = true };
            txtTenSP = new TextBox { Location = new Point(120, 32), Width = 180 };

            btnThem = new Button { Text = "Thêm", Location = new Point(70, 80), Size = new Size(80, 30) };
            btnThoat = new Button { Text = "Thoát", Location = new Point(170, 80), Size = new Size(80, 30) };

            this.Controls.AddRange(new Control[] { lblTen, txtTenSP, btnThem, btnThoat });

            btnThoat.Click += (s, e) => this.Close();
            btnThem.Click += BtnThem_Click;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSP.Text)) { MessageBox.Show("Nhập tên SP!"); return; }
            try
            {
                using (SqlConnection conn = new SqlConnection(Form1.connectionString))
                {
                    conn.Open();
                    // Insert SP và lấy MaSP vừa sinh bằng SCOPE_IDENTITY()[cite: 15]
                    string insertSP = "INSERT INTO SANPHAM (TenSP) OUTPUT INSERTED.MaSP VALUES (@TenSP)";
                    int newMaSP;
                    using (SqlCommand cmd = new SqlCommand(insertSP, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                        newMaSP = (int)cmd.ExecuteScalar();
                    }

                    // Chèn vào TONKHO ngày hiện tại với SL = 0[cite: 15]
                    string insertTonKho = "INSERT INTO TONKHO (MaSP, NgayTon, SLDau, SLNhap, SLBan) VALUES (@MaSP, CAST(GETDATE() AS DATE), 0, 0, 0)";
                    using (SqlCommand cmdTK = new SqlCommand(insertTonKho, conn))
                    {
                        cmdTK.Parameters.AddWithValue("@MaSP", newMaSP);
                        cmdTK.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Thêm thành công! Mã SP mới là: {newMaSP}");
                    txtTenSP.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }

    // =========================================================
    // 3. FORM BÁN HÀNG
    // =========================================================
    public class FormBanHang : Form
    {
        private TextBox txtMaHD, txtSoLuong;
        private ComboBox cboSanPham;
        private Button btnTaoMoi, btnThem, btnHuy, btnThanhToan;

        public FormBanHang()
        {
            VeGiaoDien();
            LoadCboSanPham();
            KhoaDieuKhien(true); // Ban đầu làm mờ các điều khiển[cite: 15]
        }

        private void VeGiaoDien()
        {
            this.Text = "Bán Hàng";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;

            btnTaoMoi = new Button { Text = "Tạo hóa đơn mới", Location = new Point(20, 20), Size = new Size(150, 30) };

            Label lblMaHD = new Label { Text = "Mã hóa đơn:", Location = new Point(20, 70), AutoSize = true };
            txtMaHD = new TextBox { Location = new Point(110, 67), Width = 100, ReadOnly = true };

            Label lblMaSP = new Label { Text = "Sản phẩm:", Location = new Point(20, 105), AutoSize = true };
            cboSanPham = new ComboBox { Location = new Point(110, 102), Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblSL = new Label { Text = "Số lượng:", Location = new Point(20, 140), AutoSize = true };
            txtSoLuong = new TextBox { Location = new Point(110, 137), Width = 100 };

            btnThem = new Button { Text = "Thêm", Location = new Point(60, 175), Size = new Size(70, 30) };
            btnHuy = new Button { Text = "Hủy", Location = new Point(140, 175), Size = new Size(70, 30) };
            btnThanhToan = new Button { Text = "Thanh toán", Location = new Point(220, 175), Size = new Size(90, 30) };

            this.Controls.AddRange(new Control[] { btnTaoMoi, lblMaHD, txtMaHD, lblMaSP, cboSanPham, lblSL, txtSoLuong, btnThem, btnHuy, btnThanhToan });

            btnTaoMoi.Click += BtnTaoMoi_Click;
            btnThem.Click += BtnThem_Click;
            btnThanhToan.Click += (s, e) => { MessageBox.Show("Đã thanh toán xong!"); KhoaDieuKhien(true); txtMaHD.Clear(); };
            btnHuy.Click += (s, e) => { KhoaDieuKhien(true); txtMaHD.Clear(); };
        }

        private void LoadCboSanPham()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Form1.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MaSP, TenSP FROM SANPHAM", conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        cboSanPham.DataSource = dt;
                        cboSanPham.DisplayMember = "TenSP";
                        cboSanPham.ValueMember = "MaSP";
                    }
                }
            }
            catch { }
        }

        private void KhoaDieuKhien(bool isLocked)
        {
            txtMaHD.Enabled = !isLocked;
            cboSanPham.Enabled = !isLocked;
            txtSoLuong.Enabled = !isLocked;
            btnThem.Enabled = !isLocked;
            btnHuy.Enabled = !isLocked;
            btnThanhToan.Enabled = !isLocked;
        }

        // Tạo hóa đơn mới, sinh mã tự động và mở khóa điều khiển[cite: 15]
        private void BtnTaoMoi_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Form1.connectionString))
                {
                    conn.Open();
                    // Thêm hóa đơn trạng thái 0, lấy mã[cite: 15]
                    string sql = "INSERT INTO HOADON (NgayTao, TinhTrangThanhToan) OUTPUT INSERTED.MaHD VALUES (CAST(GETDATE() AS DATE), 0)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        int maHD = (int)cmd.ExecuteScalar();
                        txtMaHD.Text = maHD.ToString(); // Ghi mã lên textbox[cite: 15]
                        KhoaDieuKhien(false); // Mở khóa[cite: 15]
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tạo HĐ: " + ex.Message); }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSoLuong.Text, out int slMua) || slMua <= 0)
            {
                MessageBox.Show("Số lượng mua phải lớn hơn 0!");
                return;
            }

            int maHD = int.Parse(txtMaHD.Text);
            int maSP = (int)cboSanPham.SelectedValue;

            try
            {
                using (SqlConnection conn = new SqlConnection(Form1.connectionString))
                {
                    conn.Open();
                    // Kiểm tra tồn kho trong bảng TONKHO của ngày hôm nay[cite: 15]
                    string checkTK = "SELECT (SLDau + SLNhap - SLBan) AS TonKho FROM TONKHO WHERE MaSP = @MaSP AND NgayTon = CAST(GETDATE() AS DATE)";
                    using (SqlCommand cmdCheck = new SqlCommand(checkTK, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaSP", maSP);
                        object result = cmdCheck.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Sản phẩm này chưa được tạo kho đầu ngày hôm nay. Hãy ra menu nhấn 'Tạo tồn kho'!");
                            return;
                        }

                        int tonKho = Convert.ToInt32(result);
                        if (slMua > tonKho)
                        {
                            MessageBox.Show($"Không đủ hàng! Tồn kho hiện tại chỉ còn: {tonKho}");
                            return;
                        }
                    }

                    // Chèn vào CHITIET_HOADON[cite: 15]
                    string insertCT = "INSERT INTO CHITIET_HOADON (MaHD, MaSP, SoLuong) VALUES (@MaHD, @MaSP, @SL)";
                    using (SqlCommand cmdIns = new SqlCommand(insertCT, conn))
                    {
                        cmdIns.Parameters.AddWithValue("@MaHD", maHD);
                        cmdIns.Parameters.AddWithValue("@MaSP", maSP);
                        cmdIns.Parameters.AddWithValue("@SL", slMua);
                        cmdIns.ExecuteNonQuery();
                    }

                    // Cập nhật lại SLBan trong TONKHO bằng SqlCommand Update[cite: 15]
                    string updateTK = "UPDATE TONKHO SET SLBan = SLBan + @SL WHERE MaSP = @MaSP AND NgayTon = CAST(GETDATE() AS DATE)";
                    using (SqlCommand cmdUpd = new SqlCommand(updateTK, conn))
                    {
                        cmdUpd.Parameters.AddWithValue("@SL", slMua);
                        cmdUpd.Parameters.AddWithValue("@MaSP", maSP);
                        cmdUpd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thêm vào hóa đơn thành công và đã trừ kho!");
                    txtSoLuong.Clear();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("Sản phẩm này đã có trong hóa đơn. Nếu muốn đổi số lượng hãy viết thêm chức năng Sửa!");
                else MessageBox.Show("Lỗi CSDL: " + ex.Message);
            }
        }
    }
}
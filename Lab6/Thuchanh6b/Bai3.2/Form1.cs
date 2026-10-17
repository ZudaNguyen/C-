using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai3._2
{
    public partial class Form1 : Form
    {
        private readonly string masterConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private ComboBox cboMonHoc;
        private Button btnLoc;
        private ListView lsvDiem;
        private TextBox txtTongDiem;

        public Form1()
        {
            VeGiaoDien();
            TuDongTaoDatabase();

            LoadComboBoxMonHoc();
            HienThiDuLieu("");
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
                    string sqlSetup = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MonHoc' and xtype='U') 
                        BEGIN CREATE TABLE MonHoc (MaMH NVARCHAR(20) PRIMARY KEY, TenMH NVARCHAR(100)); END;

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SinhVien' and xtype='U') 
                        BEGIN CREATE TABLE SinhVien (MaSV NVARCHAR(20) PRIMARY KEY, HoTen NVARCHAR(100), NgaySinh DATE); END;

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Diem' and xtype='U') 
                        BEGIN CREATE TABLE Diem (MaSV NVARCHAR(20), MaMH NVARCHAR(20), Diem FLOAT, PRIMARY KEY (MaSV, MaMH)); END;

                        IF NOT EXISTS (SELECT 1 FROM MonHoc)
                        BEGIN 
                            INSERT INTO MonHoc VALUES ('MH01', N'Toán Cao Cấp'), ('MH02', N'Mạng Máy Tính');
                            INSERT INTO SinhVien VALUES ('SV01', N'Trần Thanh Sơn', '1990-12-10'), ('SV02', N'Nguyễn Đại Nhơn', '1990-03-04');
                            INSERT INTO Diem VALUES ('SV01', 'MH01', 8), ('SV01', 'MH02', 7), ('SV02', 'MH01', 9);
                        END";
                    using (SqlCommand cmd = new SqlCommand(sqlSetup, conn)) { cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi CSDL: " + ex.Message); }
        }

        private void VeGiaoDien()
        {
            this.Text = "Danh sách sinh viên";
            this.Size = new Size(650, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Label lblMonHoc = new Label { Text = "Môn học", Location = new Point(30, 25), AutoSize = true };
            cboMonHoc = new ComboBox { Location = new Point(100, 22), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            btnLoc = new Button { Text = "Lọc dữ liệu", Location = new Point(380, 20), Size = new Size(100, 28) };

            lsvDiem = new ListView
            {
                Location = new Point(30, 65),
                Size = new Size(570, 300),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lsvDiem.Columns.Add("STT", 50);
            lsvDiem.Columns.Add("Tên SV", 150);
            lsvDiem.Columns.Add("Ngày sinh", 100);
            lsvDiem.Columns.Add("Tên môn học", 170);
            lsvDiem.Columns.Add("Điểm", 80);

            Label lblTongDiem = new Label { Text = "Tổng điểm", Location = new Point(350, 385), AutoSize = true };
            txtTongDiem = new TextBox { Location = new Point(430, 382), Width = 100, ReadOnly = true };

            this.Controls.AddRange(new Control[] { lblMonHoc, cboMonHoc, btnLoc, lsvDiem, lblTongDiem, txtTongDiem });

            btnLoc.Click += BtnLoc_Click;
        }

        private void LoadComboBoxMonHoc()
        {
            cboMonHoc.Items.Clear();
            cboMonHoc.Items.Add("");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT MaMH FROM MonHoc", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboMonHoc.Items.Add(reader["MaMH"].ToString());
                        }
                    }
                }
                cboMonHoc.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải môn học: " + ex.Message); }
        }

        private void HienThiDuLieu(string maMH)
        {
            lsvDiem.Items.Clear();
            float tongDiem = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT sv.HoTen, sv.NgaySinh, mh.TenMH, d.Diem 
                        FROM Diem d 
                        JOIN SinhVien sv ON d.MaSV = sv.MaSV 
                        JOIN MonHoc mh ON d.MaMH = mh.MaMH";

                    if (!string.IsNullOrEmpty(maMH))
                    {
                        query += " WHERE d.MaMH = @MaMH";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(maMH))
                        {
                            cmd.Parameters.AddWithValue("@MaMH", maMH);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int stt = 1;
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(stt.ToString());
                                item.SubItems.Add(reader["HoTen"].ToString());

                                DateTime ngaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                                item.SubItems.Add(ngaySinh.ToString("dd/MM/yyyy"));

                                item.SubItems.Add(reader["TenMH"].ToString());

                                float diem = Convert.ToSingle(reader["Diem"]);
                                item.SubItems.Add(diem.ToString());

                                lsvDiem.Items.Add(item);

                                tongDiem += diem;
                                stt++;
                            }
                        }
                    }
                }

                txtTongDiem.Text = tongDiem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu: " + ex.Message);
            }
        }

        private void BtnLoc_Click(object sender, EventArgs e)
        {
            string maDuocChon = cboMonHoc.SelectedItem != null ? cboMonHoc.SelectedItem.ToString() : "";
            HienThiDuLieu(maDuocChon);
        }
    }
}
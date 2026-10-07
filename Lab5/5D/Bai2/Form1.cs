/*
* THIẾT KẾ FORM MAIN VÀ FORM NHẬP HỒ SƠ SINH VIÊN (BÀI 2)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 08/10/2026
*
* Phát biểu đề bài: Thiết kế form main (BTVN_Tuan6) có menustrip. 
* Gắn bài tập vào menu và hiển thị form (frmBaitap1) khi chọn menu tương ứng.
*/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bai2
{
    // =======================================================
    // LỚP 1: FORM MAIN (FORM CHA MDI)
    // =======================================================
    public partial class Form1 : Form
    {
        public Form1()
        {
            VeGiaoDienFormMain();
        }

        private void VeGiaoDienFormMain()
        {
            this.Text = "BTVN_Tuan6";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true; // Cực kỳ quan trọng: Biến Form này thành Form cha

            // Khởi tạo MenuStrip
            MenuStrip msMain = new MenuStrip();

            ToolStripMenuItem menuBai1 = new ToolStripMenuItem("Bài 1");
            ToolStripMenuItem menuBai2 = new ToolStripMenuItem("Bài 2");

            msMain.Items.Add(menuBai1);
            msMain.Items.Add(menuBai2);

            this.MainMenuStrip = msMain;
            this.Controls.Add(msMain);

            // Gắn sự kiện khi click vào menu "Bài 1"
            menuBai1.Click += (s, e) => {
                // Kiểm tra xem form đã mở chưa để tránh mở trùng nhiều cửa sổ
                foreach (Form child in this.MdiChildren)
                {
                    if (child is FormBaiTap1)
                    {
                        child.Activate();
                        return;
                    }
                }

                // Nếu chưa mở thì tạo mới
                FormBaiTap1 frm = new FormBaiTap1();
                frm.MdiParent = this; // Xác định cha của nó là Form1
                frm.Show();
            };
        }
    }

    // =======================================================
    // LỚP 2: FORM BÀI TẬP 1 (FORM CON)
    // =======================================================
    public class FormBaiTap1 : Form
    {
        TextBox txtHoTen, txtMaSV;
        RadioButton radNam, radNu;
        CheckBox chkAnh, chkPhap, chkHoa;
        ComboBox cboDanToc;
        ListView lsvData;
        Button btnThem;

        public FormBaiTap1()
        {
            VeGiaoDienFormCon();
        }

        private void VeGiaoDienFormCon()
        {
            this.Text = "frmBaitap1";
            this.Size = new Size(650, 500);

            // --- 1. Họ và Tên ---
            Label lblHoTen = new Label { Text = "Họ và Tên", Location = new Point(50, 30), AutoSize = true };
            txtHoTen = new TextBox { Location = new Point(150, 27), Width = 250 };

            // --- 2. Mã sinh viên ---
            Label lblMaSV = new Label { Text = "Mã sinh viên", Location = new Point(50, 70), AutoSize = true };
            txtMaSV = new TextBox { Location = new Point(150, 67), Width = 250 };

            // --- 3. Giới tính ---
            Label lblGioiTinh = new Label { Text = "Giới tính", Location = new Point(50, 110), AutoSize = true };
            radNam = new RadioButton { Text = "Nam", Location = new Point(150, 107), AutoSize = true, Checked = true };
            radNu = new RadioButton { Text = "Nữ", Location = new Point(220, 107), AutoSize = true };

            // --- 4. Ngoại ngữ ---
            Label lblNgoaiNgu = new Label { Text = "Ngoại ngữ", Location = new Point(50, 150), AutoSize = true };
            chkAnh = new CheckBox { Text = "Anh", Location = new Point(150, 147), AutoSize = true, Checked = true };
            chkPhap = new CheckBox { Text = "Pháp", Location = new Point(220, 147), AutoSize = true };
            chkHoa = new CheckBox { Text = "Hoa", Location = new Point(290, 147), AutoSize = true };

            // --- 5. Dân tộc ---
            Label lblDanToc = new Label { Text = "Dân tộc", Location = new Point(50, 190), AutoSize = true };
            cboDanToc = new ComboBox { Location = new Point(150, 187), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboDanToc.Items.AddRange(new string[] { "Kinh", "Hoa", "Khmer", "Chăm", "Tày", "Nùng" });
            cboDanToc.SelectedIndex = 0; // Mặc định chọn "Kinh"

            // --- Nút Thêm (Logic xử lý lưu xuống bảng) ---
            btnThem = new Button { Text = "Thêm vào danh sách", Location = new Point(150, 230), Width = 150 };

            // --- Bảng hiển thị (ListView) ---
            lsvData = new ListView { Location = new Point(20, 280), Size = new Size(590, 160) };
            lsvData.View = View.Details;
            lsvData.GridLines = true;
            lsvData.FullRowSelect = true;

            // Khởi tạo các cột cho bảng
            lsvData.Columns.Add("Họ tên", 150);
            lsvData.Columns.Add("Mã sinh viên", 100);
            lsvData.Columns.Add("Giới tính", 70);
            lsvData.Columns.Add("Ngoại ngữ", 150);
            lsvData.Columns.Add("Dân tộc", 100);

            // Gắn tất cả vào Form
            this.Controls.AddRange(new Control[] {
                lblHoTen, txtHoTen, lblMaSV, txtMaSV,
                lblGioiTinh, radNam, radNu,
                lblNgoaiNgu, chkAnh, chkPhap, chkHoa,
                lblDanToc, cboDanToc, btnThem, lsvData
            });

            // Gắn sự kiện khi bấm nút Thêm
            btnThem.Click += BtnThem_Click;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Họ Tên và Mã SV!");
                return;
            }

            // Xử lý Giới tính
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";

            // Xử lý Ngoại ngữ (nối chuỗi nếu chọn nhiều cái)
            List<string> nnList = new List<string>();
            if (chkAnh.Checked) nnList.Add("Anh");
            if (chkPhap.Checked) nnList.Add("Pháp");
            if (chkHoa.Checked) nnList.Add("Hoa");
            string ngoaiNgu = string.Join(", ", nnList);

            // Tạo một dòng mới cho ListView
            ListViewItem item = new ListViewItem(txtHoTen.Text.Trim());
            item.SubItems.Add(txtMaSV.Text.Trim());
            item.SubItems.Add(gioiTinh);
            item.SubItems.Add(ngoaiNgu);
            item.SubItems.Add(cboDanToc.SelectedItem.ToString());

            // Thêm dòng đó vào bảng
            lsvData.Items.Add(item);

            // Reset nhanh để nhập người tiếp theo
            txtHoTen.Clear();
            txtMaSV.Clear();
            txtHoTen.Focus();
        }
    }
}
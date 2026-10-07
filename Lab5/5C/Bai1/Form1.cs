/*
* TỪ ĐIỂN ANH – VIỆT VÀ VIỆT – ANH (BÀI TẬP VỀ NHÀ - BÀI 1)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 07/10/2026
*
* Phát biểu đề bài: Viết chương trình từ điển Anh – Việt và Việt – Anh.
* Yêu cầu: Hỗ trợ tự động dò tìm ký tự khớp gần nhất khi nhập vào combobox; 
* hiển thị nghĩa của từ vào textbox khi nhấn phím Enter hoặc double-click; 
* danh sách các từ được lưu sẵn vào cấu trúc List.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bai1
{
    // Cấu trúc lưu từ vựng
    public class TuMuc
    {
        public string TiengAnh { get; set; } = "";
        public string TiengViet { get; set; } = "";
        public TuMuc(string anh, string viet) { TiengAnh = anh; TiengViet = viet; }
    }

    public partial class Form1 : Form
    {
        private List<TuMuc> dsTuDien = new List<TuMuc>();

        // Khai báo các thành phần giao diện
        TabControl tabControl1;
        ComboBox cboTuAnh, cboTuViet;
        ListBox lstTuAnh, lstTuViet;
        TextBox txtNghiaViet, txtNghiaAnh;
        Button btnThoat;

        public Form1()
        {
            // Bỏ qua InitializeComponent() mặc định, gọi hàm tự vẽ giao diện
            VeGiaoDienBangCode();
            KhoiTaoDuLieu();
            NapDuLieuLenGiaoDien();
        }

        // Tự động sinh giao diện không cần kéo thả
        private void VeGiaoDienBangCode()
        {
            this.Text = "TỪ ĐIỂN ANH VIỆT - VIỆT ANH";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            tabControl1 = new TabControl { Location = new Point(10, 10), Size = new Size(460, 300) };
            TabPage tab1 = new TabPage("ANH - VIỆT");
            TabPage tab2 = new TabPage("VIỆT - ANH");

            // --- Cài đặt Tab 1: ANH - VIỆT ---
            Label lblAnh1 = new Label { Text = "Tiếng Anh", Location = new Point(10, 10), AutoSize = true };
            Label lblViet1 = new Label { Text = "Tiếng Việt", Location = new Point(220, 10), AutoSize = true };
            cboTuAnh = new ComboBox { Location = new Point(10, 30), Width = 190 };
            lstTuAnh = new ListBox { Location = new Point(10, 60), Size = new Size(190, 190) };
            txtNghiaViet = new TextBox { Location = new Point(220, 30), Size = new Size(220, 220), Multiline = true, ReadOnly = true, Font = new Font("Arial", 11) };

            tab1.Controls.AddRange(new Control[] { lblAnh1, lblViet1, cboTuAnh, lstTuAnh, txtNghiaViet });

            // --- Cài đặt Tab 2: VIỆT - ANH ---
            Label lblViet2 = new Label { Text = "Tiếng Việt", Location = new Point(10, 10), AutoSize = true };
            Label lblAnh2 = new Label { Text = "Tiếng Anh", Location = new Point(220, 10), AutoSize = true };
            cboTuViet = new ComboBox { Location = new Point(10, 30), Width = 190 };
            lstTuViet = new ListBox { Location = new Point(10, 60), Size = new Size(190, 190) };
            txtNghiaAnh = new TextBox { Location = new Point(220, 30), Size = new Size(220, 220), Multiline = true, ReadOnly = true, Font = new Font("Arial", 11) };

            tab2.Controls.AddRange(new Control[] { lblViet2, lblAnh2, cboTuViet, lstTuViet, txtNghiaAnh });

            // Thêm tab vào form
            tabControl1.TabPages.Add(tab1);
            tabControl1.TabPages.Add(tab2);

            // Cài đặt nút Thoát
            btnThoat = new Button { Text = "Thoát", Location = new Point(390, 320), Size = new Size(80, 30) };

            this.Controls.Add(tabControl1);
            this.Controls.Add(btnThoat);

            // --- Gắn sự kiện (Event) tự động ---
            cboTuAnh.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { TraTuAnhViet(cboTuAnh.Text); e.Handled = e.SuppressKeyPress = true; } };
            cboTuAnh.SelectedIndexChanged += (s, e) => TraTuAnhViet(cboTuAnh.Text);
            lstTuAnh.DoubleClick += (s, e) => { if (lstTuAnh.SelectedItem != null) TraTuAnhViet(lstTuAnh.SelectedItem.ToString()); };

            cboTuViet.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { TraTuVietAnh(cboTuViet.Text); e.Handled = e.SuppressKeyPress = true; } };
            cboTuViet.SelectedIndexChanged += (s, e) => TraTuVietAnh(cboTuViet.Text);
            lstTuViet.DoubleClick += (s, e) => { if (lstTuViet.SelectedItem != null) TraTuVietAnh(lstTuViet.SelectedItem.ToString()); };

            btnThoat.Click += (s, e) => { if (MessageBox.Show("Thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) Application.Exit(); };
        }

        private void KhoiTaoDuLieu()
        {
            dsTuDien = new List<TuMuc>()
            {
                new TuMuc("student", "Sinh viên, học sinh"),
                new TuMuc("teacher", "Giáo viên, giảng viên"),
                new TuMuc("worker", "Công nhân, người lao động"),
                new TuMuc("hat", "Cái mũ, cái nón"),
                new TuMuc("mouse", "Con chuột"),
                new TuMuc("cat", "Con mèo"),
                new TuMuc("dog", "Con chó")
            };
        }

        private void NapDuLieuLenGiaoDien()
        {
            var dsAnh = dsTuDien.OrderBy(x => x.TiengAnh).Select(x => x.TiengAnh).ToArray();
            cboTuAnh.Items.AddRange(dsAnh);
            lstTuAnh.Items.AddRange(dsAnh);
            cboTuAnh.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTuAnh.AutoCompleteSource = AutoCompleteSource.ListItems;

            var dsViet = dsTuDien.OrderBy(x => x.TiengViet).Select(x => x.TiengViet).ToArray();
            cboTuViet.Items.AddRange(dsViet);
            lstTuViet.Items.AddRange(dsViet);
            cboTuViet.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTuViet.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void TraTuAnhViet(string tu)
        {
            var kq = dsTuDien.FirstOrDefault(t => t.TiengAnh.Equals(tu.Trim(), StringComparison.OrdinalIgnoreCase));
            txtNghiaViet.Text = kq != null ? kq.TiengViet : "Không tìm thấy!";
            if (kq != null) lstTuAnh.SelectedItem = kq.TiengAnh;
        }

        private void TraTuVietAnh(string tu)
        {
            var kq = dsTuDien.FirstOrDefault(t => t.TiengViet.Equals(tu.Trim(), StringComparison.OrdinalIgnoreCase));
            txtNghiaAnh.Text = kq != null ? kq.TiengAnh : "Không tìm thấy!";
            if (kq != null) lstTuViet.SelectedItem = kq.TiengViet;
        }
    }
}
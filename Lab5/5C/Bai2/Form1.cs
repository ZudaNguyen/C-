/*
* XỬ LÝ LISTBOX (BÀI TẬP VỀ NHÀ - BÀI 2)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 08/10/2026
*
* Phát biểu đề bài: Viết chương trình thêm các số tự nhiên N từ Textbox vào ListBox. 
* Thực hiện các chức năng: Tính tổng, Xóa đầu/cuối, Xóa phần tử đang chọn, 
* Tăng mỗi phần tử lên 2, Thay bằng bình phương, Chọn số chẵn, Chọn số lẻ.
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai2
{
    public partial class Form1 : Form
    {
        // Khai báo các điều khiển
        TextBox txtNhap;
        Button btnNhap, btnTong, btnXoaDauCuoi, btnXoaDangChon;
        Button btnTang2, btnBinhPhuong, btnChonChan, btnChonLe, btnKetThuc;
        ListBox lstSo;

        public Form1()
        {
            VeGiaoDienBangCode();
        }

        // Tự động sinh giao diện
        private void VeGiaoDienBangCode()
        {
            this.Text = "frmListbox";
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tiêu đề chính
            Label lblTitle = new Label { Text = "LISTBOX", Font = new Font("Arial", 16, FontStyle.Bold), ForeColor = Color.DarkOrange, AutoSize = true, Location = new Point(130, 10) };
            this.Controls.Add(lblTitle);

            // GroupBox bên trái: Nhập dữ liệu
            GroupBox grbLeft = new GroupBox { Text = "Listbox", Location = new Point(10, 40), Size = new Size(150, 300) };
            txtNhap = new TextBox { Location = new Point(10, 20), Width = 130 };
            btnNhap = new Button { Text = "Nhập", Location = new Point(10, 50), Width = 130 };

            // Yêu cầu: Listbox được chọn nhiều phần tử (MultiSimple)
            lstSo = new ListBox { Location = new Point(10, 80), Size = new Size(130, 200), SelectionMode = SelectionMode.MultiSimple };
            grbLeft.Controls.AddRange(new Control[] { txtNhap, btnNhap, lstSo });
            this.Controls.Add(grbLeft);

            // GroupBox bên phải: Xử lý
            GroupBox grbRight = new GroupBox { Text = "Xử lý Listbox", Location = new Point(170, 40), Size = new Size(200, 300) };
            btnTong = new Button { Text = "Tổng các phần tử trong List", Location = new Point(10, 20), Size = new Size(180, 30) };
            btnXoaDauCuoi = new Button { Text = "Xóa Phần tử đầu và cuối", Location = new Point(10, 60), Size = new Size(180, 30) };
            btnXoaDangChon = new Button { Text = "Xóa Phần tử đang chọn", Location = new Point(10, 100), Size = new Size(180, 30) };
            btnTang2 = new Button { Text = "Tăng mỗi phần tử lên 2", Location = new Point(10, 140), Size = new Size(180, 30) };
            btnBinhPhuong = new Button { Text = "Thay bằng bình phương", Location = new Point(10, 180), Size = new Size(180, 30) };
            btnChonChan = new Button { Text = "Chọn số chẵn", Location = new Point(10, 220), Size = new Size(180, 30) };
            btnChonLe = new Button { Text = "Chọn số lẻ", Location = new Point(10, 260), Size = new Size(180, 30) };
            grbRight.Controls.AddRange(new Control[] { btnTong, btnXoaDauCuoi, btnXoaDangChon, btnTang2, btnBinhPhuong, btnChonChan, btnChonLe });
            this.Controls.Add(grbRight);

            // Nút Kết thúc
            btnKetThuc = new Button { Text = "KẾT THÚC", Location = new Point(10, 360), Size = new Size(360, 35) };
            this.Controls.Add(btnKetThuc);

            // --- GẮN SỰ KIỆN CHO CÁC NÚT ---

            // Nút Nhập
            btnNhap.Click += (s, e) => {
                if (int.TryParse(txtNhap.Text, out int so))
                {
                    lstSo.Items.Add(so);
                    txtNhap.Clear();
                    txtNhap.Focus();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập một số tự nhiên hợp lệ!");
                }
            };

            // 1. Tính tổng các phần tử
            btnTong.Click += (s, e) => {
                int sum = 0;
                foreach (var item in lstSo.Items)
                {
                    sum += Convert.ToInt32(item);
                }
                MessageBox.Show($"Tổng các phần tử trong Listbox là: {sum}", "Thông báo");
            };

            // 2. Xóa phần tử đầu và cuối
            btnXoaDauCuoi.Click += (s, e) => {
                if (lstSo.Items.Count > 0)
                {
                    lstSo.Items.RemoveAt(0); // Xóa đầu
                    if (lstSo.Items.Count > 0)
                    {
                        lstSo.Items.RemoveAt(lstSo.Items.Count - 1); // Xóa cuối
                    }
                }
            };

            // 3. Xóa các phần tử đang chọn
            btnXoaDangChon.Click += (s, e) => {
                // Phải vòng lặp ngược để không bị lỗi tụt index khi xóa
                for (int i = lstSo.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    lstSo.Items.RemoveAt(lstSo.SelectedIndices[i]);
                }
            };

            // 4. Tăng giá trị mỗi phần tử lên 2
            btnTang2.Click += (s, e) => {
                for (int i = 0; i < lstSo.Items.Count; i++)
                {
                    int val = Convert.ToInt32(lstSo.Items[i]);
                    lstSo.Items[i] = val + 2;
                }
            };

            // 5. Thay mỗi giá trị bằng bình phương của nó
            btnBinhPhuong.Click += (s, e) => {
                for (int i = 0; i < lstSo.Items.Count; i++)
                {
                    int val = Convert.ToInt32(lstSo.Items[i]);
                    lstSo.Items[i] = val * val;
                }
            };

            // 6. Chọn các số chẵn
            btnChonChan.Click += (s, e) => {
                lstSo.ClearSelected(); // Xóa các lựa chọn cũ
                for (int i = 0; i < lstSo.Items.Count; i++)
                {
                    if (Convert.ToInt32(lstSo.Items[i]) % 2 == 0)
                    {
                        lstSo.SetSelected(i, true);
                    }
                }
            };

            // 7. Chọn các số lẻ
            btnChonLe.Click += (s, e) => {
                lstSo.ClearSelected(); // Xóa các lựa chọn cũ
                for (int i = 0; i < lstSo.Items.Count; i++)
                {
                    if (Convert.ToInt32(lstSo.Items[i]) % 2 != 0)
                    {
                        lstSo.SetSelected(i, true);
                    }
                }
            };

            // 8. KẾT THÚC
            btnKetThuc.Click += (s, e) => {
                Application.Exit();
            };
        }
    }
}
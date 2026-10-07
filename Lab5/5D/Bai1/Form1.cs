/*
* ĐẾM NGƯỢC THỜI GIAN (BÀI TẬP VỀ NHÀ - BÀI 1 - LAB 5D)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 07/10/2026
*
* Phát biểu đề bài: Thiết kế form có giao diện thực hiện đếm ngược thời gian.
* Yêu cầu: Giao diện gồm nhãn hiển thị thời gian "30:00" và nút "Bắt đầu". 
* Sử dụng Timer để giảm thời gian mỗi giây.
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai1
{
    public partial class Form1 : Form
    {
        Label lblTime;
        Button btnAction;
        System.Windows.Forms.Timer timer;

        // 30 phút = 1800 giây
        int totalSeconds = 30 * 60;

        public Form1()
        {
            VeGiaoDienBangCode();
        }

        private void VeGiaoDienBangCode()
        {
            // Cài đặt Form chính
            this.Text = "Đồng hồ";
            this.Size = new Size(250, 150);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Nhãn hiển thị thời gian
            lblTime = new Label
            {
                Text = "30:00",
                Font = new Font("Consolas", 24, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 40),
                Location = new Point(15, 10)
            };

            // Nút Bắt đầu
            btnAction = new Button
            {
                Text = "Bắt đầu",
                Size = new Size(80, 30),
                Location = new Point(75, 60),
                Cursor = Cursors.Hand
            };

            this.Controls.Add(lblTime);
            this.Controls.Add(btnAction);

            // Khởi tạo công cụ đếm thời gian (Timer)
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Mỗi 1000ms (1 giây) sẽ nhảy 1 lần
            timer.Tick += Timer_Tick; // Gắn sự kiện mỗi khi nhảy

            // Gắn sự kiện click cho nút bấm
            btnAction.Click += BtnAction_Click;
        }

        // Xử lý khi bấm nút Bắt đầu / Tạm dừng
        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
                btnAction.Text = "Tiếp tục";
            }
            else
            {
                timer.Start();
                btnAction.Text = "Tạm dừng";
            }
        }

        // Xử lý logic đếm ngược mỗi giây
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (totalSeconds > 0)
            {
                totalSeconds--;

                // Quy đổi tổng số giây ra phút và giây
                int minutes = totalSeconds / 60;
                int seconds = totalSeconds % 60;

                // Hiển thị định dạng MM:SS (ví dụ 09:05)
                lblTime.Text = $"{minutes:D2}:{seconds:D2}";
            }
            else
            {
                // Khi đếm về 0
                timer.Stop();
                MessageBox.Show("Đã hết thời gian!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Khôi phục lại trạng thái ban đầu
                btnAction.Text = "Bắt đầu";
                totalSeconds = 30 * 60;
                lblTime.Text = "30:00";
            }
        }
    }
}
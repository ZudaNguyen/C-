/*
* THIẾT KẾ GIAO DIỆN NHẬP DANH BẠ (BÀI TẬP VỀ NHÀ - BÀI 3)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 08/10/2026
*
* Phát biểu đề bài: Thiết kế giao diện nhập danh bạ dùng TreeView.
* Yêu cầu: Khi form load, TreeView chứa các node từ A-Z. Khi thêm một người 
* mới, chương trình tự động đưa vào TreeView ở vị trí node chữ cái đầu của tên.
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Form1 : Form
    {
        // Khai báo các điều khiển
        TreeView tvDanhBa;
        TextBox txtFirstName, txtLastName;
        Button btnAdd, btnExit;

        public Form1()
        {
            VeGiaoDienBangCode();
            LoadTreeViewAZ();
        }

        // Tự động sinh giao diện
        private void VeGiaoDienBangCode()
        {
            this.Text = "frmBTVN2";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // TreeView bên trái
            tvDanhBa = new TreeView { Location = new Point(10, 10), Size = new Size(200, 340) };
            this.Controls.Add(tvDanhBa);

            // GroupBox/Panel chứa các ô nhập liệu bên phải
            Panel pnlRight = new Panel { Location = new Point(220, 10), Size = new Size(250, 150), BorderStyle = BorderStyle.FixedSingle };

            Label lblFirst = new Label { Text = "First Name", Location = new Point(10, 20), AutoSize = true };
            txtFirstName = new TextBox { Location = new Point(80, 17), Width = 150 };

            Label lblLast = new Label { Text = "Last Name", Location = new Point(10, 60), AutoSize = true };
            txtLastName = new TextBox { Location = new Point(80, 57), Width = 150 };

            btnAdd = new Button { Text = "Add", Location = new Point(155, 100), Width = 75 };

            pnlRight.Controls.AddRange(new Control[] { lblFirst, txtFirstName, lblLast, txtLastName, btnAdd });
            this.Controls.Add(pnlRight);

            // Nút Exit
            btnExit = new Button { Text = "Exit", Location = new Point(395, 180), Width = 75 };
            this.Controls.Add(btnExit);

            // --- GẮN SỰ KIỆN ---

            // Nút Add
            btnAdd.Click += (s, e) => {
                string fName = txtFirstName.Text.Trim();
                string lName = txtLastName.Text.Trim();

                if (string.IsNullOrEmpty(fName))
                {
                    MessageBox.Show("Vui lòng nhập First Name!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                    return;
                }

                // Lấy chữ cái đầu tiên của First Name và chuyển thành in hoa
                string chuCaiDau = fName.Substring(0, 1).ToUpper();

                // Tìm Node cha tương ứng từ A-Z
                TreeNode[] foundNodes = tvDanhBa.Nodes.Find(chuCaiDau, false);

                if (foundNodes.Length > 0)
                {
                    TreeNode nodeCha = foundNodes[0];
                    // Thêm node con với định dạng: FirstName, LastName
                    nodeCha.Nodes.Add($"{fName}, {lName}");
                    nodeCha.Expand(); // Tự động mở rộng nhánh đó ra để xem
                }

                // Xóa trắng ô nhập liệu để nhập tiếp
                txtFirstName.Clear();
                txtLastName.Clear();
                txtFirstName.Focus();
            };

            // Nút Exit
            btnExit.Click += (s, e) => {
                Application.Exit();
            };
        }

        // Hàm nạp các ký tự A -> Z vào TreeView
        private void LoadTreeViewAZ()
        {
            tvDanhBa.Nodes.Clear();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                // Thêm Node với Name và Text đều là ký tự A, B, C...
                tvDanhBa.Nodes.Add(c.ToString(), c.ToString());
            }
        }
    }
}
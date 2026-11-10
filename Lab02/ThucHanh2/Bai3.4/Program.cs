/*
* CHƯƠNG TRÌNH CONSOLE MENU VÀ GIẢI PT BẬC 2 (EVENT, KẾ THỪA)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 24/09/2026
*
* Phát biểu đề bài: Xây dựng lớp ConsoleMenu tổng quát có chức năng hiển thị menu và kích hoạt sự kiện. Áp dụng cho bài toán giải phương trình bậc 2, với thư viện hỗ trợ mở rộng qua sự kiện, kế thừa[cite: 17].
*/

using System;

namespace Bai3_4
{
    // 1. Khai báo delegate cho sự kiện chọn Menu
    public delegate void MenuHandler(int luaChon);

    // 2. Lớp cơ sở tổng quát (ConsoleMenu)
    class ConsoleMenu
    {
        // Khai báo Event
        public event MenuHandler Choose;

        // Phương thức hiển thị ảo (virtual) để lớp con có thể ghi đè thay đổi text
        public virtual void HienThi()
        {
            Console.WriteLine("\nMenu");
            Console.WriteLine("1. Chuc nang 1");
            Console.WriteLine("2. Chuc nang 2");
            Console.WriteLine("0. Thoat chuong trinh");
        }

        // Vòng lặp chạy Menu
        public void Run()
        {
            int x = -1;
            do
            {
                HienThi();
                Console.Write("Thuc hien: ");
                if (int.TryParse(Console.ReadLine(), out x))
                {
                    Console.WriteLine($"\nBan thuc hien chuc nang {x}");
                    Console.WriteLine("----------------------------------");

                    // Kích hoạt sự kiện Choose (Nếu có hàm nào đăng ký += thì hàm đó sẽ chạy)
                    Choose?.Invoke(x);
                }
            } while (x != 0);
        }
    }

    // 3. Lớp Kế thừa ứng dụng Giải PT Bậc 2
    // Cấu trúc class PTBac2Console: ConsoleMenu giống yêu cầu đề bài
    class PTBac2Console : ConsoleMenu
    {
        private double a, b, c;

        // Ghi đè phương thức hiển thị menu cho phù hợp với bài toán
        public override void HienThi()
        {
            Console.WriteLine("\n============= MENU PT BAC 2 =============");
            Console.WriteLine("1. Nhap he so a, b, c");
            Console.WriteLine("2. Giai phuong trinh");
            Console.WriteLine("0. Thoat chuong trinh");
        }

        // Logic xử lý khi Event Choose được gọi
        public void XuLyLuaChon(int luaChon)
        {
            switch (luaChon)
            {
                case 1:
                    Console.Write("Nhap a: "); a = double.Parse(Console.ReadLine());
                    Console.Write("Nhap b: "); b = double.Parse(Console.ReadLine());
                    Console.Write("Nhap c: "); c = double.Parse(Console.ReadLine());
                    break;

                case 2:
                    if (a == 0)
                    {
                        Console.WriteLine("Day khong phai PT bac 2 (a phai khac 0).");
                        break;
                    }
                    double delta = b * b - 4 * a * c;
                    if (delta < 0)
                        Console.WriteLine("Phuong trinh vo nghiem.");
                    else if (delta == 0)
                        Console.WriteLine($"Phuong trinh co nghiem kep x = {-b / (2 * a)}");
                    else
                    {
                        double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                        double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                        Console.WriteLine($"PT co 2 nghiem phan biet: x1 = {x1}, x2 = {x2}");
                    }
                    break;

                case 0:
                    Console.WriteLine("Tam biet!");
                    break;

                default:
                    Console.WriteLine("Chuc nang khong hop le.");
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Cấu trúc app.Choose += ... giống yêu cầu đề bài
            PTBac2Console app = new PTBac2Console();

            // Đăng ký (Subscribe) hàm XuLyLuaChon vào sự kiện Choose của Menu
            app.Choose += app.XuLyLuaChon;

            // Chạy chương trình
            app.Run();
        }
    }
}
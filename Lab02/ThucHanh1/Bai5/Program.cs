/*
* CHƯƠNG TRÌNH MENU TÍNH TOÁN SỐ THỰC
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: In menu chức năng ra màn hình và xử lý các lựa chọn: nhập 2 số thực, tính x^y, tính căn bậc 2 của x và y, thoát. Áp dụng truyền tham số (tham trị, tham chiếu ref/out).
* Ý tưởng: 
*   - Sử dụng vòng lặp do-while để giữ menu hiển thị liên tục cho đến khi chọn thoát (chức năng 4).
*   - Dùng cấu trúc switch-case để điều hướng các lựa chọn 1, 2, 3, 4.
*   - Viết các phương thức riêng: phương thức nhập x, y dùng từ khóa `out` để trả về cả 2 giá trị sau khi nhập; phương thức tính toán dùng `Math.Pow` và `Math.Sqrt`.
* Mã giả:
*   LẶP:
*       In MENU và các chức năng (1: Nhập, 2: Tính x^y, 3: Căn bậc 2, 4: Thoát)
*       Đọc chon_chuc_nang
*       NẾU chon == 1: Gọi hàm NhapHaiSo(out x, out y)
*       NẾU chon == 2: In kết quả x^y
*       NẾU chon == 3: In căn bậc 2 của x và y (xét điều kiện >= 0)
*       NẾU chon == 4: Kết thúc vòng lặp
*   KHI chon != 4
*/

using System;

namespace Bai5
{
    class Program
    {
        // Phương thức nhập 2 số thực sử dụng tham số out để truyền giá trị ra ngoài
        static void NhapHaiSo(out double x, out double y)
        {
            Console.Write("Nhap so thuc x: ");
            while (!double.TryParse(Console.ReadLine(), out x))
            {
                Console.Write("Gia tri khong hop le! Vui long nhap lai x: ");
            }

            Console.Write("Nhap so thuc y: ");
            while (!double.TryParse(Console.ReadLine(), out y))
            {
                Console.Write("Gia tri khong hop le! Vui long nhap lai y: ");
            }

            Console.WriteLine("Da luu gia tri x va y thanh cong!");
        }

        // Phương thức tính x^y
        static void TinhLuyThua(double x, double y)
        {
            double ketQua = Math.Pow(x, y);
            Console.WriteLine($"Ket qua {x}^{y} = {ketQua}");
        }

        // Phương thức tính căn bậc 2 của x và y
        static void TinhCanBacHai(double x, double y)
        {
            if (x >= 0)
            {
                Console.WriteLine($"Can bac 2 cua x ({x}) la: {Math.Sqrt(x)}");
            }
            else
            {
                Console.WriteLine($"x = {x} < 0, khong the tinh can bac 2 trong tap so thuc!");
            }

            if (y >= 0)
            {
                Console.WriteLine($"Can bac 2 cua y ({y}) la: {Math.Sqrt(y)}");
            }
            else
            {
                Console.WriteLine($"y = {y} < 0, khong the tinh can bac 2 trong tap so thuc!");
            }
        }

        static void Main(string[] args)
        {
            double x = 0;
            double y = 0;
            bool daNhap = false; // Cờ kiểm tra xem người dùng đã thực hiện chức năng 1 chưa
            int luaChon = 0;

            do
            {
                Console.WriteLine("\n================== MENU ==================");
                Console.WriteLine("1. Nhap hai gia tri so thuc cho x, y");
                Console.WriteLine("2. Tinh x^y");
                Console.WriteLine("3. Tinh can bac 2 cua x va y");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");

                // Đọc lựa chọn của người dùng
                if (!int.TryParse(Console.ReadLine(), out luaChon))
                {
                    Console.WriteLine("Lua chon phai la so nguyen tu 1 den 4!");
                    continue;
                }

                Console.WriteLine("------------------------------------------");
                switch (luaChon)
                {
                    case 1:
                        NhapHaiSo(out x, out y);
                        daNhap = true;
                        break;

                    case 2:
                        if (!daNhap)
                        {
                            Console.WriteLine("Vui long thuc hien chuc nang 1 (Nhap hai so) truoc!");
                        }
                        else
                        {
                            TinhLuyThua(x, y);
                        }
                        break;

                    case 3:
                        if (!daNhap)
                        {
                            Console.WriteLine("Vui long thuc hien chuc nang 1 (Nhap hai so) truoc!");
                        }
                        else
                        {
                            TinhCanBacHai(x, y);
                        }
                        break;

                    case 4:
                        Console.WriteLine("Dang thoat chuong trinh...");
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le! Vui long chon tu 1 den 4.");
                        break;
                }

            } while (luaChon != 4);
        }
    }
}
/*
* CHƯƠNG TRÌNH NHẬP SỐ NGUYÊN VÀ TÍNH LŨY THỪA
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Viết chương trình nhập hai số nguyên x, y. Tính x^y và xuất kết quả theo định dạng[cite: 5].
* Ý tưởng: Đọc hai giá trị từ bàn phím, sử dụng int.Parse() để ép kiểu dữ liệu từ chuỗi sang số nguyên. Sử dụng hàm Math.Pow(x, y) trong thư viện toán học để tính x^y, sau đó xuất kết quả.
* Mã giả:
*   In "Nhap so nguyen x: "
*   x = Đọc và ép kiểu số nguyên
*   In "Nhap so nguyen y: "
*   y = Đọc và ép kiểu số nguyên
*   ketQua = x^y
*   In "Ket qua " + x + " mu " + y + " la: " + ketQua
*/

using System;

namespace Bai3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Nhập số nguyên x
            Console.Write("Nhap so nguyen x: ");
            int x = int.Parse(Console.ReadLine());

            // Nhập số nguyên y
            Console.Write("Nhap so nguyen y: ");
            int y = int.Parse(Console.ReadLine());

            // Tính x lũy thừa y (Hàm Math.Pow trả về kiểu double)
            double ketQua = Math.Pow(x, y);

            // Xuất kết quả ra màn hình console theo định dạng yêu cầu
            Console.WriteLine($"Ket qua {x} mu {y} la: {ketQua}");

            // Dừng màn hình console lại để xem kết quả
            Console.ReadLine();
        }
    }
}
/*
* CHƯƠNG TRÌNH SẮP XẾP MẢNG HỌ TÊN
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 21/09/2026
*
* Phát biểu đề bài: Nhập vào một mảng họ tên của n người. Hãy sắp xếp mảng đó theo thứ tự tăng dần[cite: 10].
* Ý tưởng: Khai báo một mảng chuỗi (string array). Sử dụng phương thức có sẵn Array.Sort() để sắp xếp mảng chuỗi theo thứ tự từ điển (A-Z).
*/

using System;

namespace Bai16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so luong nguoi (n): ");
            int n = int.Parse(Console.ReadLine());

            string[] danhSachHoTen = new string[n];

            // Nhập danh sách họ tên
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Nhap ho ten nguoi thu {i + 1}: ");
                danhSachHoTen[i] = Console.ReadLine();
            }

            // Sắp xếp mảng tăng dần (theo thứ tự Alphabet A-Z)
            Array.Sort(danhSachHoTen);

            // In kết quả
            Console.WriteLine("\n--- DANH SACH SAU KHI SAP XEP TANG DAN ---");
            foreach (string ten in danhSachHoTen)
            {
                Console.WriteLine(ten);
            }

            Console.ReadLine();
        }
    }
}
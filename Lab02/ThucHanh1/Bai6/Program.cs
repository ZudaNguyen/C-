/*
* CHƯƠNG TRÌNH TÌM GIÁ TRỊ LỚN NHẤT CỦA BA SỐ NGUYÊN
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Xây dựng lớp có phương thức tìm giá trị lớn nhất của ba số nguyên[cite: 8].
* Ý tưởng: Tạo lớp XuLyToanHoc chứa phương thức TimMax. Dùng hàm Math.Max() lồng nhau để tìm số lớn nhất và return kết quả.
*/

using System;

namespace Bai6
{
    // Xây dựng lớp theo yêu cầu
    class XuLyToanHoc
    {
        // Phương thức trả về giá trị (return)
        public int TimMax(int a, int b, int c)
        {
            return Math.Max(a, Math.Max(b, c));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so nguyen thu nhat: ");
            int so1 = int.Parse(Console.ReadLine());
            Console.Write("Nhap so nguyen thu hai: ");
            int so2 = int.Parse(Console.ReadLine());
            Console.Write("Nhap so nguyen thu ba: ");
            int so3 = int.Parse(Console.ReadLine());

            // Khởi tạo đối tượng từ lớp đã xây dựng
            XuLyToanHoc xuLy = new XuLyToanHoc();

            // Gọi phương thức
            int max = xuLy.TimMax(so1, so2, so3);

            Console.WriteLine($"Gia tri lon nhat la: {max}");
            Console.ReadLine();
        }
    }
}
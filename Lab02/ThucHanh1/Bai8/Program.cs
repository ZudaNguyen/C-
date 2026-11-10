/*
* CHƯƠNG TRÌNH HOÁN VỊ HAI SỐ THỰC
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 21/09/2026
*
* Phát biểu đề bài: Xây dựng lớp có phương thức hoán vị hai số thực[cite: 8].
* Ý tưởng: Dùng từ khóa ref để truyền tham chiếu. Việc thay đổi giá trị của biến bên trong phương thức sẽ làm thay đổi trực tiếp biến ở hàm Main.
*/

using System;

namespace Bai8
{
    class TienIch
    {
        // Phương thức sử dụng tham chiếu ref
        public void HoanVi(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so thuc thu nhat (a): ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Nhap so thuc thu hai (b): ");
            double b = double.Parse(Console.ReadLine());

            Console.WriteLine($"Truoc khi hoan vi: a = {a}, b = {b}");

            TienIch tienIch = new TienIch();
            // Truyền tham chiếu ref khi gọi hàm
            tienIch.HoanVi(ref a, ref b);

            Console.WriteLine($"Sau khi hoan vi: a = {a}, b = {b}");
            Console.ReadLine();
        }
    }
}
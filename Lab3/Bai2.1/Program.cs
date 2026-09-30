/*
* CHƯƠNG TRÌNH TRUY VẤN LINQ TRÊN MẢNG SỐ NGUYÊN (BÀI 2.1)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 30/09/2026
*
* Phát biểu đề bài: Cho mảng số nguyên. Thực hiện lọc các phần tử chia hết cho 4 và 3, nhỏ hơn hoặc bằng 3, và tạo dãy mới với quy tắc: số chẵn chia đôi, số lẻ giữ nguyên.
* Ý tưởng: 
*   - Sử dụng cả Query Syntax và Method Syntax của LINQ để truy vấn mảng[cite: 1].
*   - Dùng Where() cho điều kiện lọc và Select() để biến đổi giá trị của phần tử[cite: 1].
*/

using System;
using System.Linq;

namespace BaiThucHanhLINQ_Bai21
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] mangSo = { 50, 42, 16, 3, 9, 8, 12, 7, 24, 0 }; 

            // a. Liệt kê các phần tử chia hết cho 4 và 3[cite: 1]
            Console.WriteLine("a. Các phần tử chia hết cho 4 và 3:");
            var a_Query = from n in mangSo where n % 4 == 0 && n % 3 == 0 select n;
            var a_Method = mangSo.Where(n => n % 4 == 0 && n % 3 == 0);
            Console.WriteLine($"   Query Syntax: {string.Join(", ", a_Query)}");
            Console.WriteLine($"   Method Syntax: {string.Join(", ", a_Method)}\n");

            // b. Liệt kê các phần tử nhỏ hơn hoặc bằng 3[cite: 1]
            Console.WriteLine("b. Các phần tử nhỏ hơn hoặc bằng 3:");
            var b_Query = from n in mangSo where n <= 3 select n;
            var b_Method = mangSo.Where(n => n <= 3);
            Console.WriteLine($"   Query Syntax: {string.Join(", ", b_Query)}");
            Console.WriteLine($"   Method Syntax: {string.Join(", ", b_Method)}\n");

            // c. Tạo một dãy mới: số chẵn chia đôi, số lẻ giữ nguyên giá trị[cite: 1]
            Console.WriteLine("c. Dãy mới (chẵn chia đôi, lẻ giữ nguyên):");
            var c_Query = from n in mangSo select n % 2 == 0 ? n / 2 : n;
            var c_Method = mangSo.Select(n => n % 2 == 0 ? n / 2 : n);
            Console.WriteLine($"   Query Syntax: {string.Join(", ", c_Query)}");
            Console.WriteLine($"   Method Syntax: {string.Join(", ", c_Method)}");

            Console.ReadLine();
        }
    }
}
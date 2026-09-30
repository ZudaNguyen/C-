/*
* CHƯƠNG TRÌNH TRUY VẤN LINQ TRÊN MẢNG CHUỖI (BÀI 2.2)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 30/09/2026
*
* Phát biểu đề bài: Cho mảng chuỗi các từ. Thực hiện lọc chuỗi theo độ dài, sắp xếp, biến đổi định dạng chữ hoa/thường, tìm kiếm ký tự "u" và chọn từ bắt đầu bằng chữ in hoa.
* Ý tưởng: 
*   - Sử dụng Where() để lọc (theo Length, Contains, IsUpper)[cite: 1].
*   - Sử dụng OrderBy() để sắp xếp tăng dần theo ký tự đầu tiên[cite: 1].
*   - Sử dụng Select() kết hợp ToLower() và ToUpper() để định dạng lại chuỗi kết quả[cite: 1].
*/

using System;
using System.Linq;

namespace BaiThucHanhLINQ_Bai22
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] mangChuoi = { "đầu", "lòng", "hai", "ả", "tố", "nga",
                                   "Thúy", "Kiều", "là", "chị", "em", "là", "Thúy", "Vân" }; 

            // a. Liệt kê các phần tử có 4 ký tự và sắp xếp tăng dần theo ký tự đầu tiên[cite: 1]
            Console.WriteLine("a. Các phần tử có 4 ký tự (sắp xếp tăng dần theo ký tự đầu):");
            var a_Query = from s in mangChuoi where s.Length == 4 orderby s[0] ascending select s;
            var a_Method = mangChuoi.Where(s => s.Length == 4).OrderBy(s => s[0]);
            Console.WriteLine($"   Query Syntax: {string.Join(", ", a_Query)}");
            Console.WriteLine($"   Method Syntax: {string.Join(", ", a_Method)}\n");

            // b. Biến đổi mỗi phần tử thành dạng: <chữ thường> - <CHỮ HOA>[cite: 1]
            Console.WriteLine("b. Biến đổi dạng <chữ thường> - <CHỮ HOA>:");
            var b_Query = from s in mangChuoi select $"{s.ToLower()} - {s.ToUpper()}";
            var b_Method = mangChuoi.Select(s => $"{s.ToLower()} - {s.ToUpper()}");
            Console.WriteLine($"   Query Syntax (3 ptử đầu): {string.Join(" | ", b_Query.Take(3))}");
            Console.WriteLine($"   Method Syntax (3 ptử đầu): {string.Join(" | ", b_Method.Take(3))}\n");

            // c. Liệt kê các phần tử có chứa ký tự “u”[cite: 1]
            Console.WriteLine("c. Các phần tử chứa ký tự 'u':");
            var c_Query = from s in mangChuoi where s.Contains("u") select s;
            var c_Method = mangChuoi.Where(s => s.Contains("u"));
            Console.WriteLine($"   Query Syntax: {string.Join(", ", c_Query)}");
            Console.WriteLine($"   Method Syntax: {string.Join(", ", c_Method)}\n");

            // d. Liệt kê các từ “Thúy Kiều Thúy Vân” bằng cách chọn các phần tử bắt đầu bằng chữ in hoa[cite: 1]
            Console.WriteLine("d. Các phần tử bắt đầu bằng chữ in hoa:");
            var d_Query = from s in mangChuoi where char.IsUpper(s[0]) select s;
            var d_Method = mangChuoi.Where(s => char.IsUpper(s[0]));
            Console.WriteLine($"   Query Syntax: {string.Join(" ", d_Query)}");
            Console.WriteLine($"   Method Syntax: {string.Join(" ", d_Method)}");

            Console.ReadLine();
        }
    }
}
/*
* CHƯƠNG TRÌNH XỬ LÝ CHUỖI (CHỮ HOA, CHỮ THƯỜNG, ĐẾM TỪ)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Nhập một chuỗi gồm nhiều từ. Hãy chuyển chuỗi đó sang ký tự thường, sang ký tự hoa, đếm số từ trong chuỗi[cite: 9].
* Ý tưởng: Dùng các phương thức có sẵn ToLower() và ToUpper() để chuyển đổi. Để đếm từ, dùng phương thức Split() tách chuỗi dựa trên khoảng trắng và loại bỏ các khoảng trắng thừa.
*/

using System;

namespace Bai12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap vao mot chuoi gom nhieu tu: ");
            string chuoi = Console.ReadLine();

            // Chuyển sang chữ thường và chữ hoa
            Console.WriteLine($"Chuoi ky tu thuong: {chuoi.ToLower()}");
            Console.WriteLine($"Chuoi ky tu hoa: {chuoi.ToUpper()}");

            // Đếm số từ
            // Tách chuỗi theo khoảng trắng và bỏ qua các khoảng trắng liên tiếp
            string[] cacTu = chuoi.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"So tu trong chuoi la: {cacTu.Length}");

            Console.ReadLine();
        }
    }
}
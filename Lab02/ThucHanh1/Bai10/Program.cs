/*
* CHƯƠNG TRÌNH KIỂM TRA CHUỖI ĐỐI XỨNG
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Viết phương thức thành viên kiểm tra chuỗi có đối xứng hay không[cite: 9].
* Ý tưởng: Xây dựng lớp XuLyChuoi. Dùng hai biến chạy từ đầu (trái) và cuối (phải) của chuỗi, so sánh từng cặp ký tự. Nếu có cặp khác nhau thì không đối xứng.
*/

using System;

namespace Bai10
{
    class XuLyChuoi
    {
        // Phương thức kiểm tra chuỗi đối xứng
        public bool KiemTraDoiXung(string chuoi)
        {
            int left = 0;
            int right = chuoi.Length - 1;

            while (left < right)
            {
                if (chuoi[left] != chuoi[right])
                {
                    return false;
                }
                left++;
                right--;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap vao mot chuoi de kiem tra: ");
            string input = Console.ReadLine();

            XuLyChuoi xuLy = new XuLyChuoi();

            if (xuLy.KiemTraDoiXung(input))
            {
                Console.WriteLine("Day LA chuoi doi xung.");
            }
            else
            {
                Console.WriteLine("Day KHONG PHAI la chuoi doi xung.");
            }

            Console.ReadLine();
        }
    }
}
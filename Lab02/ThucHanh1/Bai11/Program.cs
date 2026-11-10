/*
* CHƯƠNG TRÌNH ĐẢO NGƯỢC CHUỖI
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Viết phương thức thành viên trả về chuỗi là đảo của một chuỗi[cite: 9].
* Ý tưởng: Chuyển chuỗi thành mảng ký tự (char array), dùng hàm Array.Reverse để đảo ngược mảng, sau đó khởi tạo lại chuỗi mới từ mảng đã đảo.
*/

using System;

namespace Bai11
{
    class XuLyChuoi
    {
        // Phương thức trả về chuỗi đảo ngược
        public string DaoChuoi(string chuoi)
        {
            char[] mangKyTu = chuoi.ToCharArray();
            Array.Reverse(mangKyTu);
            return new string(mangKyTu);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap vao mot chuoi: ");
            string input = Console.ReadLine();

            XuLyChuoi xuLy = new XuLyChuoi();
            string chuoiDao = xuLy.DaoChuoi(input);

            Console.WriteLine($"Chuoi sau khi dao nguoc la: {chuoiDao}");

            Console.ReadLine();
        }
    }
}
/*
* CHƯƠNG TRÌNH XUẤT VÀ NHẬP CHUỖI
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Viết chương trình nhập họ tên và xuất họ tên đã nhập ra màn hình console[cite: 3].
* Ý tưởng: Sử dụng Console.Write để in thông báo yêu cầu nhập, Console.ReadLine để đọc chuỗi văn bản người dùng nhập vào, sau đó dùng Console.WriteLine để xuất câu chào kèm theo tên.
* Mã giả:
*   In thông báo "Nhap ho ten cua ban: "
*   ho_ten = Đọc dữ liệu từ bàn phím
*   In kết quả "Chao ban " + ho_ten + "!"
*/

using System;

namespace Bai2_XuatNhapChuoi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Yêu cầu người dùng nhập họ tên (dùng Write để không xuống dòng)
            Console.Write("Nhap ho ten cua ban: ");

            // Đọc dữ liệu nhập từ bàn phím và gán vào biến hoTen
            string hoTen = Console.ReadLine();

            // Xuất câu chào ra màn hình console theo đúng định dạng
            Console.WriteLine($"Chao ban {hoTen}!");

            // Dừng màn hình console lại để xem kết quả
            Console.ReadLine();
        }
    }
}
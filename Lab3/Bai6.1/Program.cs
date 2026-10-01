/*
* XÂY DỰNG LỚP HỆ (BÀI 6.1)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 01/10/2026
*
* Phát biểu đề bài: Xây dựng lớp He và tạo phương thức DS_He() trả về danh sách dữ liệu mẫu.
*/

using System;
using System.Collections.Generic;

namespace BaiThucHanhLINQ_Bai61
{
    public class He
    {
        public string MaHe { get; set; } = "";
        public string TenHe { get; set; } = "";
    }

    public class DuLieuHe
    {
        public static List<He> DS_He()
        {
            return new List<He>
            {
                new He { MaHe = "KTV", TenHe = "Kỹ thuật viên" },
                new He { MaHe = "CD", TenHe = "Chuyên đề" },
                new He { MaHe = "QT", TenHe = "Chứng chỉ quốc tế" }
            };
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== KIỂM TRA DỮ LIỆU HỆ (BÀI 6.1) ===\n");

            List<He> danhSachHe = DuLieuHe.DS_He();

            foreach (var h in danhSachHe)
            {
                Console.WriteLine($"Mã hệ: {h.MaHe,-4} | Tên hệ: {h.TenHe}");
            }

            Console.ReadLine();
        }
    }
}
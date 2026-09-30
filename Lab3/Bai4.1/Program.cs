/*
* XÂY DỰNG NGUỒN DỮ LIỆU ĐỐI TƯỢNG (BÀI 4.1)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 30/09/2026
*
* Phát biểu đề bài: Tạo lớp MonHoc và lớp DuLieu chứa phương thức tĩnh DS_Mon() trả về danh sách (List<MonHoc>) các môn học theo bảng dữ liệu.
* Ý tưởng: 
*   - Định nghĩa lớp MonHoc với các thuộc tính cơ bản (MaMon, TenMon, He, SoTiet).
*   - Tạo một List<MonHoc> trong phương thức tĩnh và khởi tạo hàng loạt các đối tượng môn học tương ứng với dữ liệu mẫu.
*/

using System;
using System.Collections.Generic;

namespace BaiThucHanhLINQ_Bai41
{
    // Tạo lớp MonHoc gồm các thuộc tính
    public class MonHoc
    {
        public string MaMon { get; set; } = "";
        public string TenMon { get; set; } = "";
        public string He { get; set; } = "";
        public byte SoTiet { get; set; }
    }

    // Tạo lớp DuLieu có phương thức tĩnh DS_Mon() trả về List<MonHoc>
    public class DuLieu
    {
        public static List<MonHoc> DS_Mon()
        {
            return new List<MonHoc>
            {
                new MonHoc { MaMon = "HP2_1", TenMon = "Nền tảng C#", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP2_2", TenMon = "Công nghệ ADO.NET", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP3_1", TenMon = "Lập trình Windows Forms", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP3_2", TenMon = "Xây dựng ứng dụng Windows Forms", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP4_1", TenMon = "Lập trình Web với HTML, CSS và JavaScript", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP4_2", TenMon = "Xây dựng ứng dụng Web với ASP.NET", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP5_1", TenMon = "Lập trình CSDL SQL Server căn bản", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP5_2", TenMon = "Lập trình CSDL SQL Server nâng cao", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "JLCB",  TenMon = "Joomla cơ bản", He = "CD", SoTiet = 72 },
                new MonHoc { MaMon = "LINQ",  TenMon = "Language-Integrated Query", He = "CD", SoTiet = 64 },
                new MonHoc { MaMon = "DAWEB", TenMon = "Đồ án thực tế Web với ASP.NET", He = "CD", SoTiet = 40 },
                new MonHoc { MaMon = "DAWIN", TenMon = "Đồ án thực tế Windows Forms", He = "CD", SoTiet = 40 },
                new MonHoc { MaMon = "CC++",  TenMon = "Lập trình hướng đối tượng với C/C++", He = "CD", SoTiet = 128 },
                new MonHoc { MaMon = "JQUE",  TenMon = "JQuery", He = "CD", SoTiet = 22 },
                new MonHoc { MaMon = "XML",   TenMon = "Công nghệ XML", He = "CD", SoTiet = 32 },
                new MonHoc { MaMon = "CRYS",  TenMon = "Crystal Report trong Visual Studio", He = "CD", SoTiet = 32 },
                new MonHoc { MaMon = "BWEB",  TenMon = "HTML, CSS và JavaScript", He = "CD", SoTiet = 32 },
                new MonHoc { MaMon = "XYZ",   TenMon = "Chưa đặt tên môn", He = "", SoTiet = 0 }
            };
        }
    }

    // Lớp Program để kiểm tra dữ liệu
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== KIỂM TRA DỮ LIỆU MÔN HỌC (BÀI 4.1) ===\n");

            // Lấy danh sách từ lớp dữ liệu
            List<MonHoc> danhSachMonHoc = DuLieu.DS_Mon();

            // In ra màn hình để kiểm tra
            foreach (var mon in danhSachMonHoc)
            {
                Console.WriteLine($"Mã: {mon.MaMon,-6} | Tên: {mon.TenMon,-43} | Hệ: {mon.He,-4} | Số tiết: {mon.SoTiet}");
            }

            Console.ReadLine();
        }
    }
}
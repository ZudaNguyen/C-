/*
* CHƯƠNG TRÌNH TRUY VẤN CƠ BẢN TRÊN LIST<MONHOC> (BÀI 5.1)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 01/10/2026
*
* Phát biểu đề bài: Thực hiện các truy vấn LINQ cơ bản trên danh sách môn học như lọc theo tên, hệ, và sắp xếp dữ liệu.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace BaiThucHanhLINQ_Bai51
{
    public class MonHoc
    {
        public string MaMon { get; set; } = "";
        public string TenMon { get; set; } = "";
        public string He { get; set; } = "";
        public byte SoTiet { get; set; }
    }

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

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<MonHoc> ds = DuLieu.DS_Mon();

            // a. Liệt kê tên các môn học bắt đầu bằng “Lập trình”
            Console.WriteLine("a. Các môn học bắt đầu bằng 'Lập trình':");
            var cauA = ds.Where(m => m.TenMon.StartsWith("Lập trình")).Select(m => m.TenMon);
            foreach (var ten in cauA) Console.WriteLine($"   - {ten}");

            // b. Liệt kê các môn thuộc hệ “CD”, sắp xếp số tiết giảm dần rồi mã môn tăng dần
            Console.WriteLine("\nb. Các môn hệ 'CD' (sắp xếp số tiết giảm dần, mã môn tăng dần):");
            var cauB = ds.Where(m => m.He == "CD").OrderByDescending(m => m.SoTiet).ThenBy(m => m.MaMon);
            foreach (var m in cauB) Console.WriteLine($"   - {m.MaMon}: {m.TenMon} ({m.SoTiet} tiết)");

            // c. Liệt kê các môn có tên chứa từ “web”, chỉ lấy Tên môn và Hệ
            Console.WriteLine("\nc. Các môn có tên chứa từ 'web' (Không phân biệt hoa thường):");
            var cauC = ds.Where(m => m.TenMon.ToLower().Contains("web")).Select(m => new { m.TenMon, m.He });
            foreach (var m in cauC) Console.WriteLine($"   - Tên: {m.TenMon} | Hệ: {m.He}");

            // d. Liệt kê các môn thuộc hệ “KTV”, sắp xếp tăng dần theo Mã môn
            Console.WriteLine("\nd. Các môn hệ 'KTV' (sắp xếp mã môn tăng dần):");
            var cauD = ds.Where(m => m.He == "KTV").OrderBy(m => m.MaMon);
            foreach (var m in cauD) Console.WriteLine($"   - {m.MaMon}: {m.TenMon}");

            Console.ReadLine();
        }
    }
}
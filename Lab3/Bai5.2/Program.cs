/*
* CHƯƠNG TRÌNH THỐNG KÊ TRÊN LIST<MONHOC> (BÀI 5.2)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 01/10/2026
*
* Phát biểu đề bài: Thực hiện các truy vấn thống kê, đếm, tính tổng, tìm max/min và phân nhóm dữ liệu (GroupBy) trên danh sách môn học.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace BaiThucHanhLINQ_Bai52
{
    // (Lưu ý: Nếu bạn để Bài 5.2 chung Project với Bài 5.1 thì có thể xóa đoạn khai báo class MonHoc và DuLieu này đi để tránh trùng lặp)
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

            // a. Cho biết tổng số môn hiện có
            Console.WriteLine($"a. Tổng số môn hiện có: {ds.Count}");

            // b. Đếm số môn có tên bắt đầu bằng “Lập trình”
            Console.WriteLine($"b. Số môn có tên bắt đầu bằng 'Lập trình': {ds.Count(m => m.TenMon.StartsWith("Lập trình"))}");

            // c. Tính tổng số tiết của hệ Kỹ thuật viên (KTV)
            Console.WriteLine($"c. Tổng số tiết của hệ KTV: {ds.Where(m => m.He == "KTV").Sum(m => m.SoTiet)}");

            // d. Cho biết tổng số môn của mỗi hệ: Hệ, Tổng số môn
            Console.WriteLine("\nd. Tổng số môn của mỗi hệ:");
            var cauD = ds.GroupBy(m => m.He).Select(g => new { He = string.IsNullOrEmpty(g.Key) ? "Chưa rõ" : g.Key, TongSoMon = g.Count() });
            foreach (var item in cauD) Console.WriteLine($"   - Hệ {item.He}: {item.TongSoMon} môn");

            // e. Nhóm theo Số tiết; in Số tiết và Tổng số môn, sắp xếp giảm dần theo Số tiết
            Console.WriteLine("\ne. Nhóm theo Số tiết (Sắp xếp giảm dần):");
            var cauE = ds.GroupBy(m => m.SoTiet).Select(g => new { SoTiet = g.Key, TongSoMon = g.Count() }).OrderByDescending(x => x.SoTiet);
            foreach (var item in cauE) Console.WriteLine($"   - Số tiết {item.SoTiet}: {item.TongSoMon} môn");

            // f. Cho biết thông tin môn học có số tiết cao nhất
            Console.WriteLine("\nf. Thông tin môn học có số tiết cao nhất:");
            int maxTiet = ds.Max(m => m.SoTiet);
            var cauF = ds.Where(m => m.SoTiet == maxTiet);
            foreach (var m in cauF) Console.WriteLine($"   - {m.MaMon} - {m.TenMon} ({m.SoTiet} tiết)");

            // g. Thống kê theo Hệ: tổng số môn, tổng số tiết, số tiết cao nhất, số tiết thấp nhất
            Console.WriteLine("\ng. Thống kê chi tiết theo Hệ:");
            var cauG = ds.GroupBy(m => m.He).Select(g => new {
                He = string.IsNullOrEmpty(g.Key) ? "Chưa rõ" : g.Key,
                TongMon = g.Count(),
                TongTiet = g.Sum(m => m.SoTiet),
                MaxTiet = g.Max(m => m.SoTiet),
                MinTiet = g.Min(m => m.SoTiet)
            });
            foreach (var item in cauG)
                Console.WriteLine($"   - Hệ {item.He,-7} | Tổng môn: {item.TongMon,-2} | Tổng tiết: {item.TongTiet,-4} | Max: {item.MaxTiet,-3} | Min: {item.MinTiet}");

            // h. Liệt kê các môn học được phân nhóm theo Hệ
            Console.WriteLine("\nh. Các môn học phân nhóm theo Hệ:");
            var cauH = ds.GroupBy(m => m.He);
            foreach (var nhom in cauH)
            {
                Console.WriteLine($"   * Hệ '{nhom.Key}':");
                foreach (var m in nhom) Console.WriteLine($"     - {m.TenMon}");
            }

            // i. Liệt kê các môn học được phân nhóm theo Số tiết và tăng dần theo Số tiết
            Console.WriteLine("\ni. Các môn học phân nhóm theo Số tiết (tăng dần theo số tiết):");
            var cauI = ds.GroupBy(m => m.SoTiet).OrderBy(g => g.Key);
            foreach (var nhom in cauI)
            {
                Console.WriteLine($"   * Nhóm {nhom.Key} tiết:");
                foreach (var m in nhom) Console.WriteLine($"     - {m.TenMon}");
            }

            // j. Với hệ KTV, phân nhóm theo học phần HP2, HP3, HP4, HP5; sắp xếp theo Mã môn
            Console.WriteLine("\nj. Hệ KTV phân nhóm theo học phần (HP2, HP3, HP4, HP5):");
            var cauJ = ds.Where(m => m.He == "KTV")
                         .GroupBy(m => m.MaMon.Substring(0, 3)) // Lấy 3 ký tự đầu làm nhóm (VD: HP2)
                         .OrderBy(g => g.Key);
            foreach (var nhom in cauJ)
            {
                Console.WriteLine($"   * Học phần {nhom.Key}:");
                foreach (var m in nhom.OrderBy(x => x.MaMon))
                    Console.WriteLine($"     - {m.MaMon}: {m.TenMon}");
            }

            // k. Phân nhóm theo Hệ, chỉ lấy các môn có Số tiết > 40; trong mỗi nhóm sắp xếp theo Mã môn
            Console.WriteLine("\nk. Phân nhóm theo Hệ (Chỉ lấy môn > 40 tiết, sắp xếp theo Mã môn):");
            var cauK = ds.Where(m => m.SoTiet > 40)
                         .GroupBy(m => m.He)
                         .OrderBy(g => g.Key);
            foreach (var nhom in cauK)
            {
                Console.WriteLine($"   * Hệ {nhom.Key}:");
                foreach (var m in nhom.OrderBy(x => x.MaMon))
                    Console.WriteLine($"     - {m.MaMon}: {m.TenMon} ({m.SoTiet} tiết)");
            }

            Console.ReadLine();
        }
    }
}
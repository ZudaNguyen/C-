/*
* JOIN VÀ CÁC TOÁN TỬ TẬP HỢP (BÀI 6.2)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 01/10/2026
*
* Phát biểu đề bài: Thực hiện các truy vấn kết nối (join), left/full outer join, và các toán tử tập hợp giữa danh sách Môn học và Hệ.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace BaiThucHanhLINQ_Bai62
{
    public class He
    {
        public string MaHe { get; set; } = "";
        public string TenHe { get; set; } = "";
    }

    public class MonHoc
    {
        public string MaMon { get; set; } = "";
        public string TenMon { get; set; } = "";
        public string He { get; set; } = "";
        public byte SoTiet { get; set; }
    }

    public class DuLieu
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

        public static List<MonHoc> DS_Mon()
        {
            return new List<MonHoc>
            {
                new MonHoc { MaMon = "HP2_1", TenMon = "Nền tảng C#", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP2_2", TenMon = "Công nghệ ADO.NET", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP3_1", TenMon = "Lập trình Windows Forms", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "HP4_2", TenMon = "Xây dựng ứng dụng Web với ASP.NET", He = "KTV", SoTiet = 64 },
                new MonHoc { MaMon = "JLCB",  TenMon = "Joomla cơ bản", He = "CD", SoTiet = 72 },
                new MonHoc { MaMon = "LINQ",  TenMon = "Language-Integrated Query", He = "CD", SoTiet = 64 },
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
            List<He> dsHe = DuLieu.DS_He();
            List<MonHoc> dsMon = DuLieu.DS_Mon();

            // a. Dùng join để liệt kê: Tên hệ, Mã môn, Tên môn
            Console.WriteLine("a. Liệt kê (Tên hệ, Mã môn, Tên môn):");
            var cauA = from h in dsHe
                       join m in dsMon on h.MaHe equals m.He
                       select new { h.TenHe, m.MaMon, m.TenMon };
            foreach (var item in cauA) Console.WriteLine($"   - Hệ: {item.TenHe,-15} | {item.MaMon,-6} | {item.TenMon}");

            // b. Liệt kê cả những hệ chưa có môn học (Left Outer Join)
            Console.WriteLine("\nb. Liệt kê cả hệ chưa có môn học (Left Outer Join):");
            var cauB = from h in dsHe
                       join m in dsMon on h.MaHe equals m.He into hg
                       from m in hg.DefaultIfEmpty()
                       select new
                       {
                           TenHe = h.TenHe,
                           MaMon = m?.MaMon ?? "[Trống]",
                           TenMon = m?.TenMon ?? "[Không có môn nào]"
                       };
            foreach (var item in cauB) Console.WriteLine($"   - Hệ: {item.TenHe,-18} | {item.MaMon,-7} | {item.TenMon}");

            // c. Liệt kê cả hệ chưa có môn học và môn học chưa khai báo hệ (Full Outer Join)
            Console.WriteLine("\nc. Liệt kê cả hệ chưa có môn học và môn chưa có hệ:");
            var rightOnlyC = from m in dsMon
                             where !dsHe.Any(h => h.MaHe == m.He)
                             select new
                             {
                                 TenHe = "[Hệ chưa khai báo]",
                                 MaMon = m.MaMon,
                                 TenMon = m.TenMon
                             };
            var cauC = cauB.Concat(rightOnlyC);
            foreach (var item in cauC) Console.WriteLine($"   - Hệ: {item.TenHe,-18} | {item.MaMon,-7} | {item.TenMon}");

            // d. Chỉ liệt kê những hệ chưa có môn học và những môn học chưa khai báo hệ
            Console.WriteLine("\nd. Chỉ liệt kê hệ chưa có môn và môn chưa có hệ:");
            var heChuaCoMon = cauB.Where(x => x.MaMon == "[Trống]");
            var cauD = heChuaCoMon.Concat(rightOnlyC);
            foreach (var item in cauD) Console.WriteLine($"   - Hệ: {item.TenHe,-18} | {item.MaMon,-7} | {item.TenMon}");

            // e. Lấy 5 môn học đầu tiên có số tiết giảm dần; hiển thị Tên hệ, Mã môn, Tên môn, Số tiết
            Console.WriteLine("\ne. Top 5 môn có số tiết giảm dần:");
            var cauE = (from m in dsMon
                        join h in dsHe on m.He equals h.MaHe into hg
                        from h in hg.DefaultIfEmpty()
                        orderby m.SoTiet descending
                        select new
                        {
                            TenHe = h?.TenHe ?? "[Chưa rõ hệ]",
                            m.MaMon,
                            m.TenMon,
                            m.SoTiet
                        }).Take(5);
            foreach (var item in cauE) Console.WriteLine($"   - {item.TenHe,-15} | {item.MaMon,-5} | {item.SoTiet} tiết | {item.TenMon}");

            // f. Cho biết tổng số môn học của mỗi hệ: Mã hệ, Tên hệ, Tổng số môn
            Console.WriteLine("\nf. Tổng số môn học của mỗi hệ:");
            var cauF = from h in dsHe
                       join m in dsMon on h.MaHe equals m.He into hg
                       select new { h.MaHe, h.TenHe, TongSoMon = hg.Count() };
            foreach (var item in cauF) Console.WriteLine($"   - [{item.MaHe}] {item.TenHe,-18}: {item.TongSoMon} môn");

            // g. Cho biết có bao nhiêu loại Số tiết khác nhau trong danh sách môn học
            int soLoaiSoTiet = dsMon.Select(m => m.SoTiet).Distinct().Count();
            Console.WriteLine($"\ng. Số loại Số tiết khác nhau: {soLoaiSoTiet}");

            // h. Tìm môn học đầu tiên có tên bắt đầu bằng “Lập trình”
            Console.WriteLine("\nh. Môn học đầu tiên bắt đầu bằng 'Lập trình':");
            var cauH = dsMon.FirstOrDefault(m => m.TenMon.StartsWith("Lập trình"));
            if (cauH != null)
                Console.WriteLine($"   - {cauH.MaMon}: {cauH.TenMon}");

            // i. Liệt kê các môn theo từng hệ, đánh số thứ tự trong mỗi nhóm
            Console.WriteLine("\ni. Liệt kê môn theo từng hệ (có đánh số thứ tự):");
            var cauI = dsMon.GroupBy(m => m.He);
            foreach (var nhom in cauI)
            {
                string tenHe = dsHe.FirstOrDefault(h => h.MaHe == nhom.Key)?.TenHe ?? "[Chưa có hệ]";
                Console.WriteLine($"   * Hệ: {tenHe}");
                int stt = 1;
                foreach (var m in nhom)
                {
                    Console.WriteLine($"     {stt++}. {m.MaMon} - {m.TenMon}");
                }
            }

            Console.ReadLine();
        }
    }
}
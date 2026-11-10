/*
* CHƯƠNG TRÌNH TÍNH LƯƠNG NHÂN VIÊN (KẾ THỪA)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 24/09/2026
*
* Phát biểu đề bài: Công ty X có nhân viên kinh doanh và nhân viên sản xuất. Tính lương cho từng loại nhân viên theo công thức quy định.
* Ý tưởng: 
*   - Lớp cha NhanVien có MaNV, HoTen và phương thức ảo TinhLuong().
*   - Lớp NVKinhDoanh kế thừa NhanVien, thêm LuongCoBan, SoHopDong. Lương = Cơ bản + (Hợp đồng * 500.000).
*   - Lớp NVSanXuat kế thừa NhanVien, thêm SoSanPham. Lương = Sản phẩm * 1000 (thưởng 5% nếu > 3000 SP)[cite: 18].
*/

using System;
using System.Collections.Generic;

namespace Bai3_5
{
    // 1. Lớp cơ sở (Lớp cha)
    class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }

        public virtual void Nhap()
        {
            Console.Write("Nhap Ma NV: ");
            MaNV = Console.ReadLine();
            Console.Write("Nhap Ho ten: ");
            HoTen = Console.ReadLine();
        }

        // Phương thức ảo để lớp con ghi đè
        public virtual double TinhLuong()
        {
            return 0;
        }

        public virtual void Xuat()
        {
            Console.WriteLine($"[{MaNV}] {HoTen} | Luong: {TinhLuong():N0} VND");
        }
    }

    // 2. Lớp Nhân viên Kinh doanh
    class NVKinhDoanh : NhanVien
    {
        public double LuongCoBan { get; set; }
        public int SoHopDong { get; set; }

        public override void Nhap()
        {
            Console.WriteLine("--- Nhap Thong Tin NV Kinh Doanh ---");
            base.Nhap();
            Console.Write("Nhap Luong co ban: ");
            LuongCoBan = double.Parse(Console.ReadLine());
            Console.Write("Nhap So hop dong ky ket: ");
            SoHopDong = int.Parse(Console.ReadLine());
        }

        public override double TinhLuong()
        {
            return LuongCoBan + (SoHopDong * 500000);
        }
    }

    // 3. Lớp Nhân viên Sản xuất
    class NVSanXuat : NhanVien
    {
        public int SoSanPham { get; set; }

        public override void Nhap()
        {
            Console.WriteLine("--- Nhap Thong Tin NV San Xuat ---");
            base.Nhap();
            Console.Write("Nhap So luong san pham: ");
            SoSanPham = int.Parse(Console.ReadLine());
        }

        public override double TinhLuong()
        {
            double luong = SoSanPham * 1000;
            if (SoSanPham > 3000)
            {
                luong += luong * 0.05; // Thưởng thêm 5%
            }
            return luong;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<NhanVien> danhSachNV = new List<NhanVien>();

            Console.Write("Nhap so luong nhan vien: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nChon loai nhan vien thu {i + 1} (1: Kinh doanh, 2: San xuat): ");
                int loai = int.Parse(Console.ReadLine());

                NhanVien nv;
                if (loai == 1)
                    nv = new NVKinhDoanh();
                else
                    nv = new NVSanXuat();

                nv.Nhap();
                danhSachNV.Add(nv);
            }

            Console.WriteLine("\n=== BANG LUONG NHAN VIEN ===");
            foreach (NhanVien nv in danhSachNV)
            {
                nv.Xuat(); // Đa hình: Tự động gọi hàm Xuat() và TinhLuong() tương ứng của lớp con
            }

            Console.ReadLine();
        }
    }
}
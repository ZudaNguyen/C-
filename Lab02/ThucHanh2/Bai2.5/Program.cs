/*
* CHƯƠNG TRÌNH TÍNH TỔNG LƯƠNG PHÒNG BAN
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Một phòng ban có n nhân viên. Mỗi ngày vắng trừ 100.000 VNĐ. Hãy tính tổng lương của phòng ban[cite: 16].
* Ý tưởng:
*   - Sử dụng lại lớp NhanVien (có phương thức TinhLuong()).
*   - Tạo lớp PhongBan chứa mảng hoặc List các NhanVien. Hàm TongLuong() cộng dồn lương thực lãnh của mọi người.
*/

using System;
using System.Collections.Generic;

namespace Bai2_5_LuongNhanVien
{
    class NhanVien
    {
        public string HoTen { get; set; }
        public double MucLuong { get; set; }
        public int SoNgayVang { get; set; }

        public void Nhap()
        {
            Console.Write("  Ho ten: ");
            HoTen = Console.ReadLine();
            Console.Write("  Muc luong co ban: ");
            MucLuong = double.Parse(Console.ReadLine());
            Console.Write("  So ngay vang: ");
            SoNgayVang = int.Parse(Console.ReadLine());
        }

        public double TinhLuong()
        {
            return MucLuong - (SoNgayVang * 100000); // Trừ 100k mỗi ngày vắng
        }
    }

    class PhongBan
    {
        private List<NhanVien> danhSachNV;

        public PhongBan()
        {
            danhSachNV = new List<NhanVien>();
        }

        public void Nhap()
        {
            Console.Write("Nhap so luong nhan vien cua phong ban (n): ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nNhap thong tin nhan vien thu {i + 1}:");
                NhanVien nv = new NhanVien();
                nv.Nhap();
                danhSachNV.Add(nv);
            }
        }

        public double TinhTongLuongPhongBan()
        {
            double tongLuong = 0;
            foreach (NhanVien nv in danhSachNV)
            {
                tongLuong += nv.TinhLuong();
            }
            return tongLuong;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PhongBan pb = new PhongBan();
            pb.Nhap();

            double tong = pb.TinhTongLuongPhongBan();
            Console.WriteLine($"\n=== TONG LUONG CUA CA PHONG BAN LA: {tong:N0} VNĐ ===");

            Console.ReadLine();
        }
    }
}
/*
* CHƯƠNG TRÌNH TÍNH LƯƠNG NHÂN VIÊN
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 21/09/2026
*
* Phát biểu đề bài: Viết chương trình nhập thông tin một nhân viên (họ tên, mức lương, số ngày vắng). Tính và xuất lương của nhân viên, biết rằng một ngày vắng sẽ bị trừ 100.000 VNĐ[cite: 9].
* Ý tưởng: Khai báo lớp NhanVien. Tính lương thực lãnh = Mức lương ban đầu - (Số ngày vắng * 100000).
*/

using System;

namespace Bai14
{
    // Xây dựng lớp NhanVien
    class NhanVien
    {
        public string HoTen;
        public double MucLuong;
        public int SoNgayVang;

        public void Nhap()
        {
            Console.Write("Nhap Ho ten nhan vien: ");
            HoTen = Console.ReadLine();
            Console.Write("Nhap Muc luong co ban (VND): ");
            MucLuong = double.Parse(Console.ReadLine());
            Console.Write("Nhap So ngay vang: ");
            SoNgayVang = int.Parse(Console.ReadLine());
        }

        public double TinhLuong()
        {
            // Trừ 100.000 VNĐ cho mỗi ngày vắng
            return MucLuong - (SoNgayVang * 100000);
        }

        public void Xuat()
        {
            Console.WriteLine("\n--- BANG LUONG NHAN VIEN ---");
            Console.WriteLine($"Ho ten: {HoTen}");
            Console.WriteLine($"Muc luong goc: {MucLuong} VND");
            Console.WriteLine($"So ngay vang: {SoNgayVang} ngay");
            Console.WriteLine($"LUONG THUC LANH: {TinhLuong()} VND");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            NhanVien nv = new NhanVien();
            nv.Nhap();
            nv.Xuat();

            Console.ReadLine();
        }
    }
}
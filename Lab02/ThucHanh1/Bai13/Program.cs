/*
* CHƯƠNG TRÌNH QUẢN LÝ THÔNG TIN SINH VIÊN
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Xây dựng lớp sinh viên để lưu trữ 1 sinh viên (mã sinh viên, họ tên, địa chỉ, sinh viên năm thứ mấy). Hãy nhập xuất 1 sinh viên[cite: 9].
* Ý tưởng: Khai báo lớp SinhVien với các thuộc tính tương ứng. Tạo hai phương thức Nhap() và Xuat() bên trong lớp để xử lý dữ liệu.
*/

using System;

namespace Bai13
{
    // Xây dựng lớp SinhVien
    class SinhVien
    {
        public string MaSV;
        public string HoTen;
        public string DiaChi;
        public int NamHoc;

        public void Nhap()
        {
            Console.Write("Nhap Ma SV: ");
            MaSV = Console.ReadLine();
            Console.Write("Nhap Ho ten: ");
            HoTen = Console.ReadLine();
            Console.Write("Nhap Dia chi: ");
            DiaChi = Console.ReadLine();
            Console.Write("Sinh vien nam thu may: ");
            NamHoc = int.Parse(Console.ReadLine());
        }

        public void Xuat()
        {
            Console.WriteLine("\n--- THONG TIN SINH VIEN ---");
            Console.WriteLine($"Ma SV: {MaSV}");
            Console.WriteLine($"Ho ten: {HoTen}");
            Console.WriteLine($"Dia chi: {DiaChi}");
            Console.WriteLine($"Nam hoc: thu {NamHoc}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Khởi tạo đối tượng sinh viên và gọi phương thức
            SinhVien sv = new SinhVien();
            sv.Nhap();
            sv.Xuat();

            Console.ReadLine();
        }
    }
}
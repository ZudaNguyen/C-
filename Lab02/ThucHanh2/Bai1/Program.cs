/*
* CHƯƠNG TRÌNH TÍNH TUỔI SINH VIÊN (OOP)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 22/09/2026
*
* Phát biểu đề bài: Xây dựng lớp có đầy đủ Field, Constructor, Property, Method. Viết chương trình nhập thông tin sinh viên (họ tên, năm sinh). Tính và xuất tuổi sinh viên này[cite: 12].
* Ý tưởng: 
*   - Tạo lớp SinhVien.
*   - Fields: _hoTen, _namSinh (để private che giấu dữ liệu).
*   - Properties: HoTen, NamSinh (để public giao tiếp với bên ngoài).
*   - Constructor: Hàm tạo mặc định và hàm tạo có tham số.
*   - Methods: Nhap(), Xuat(), và TinhTuoi() (lấy năm hiện tại trừ năm sinh).
*/

using System;

namespace Bai1_1
{
    // Xây dựng lớp SinhVien theo chuẩn OOP
    class SinhVien
    {
        // 1. Fields (Trường dữ liệu - thường để private)
        private string _hoTen;
        private int _namSinh;

        // 2. Constructors (Hàm khởi tạo)
        // Constructor mặc định (không tham số)
        public SinhVien()
        {
            _hoTen = "Chua cap nhat";
            _namSinh = 2000;
        }

        // Constructor có tham số
        public SinhVien(string hoTen, int namSinh)
        {
            _hoTen = hoTen;
            _namSinh = namSinh;
        }

        // 3. Properties (Thuộc tính - dùng để get/set giá trị cho Field)
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }

        public int NamSinh
        {
            get { return _namSinh; }
            set { _namSinh = value; }
        }

        // 4. Methods (Phương thức)
        // Phương thức tính tuổi
        public int TinhTuoi()
        {
            int namHienTai = DateTime.Now.Year;
            return namHienTai - _namSinh;
        }

        // Phương thức nhập thông tin
        public void Nhap()
        {
            Console.Write("Nhap ho ten sinh vien: ");
            HoTen = Console.ReadLine(); // Gán qua Property

            Console.Write("Nhap nam sinh: ");
            NamSinh = int.Parse(Console.ReadLine()); // Gán qua Property
        }

        // Phương thức xuất thông tin
        public void Xuat()
        {
            Console.WriteLine("\n--- THONG TIN SINH VIEN ---");
            Console.WriteLine($"Ho ten: {HoTen}");
            Console.WriteLine($"Nam sinh: {NamSinh}");
            Console.WriteLine($"Tuoi hien tai: {TinhTuoi()} tuoi");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Khởi tạo đối tượng sinh viên bằng Constructor mặc định
            SinhVien sv = new SinhVien();

            // Gọi phương thức nhập từ đối tượng
            sv.Nhap();

            // Gọi phương thức xuất (đã bao gồm tính tuổi) từ đối tượng
            sv.Xuat();

            Console.ReadLine();
        }
    }
}
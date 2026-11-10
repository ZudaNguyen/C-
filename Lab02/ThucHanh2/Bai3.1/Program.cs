/*
* CHƯƠNG TRÌNH SẮP XẾP ĐỐI TƯỢNG BẰNG ARRAY.SORT
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Dùng phương thức tĩnh Array.Sort(...) để sắp xếp các đối tượng của một lớp nào đó.
* Ý tưởng: Tạo lớp SinhVien thực thi interface IComparable<SinhVien>. Cài đặt phương thức CompareTo để chỉ định tiêu chí sắp xếp (ví dụ: sắp xếp tăng dần theo Điểm).
*/

using System;

namespace Bai3_1
{
    // Lớp SinhVien kế thừa IComparable để hỗ trợ sắp xếp
    class SinhVien : IComparable<SinhVien>
    {
        public string HoTen { get; set; }
        public double Diem { get; set; }

        public SinhVien(string hoTen, double diem)
        {
            HoTen = hoTen;
            Diem = diem;
        }

        // Cài đặt phương thức so sánh (Bắt buộc khi dùng IComparable)
        public int CompareTo(SinhVien other)
        {
            if (other == null) return 1;
            // Sắp xếp tăng dần theo điểm
            return this.Diem.CompareTo(other.Diem);
        }

        public override string ToString()
        {
            return $"[{HoTen} - Diem: {Diem}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SinhVien[] danhSach = new SinhVien[]
            {
                new SinhVien("Vu", 8.5),
                new SinhVien("An", 9.2),
                new SinhVien("Phat", 7.0)
            };

            Console.WriteLine("--- TRUOC KHI SAP XEP ---");
            foreach (var sv in danhSach) Console.WriteLine(sv);

            // Sử dụng Array.Sort() - Nó sẽ tự động gọi hàm CompareTo bên trong lớp SinhVien
            Array.Sort(danhSach);

            Console.WriteLine("\n--- SAU KHI SAP XEP (Tang dan theo diem) ---");
            foreach (var sv in danhSach) Console.WriteLine(sv);

            Console.ReadLine();
        }
    }
}
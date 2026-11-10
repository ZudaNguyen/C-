/*
* CHƯƠNG TRÌNH QUẢN LÝ ĐIỂM THÍ SINH (ĐA HÌNH)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 24/09/2026
*
* Phát biểu đề bài: Quản lý cuộc thi gồm thí sinh Chuyên và Siêu cúp. Nhập thông tin và tính tổng điểm dựa trên quy tắc riêng của từng đối tượng[cite: 18].
*/

using System;
using System.Collections.Generic;

namespace Bai3_6
{
    // 1. Lớp cơ sở
    abstract class ThiSinh
    {
        public string SBD { get; set; }
        public string HoTen { get; set; }
        public double Bai1 { get; set; }
        public double Bai2 { get; set; }
        public double Bai3 { get; set; }
        protected double TongDiem;

        public virtual void Nhap()
        {
            Console.Write("SBD: "); SBD = Console.ReadLine();
            Console.Write("Ho ten: "); HoTen = Console.ReadLine();
            Console.Write("Diem Bai 1: "); Bai1 = double.Parse(Console.ReadLine());
            Console.Write("Diem Bai 2: "); Bai2 = double.Parse(Console.ReadLine());
            Console.Write("Diem Bai 3: "); Bai3 = double.Parse(Console.ReadLine());
        }

        // Phương thức thuần ảo bắt buộc lớp con phải tự định nghĩa
        public abstract void TinhTongDiem();

        public virtual void Xuat()
        {
            TinhTongDiem();
            Console.Write($"[{SBD}] {HoTen} | B1: {Bai1}, B2: {Bai2}, B3: {Bai3} | TONG DIEM: {TongDiem}");
        }
    }

    // 2. Thí sinh Chuyên
    class ThiSinhChuyen : ThiSinh
    {
        public double TiengAnh { get; set; }

        public override void Nhap()
        {
            Console.WriteLine("--- Thi sinh Chuyen ---");
            base.Nhap();
            Console.Write("Diem Tieng Anh: ");
            TiengAnh = double.Parse(Console.ReadLine());
        }

        public override void TinhTongDiem()
        {
            TongDiem = Bai1 + Bai2 + Bai3;
            // Xử lý điểm thưởng tiếng Anh
            if (TiengAnh >= 7 && TiengAnh <= 8)
                TongDiem += 1;
            else if (TiengAnh >= 9 && TiengAnh <= 10)
                TongDiem += 2;
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($" (Diem TA: {TiengAnh})");
        }
    }

    // 3. Thí sinh Siêu Cúp
    class ThiSinhSieuCup : ThiSinh
    {
        public double CSDL { get; set; }

        public override void Nhap()
        {
            Console.WriteLine("--- Thi sinh Sieu Cup ---");
            base.Nhap();
            Console.Write("Diem CSDL: ");
            CSDL = double.Parse(Console.ReadLine());
        }

        public override void TinhTongDiem()
        {
            // Tổng điểm của 4 bài thi
            TongDiem = Bai1 + Bai2 + Bai3 + CSDL;
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($" (Diem CSDL: {CSDL})");
        }
    }

    // 4. Lớp Cuộc thi quản lý danh sách
    class CuocThi
    {
        private List<ThiSinh> danhSachTS = new List<ThiSinh>();

        public void NhapDanhSach()
        {
            Console.Write("Nhap so luong thi sinh tham gia: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nChon doi tuong thi sinh thu {i + 1} (1: Chuyen, 2: Sieu cup): ");
                int luaChon = int.Parse(Console.ReadLine());

                ThiSinh ts;
                if (luaChon == 1)
                    ts = new ThiSinhChuyen();
                else
                    ts = new ThiSinhSieuCup();

                ts.Nhap();
                danhSachTS.Add(ts);
            }
        }

        public void XuatDanhSach()
        {
            Console.WriteLine("\n=== KET QUA CUOC THI ===");
            foreach (ThiSinh ts in danhSachTS)
            {
                ts.Xuat();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CuocThi ct = new CuocThi();
            ct.NhapDanhSach();
            ct.XuatDanhSach();

            Console.ReadLine();
        }
    }
}
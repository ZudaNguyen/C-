/*
* CHƯƠNG TRÌNH DÃY PHÂN SỐ
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Xây dựng lớp chứa n phân số. Hãy tính tổng của n phân số đó[cite: 16].
* Ý tưởng:
*   - Kế thừa lại ý tưởng lớp PhanSo (có phép cộng và rút gọn).
*   - Tạo lớp DayPhanSo chứa mảng các PhanSo. Hàm TinhTong() sẽ cộng dồn từng phần tử lại.
*/

using System;

namespace Bai2_4_DayPhanSo
{
    class PhanSo
    {
        public int TuSo { get; set; }
        public int MauSo { get; set; }

        public PhanSo(int tu, int mau)
        {
            TuSo = tu;
            MauSo = mau;
            RutGon();
        }

        private void RutGon()
        {
            int a = Math.Abs(TuSo);
            int b = Math.Abs(MauSo);
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            if (a > 0)
            {
                TuSo /= a;
                MauSo /= a;
            }
        }

        // Nạp chồng phép cộng 2 phân số
        public static PhanSo operator +(PhanSo p1, PhanSo p2)
        {
            return new PhanSo(p1.TuSo * p2.MauSo + p2.TuSo * p1.MauSo, p1.MauSo * p2.MauSo);
        }

        public override string ToString() => MauSo == 1 ? $"{TuSo}" : $"{TuSo}/{MauSo}";
    }

    class DayPhanSo
    {
        private PhanSo[] danhSach;
        private int n;

        public void Nhap()
        {
            Console.Write("Nhap so luong phan so (n): ");
            n = int.Parse(Console.ReadLine());
            danhSach = new PhanSo[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nPhan so thu {i + 1}:");
                Console.Write("  Tu so: ");
                int tu = int.Parse(Console.ReadLine());
                Console.Write("  Mau so: ");
                int mau = int.Parse(Console.ReadLine());
                danhSach[i] = new PhanSo(tu, mau);
            }
        }

        public void Xuat()
        {
            Console.Write("\nDay phan so la: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(danhSach[i].ToString() + (i < n - 1 ? " , " : ""));
            }
            Console.WriteLine();
        }

        // Tính tổng dãy
        public PhanSo TinhTong()
        {
            if (n == 0) return new PhanSo(0, 1);

            PhanSo tong = danhSach[0];
            for (int i = 1; i < n; i++)
            {
                tong = tong + danhSach[i];
            }
            return tong;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DayPhanSo day = new DayPhanSo();
            day.Nhap();
            day.Xuat();

            PhanSo tong = day.TinhTong();
            Console.WriteLine($"\nTong cua {day} phan so tren la: {tong}");

            Console.ReadLine();
        }
    }
}
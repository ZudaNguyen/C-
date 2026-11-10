/*
* CHƯƠNG TRÌNH XÂY DỰNG LỚP ĐA THỨC
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Xây dựng lớp đa thức gồm n+1 đơn thức. Viết các constructor, indexer, nhập/xuất và tính giá trị của đa thức tại x.
* Ý tưởng: 
*   - Lưu trữ bậc của đa thức (n) và một mảng số thực heSo[] có kích thước n+1 để chứa các hệ số a0, a1... an.
*   - Indexer this[int i] dùng để thao tác trực tiếp với hệ số thứ i.
*/

using System;

namespace Bai2_3_DaThuc
{
    class DaThuc
    {
        private int n; // Bậc của đa thức
        private double[] heSo; // Mảng lưu n+1 hệ số

        // a. Các loại constructor
        public DaThuc()
        {
            n = 0;
            heSo = new double[1] { 0 };
        }

        public DaThuc(int bac)
        {
            n = bac;
            heSo = new double[n + 1];
        }

        // b. Indexer để truy cập đơn thức thứ i
        public double this[int i]
        {
            get
            {
                if (i >= 0 && i <= n) return heSo[i];
                throw new IndexOutOfRangeException("Chi so vuot qua bac cua da thuc.");
            }
            set
            {
                if (i >= 0 && i <= n) heSo[i] = value;
                else throw new IndexOutOfRangeException("Chi so vuot qua bac cua da thuc.");
            }
        }

        // c. Nhập đa thức
        public void Nhap()
        {
            Console.Write("Nhap bac cua da thuc (n): ");
            n = int.Parse(Console.ReadLine());
            heSo = new double[n + 1];

            for (int i = 0; i <= n; i++)
            {
                Console.Write($"Nhap he so a{i} (cho x^{i}): ");
                heSo[i] = double.Parse(Console.ReadLine());
            }
        }

        // c. Xuất đa thức
        public void Xuat()
        {
            Console.Write("P(x) = ");
            for (int i = 0; i <= n; i++)
            {
                if (heSo[i] != 0)
                {
                    if (i > 0 && heSo[i] > 0) Console.Write(" + ");
                    else if (heSo[i] < 0) Console.Write(" - ");

                    double val = Math.Abs(heSo[i]);
                    if (i == 0) Console.Write($"{val}");
                    else if (i == 1) Console.Write($"{val}x");
                    else Console.Write($"{val}x^{i}");
                }
            }
            Console.WriteLine();
        }

        // d. Tính giá trị của đa thức
        public double TinhGiaTri(double x)
        {
            double ketQua = 0;
            for (int i = 0; i <= n; i++)
            {
                ketQua += heSo[i] * Math.Pow(x, i);
            }
            return ketQua;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DaThuc p = new DaThuc();
            p.Nhap();
            p.Xuat();

            // Test Indexer
            Console.WriteLine($"\nHe so a0 cua da thuc la: {p[0]}");

            // Tính giá trị
            Console.Write("\nNhap gia tri x can tinh: ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine($"Gia tri P({x}) = {p.TinhGiaTri(x)}");

            Console.ReadLine();
        }
    }
}
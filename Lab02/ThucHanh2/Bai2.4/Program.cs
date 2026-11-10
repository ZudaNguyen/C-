/*
* CHƯƠNG TRÌNH XÂY DỰNG LỚP MẢNG 2 CHIỀU
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Xây dựng lớp mảng 2 chiều kích thước nxm. Viết constructor, indexer [i,j], nhập/xuất, tìm các số nguyên tố[cite: 15].
*/

using System;
using System.Collections.Generic;

namespace Bai2_4
{
    class MangHaiChieu
    {
        private int[,] matrix;
        private int soDong;
        private int soCot;

        // a. Các loại constructor
        public MangHaiChieu()
        {
            soDong = 0;
            soCot = 0;
            matrix = new int[0, 0];
        }

        public MangHaiChieu(int dong, int cot)
        {
            soDong = dong;
            soCot = cot;
            matrix = new int[soDong, soCot];
        }

        // b. Indexer truy cập phần tử (i, j)
        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < soDong && j >= 0 && j < soCot) return matrix[i, j];
                throw new IndexOutOfRangeException("Chi so vuot qua kich thuoc ma tran.");
            }
            set
            {
                if (i >= 0 && i < soDong && j >= 0 && j < soCot) matrix[i, j] = value;
                else throw new IndexOutOfRangeException("Chi so vuot qua kich thuoc ma tran.");
            }
        }

        // c. Nhập mảng
        public void Nhap()
        {
            Console.Write("Nhap so dong (n): ");
            soDong = int.Parse(Console.ReadLine());
            Console.Write("Nhap so cot (m): ");
            soCot = int.Parse(Console.ReadLine());

            matrix = new int[soDong, soCot];

            for (int i = 0; i < soDong; i++)
            {
                for (int j = 0; j < soCot; j++)
                {
                    Console.Write($"Nhap phan tu [{i},{j}]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }

        // c. Xuất mảng
        public void Xuat()
        {
            Console.WriteLine("--- MA TRAN ---");
            for (int i = 0; i < soDong; i++)
            {
                for (int j = 0; j < soCot; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        // Hàm phụ kiểm tra nguyên tố
        private bool KiemTraNguyenTo(int so)
        {
            if (so < 2) return false;
            for (int i = 2; i <= Math.Sqrt(so); i++)
                if (so % i == 0) return false;
            return true;
        }

        // d. Tìm các số nguyên tố trong mảng
        public int[] TimSoNguyenTo()
        {
            List<int> primes = new List<int>();
            for (int i = 0; i < soDong; i++)
            {
                for (int j = 0; j < soCot; j++)
                {
                    if (KiemTraNguyenTo(matrix[i, j]))
                    {
                        primes.Add(matrix[i, j]);
                    }
                }
            }
            return primes.ToArray();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MangHaiChieu m2c = new MangHaiChieu();
            m2c.Nhap();
            m2c.Xuat();

            // Test Indexer
            Console.WriteLine($"\nGia tri tai dong 0 cot 0 la: {m2c[0, 0]}");

            int[] nguyenTo = m2c.TimSoNguyenTo();
            Console.WriteLine("Cac so nguyen to trong ma tran la:");
            Console.WriteLine(string.Join(", ", nguyenTo));

            Console.ReadLine();
        }
    }
}
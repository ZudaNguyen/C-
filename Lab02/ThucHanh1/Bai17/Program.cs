/*
* CHƯƠNG TRÌNH XỬ LÝ MẢNG 2 CHIỀU (SINH NGẪU NHIÊN & TÁCH CHẴN LẺ)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 21/09/2026
*
* Phát biểu đề bài: Viết các phương thức: Sinh ngẫu nhiên mảng A[nxm] đoạn [10, 100], in mảng, trả về hai mảng (mảng số chẵn và mảng số lẻ)[cite: 10].
* Ý tưởng: Xây dựng lớp MangHaiChieu. Dùng Random.Next(10, 101) để sinh số. Dùng từ khóa 'out' để trả về cùng lúc 2 mảng 1 chiều chứa số chẵn và số lẻ bằng cách duyệt qua ma trận và lưu vào List tạm trước khi xuất ra.
*/

using System;
using System.Collections.Generic;

namespace Bai17
{
    class MangHaiChieu
    {
        private int[,] A;
        private int n;
        private int m;

        // Sinh ngẫu nhiên mảng
        public void SinhMangNgauNhien(int dong, int cot)
        {
            n = dong;
            m = cot;
            A = new int[n, m];
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // Next(10, 101) sẽ sinh số từ 10 đến 100
                    A[i, j] = rnd.Next(10, 101);
                }
            }
        }

        // In mảng 2 chiều
        public void InMang()
        {
            Console.WriteLine("Ma tran A:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(A[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        // Trả về hai mảng: chẵn và lẻ
        public void TraVeChanLe(out int[] mangChan, out int[] mangLe)
        {
            List<int> danhSachChan = new List<int>();
            List<int> danhSachLe = new List<int>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (A[i, j] % 2 == 0)
                        danhSachChan.Add(A[i, j]);
                    else
                        danhSachLe.Add(A[i, j]);
                }
            }

            mangChan = danhSachChan.ToArray();
            mangLe = danhSachLe.ToArray();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so dong (n): ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Nhap so cot (m): ");
            int m = int.Parse(Console.ReadLine());

            MangHaiChieu maTran = new MangHaiChieu();

            // 1. Sinh ngẫu nhiên
            maTran.SinhMangNgauNhien(n, m);

            // 2. In mảng
            maTran.InMang();

            // 3. Trả về và in mảng chẵn, lẻ
            maTran.TraVeChanLe(out int[] mangChan, out int[] mangLe);

            Console.WriteLine("\nCac so chan trong ma tran la:");
            Console.WriteLine(string.Join(", ", mangChan));

            Console.WriteLine("Cac so le trong ma tran la:");
            Console.WriteLine(string.Join(", ", mangLe));

            Console.ReadLine();
        }
    }
}
/*
* CHƯƠNG TRÌNH XỬ LÝ MẢNG 1 CHIỀU
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 21/09/2026
*
* Phát biểu đề bài: Viết các phương thức thành viên: nhập mảng n phần tử, in mảng, tìm phần tử lớn nhất và nhỏ nhất, trả về mảng các số nguyên tố[cite: 10].
* Ý tưởng: Xây dựng lớp MangMotChieu lưu trữ mảng. Dùng List<int> để lọc các số nguyên tố sau đó chuyển về dạng mảng (ToArray) để trả về đúng yêu cầu.
*/

using System;
using System.Collections.Generic;

namespace Bai15
{
    class MangMotChieu
    {
        private int[] arr;
        private int n;

        // Phương thức nhập mảng
        public void NhapMang()
        {
            Console.Write("Nhap so luong phan tu n: ");
            n = int.Parse(Console.ReadLine());
            arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Nhap phan tu arr[{i}]: ");
                arr[i] = int.Parse(Console.ReadLine());
            }
        }

        // Phương thức in mảng
        public void InMang()
        {
            Console.Write("Cac phan tu trong mang la: ");
            foreach (int item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        // Phương thức tìm phần tử lớn nhất và nhỏ nhất (dùng tham chiếu out)
        public void TimMaxMin(out int max, out int min)
        {
            max = arr[0];
            min = arr[0];
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > max) max = arr[i];
                if (arr[i] < min) min = arr[i];
            }
        }

        // Hàm phụ kiểm tra số nguyên tố
        private bool KiemTraNguyenTo(int so)
        {
            if (so < 2) return false;
            for (int i = 2; i <= Math.Sqrt(so); i++)
            {
                if (so % i == 0) return false;
            }
            return true;
        }

        // Phương thức trả về mảng các số nguyên tố
        public int[] TraVeMangNguyenTo()
        {
            // Dùng List để lưu trữ tạm vì chưa biết trước có bao nhiêu số nguyên tố
            List<int> danhSachSNT = new List<int>();
            foreach (int item in arr)
            {
                if (KiemTraNguyenTo(item))
                {
                    danhSachSNT.Add(item);
                }
            }
            // Chuyển List thành mảng (Array) để trả về
            return danhSachSNT.ToArray();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MangMotChieu mang = new MangMotChieu();
            mang.NhapMang();
            mang.InMang();

            // Tìm Max Min
            mang.TimMaxMin(out int max, out int min);
            Console.WriteLine($"Phan tu lon nhat: {max}, Nho nhat: {min}");

            // Lấy mảng nguyên tố
            int[] mangSNT = mang.TraVeMangNguyenTo();
            Console.Write("Mang cac so nguyen to la: ");
            if (mangSNT.Length > 0)
            {
                foreach (int snt in mangSNT) Console.Write(snt + " ");
            }
            else
            {
                Console.Write("Khong co so nguyen to nao.");
            }

            Console.ReadLine();
        }
    }
}
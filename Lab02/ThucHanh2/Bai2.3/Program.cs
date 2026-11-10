/*
* CHƯƠNG TRÌNH XÂY DỰNG LỚP DÃY SỐ (MẢNG 1 CHIỀU)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Xây dựng lớp dãy số chứa n số nguyên. Viết các loại constructor, indexer, nhập/xuất, tìm các số chẵn[cite: 15].
* Ý tưởng: 
*   - Lưu trữ mảng int[] arr bên trong.
*   - Cung cấp Indexer để thao tác với từng phần tử.
*   - Hàm TimSoChan sẽ trả về một mảng nguyên thủy chứa các số thỏa điều kiện.
*/

using System;
using System.Collections.Generic;

namespace Bai2_3
{
    class DaySo
    {
        private int[] arr;
        private int n;

        // a. Các loại Constructor
        // Constructor mặc định
        public DaySo()
        {
            n = 0;
            arr = new int[0];
        }

        // Constructor khởi tạo mảng với kích thước n
        public DaySo(int kichThuoc)
        {
            n = kichThuoc;
            arr = new int[n];
        }

        // b. Indexer để truy cập phần tử thứ i
        public int this[int i]
        {
            get
            {
                if (i >= 0 && i < n) return arr[i];
                throw new IndexOutOfRangeException("Chi so khong hop le.");
            }
            set
            {
                if (i >= 0 && i < n) arr[i] = value;
                else throw new IndexOutOfRangeException("Chi so khong hop le.");
            }
        }

        // c. Nhập dãy số
        public void Nhap()
        {
            Console.Write("Nhap so luong phan tu cua day (n): ");
            n = int.Parse(Console.ReadLine());
            arr = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Phan tu [{i}]: ");
                arr[i] = int.Parse(Console.ReadLine());
            }
        }

        // c. Xuất dãy số
        public void Xuat()
        {
            Console.Write("Day so: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }

        // d. Tìm các số chẵn
        public int[] TimSoChan()
        {
            List<int> listChan = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    listChan.Add(arr[i]);
                }
            }
            return listChan.ToArray(); // Chuyển List thành mảng trả về
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DaySo ds = new DaySo();
            ds.Nhap();
            ds.Xuat();

            // Test Indexer
            Console.WriteLine($"\nPhan tu tai vi tri index 0 la: {ds[0]}");

            int[] chan = ds.TimSoChan();
            Console.Write("Cac so chan trong day la: ");
            Console.WriteLine(string.Join(", ", chan));

            Console.ReadLine();
        }
    }
}
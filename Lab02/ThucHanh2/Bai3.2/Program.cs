/*
* CHƯƠNG TRÌNH VIẾT HÀM SẮP XẾP TỔNG QUÁT (INTERFACE)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 24/09/2026
*
* Phát biểu đề bài: Viết phương thức sắp xếp một mảng tổng quát bằng interface (mô phỏng phương thức Array.Sort(...)).
* Ý tưởng: Viết hàm Generic <T> giới hạn T phải là IComparable<T>. Dùng thuật toán Bubble Sort duyệt mảng và gọi hàm CompareTo() để so sánh và hoán vị.
*/

using System;

namespace Bai3_2
{
    class Program
    {
        // Hàm sắp xếp tổng quát (Mô phỏng Array.Sort)
        // Ràng buộc (where T : IComparable<T>) đảm bảo kiểu T có thể so sánh được
        static void MySort<T>(T[] arr) where T : IComparable<T>
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Nếu arr[j] lớn hơn arr[j+1] thì hoán vị
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        T temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // Test với mảng số nguyên
            int[] mangSo = { 42, 15, 8, 23, 4 };
            Console.WriteLine("Mang so nguyen ban dau: " + string.Join(", ", mangSo));

            MySort(mangSo); // Gọi hàm sắp xếp tự viết
            Console.WriteLine("Mang so nguyen sau khi sap xep: " + string.Join(", ", mangSo));

            // Test với mảng chuỗi
            string[] mangChuoi = { "Vu", "Phat", "An", "Binh" };
            Console.WriteLine("\nMang chuoi ban dau: " + string.Join(", ", mangChuoi));

            MySort(mangChuoi);
            Console.WriteLine("Mang chuoi sau khi sap xep: " + string.Join(", ", mangChuoi));

            Console.ReadLine();
        }
    }
}
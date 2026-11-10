/*
* CHƯƠNG TRÌNH VIẾT HÀM SẮP XẾP TỔNG QUÁT (DELEGATE)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 24/09/2026
*
* Phát biểu đề bài: Viết phương thức sắp xếp một mảng tổng quát bằng delegate[cite: 17].
* Ý tưởng: Khai báo một Delegate tên là SoSanh. Hàm MySort sẽ nhận mảng và delegate này làm tham số. Khi cần so sánh, hàm sẽ gọi delegate thay vì dùng toán tử > hay <.
*/

using System;

namespace Bai3_3
{
    // Khai báo một delegate nhận 2 tham số kiểu T và trả về bool (kiểm tra có cần đổi chỗ không)
    public delegate bool SoSanhDelegate<T>(T a, T b);

    class Program
    {
        // Hàm sắp xếp nhận Delegate làm tham số
        static void MySort<T>(T[] arr, SoSanhDelegate<T> kieuSoSanh)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Dùng delegate để quyết định việc hoán vị
                    if (kieuSoSanh(arr[j], arr[j + 1]))
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
            int[] mangSo = { 42, 15, 8, 23, 4 };
            Console.WriteLine("Mang ban dau: " + string.Join(", ", mangSo));

            // Gọi MySort, truyền vào biểu thức Lambda làm Delegate
            // (a, b) => a > b: Sắp xếp tăng dần
            MySort(mangSo, (a, b) => a > b);
            Console.WriteLine("Sap xep tang dan: " + string.Join(", ", mangSo));

            // Thay đổi quy tắc so sánh ngay lúc gọi hàm mà không cần sửa code MySort
            // (a, b) => a < b: Sắp xếp giảm dần
            MySort(mangSo, (a, b) => a < b);
            Console.WriteLine("Sap xep giam dan: " + string.Join(", ", mangSo));

            Console.ReadLine();
        }
    }
}
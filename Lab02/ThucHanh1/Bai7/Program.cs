/*
* CHƯƠNG TRÌNH KIỂM TRA SỐ NGUYÊN TỐ
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Xây dựng lớp có phương thức kiểm tra n có phải là số nguyên tố hay không[cite: 8].
* Ý tưởng: Số nguyên tố là số lớn hơn 1 và chỉ chia hết cho 1 và chính nó. Dùng vòng lặp chạy từ 2 đến căn bậc 2 của n để kiểm tra, phương thức trả về kiểu bool (true/false).
*/

using System;

namespace Bai7
{
    class KiemTraToanHoc
    {
        // Phương thức trả về bool
        public bool KiemTraNguyenTo(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so nguyen n can kiem tra: ");
            int n = int.Parse(Console.ReadLine());

            KiemTraToanHoc kt = new KiemTraToanHoc();

            // Gọi phương thức và xử lý kết quả bool
            if (kt.KiemTraNguyenTo(n))
            {
                Console.WriteLine($"{n} LA so nguyen to.");
            }
            else
            {
                Console.WriteLine($"{n} KHONG PHAI la so nguyen to.");
            }
            Console.ReadLine();
        }
    }
}
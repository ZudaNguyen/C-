/*
* CHƯƠNG TRÌNH THỐNG KÊ VÀ PHÂN NHÓM MẢNG SỐ (BÀI 3.1)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 30/09/2026
*
* Phát biểu đề bài: Cho mảng số nguyên. Thực hiện đếm phần tử chẵn/lẻ, tính tổng/max/min, đếm số giá trị khác nhau và phân nhóm theo số dư khi chia cho 5[cite: 2].
* Ý tưởng: 
*   - Sử dụng các phương thức mở rộng của LINQ như Count(), Sum(), Max(), Min(), Distinct() để thống kê[cite: 2].
*   - Sử dụng GroupBy() với biểu thức lambda (n % 5) để phân nhóm các phần tử[cite: 2].
*/

using System;
using System.Linq;

namespace BaiThucHanhLINQ_Bai31
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] mangSo = { 50, 42, 12, 3, 9, 8, 1, 50, 3, 42, 85 }; 

            // a. Cho biết tổng số phần tử, số phần tử chẵn và số phần tử lẻ[cite: 2]
            Console.WriteLine("a. Thống kê số lượng:");
            Console.WriteLine($"   - Tổng số phần tử: {mangSo.Count()}");
            Console.WriteLine($"   - Số phần tử chẵn: {mangSo.Count(n => n % 2 == 0)}");
            Console.WriteLine($"   - Số phần tử lẻ: {mangSo.Count(n => n % 2 != 0)}\n");

            // b. Tính tổng các giá trị, giá trị lớn nhất và giá trị nhỏ nhất[cite: 2]
            Console.WriteLine("b. Thống kê giá trị:");
            Console.WriteLine($"   - Tổng các giá trị: {mangSo.Sum()}");
            Console.WriteLine($"   - Giá trị lớn nhất: {mangSo.Max()}");
            Console.WriteLine($"   - Giá trị nhỏ nhất: {mangSo.Min()}\n");

            // c. Cho biết có bao nhiêu giá trị khác nhau trong mảng[cite: 2]
            int soGiaTriKhacNhau = mangSo.Distinct().Count();
            Console.WriteLine($"c. Số giá trị khác nhau trong mảng: {soGiaTriKhacNhau}\n");

            // d. Phân nhóm các phần tử theo số dư khi chia cho 5[cite: 2]
            Console.WriteLine("d. Phân nhóm theo số dư khi chia cho 5:");
            var nhomSoDu = mangSo.GroupBy(n => n % 5).OrderBy(g => g.Key);
            foreach (var nhom in nhomSoDu)
            {
                Console.WriteLine($"   - Số dư {nhom.Key}: {string.Join(", ", nhom)}");
            }

            Console.ReadLine();
        }
    }
}
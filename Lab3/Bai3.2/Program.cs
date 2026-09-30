/*
* CHƯƠNG TRÌNH THỐNG KÊ VÀ PHÂN NHÓM MẢNG CHUỖI (BÀI 3.2)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 30/09/2026
*
* Phát biểu đề bài: Cho mảng chuỗi tên món ăn. Thực hiện tìm món có tên ngắn/dài nhất, phân nhóm theo từ đầu tiên, và đếm số món bắt đầu bằng từ "Bánh"[cite: 2].
* Ý tưởng: 
*   - Sử dụng Min()/Max() kết hợp Length để tìm độ dài ngắn/dài nhất, sau đó dùng Where() để lọc[cite: 2].
*   - Sử dụng GroupBy() kết hợp Split(' ')[0] để trích xuất từ đầu tiên làm khóa phân nhóm[cite: 2].
*   - Sử dụng Count() kết hợp điều kiện StartsWith() hoặc Split để đếm[cite: 2].
*/

using System;
using System.Linq;

namespace BaiThucHanhLINQ_Bai32
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] monAn = { "Bún bò Huế", "Hủ tiếu heo", "Bánh canh", "Bánh mì",
                               "Nước Cà phê", "Mì quảng", "Cơm tấm", "Nước Chanh dây",
                               "Mì xào", "Bún riêu", "Bánh cuốn", "Mì gói", "Bún chả", "Hủ tiếu Nam vang" }; 

            // a. Tìm các phần tử có chiều dài ngắn nhất và dài nhất[cite: 2]
            int minLen = monAn.Min(s => s.Length);
            int maxLen = monAn.Max(s => s.Length);

            var nganNhat = monAn.Where(s => s.Length == minLen);
            var daiNhat = monAn.Where(s => s.Length == maxLen);

            Console.WriteLine("a. Chiều dài tên món ăn:");
            Console.WriteLine($"   - Ngắn nhất ({minLen} ký tự): {string.Join(", ", nganNhat)}");
            Console.WriteLine($"   - Dài nhất ({maxLen} ký tự): {string.Join(", ", daiNhat)}\n");

            // b. Phân nhóm theo từ đầu tiên của tên món và liệt kê các phần tử trong từng nhóm[cite: 2]
            Console.WriteLine("b. Phân nhóm theo từ đầu tiên:");
            var nhomMonAn = monAn.GroupBy(s => s.Split(' ')[0]);
            foreach (var nhom in nhomMonAn)
            {
                Console.WriteLine($"   - Nhóm '{nhom.Key}': {string.Join(", ", nhom)}");
            }

            // c. Đếm số phần tử có từ đầu tiên là “Bánh”[cite: 2]
            int demBanh = monAn.Count(s => s.Split(' ')[0] == "Bánh");
            Console.WriteLine($"\nc. Số phần tử có từ đầu tiên là \"Bánh\": {demBanh}");

            Console.ReadLine();
        }
    }
}
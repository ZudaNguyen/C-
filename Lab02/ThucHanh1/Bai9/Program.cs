/*
* CHƯƠNG TRÌNH TÌM MAX VÀ MIN BẰNG THAM CHIẾU OUT
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Xây dựng lớp có phương thức tìm giá trị lớn nhất và giá trị nhỏ nhất của ba số thực[cite: 8].
* Ý tưởng: Một phương thức bình thường chỉ return được 1 giá trị. Sử dụng từ khóa out để phương thức có thể trả về cùng lúc cả giá trị Max và Min.
*/

using System;

namespace Bai9
{
    class XuLySoThuc
    {
        // Phương thức sử dụng tham chiếu out để trả về 2 kết quả
        public void TimMaxMin(double a, double b, double c, out double max, out double min)
        {
            max = Math.Max(a, Math.Max(b, c));
            min = Math.Min(a, Math.Min(b, c));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so thuc a: ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Nhap so thuc b: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Nhap so thuc c: ");
            double c = double.Parse(Console.ReadLine());

            // Khai báo biến để hứng giá trị từ phương thức
            double giaTriMax, giaTriMin;

            XuLySoThuc xuLy = new XuLySoThuc();
            // Gọi hàm và truyền từ khóa out
            xuLy.TimMaxMin(a, b, c, out giaTriMax, out giaTriMin);

            Console.WriteLine($"Gia tri lon nhat la: {giaTriMax}");
            Console.WriteLine($"Gia tri nho nhat la: {giaTriMin}");
            Console.ReadLine();
        }
    }
}
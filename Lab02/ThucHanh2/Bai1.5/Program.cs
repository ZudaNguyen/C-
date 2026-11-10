/*
* CHƯƠNG TRÌNH XÂY DỰNG LỚP ĐƠN THỨC
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 22/09/2026
*
* Phát biểu đề bài: Xây dựng lớp Đơn thức thực hiện tính giá trị P(x) = a.x^n và tính đạo hàm Q(x) = P'(x) = a.n.x^(n-1)[cite: 14].
* Ý tưởng: 
*   - Lưu trữ hệ số a (số thực) và số mũ n (số nguyên).
*   - Phương thức TinhGiaTri(x) dùng hàm Math.Pow để tính lũy thừa.
*   - Phương thức DaoHam() trả về một đối tượng DonThuc mới với hệ số mới (a*n) và số mũ mới (n-1). Nếu n=0 thì đạo hàm bằng 0.
*/

using System;

namespace Bai1_5
{
    class DonThuc
    {
        private double heSo; // a
        private int soMu;    // n

        // Constructor
        public DonThuc(double heSo, int soMu)
        {
            this.heSo = heSo;
            this.soMu = soMu >= 0 ? soMu : 0; // Đảm bảo số mũ không âm theo đề bài
        }

        // Xuất đơn thức ra chuỗi để dễ nhìn
        public override string ToString()
        {
            if (heSo == 0) return "0";
            if (soMu == 0) return $"{heSo}";
            if (soMu == 1) return $"{heSo}x";
            return $"{heSo}x^{soMu}";
        }

        // (a) Tính giá trị đơn thức P(x) = a.x^n
        public double TinhGiaTri(double x)
        {
            return heSo * Math.Pow(x, soMu);
        }

        // (b) Đạo hàm đơn thức Q(x) = P'(x) = a.n.x^(n-1)
        public DonThuc DaoHam()
        {
            if (soMu == 0)
            {
                // Đạo hàm của hằng số là 0
                return new DonThuc(0, 0);
            }

            // Hệ số mới = a * n
            double heSoMoi = heSo * soMu;
            // Số mũ mới = n - 1
            int soMuMoi = soMu - 1;

            return new DonThuc(heSoMoi, soMuMoi);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap he so a: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Nhap so mu n (so nguyen khong am): ");
            int n = int.Parse(Console.ReadLine());

            // Khởi tạo đơn thức
            DonThuc P = new DonThuc(a, n);
            Console.WriteLine($"\nDon thuc vua nhap P(x) = {P.ToString()}");

            // Test tính giá trị
            Console.Write("\nNhap gia tri x de tinh P(x): ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine($"Gia tri cua P({x}) = {P.TinhGiaTri(x)}");

            // Test đạo hàm
            DonThuc Q = P.DaoHam();
            Console.WriteLine($"\nDao ham cua don thuc la Q(x) = {Q.ToString()}");

            Console.ReadLine();
        }
    }
}
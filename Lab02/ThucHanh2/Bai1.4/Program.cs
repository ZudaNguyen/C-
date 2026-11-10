/*
* CHƯƠNG TRÌNH THIẾT KẾ LỚP PHÂN SỐ
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 22/09/2026
*
* Phát biểu đề bài: Thiết kế lớp Phân số có Constructor (mặc nhiên, sao chép, tham số), Override ToString(), và Overload các toán tử một ngôi, hai ngôi, so sánh.
* Ý tưởng: 
*   - Lưu trữ Tử số và Mẫu số. Luôn tối giản phân số sau khi tính toán bằng cách tìm Ước chung lớn nhất (UCLN).
*   - Sử dụng từ khóa 'operator' để nạp chồng các phép toán +, -, *, /, ==, !=, >, <, >=, <=.
*/

using System;

namespace Bai1_4
{
    class PhanSo
    {
        private int tuSo;
        private int mauSo;

        // 1. Constructors
        // Constructor mặc nhiên
        public PhanSo()
        {
            tuSo = 0;
            mauSo = 1;
        }

        // Constructor có tham số
        public PhanSo(int tu, int mau)
        {
            if (mau == 0)
                throw new ArgumentException("Mau so khong duoc bang 0.");
            tuSo = tu;
            mauSo = mau;
            RutGon();
        }

        // Constructor sao chép
        public PhanSo(PhanSo p)
        {
            this.tuSo = p.tuSo;
            this.mauSo = p.mauSo;
        }

        // Hàm tìm Ước chung lớn nhất để rút gọn
        private int TimUCLN(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Hàm rút gọn phân số
        private void RutGon()
        {
            int ucln = TimUCLN(tuSo, mauSo);
            tuSo /= ucln;
            mauSo /= ucln;

            // Đảm bảo dấu trừ luôn nằm ở tử số
            if (mauSo < 0)
            {
                tuSo = -tuSo;
                mauSo = -mauSo;
            }
        }

        // 2. Override hàm ToString()
        public override string ToString()
        {
            if (mauSo == 1) return tuSo.ToString();
            if (tuSo == 0) return "0";
            return $"{tuSo}/{mauSo}";
        }

        // 3. Overload các toán tử
        // Một ngôi: +, -
        public static PhanSo operator +(PhanSo a) => a;
        public static PhanSo operator -(PhanSo a) => new PhanSo(-a.tuSo, a.mauSo);

        // Hai ngôi: +, -, *, /
        public static PhanSo operator +(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.mauSo + b.tuSo * a.mauSo, a.mauSo * b.mauSo);
        }

        public static PhanSo operator -(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.mauSo - b.tuSo * a.mauSo, a.mauSo * b.mauSo);
        }

        public static PhanSo operator *(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.tuSo, a.mauSo * b.mauSo);
        }

        public static PhanSo operator /(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.mauSo, a.mauSo * b.tuSo);
        }

        // So sánh: >, <, >=, <=, ==, !=
        // Chuyển về số thực để so sánh cho nhanh gọn trong bài thực hành
        public static bool operator >(PhanSo a, PhanSo b) => (double)a.tuSo / a.mauSo > (double)b.tuSo / b.mauSo;
        public static bool operator <(PhanSo a, PhanSo b) => (double)a.tuSo / a.mauSo < (double)b.tuSo / b.mauSo;
        public static bool operator >=(PhanSo a, PhanSo b) => (double)a.tuSo / a.mauSo >= (double)b.tuSo / b.mauSo;
        public static bool operator <=(PhanSo a, PhanSo b) => (double)a.tuSo / a.mauSo <= (double)b.tuSo / b.mauSo;
        public static bool operator ==(PhanSo a, PhanSo b) => a.tuSo == b.tuSo && a.mauSo == b.mauSo;
        public static bool operator !=(PhanSo a, PhanSo b) => !(a == b);

        // Bắt buộc override Equals và GetHashCode khi đã overload toán tử ==, !=
        public override bool Equals(object obj)
        {
            if (obj is PhanSo)
                return this == (PhanSo)obj;
            return false;
        }
        public override int GetHashCode() => tuSo.GetHashCode() ^ mauSo.GetHashCode();
    }

    class Program
    {
        static void Main(string[] args)
        {
            PhanSo p1 = new PhanSo(1, 2);
            PhanSo p2 = new PhanSo(3, 4);

            Console.WriteLine($"Phan so 1 (p1): {p1}");
            Console.WriteLine($"Phan so 2 (p2): {p2}");

            Console.WriteLine("\n--- PHEP TOAN ---");
            Console.WriteLine($"p1 + p2 = {p1 + p2}");
            Console.WriteLine($"p1 - p2 = {p1 - p2}");
            Console.WriteLine($"p1 * p2 = {p1 * p2}");
            Console.WriteLine($"p1 / p2 = {p1 / p2}");
            Console.WriteLine($"Toan tu 1 ngoi (-p1) = {-p1}");

            Console.WriteLine("\n--- SO SANH ---");
            Console.WriteLine($"p1 > p2: {p1 > p2}");
            Console.WriteLine($"p1 < p2: {p1 < p2}");
            Console.WriteLine($"p1 == p2: {p1 == p2}");

            Console.ReadLine();
        }
    }
}
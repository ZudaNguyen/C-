/*
* CHƯƠNG TRÌNH THIẾT KẾ LỚP POINT (ĐIỂM)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 22/09/2026
*
* Phát biểu đề bài: Thiết kế lớp Point có Field (x,y), Property (X,Y), default constructor, Input/Output, override ToString, phép toán (+, -, lấy âm) và tính khoảng cách, trung điểm bằng phương thức tĩnh/thành viên.
* Ý tưởng: 
*   - Sử dụng nạp chồng toán tử (operator overloading) cho các phép +, -, lấy âm.
*   - Phương thức thành viên (gọi từ 1 đối tượng, truyền vào 1 đối tượng khác).
*   - Phương thức tĩnh (static) truyền vào 2 đối tượng cần xử lý.
*/

using System;

namespace Bai1_2
{
    class Point
    {
        // 1. Field
        private double x;
        private double y;

        // 2. Property
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        // 3. Constructor
        // Default constructor khởi tạo giá trị ban đầu cho x và y bằng 0
        public Point()
        {
            this.x = 0;
            this.y = 0;
        }

        // Constructor có tham số (Hỗ trợ thêm để gán giá trị nhanh)
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // 4. Method: Input và Output
        public void Input()
        {
            Console.Write("Nhap x: ");
            this.x = double.Parse(Console.ReadLine());
            Console.Write("Nhap y: ");
            this.y = double.Parse(Console.ReadLine());
        }

        public void Output()
        {
            Console.WriteLine(this.ToString());
        }

        // 5. Override hàm ToString() để xuất Point
        public override string ToString()
        {
            return $"({x}, {y})";
        }

        // 6. Phép toán: +, -, lấy âm (-)
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public static Point operator -(Point a)
        {
            return new Point(-a.x, -a.y);
        }

        // 7. (a) Khoảng cách giữa 2 điểm
        // Phương thức thành viên
        public double KhoangCach(Point other)
        {
            return Math.Sqrt(Math.Pow(this.x - other.x, 2) + Math.Pow(this.y - other.y, 2));
        }

        // Phương thức tĩnh
        public static double KhoangCach(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        // 8. (b) Trung điểm của 2 điểm
        // Phương thức thành viên
        public Point TrungDiem(Point other)
        {
            return new Point((this.x + other.x) / 2, (this.y + other.y) / 2);
        }

        // Phương thức tĩnh
        public static Point TrungDiem(Point a, Point b)
        {
            return new Point((a.x + b.x) / 2, (a.y + b.y) / 2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- NHAP DIEM A ---");
            Point A = new Point();
            A.Input();

            Console.WriteLine("--- NHAP DIEM B ---");
            Point B = new Point();
            B.Input();

            Console.WriteLine("\n--- KET QUA ---");
            Console.WriteLine($"Diem A: {A.ToString()}");
            Console.WriteLine($"Diem B: {B.ToString()}");

            // Phép toán
            Point tong = A + B;
            Point hieu = A - B;
            Point amA = -A;
            Console.WriteLine($"A + B = {tong.ToString()}");
            Console.WriteLine($"A - B = {hieu.ToString()}");
            Console.WriteLine($"Am cua A = {amA.ToString()}");

            // Khoảng cách
            Console.WriteLine($"Khoang cach (Thanh vien): {A.KhoangCach(B)}");
            Console.WriteLine($"Khoang cach (Tinh): {Point.KhoangCach(A, B)}");

            // Trung điểm
            Console.WriteLine($"Trung diem (Thanh vien): {A.TrungDiem(B).ToString()}");
            Console.WriteLine($"Trung diem (Tinh): {Point.TrungDiem(A, B).ToString()}");

            Console.ReadLine();
        }
    }
}
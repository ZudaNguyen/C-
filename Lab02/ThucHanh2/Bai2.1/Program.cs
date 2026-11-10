/*
* CHƯƠNG TRÌNH THIẾT KẾ LỚP ARRAYPOINT
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Thiết kế lớp ArrayPoint lưu trữ các Point. Có Field là một ArrayList các Point và Indexer cho phép truy cập Point thứ i.
* Ý tưởng: 
*   - Sử dụng System.Collections.ArrayList để làm kho lưu trữ động.
*   - Tạo Indexer 'public Point this[int i]' để có thể gọi arrPoint[i] thay vì phải viết các hàm Get/Set rườm rà.
*/

using System;
using System.Collections; // Thư viện chứa ArrayList

namespace Bai2_1
{
    // Lớp Point cơ bản (Lấy từ bài 1.2 sang để có đối tượng test)
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point(double x, double y) { X = x; Y = y; }
        public override string ToString() => $"({X}, {Y})";
    }

    // Lớp ArrayPoint quản lý danh sách các Point
    class ArrayPoint
    {
        // Field: Một ArrayList
        private ArrayList points;

        public ArrayPoint()
        {
            points = new ArrayList();
        }

        // Phương thức hỗ trợ thêm Point vào danh sách
        public void AddPoint(Point p)
        {
            points.Add(p);
        }

        // Số lượng phần tử hiện có
        public int Count
        {
            get { return points.Count; }
        }

        // TẠO INDEXER
        public Point this[int i]
        {
            get
            {
                if (i >= 0 && i < points.Count)
                    return (Point)points[i]; // Ép kiểu vì ArrayList lưu trữ dưới dạng Object
                throw new IndexOutOfRangeException("Chi so vuot qua gioi han cua ArrayList.");
            }
            set
            {
                if (i >= 0 && i < points.Count)
                    points[i] = value;
                else
                    throw new IndexOutOfRangeException("Chi so vuot qua gioi han cua ArrayList.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArrayPoint arr = new ArrayPoint();
            arr.AddPoint(new Point(1.5, 2.0));
            arr.AddPoint(new Point(-3.2, 5.1));
            arr.AddPoint(new Point(0, 0));

            Console.WriteLine("--- DANH SACH CAC POINT TRONG ARRAYLIST ---");
            // Sử dụng Indexer để truy cập như mảng thông thường
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine($"Point thu {i}: {arr[i].ToString()}");
            }

            Console.WriteLine("\n--- TEST THAY DOI GIA TRI BANG INDEXER ---");
            arr[2] = new Point(9.9, 9.9); // Gán giá trị mới thông qua Indexer
            Console.WriteLine($"Point thu 2 sau khi doi: {arr[2].ToString()}");

            Console.ReadLine();
        }
    }
}
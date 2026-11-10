/*
* CHƯƠNG TRÌNH ĐỊNH NGHĨA LỚP PERSON
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 22/09/2026
*
* Phát biểu đề bài: Định nghĩa lớp Person với các dữ liệu: id, name, yob, yod. Có Default/Copy constructor, Input(), Output() và IsLiving()[cite: 13].
* Ý tưởng: 
*   - Copy constructor cho phép tạo ra một đối tượng mới có dữ liệu giống hệt đối tượng được truyền vào.
*   - Hàm IsLiving trả về true nếu yod == 0 (tức là chưa mất), ngược lại trả về false.
*/

using System;

namespace Bai1_3
{
    class Person
    {
        // Dữ liệu thành viên
        private string id;
        private string name;
        private int yob; // năm sinh
        private int yod; // năm mất (0 nếu còn sống)

        // Default Constructor
        public Person()
        {
            id = "Chưa có";
            name = "Chưa có";
            yob = 0;
            yod = 0;
        }

        // Copy constructor
        public Person(Person other)
        {
            this.id = other.id;
            this.name = other.name;
            this.yob = other.yob;
            this.yod = other.yod;
        }

        // Nhập dữ liệu
        public void Input()
        {
            Console.Write("Nhap ID: ");
            id = Console.ReadLine();
            Console.Write("Nhap ten (Name): ");
            name = Console.ReadLine();
            Console.Write("Nhap nam sinh (YOB): ");
            yob = int.Parse(Console.ReadLine());
            Console.Write("Nhap nam mat (YOD, nhap 0 neu con song): ");
            yod = int.Parse(Console.ReadLine());
        }

        // Xuất dữ liệu
        public void Output()
        {
            Console.WriteLine($"ID: {id} | Ten: {name} | Nam sinh: {yob} | Nam mat: {(yod == 0 ? "Chua mat" : yod.ToString())}");
        }

        // Kiểm tra còn sống hay không
        public bool IsLiving()
        {
            // Trả về false hay true tùy thuộc vào yod bằng 0 hay khác 0
            if (yod == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test nhập xuất cơ bản
            Console.WriteLine("--- NHAP THONG TIN PERSON 1 ---");
            Person p1 = new Person();
            p1.Input();

            Console.WriteLine("\n--- THONG TIN PERSON 1 ---");
            p1.Output();
            if (p1.IsLiving())
                Console.WriteLine("-> Nguoi nay hien dang con song (IsLiving = True)");
            else
                Console.WriteLine("-> Nguoi nay da mat (IsLiving = False)");

            // Test Copy Constructor
            Console.WriteLine("\n--- TAO PERSON 2 (Copy tu Person 1) ---");
            Person p2 = new Person(p1); // Sử dụng copy constructor
            p2.Output();

            Console.ReadLine();
        }
    }
}
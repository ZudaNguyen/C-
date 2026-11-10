/*
* CHƯƠNG TRÌNH QUẢN LÝ NHÂN KHẨU (PERSONLIST)
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 23/09/2026
*
* Phát biểu đề bài: Thiết kế lớp PersonList dựa trên lớp Person. Bao gồm constructor, Input/Output, Add(Person), LivingPeople().
* Ý tưởng: 
*   - Sử dụng List<Person> (mạnh mẽ và an toàn kiểu hơn ArrayList) để quản lý danh sách.
*   - Hàm LivingPeople duyệt qua danh sách hiện tại, nếu ai có Yod == 0 (còn sống) thì Add vào một PersonList mới và trả về đối tượng này.
*/

using System;
using System.Collections.Generic; // Thư viện chứa List<>

namespace Bai2_2
{
    // Lớp Person cơ bản (Từ bài 1.3)
    class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Yob { get; set; }
        public int Yod { get; set; }

        public Person() { }
        public Person(Person other)
        {
            this.Id = other.Id; this.Name = other.Name; this.Yob = other.Yob; this.Yod = other.Yod;
        }
        public void Input()
        {
            Console.Write("ID: "); Id = Console.ReadLine();
            Console.Write("Name: "); Name = Console.ReadLine();
            Console.Write("Year of Birth: "); Yob = int.Parse(Console.ReadLine());
            Console.Write("Year of Death (0 neu con song): "); Yod = int.Parse(Console.ReadLine());
        }
        public void Output()
        {
            Console.WriteLine($"[{Id}] {Name} | Sinh: {Yob} | Mat: {(Yod == 0 ? "Chua" : Yod.ToString())}");
        }
        public bool IsLiving() { return Yod == 0; }
    }

    // Lớp PersonList
    class PersonList
    {
        private List<Person> danhSach;

        // Default Constructor
        public PersonList()
        {
            danhSach = new List<Person>();
        }

        // Copy Constructor
        public PersonList(PersonList other)
        {
            danhSach = new List<Person>();
            foreach (Person p in other.danhSach)
            {
                // Thêm bản sao của từng Person vào danh sách mới
                this.danhSach.Add(new Person(p));
            }
        }

        // Thêm một Person
        public void Add(Person x)
        {
            danhSach.Add(x);
        }

        // Nhập danh sách
        public void Input()
        {
            Console.Write("Nhap so luong nguoi can quan ly: ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Nhap nguoi thu {i + 1} ---");
                Person p = new Person();
                p.Input();
                this.Add(p);
            }
        }

        // Xuất danh sách
        public void Output()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach trong.");
                return;
            }
            foreach (Person p in danhSach)
            {
                p.Output();
            }
        }

        // Trả về một PersonList chứa những người còn sống
        public PersonList LivingPeople()
        {
            PersonList listConSong = new PersonList();
            foreach (Person p in danhSach)
            {
                if (p.IsLiving())
                {
                    listConSong.Add(p);
                }
            }
            return listConSong;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PersonList ql = new PersonList();
            ql.Input();

            Console.WriteLine("\n=== DANH SACH TOAN BO NHAN KHAU ===");
            ql.Output();

            Console.WriteLine("\n=== DANH SACH NHUNG NGUOI CON SONG ===");
            PersonList qlConSong = ql.LivingPeople();
            qlConSong.Output();

            Console.ReadLine();
        }
    }
}
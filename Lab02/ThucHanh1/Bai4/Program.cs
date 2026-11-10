/*
* CHƯƠNG TRÌNH NHẬP SỐ NGUYÊN VÀ XỬ LÝ LỖI
* Tác giả : Nguyễn Huỳnh Hoàng Vũ
* Ngày viết: 20/09/2026
*
* Phát biểu đề bài: Làm lại bài 3, nhưng thông báo lỗi khi x hay y không phải là số nguyên[cite: 6].
* Ý tưởng: Thay vì dùng int.Parse() dễ gây crash chương trình khi nhập chữ, ta sử dụng int.TryParse() để kiểm tra tính hợp lệ của dữ liệu nhập vào. Nếu chuyển đổi thành công thì mới tính toán, nếu không thì xuất câu thông báo lỗi.
* Mã giả:
*   In "Nhap so nguyen x: "
*   chuoi_x = Đọc chuỗi nhập
*   In "Nhap so nguyen y: "
*   chuoi_y = Đọc chuỗi nhập
*   NẾU (chuoi_x là số nguyên x VÀ chuoi_y là số nguyên y) THÌ:
*       ket_qua = x^y
*       In "Ket qua " + x + " mu " + y + " la: " + ket_qua
*   NGƯỢC LẠI:
*       In "Loi: x hoac y khong phai la so nguyen!"
*/

using System;

namespace Bai4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Nhập x dưới dạng chuỗi
            Console.Write("Nhap so nguyen x: ");
            string inputX = Console.ReadLine();

            // Nhập y dưới dạng chuỗi
            Console.Write("Nhap so nguyen y: ");
            string inputY = Console.ReadLine();

            // Sử dụng TryParse để ép kiểu và kiểm tra tính hợp lệ
            // Nếu thành công sẽ trả về true và gán giá trị vào biến x, y
            bool isXValid = int.TryParse(inputX, out int x);
            bool isYValid = int.TryParse(inputY, out int y);

            // Kiểm tra xem cả hai đều hợp lệ hay không
            if (isXValid && isYValid)
            {
                // Thực hiện tính toán nếu dữ liệu đúng
                double ketQua = Math.Pow(x, y);
                Console.WriteLine($"Ket qua {x} mu {y} la: {ketQua}");
            }
            else
            {
                // Thông báo lỗi nếu dữ liệu nhập vào chứa chữ cái hoặc ký tự lạ
                Console.WriteLine("Loi: x hoac y khong phai la so nguyen!");
            }

            // Dừng màn hình console lại để xem kết quả
            Console.ReadLine();
        }
    }
}
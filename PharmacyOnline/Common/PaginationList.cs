namespace PharmacyOnline.Common
{
    public class PaginationList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int totalPage { get; set; }

        public PaginationList( List<T> items, int count, int pageIndex, int pageSize) {
            PageIndex = pageIndex;
            totalPage = (int)Math.Ceiling(count / (double)pageSize); 

            AddRange(items);
        }

        public static PaginationList<T> Create( IQueryable<T> source, int pageIndex, int pageSize )
        {
            var count = source.Count();

            var items = source.Skip( (pageIndex - 1)*pageSize ).Take(pageSize).ToList();

            return new PaginationList<T>(items, count, pageIndex, pageSize);

        }

    }
}

/*
           "T" là một kiểu tham số -> ta có thể sử dụng bất kỳ kiểu nào ta muốn khi tạo một thể hiện của PaginationList

            controller -> nhận vào các mục, số lượng tổng cộng các mục chỉ số trang hiện tại và kích thước trang...
            totalPage được tính bằng cách: lấy số lượng tổng cộng chia cho kích thước trang và làm tròn.

            => Thêm các mục vào danh sách bằng method AddRange.

            ? tại sao phải thêm các mục vào danh sách bằng method AddRange, AddRage để làm gì ?
            -> AddRage được sử dụng để THÊM MỘT TẬP HỢP CÁC MỤC VÀO CUỐI DANH SÁCH 
            trong trường hợp của ta: AddRange(items) được sử dụng thêm all các mục từ "items" vào PaginationList 
            Điều này hữu ích khi ta muốn thêm nhiều mục danh sách mà không cần sử dụng phương thức Add() nhiều lần!

            ex:
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            List<int> moreNumbers = new List<int>();
            moreNumbers.AddRange(numbers);

            => moreNumbers sẽ chứa tất cả các số từ numbers

            IQueryable<T> là 1 interface -> sử dụng ĐỂ TẠO & THỰC THI TRUY VẤN TRÊN một nguồn dữ liệu cụ thể ( cho phép duyệt qua kết quả của truy vấn )
            ex:
                IQueryable<User> users = _context.Users;
                IQueryable<User> adults = users.Where( u => u.Age > 18 );
                List<User> ListAdults = adults.ToList();
        
            => kiểu dữ liệu cho phép truy vấn rồi gán vào list or vào mảng ...

        */
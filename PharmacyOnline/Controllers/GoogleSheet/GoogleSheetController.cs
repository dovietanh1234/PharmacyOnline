using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnline.Entities;

namespace PharmacyOnline.Controllers.GoogleSheet
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleSheetController : ControllerBase
    {
        private readonly SheetsService _sheetsService;
        private readonly PharmacyOnlineContext _context;
        private readonly IConfiguration _configuration;

        public GoogleSheetController(SheetsService sheetsService, PharmacyOnlineContext context, IConfiguration configuration)
        {
            _sheetsService = sheetsService;
            _context = context;
            _configuration = configuration;
        }

        // HÀM NÀY ĐƯA DATA TRONG DB VÀO GG SHET
        [HttpGet]
        [Route("insert/data/googleSheet")]
        public async Task<IActionResult> insertDatainGGSheet()
        {
            
                  // get data:
                var products = _context.Products.ToList().Take(5);

                // Id's google( any gg sheet we create or existed before ) sheet to show data: 
                var spreadSheetId = _configuration.GetSection("GGSHEET:GGSHEETSpreadSheet").Value;

                // tên của trang bạn muốn truy cập và hiện dữ liệu:
                var range = "Sheet1!A1:E5";

                // create a list to contains data from DB's data:
                var values = new List<IList<object>>();

                // chuyển đổi dữ liệu từ cơ sở dữ liệu thành danh sách:
                foreach(var product in products)
                {
                
                values.Add(new List<object>
                    {
                        product.Id, product.ProductName, product.Title, product.Thumbnail
                    });
                }

                // tạo một yêu cầu mới để ghi dữ liệu vào google sheet:
                var request = _sheetsService.Spreadsheets.Values.Update(new Google.Apis.Sheets.v4.Data.ValueRange
                {
                    Values = values
                }, spreadSheetId, range);

                request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

                // thực hiện yêu cầu:
                var response = await request.ExecuteAsync();

                return Ok("send thành công");

        }

        /*
         new List<IList<object>>(); -> DÙNG ĐỂ: Tạo một danh sách mới ( có chứa các danh sách con khác bên trong nó )
         IList<>: đây là 1 interface trong c# -> dùng để: định nghĩa một TẬP HỢP CÁC PHƯƠNG THỨC MÀ DANH SÁCH NÊN CÓ + với việc sử dụng Object để lưu trữ một đối tượng của bất kỳ loại nào

        ? tại sao phải sử dụng IList<object> thay vì List<object>:
        IList<object>: biến này có thể lưu trữ bất kỳ đối tượng nào! 
        List<object>: nếu ta biết chắc chắn rằng -> không cần đến sự linh hoạt thì sử dụng List<object>
        Lợi ích IList: có thể THAY ĐỔI LOẠI CỤ THỂ CỦA DANH SÁCH ( Mà KHÔNG CẦN thay đổi kiểu của Biến )

        ví dụ về IList:
        IList<object> conmeo = new List<object>;
        conmeo.Add( new Class1{ ... } )
        conmeo.Add( new Class2{ ... } )

        *LƯU Ý: Interface chỉ được lấy làm KIỂU DỮ LIỆU CHỨ NÓ KHÔNG THỂ KHỞI TẠO ĐỐI TƯỢNG 
         */


        // hàm này để lấy data từ gg sheet đưa vào DATABASE
        [HttpGet]
        [Route("get/data/googleSheet")]
        public async Task<IActionResult> getData()
        {
                // ID của Google Sheet của bạn
                var spreadsheetId = _configuration.GetSection("GGSHEET:GGSHEETSpreadSheet").Value;

                // Tên của trang bạn muốn truy cập
                var range = "Sheet1!A14:B17";

                var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = await request.ExecuteAsync();

                return Ok(response.Values);
        }











    }
}

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


        [HttpGet]
        [Route("insert/data/googleSheet")]
        public async Task<IActionResult> insertDatainGGSheet()
        {
            
                  // get data:
                var products = _context.Products.ToList().Take(5);

                // Id's google( any gg sheet we create or existed before ) sheet to show data: 
                var spreadSheetId = _configuration.GetSection("GGSHEET:GGSHEETSpreadSheet").Value;

                // tên của trang bạn muốn truy cập và hiện dữ liệu:
                var range = "Sheet1!A23:E28";

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




        [HttpGet]
        [Route("get/data/googleSheet")]
        public async Task<IActionResult> getData()
        {
            

                // ID của Google Sheet của bạn
                var spreadsheetId = _configuration.GetSection("GGSHEET:GGSHEETSpreadSheet").Value;

                // Tên của trang bạn muốn truy cập
                var range = "Sheet1!A1:E5";

                var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = await request.ExecuteAsync();

                return Ok(response.Values);
        }











    }
}

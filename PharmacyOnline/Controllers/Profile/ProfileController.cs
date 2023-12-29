using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Models.ProfileModel;
using PharmacyOnline.Services.ProfileService;
using System.Security.Claims;

namespace PharmacyOnline.Controllers.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepo _ProfileService;
        private readonly PharmacyOnlineContext _context;
        private readonly SheetsService _sheetsService;
        private readonly IConfiguration _configuration;

        public ProfileController(IProfileRepo iprofileS, PharmacyOnlineContext context, SheetsService sheetsService, IConfiguration configuration)
        {
            _ProfileService = iprofileS;
            _context = context;
            _sheetsService = sheetsService;
            _configuration = configuration;
        }

        [HttpPost, Authorize(Roles = "Candidate")] 
        [Route("candidate/create")]
        public async Task<IActionResult> createProfile(ProfileModel model)
        {


            try
            {
                  #region GET ID THROUGH TOKEN
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (!identity.IsAuthenticated)
                {
                    return Unauthorized("invalid  token! please try later");
                }

                int idCandidate = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                #endregion

                // check 2 file has value:
                if (model.FileCv == null && model.Thumbnail == null)
                {
                    return Ok(await _ProfileService.createProfile(model, idCandidate, "", ""));
                }
                else if (model.FileCv != null && model.Thumbnail != null)
                {
                    #region HANLDE FILE IMAGE
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Thumbnail.FileName);

                    string filename2 = Guid.NewGuid().ToString() + Path.GetExtension(model.FileCv.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProfileThumbnail");

                    var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/FileCv");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    if (!Directory.Exists(filePath2))
                    {
                        Directory.CreateDirectory(filePath2);
                    }

                    var upload = Path.Combine(filePath, filename);

                    var upload2 = Path.Combine(filePath2, filename2);

                    model.Thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                    model.FileCv.CopyTo(new FileStream(upload2, FileMode.Create));

                    string fileThumbnail = $"{Request.Scheme}://{Request.Host}/Uploads/ProfileThumbnail/{filename}";

                    string fileCV = $"{Request.Scheme}://{Request.Host}/Uploads/FileCv/{filename2}";

                    #endregion

                    // int idCandidate, string fileThumbnail, string fileCV

                    return Ok(await _ProfileService.createProfile(model, idCandidate, fileThumbnail, fileCV));

                }
                else if (model.FileCv != null && model.Thumbnail == null)
                {
                    string filename2 = Guid.NewGuid().ToString() + Path.GetExtension(model.FileCv.FileName);
                    var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/FileCv");
                    if (!Directory.Exists(filePath2))
                    {
                        Directory.CreateDirectory(filePath2);
                    }
                    var upload2 = Path.Combine(filePath2, filename2);
                    model.FileCv.CopyTo(new FileStream(upload2, FileMode.Create));
                    string fileCV = $"{Request.Scheme}://{Request.Host}/Uploads/FileCv/{filename2}";

                    return Ok(await _ProfileService.createProfile(model, idCandidate, "", fileCV));
                }
                else if (model.FileCv == null && model.Thumbnail != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Thumbnail.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProfileThumbnail");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var upload = Path.Combine(filePath, filename);
                    model.Thumbnail.CopyTo(new FileStream(upload, FileMode.Create));
                    string fileThumbnail = $"{Request.Scheme}://{Request.Host}/Uploads/ProfileThumbnail/{filename}";
                    return Ok(await _ProfileService.createProfile(model, idCandidate, fileThumbnail, ""));

                }

                return BadRequest("Error! has error occured");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost, Authorize(Roles = "Candidate")] 
        [Route("candidate/update")]
        public async Task<IActionResult> updateProfile(ProfileModel model, string idProfileDetail)
        {


            try
            {
                #region GET ID THROUGH TOKEN
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (!identity.IsAuthenticated)
                {
                    return Unauthorized("invalid  token! please try later");
                }

                int idCandidate = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                if (idCandidate != model.IdCandidate)
                {
                    return BadRequest( new
                    {
                        status= 403,
                        statusMessage = "you are not allow to access data"
                    } );
                }

                #endregion


                // check 2 file has value:
                if (model.FileCv == null && model.Thumbnail == null)
                {
                    return Ok(await _ProfileService.updateProfile(model, idProfileDetail, "", ""));
                }
                else if (model.FileCv != null && model.Thumbnail != null)
                {
                    #region HANLDE FILE IMAGE
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Thumbnail.FileName);

                    string filename2 = Guid.NewGuid().ToString() + Path.GetExtension(model.FileCv.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProfileThumbnail");

                    var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/FileCv");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    if (!Directory.Exists(filePath2))
                    {
                        Directory.CreateDirectory(filePath2);
                    }

                    var upload = Path.Combine(filePath, filename);

                    var upload2 = Path.Combine(filePath2, filename2);

                    model.Thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                    model.FileCv.CopyTo(new FileStream(upload2, FileMode.Create));

                    string fileThumbnail = $"{Request.Scheme}://{Request.Host}/Uploads/ProfileThumbnail/{filename}";

                    string fileCV = $"{Request.Scheme}://{Request.Host}/Uploads/FileCv/{filename2}";

                    #endregion

                    // int idCandidate, string fileThumbnail, string fileCV

                    return Ok(await _ProfileService.updateProfile(model, idProfileDetail, fileThumbnail, fileCV));

                }
                else if (model.FileCv != null && model.Thumbnail == null)
                {
                    string filename2 = Guid.NewGuid().ToString() + Path.GetExtension(model.FileCv.FileName);
                    var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/FileCv");
                    if (!Directory.Exists(filePath2))
                    {
                        Directory.CreateDirectory(filePath2);
                    }
                    var upload2 = Path.Combine(filePath2, filename2);
                    model.FileCv.CopyTo(new FileStream(upload2, FileMode.Create));
                    string fileCV = $"{Request.Scheme}://{Request.Host}/Uploads/FileCv/{filename2}";

                    return Ok(await _ProfileService.updateProfile(model, idProfileDetail, "", fileCV));
                }
                else if (model.FileCv == null && model.Thumbnail != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Thumbnail.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProfileThumbnail");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var upload = Path.Combine(filePath, filename);
                    model.Thumbnail.CopyTo(new FileStream(upload, FileMode.Create));
                    string fileThumbnail = $"{Request.Scheme}://{Request.Host}/Uploads/ProfileThumbnail/{filename}";
                    return Ok(await _ProfileService.updateProfile(model, idProfileDetail, fileThumbnail, ""));

                }

                return BadRequest("Error! has error occured");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = "Candidate")] 
        [Route("candidate/submit")]
        public async Task<IActionResult> submitProfile(int idCandidate,string idProfileDetail)
        {
            #region GET ID THROUGH TOKEN
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int idCandidate2 = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (idCandidate2 != idCandidate)
            {
                return BadRequest(new
                {
                    status = 403,
                    statusMessage = "you are not allow to access data"
                });
            }

            #endregion

            return Ok(await _ProfileService.Submit(idProfileDetail));
        }

        [HttpGet, Authorize(Roles = "Candidate")] 
        [Route("candidate/cancel")]
        public async Task<IActionResult> cancelSubmitted(int idCandidate, string idProfileDetail)
        {
            #region GET ID THROUGH TOKEN
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int idCandidate2 = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (idCandidate2 != idCandidate)
            {
                return BadRequest(new
                {
                    status = 403,
                    statusMessage = "you are not allow to access data"
                });
            }

            #endregion

            return Ok(await _ProfileService.cancelSubmitted(idProfileDetail));
        }

        [HttpGet, Authorize(Roles = "Candidate")] 
        [Route("candidate/delete")]
        public async Task<IActionResult> deleteResume(int idCandidate, string idProfileDetail)
        {
            #region GET ID THROUGH TOKEN
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int idCandidate2 = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (idCandidate2 != idCandidate)
            {
                return BadRequest(new
                {
                    status = 403,
                    statusMessage = "you are not allow to access data"
                });
            }

            #endregion

            return Ok(await _ProfileService.deleteSubmitted(idProfileDetail));
        }

        [HttpGet, Authorize(Roles = "Candidate")] 
        [Route("candidate/getprofile")]
        public async Task<IActionResult> getLocal(int idCandidate)
        {
            #region GET ID THROUGH TOKEN
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int idCandidate2 = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (idCandidate2 != idCandidate)
            {
                return BadRequest(new
                {
                    status = 403,
                    statusMessage = "you are not allow to access data"
                });
            }

            #endregion
            return Ok(await _ProfileService.GetPLocal(idCandidate));
        }

        [HttpGet, Authorize(Roles = "Candidate")] 
        [Route("candidate/gethistory")]
        public async Task<IActionResult> getHistory(int idCandidate)
        {
            #region GET ID THROUGH TOKEN
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int idCandidate2 = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (idCandidate2 != idCandidate)
            {
                return BadRequest(new
                {
                    status = 403,
                    statusMessage = "you are not allow to access data"
                });
            }

            #endregion

            return Ok(await _ProfileService.GetHistory(idCandidate));
        }




        //string idProfileDetail, int isQualified, string? body

        [HttpPost]  // , Authorize(Roles = "Admin")
        [Route("admin/approved")]
        public async Task<IActionResult> AdminApprovedProfile(approvedCV model)
        {
            try
            {
                    var Element = await _context.PersonalDetails.FirstOrDefaultAsync(p => p.Id == model.idProfileDetail);
                    if (Element == null) return Ok(
                        new Models.Candidate.result
                        {
                            status = 404,
                            statusMessage = "not found the CV"
                        } 
                    );

                    if (model.isQualified == 1)
                    {
                    var numberSheet = await _context.GoogleSheetNumbers.FirstOrDefaultAsync( g => g.Id == 1 );

                    if (numberSheet == null) return Ok(new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "Error! has error occured"
                    });

                    numberSheet.Ggsheetcolumn += 1;
                    await _context.SaveChangesAsync();

                    var spreadSheetId = _configuration.GetSection("GGSHEET:GGSHEETSpreadSheet").Value;
                    var range = $"Sheet2!A{numberSheet.Ggsheetcolumn}:G{numberSheet.Ggsheetcolumn}";

                    var values = new List<IList<object>>();

                    values.Add(new List<object>
                    {
                        Element.Id, Element.Fullname, Element.Age, Element.Email, Element.Number, model.body.appointment, model.body.interviewAddress
                    });

                    var request = _sheetsService.Spreadsheets.Values.Update(new Google.Apis.Sheets.v4.Data.ValueRange { 
                        Values = values
                    }, spreadSheetId, range);

                    request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

                    var response = await request.ExecuteAsync();
                }

                return Ok(await _ProfileService.ApprovingAdmin(model.idProfileDetail, model.isQualified, model.body, Element));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error! exception ${ex}");
            }

            

        }

        [HttpGet, Authorize(Roles = "Admin")] 
        [Route("admin/search")]
        public async Task<IActionResult> AdminGetSubmitP(string search, int page = 1)
        {
            return Ok(await _ProfileService.searchSdtGmailAdmin(search, page));
        }

        [HttpGet, Authorize(Roles = "Admin")]  
        [Route("admin/getlist/submitted")]
        public async Task<IActionResult> AdminGetSubmitP(int page = 1)
        {
            return Ok(await _ProfileService.GetListSubmittedAdmin(page));
        }

        
        [HttpGet, Authorize(Roles = "Admin")]  // isQualified =  UNQUALIFIED || QUALIFIED
        [Route("admin/getlist/history")]
        public async Task<IActionResult> adminGetHistory(string? isQualified = null, int page = 1) //string? isQualified
        {
            return Ok(await _ProfileService.GetHistoryProfileAdmin(page, isQualified));
        }


        [HttpGet, Authorize(Roles = "Admin")]  //
        [Route("admin/getdetail")]
        public async Task<IActionResult> detailCV(string IdProfile)
        {
            return Ok(await _ProfileService.detailProfile(IdProfile));
        }


        // File cho admin có thể tải file cv và xem file cv của ứng viên | MAI TEST LẠI API NÀY ĐỔI LẠI ĐƯỜNG DẪN PATH VÀO LẤY DỮ LIỆU
        // install MimeTypes 4.0.0
        [HttpGet, Authorize(Roles = "Admin")] 
        [Route("admin/download/fileCV")]
        public async Task<IActionResult> DownloadFile(string idProfileDetail)
        {
            try
            {

                var profileDT = await _context.PersonalDetails.FirstOrDefaultAsync(pf => pf.Id == idProfileDetail);

                if (profileDT == null) return NotFound("not found the data");

                if (string.IsNullOrEmpty(profileDT.FileCv))
                {
                    return BadRequest("this profile doesnt have profile CV file");
                }

                string result = profileDT.FileCv;

                // lấy vị trí cuối cùng của "/"
                int lastIndex = result.LastIndexOf('/');
                // cắt chuỗi từ vị trí "/" + 1 đến hết chuỗi
                string filename = result.Substring(lastIndex + 1);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/FileCv", filename);

                // check xem file is exist
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                // tạo một luồng từ tệp:
                var memory = new MemoryStream();

                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                // lấy các mine từ tên tệp:
                var mimeType = MimeKit.MimeTypes.GetMimeType(filename);

                // trả về như một file stream result:
                return File(memory, mimeType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }







    }
}

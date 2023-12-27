using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnline.DTO.ManageCandidatesDTO;
using PharmacyOnline.Services.ManageCandidates;
using System;
using System.Security.Claims;

namespace PharmacyOnline.Controllers.ManageCandidates
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCandidatesController : ControllerBase
    {
        private readonly IManageCanRepo _IManageCan;

        public ManageCandidatesController(IManageCanRepo manageCan)
        {
            _IManageCan = manageCan;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("admin/search&getlist")]
        public async Task<IActionResult> Index(string? search, int page)
        {
            return Ok( await _IManageCan.getListCans(search, page) );
        }


      
        /*  [HttpGet, Authorize(Roles = "Admin")]
        [Route("candidate/search")]

        public async Task<IActionResult> search(string keyword, int page)
        {
            return Ok(await _IManageCan.searchCans(keyword, page));
        }*/

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("admin/toggle/user")]
        public async Task<IActionResult> toggleCand(int id)
        {
            return Ok(await _IManageCan.toggleU(id));
        }

        [HttpPost, Authorize(Roles = "Candidate")]
        [Route("candidate/update/inform")]
        public async Task<IActionResult> update([FromForm] updateCan model)
        {
            #region CHECK TOKEN:
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int IdCategory = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if ( IdCategory != model.id )
            {
                return BadRequest(new
                {
                    status = 403,
                    statusMessage = "you are not allow."
                });
            }


            #endregion

            if ( model.thumbnail != null )
            {
                #region HANLDE FILE IMAGE
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/CanThumbnail");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var upload = Path.Combine(filePath, filename);

                model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";
                #endregion

                return Ok( await _IManageCan.updateCan( model, url ) );
            }
            return Ok(await _IManageCan.updateCan(model, ""));
        }

        [HttpGet, Authorize(Roles = "Candidate")]
        [Route("candidate/getinforms/token")]
        public async Task<IActionResult> getCandidateToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("invalid  token! please try later");
            }

            int IdCategory = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            return Ok(await _IManageCan.getProfileToken(IdCategory));
        }




    }
}

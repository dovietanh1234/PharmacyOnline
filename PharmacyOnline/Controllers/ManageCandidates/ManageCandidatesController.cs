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

        [HttpGet]
        [Route("list/candidates")]

        public async Task<IActionResult> Index(int page)
        {
            return Ok( await _IManageCan.getListCans(page) );
        }

        [HttpGet]
        [Route("candidate/search")]

        public async Task<IActionResult> search(string keyword, int page)
        {
            return Ok(await _IManageCan.searchCans(keyword, page));
        }


        [HttpGet, Authorize(Roles = "Candidate")]
        [Route("get/candidate")]
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

        [HttpGet]
        [Route("toggle/candidate")]

        public async Task<IActionResult> toggleCand(int id)
        {
            return Ok(await _IManageCan.toggleU(id));
        }

        [HttpPost]
        [Route("update/candidate")]

        public async Task<IActionResult> update([FromForm] updateCan model)
        {
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




    }
}

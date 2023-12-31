﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyOnline.Common;
using PharmacyOnline.Entities;
using PharmacyOnline.Services.Statistics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PharmacyOnline.Controllers.Statistics
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {

        private readonly IStatistics _IstatisticService;
        private readonly PharmacyOnlineContext _context;

        public StatisticController(IStatistics statisticsService, PharmacyOnlineContext context)
        {
            _IstatisticService = statisticsService;
            _context = context;

        }

        [HttpGet, Authorize(Roles = "Admin")] 
        [Route("admin/get")]
        public async Task<IActionResult> Statistics() {

            return Ok( new
            {
                numberCVSubmittedMonth = await _IstatisticService.getSumitedMonth(),
                numberCVCheckedMonth = await _IstatisticService.getCheckedMonth(),
                numberCVQualifiedMonth = await _IstatisticService.getQualifiedMonth(),
                numberCVUnQualifiedMonth = await _IstatisticService.getUnQualifiedMonth(),
                numberCVSubmittedWeek = await _IstatisticService.getSumitedWeek(),
                numberCVCheckedWeek = await _IstatisticService.getCheckedWeek(),
                numberCVQualifiedWeek = await _IstatisticService.getQualifiedWeek(),
                numberCVUnQualifiedWeek = await _IstatisticService.getUnQualifiedWeek(),
                numberCVSubmittedDay = await _IstatisticService.getSumitedDay(),
                numberCVCheckedDay = await _IstatisticService.getCheckedDay(),
                numberCVQualifiedDay = await _IstatisticService.getQualifiedDay(),
                numberCVUnQualifiedDay = await _IstatisticService.getUnQualifiedDay()
            });
        }





    }
}

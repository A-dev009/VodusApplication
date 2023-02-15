using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using VodusApplication.Models;

namespace VodusApplication.Controllers
{
    public class VodusController : Controller
    {
        private readonly ILogger<VodusController> _logger;

        public VodusController(ILogger<VodusController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var vodusDataList = new List<VodusData>();
            return View(vodusDataList);
        }

        [HttpPost]
        public IActionResult Index(IFormFile vodusExcelFile)
        {

            var vodusDataList = new List<VodusData>();
            if (vodusExcelFile != null && Path.GetExtension(vodusExcelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {

            using (var stream = vodusExcelFile.OpenReadStream())
            {
                

                using (var package = new ExcelPackage(stream))
                {
                    if (package?.Workbook.Worksheets != null)
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var rowCount = worksheet.Dimension.Rows;


                        for (int row =2; row <= rowCount; row++)
                        {
                            if (worksheet.Cells[row, 1].Value == null && worksheet.Cells[row, 2].Value == null && worksheet.Cells[row, 3].Value == null && worksheet.Cells[row, 4].Value == null && worksheet.Cells[row, 5].Value == null && worksheet.Cells[row, 6].Value == null && worksheet.Cells[row, 7].Value == null)
                            {
                                continue;
                            }
                          
                            var pageContext = new VodusData();
                            pageContext.Page = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();
                            pageContext.PromoTitle = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim();
                            pageContext.PromoDescription = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim();
                            pageContext.TermsCondition = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim();
                            pageContext.StartDate = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim();
                            pageContext.EndDate = (worksheet.Cells[row, 6].Value ?? string.Empty).ToString().Trim();
                            pageContext.ImageUrl = (worksheet.Cells[row, 7].Value ?? string.Empty).ToString().Trim();
                                vodusDataList.Add(pageContext);
                        }
                    }

                }
            }

            }
            return View(vodusDataList);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

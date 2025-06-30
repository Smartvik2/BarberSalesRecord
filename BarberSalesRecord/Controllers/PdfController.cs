using BarberSalesRecord.DTOs;
using BarberSalesRecord.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner")]
    public class PdfController : ControllerBase
    {
        [HttpPost("export-pdf")]
        public IActionResult ExportPdf([FromBody] ProfitReportDto report)
        {
            var pdfGenerator = new ProfitPdfGenerator();
            var file = pdfGenerator.GenerateReport(report);

            return File(file, "application/pdf", "ProfitReport.pdf");
        }

    }
}

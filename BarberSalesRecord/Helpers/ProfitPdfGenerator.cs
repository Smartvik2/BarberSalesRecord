using BarberSalesRecord.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BarberSalesRecord.Helpers
{
    public class ProfitPdfGenerator
    {
        public byte[] GenerateReport(ProfitReportDto report)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.Content()
                        .Column(col =>
                        {
                            col.Item().Text($"{report.ShopName} - Profit Report")
                                .FontSize(20).Bold().AlignCenter();

                            col.Item().Element(e =>
                            {
                                e.AlignCenter().PaddingBottom(10)
                                 .Text($"Period: {report.StartDate:MMM dd, yyyy} - {report.EndDate:MMM dd, yyyy}");
                            });



                            col.Item().Text($"Total Income: ₦{report.TotalIncome:N0}");
                            col.Item().Text($"Total Expenses: ₦{report.TotalExpenses:N0}");
                            col.Item().Text($"Total Barber Profits: ₦{report.TotalBarberProfits:N0}");
                            col.Item().Text($"Shop Profit: ₦{report.ShopProfit:N0}");

                            col.Item().PaddingVertical(10).Text("Barber Breakdown:").FontSize(14).Bold();

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Barber
                                    columns.RelativeColumn(); // Income
                                    columns.RelativeColumn(); // Profit
                                });

                                // Header
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Barber").Bold();
                                    header.Cell().Element(CellStyle).Text("Total Income").Bold();
                                    header.Cell().Element(CellStyle).Text("Profit").Bold();
                                });

                                // Rows
                                foreach (var barber in report.BarberProfits)
                                {
                                    table.Cell().Element(CellStyle).Text(barber.BarberName);
                                    table.Cell().Element(CellStyle).Text($"₦{barber.TotalIncome:N0}");
                                    table.Cell().Element(CellStyle).Text($"₦{barber.Profit:N0}");
                                }

                                IContainer CellStyle(IContainer container) =>
                                    container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5);
                            });
                        });
                });
            }).GeneratePdf();
        }
    }
}

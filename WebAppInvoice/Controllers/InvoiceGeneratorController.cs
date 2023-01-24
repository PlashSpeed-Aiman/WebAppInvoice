using Microsoft.AspNetCore.Mvc;
using ProjectAlpha1.Document;
using QuestPDF.Fluent;

namespace WebAppInvoice.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceGeneratorController : ControllerBase
{
    
    [HttpGet(Name = "GenerateInvoice")]
    //InvoiceModel invoiceModel
    public async Task<IActionResult> GenerateInvoice()
    {
        var stream = await Task.Run(() => new InvoiceDocument(InvoiceDocumentDataSource.GetInvoiceDetails()).GeneratePdf());
        
        return new FileContentResult(stream, "application/pdf") {
            FileDownloadName = "Invoice.pdf"
        };

    }
    [HttpPost(template: "GetInvoice")]
    public async Task<IActionResult> GenerateInvoice(InvoiceModel invoiceModel)
    {
        var stream = await Task.Run(() => new InvoiceDocument(invoiceModel).GeneratePdf());
        
        return new FileContentResult(stream, "application/pdf") {
            FileDownloadName = "Invoice.pdf"
        };

    }
    // [HttpGet(Name = "GetInvoice")]
    async Task<IActionResult> DownloadFileAsync(string fileName){
        using(var net = new System.Net.WebClient()) {
            byte[] data = await net.DownloadDataTaskAsync(fileName);
            return new FileContentResult(data, "application/pdf") {
                FileDownloadName = "Invoice.pdf"
            };
        }
    }
}
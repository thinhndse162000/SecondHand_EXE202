using Application.IServices.IVnPayService;
using Application.IServices.IVnPayService;
using Domain.Models;
using Domain.Response.VnPay;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/vnpay")]
    public class VnPayController : Controller
    {
        private readonly IVnPayService _vnPayService;
        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("url")]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }

        [HttpPost("callback")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}

using testKetNoi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using testKetNoi.Repository;
using Azure.Core;
using testKetNoi.Interfaces;
using testKetNoi.VNPayModels;
using testKetNoi.Dto;
using AutoMapper;

namespace testKetNoi.Controllers
{
    [Route("api/HocPhi/")]
    [ApiController]
    public class VnPayController : Controller
    {
        private readonly IVnPayService _vnPayService;
        public IMapper mapper;
        private readonly ISinhVienRepository sinhVienRepository;
        private readonly IHocPhiRepository hocPhiRepository;
        public VnPayController(IVnPayService vnPayService, IHocPhiRepository hocPhiRepository,ISinhVienRepository sinhVienRepository,IMapper mapper)
        {
            _vnPayService = vnPayService;
            this.mapper= mapper;
            this.sinhVienRepository = sinhVienRepository;
            this.hocPhiRepository = hocPhiRepository;
        }
        [HttpGet("getlink/{cccd}")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult CreatePaymentUrl(string cccd)
        {
            if (!sinhVienRepository.SinhVienExists(cccd))
            {
                return NotFound();
            }
            if (sinhVienRepository.isPay(cccd))
            {
                return Ok("Sinh viên đã thanh toán học phí online!");
            }
            PaymentInformationModel model = new PaymentInformationModel();
            var description = hocPhiRepository.getChiTietHocPhi(cccd);
            model.Amount = (double)hocPhiRepository.getTongHocPhi(cccd);
            model.OrderType = "hocphi";
            model.Name = cccd+"ThanhToanHocPhi,";
            model.OrderDescription="";
            if (description.PhiDongPhuc != 0)
            {
                model.OrderDescription += "DongPhuc,";
            }
            if (description.PhiKTX != 0)
            {
                model.OrderDescription += "KTX";
            }
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);   
            return Ok(url);
        }
        // orderDes in response = modelName + modelDes + modelAmount in PaymentInformationModel
        //MaHD = response.paymentId
        [HttpGet("ketquathanhtoan")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            var cccd = "";
            if (response.VnPayResponseCode == "00")
            {
                var hoaDon = new HoaDonDto()
                {
                    MaHD = response.PaymentId,
                    SoTien = Convert.ToDecimal(response.Amount),
                    ThoiDiem=response.Date,
                    NoiDung=response.OrderDescription
                };
                cccd = response.OrderDescription.Substring(0, 12);
                hocPhiRepository.SaveBill(mapper.Map<HoaDon>(hoaDon), cccd);
            }
            
            return Json(response);
        }
    }
}

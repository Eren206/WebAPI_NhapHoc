using testKetNoi.Data;
using testKetNoi.Models;
using testKetNoi.VNPayModels;

namespace testKetNoi.Interfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
        string GetConfig();
    }
}

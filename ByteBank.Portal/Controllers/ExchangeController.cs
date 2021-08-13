using ByteBank.Application.Abstracts;
using ByteBank.Services;
using ByteBank.Services.Intefaces;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeServiceTest();
        }

        public async Task<string> MXN()
        {
            var pageText = await View();//
            var value = await _exchangeService.Calculate("MXN", "BRL", 1);
            var finalValue = pageText.Replace("{reais}", value.ToString());
            return finalValue.ToString();
        }

        public async Task<string> USD()
        {
            var pageText = await View();//_exchangeService.Calculate("USD", "BRL", 1);
            var value = await _exchangeService.Calculate("MXN", "BRL", 1);
            var finalValue = pageText.Replace("{reais}", value.ToString());
            return finalValue.ToString();
        }
    }
}

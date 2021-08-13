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
            var pageText = await View();
            var value = await _exchangeService.Calculate("MXN", "BRL", 1);
            var finalValue = pageText.Replace("{reais}", value.ToString());
            return finalValue.ToString();
        }

        public async Task<string> USD()
        {
            var pageText = await View();
            var value = await _exchangeService.Calculate("MXN", "BRL", 1);
            var finalValue = pageText.Replace("{reais}", value.ToString());
            return finalValue.ToString();
        }

        public async Task<string> Calculate(string sourceCurrency, string destCurrency, decimal value)
        {
            var finalValue = await _exchangeService.Calculate(sourceCurrency, destCurrency, value);
            var pageText = await View();
            var page = 
                pageText
                .Replace("{finalValue}", finalValue.ToString())
                .Replace("{destCurrency}", destCurrency)
                .Replace("{sourceCurrency}", sourceCurrency)
                .Replace("{value}", value.ToString());

            return page;
        }

        public async Task<string> Calculate(string sourceCurrency, decimal value) =>
            await Calculate(sourceCurrency, "BRL", value);
    }
}

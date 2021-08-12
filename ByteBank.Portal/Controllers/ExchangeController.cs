using ByteBank.Services;
using ByteBank.Services.Intefaces;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controllers
{
    public class ExchangeController
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeServiceTest();
        }

        public async Task<string> MXN()
        {
            var finalValue = await _exchangeService.Calculate("MXN", "BRL", 1);

            return finalValue.ToString();
        }

        public async Task<string> USD()
        {
            var finalValue = await _exchangeService.Calculate("USD", "BRL", 1);
            return finalValue.ToString();
        }
    }
}

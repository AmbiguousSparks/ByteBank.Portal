using ByteBank.Services.Intefaces;
using System;
using System.Threading.Tasks;

namespace ByteBank.Services
{
    public class ExchangeServiceTest : IExchangeService
    {
        private readonly Random _rdm = new Random();
        public async Task<decimal> Calculate(string sourceCurrency, string destCurrency, decimal value)
        {
            return await Task.Run(() =>
            {
                return value * (decimal)_rdm.NextDouble();
            });
        }
    }
}

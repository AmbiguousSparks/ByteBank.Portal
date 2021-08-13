using System.Threading.Tasks;

namespace ByteBank.Services.Intefaces
{
    public interface IExchangeService
    {
        /// <summary>
        /// Calculate the exchange based on a source currency and a destiny currency
        /// </summary>
        /// <param name="sourceCurrency">the source currency. eg. USD</param>
        /// <param name="destCurrency">the destiny currency. eg. BRL</param>
        /// <param name="value">the value to be calculated</param>
        /// <returns></returns>
        Task<decimal> Calculate(string sourceCurrency, string destCurrency, decimal value);
    }
}

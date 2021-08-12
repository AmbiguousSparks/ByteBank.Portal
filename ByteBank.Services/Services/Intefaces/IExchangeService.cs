﻿using System.Threading.Tasks;

namespace ByteBank.Services.Services.Intefaces
{
    public interface IExchangeService
    {
        Task<decimal> Calculate(string sourceCurrency, string destCurrency, decimal value);
    }
}

using System;

namespace Mc2.TrustWallet.Asset.Utilities.Exceptions
{
    /// <summary>
    /// Symbol name with illegal character
    /// </summary>
    public class BadSymbolNameException : Exception
    {
        public BadSymbolNameException(string symbol) : base($"Bad symbol name: {symbol}")
        {

        }
    }
}

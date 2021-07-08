using System;
using TrustWallet.Asset.ModelsStandard.Interfaces;

namespace TrustWallet.Asset.Utilities.Exceptions
{
    /// <summary>
    /// Symbol name with illegal character
    /// </summary>
    public class BadSymbolNameException : Exception
    {
        public BadSymbolNameException(IAssetString token) : base($"Bad symbol name: {token.Symbol}")
        {

        }
    }
}

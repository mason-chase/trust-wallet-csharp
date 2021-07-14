using System;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.Utilities.Exceptions
{
    /// <summary>
    /// Symbol name with illegal character
    /// </summary>
    public class BadSymbolNameException : Exception
    {
        public BadSymbolNameException(IAsset token) : base($"Bad symbol name: {token.Symbol}")
        {

        }
    }
}

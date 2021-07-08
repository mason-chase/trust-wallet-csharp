using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustWallet.Asset.ModelsStandard.Exceptions
{
    public class InvalidBlockchainCodeException : Exception
    {
        public InvalidBlockchainCodeException(string code) : base($"Invalid Blockchain code: {code}") { }
    }
}

﻿using System.Collections.Generic;
using System.Text;

namespace TrustWallet.Asset.Models
{

    /*
    "name": "Tokenlon",
    "website": "https://tokenlon.im/lon",
    "source_code": "https://etherscan.io/address/0x0000000000095413afc295d19edeb1ad7b71c952#code",
    "white_paper": "https://tokenlon.im/files/Tokenlon-litepaper_en-us.pdf",
    "description": "LON backs the Tokenlon decentralized exchange and payment settlement protocol based on Ethereum blockchain technology. It currently powers Tokenlon DEX, a decentralized exchange and payment settlement protocol which aims to provide a secure, reliable and seamless trading experience to the masses.",
    "socials": [
        {
            "name": "Twitter",
            "url": "https://twitter.com/tokenlon",
            "handle": "tokenlon"
        },
        {
            "name": "Medium",
            "url": "https://medium.com/@tokenlon",
            "handle": "tokenlon"
        },
        {
            "name": "Discord",
            "url": "https://discord.gg/nPmsMrG",
            "handle": "Tokenlon"
        }
    ],
    "explorer": "https://etherscan.io/token/0x0000000000095413afc295d19edeb1ad7b71c952",
    "type": "ERC20",
    "symbol": "LON",
    "decimals": 18,
    "status": "active",
    "id": "0x0000000000095413afC295d19EDeb1Ad7B71c952"
    */

    public enum AssetType
    {
        Coin,
        Erc20,
        Trc10,
        Trc20
    }
}

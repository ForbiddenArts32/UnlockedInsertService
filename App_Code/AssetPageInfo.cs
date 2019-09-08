using System;
using System.Collections.Generic;

namespace UnlockedInsertService
{
    internal struct AssetPageInfo
    {
        public string Page;
        public ulong UserId;
        public ulong ProductId;
        public string AssetType;
        public string XcsrfToken;

        public AssetPageInfo(string page)
        {
            Page = page;
            UserId = ulong.Parse(ExtractStringByBrackets(page, "data-expected-seller-id=\"", "\"", 64));
            ProductId = ulong.Parse(ExtractStringByBrackets(page, "data-product-id=\"", "\"", 64));
            AssetType = ExtractStringByBrackets(page, "data-asset-type=\"", "\"", 64);
            XcsrfToken = ExtractStringByBrackets(page, "Roblox.XsrfToken.setToken('", "');", 32);
        }

        private static string ExtractStringByBrackets(string document, string leftBracket, string rightBracket, int maxLength)
        {
            int indice1 = document.IndexOf(leftBracket);
            int indice2 = indice1 + leftBracket.Length;
            int indice3 = document.IndexOf(rightBracket, indice2);
            if (indice1 == -1 || indice3 == -1 || indice3 - indice2 > maxLength)
                throw new IndexOutOfRangeException("Cannot find the bracketed string.");
            return document.Substring(indice2, indice3 - indice2);
        }
    }
}
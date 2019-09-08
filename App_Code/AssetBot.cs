using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web;

namespace UnlockedInsertService
{
    internal static class AssetBot
    {
        public static ulong WhitelistModel(ulong assetId)
        {
            if (!WhitelistCache.ContainsKey(assetId) || DateTime.UtcNow > WhitelistCache[assetId].ExpirationDate)
            {
                // Extract info from the page.
                AssetPageInfo info = GetAssetPage(assetId);

                // Ensure the thing is a model.
                if (info.AssetType != "Model")
                    throw new FormatException("The asset requested is not a Model.");

                // Post the take request.
                string requestString = string.Format(TakeItemUrl, info.ProductId, info.UserId);
                WebClient client = NewPseudoHumanClient();
                client.Headers["X-CSRF-TOKEN"] = info.XcsrfToken;
                try
                {
                    client.UploadString(requestString, "");
                    Debug.WriteLine("Whitelisted " + assetId + ".");
                    WhitelistCache[assetId] = new AssetIdCache(assetId, assetId, DateTime.UtcNow + new TimeSpan(1, 0, 0, 0));
                    return assetId;
                }
                finally
                { client.Dispose(); }
            }
            else
            {
                Debug.WriteLine("The asset ID " + assetId + " has recently been whitelisted. Using cache.");
                return WhitelistCache[assetId].UsableAssetId;
            }
        }
        public static ulong? CheckRbxUserId()
        {
            WebClient client = NewPseudoHumanClient();
            try
            {
                string response = client.DownloadString("https://assetgame.roblox.com/game/GetCurrentUser.ashx");
                ulong userId;
                return ulong.TryParse(response, out userId) ? userId : (ulong?)null;
            }
            finally
            { client.Dispose(); }
        }

        private const string TakeItemUrl =
            "https://www.roblox.com/api/item.ashx?rqtype=purchase&productID={0}&expectedCurrency=1&expectedPrice=0&expectedSellerID={1}&userAssetID=";

        // RAM-Based cache that helps prevent spamming the Roblox website with the same asset IDs.
        private static readonly Dictionary<ulong, AssetIdCache> WhitelistCache = new Dictionary<ulong, AssetIdCache>();

        private static WebClient NewPseudoHumanClient()
        {
            WebClient client = new WebClient();
            client.Headers["Cookie"] = ".ROBLOSECURITY=" + Secrets.RbxAuthToken + ";";
            return client;
        }
        private static AssetPageInfo GetAssetPage(ulong assetId)
        {
            // Look at the page.
            WebClient client = NewPseudoHumanClient();
            try
            {
                string page = client.DownloadString("https://www.roblox.com/library/" + assetId.ToString() + "/");

                // Extract info from the page.
                return new AssetPageInfo(page);
            }
            finally
            { client.Dispose(); }
        }
    }
}
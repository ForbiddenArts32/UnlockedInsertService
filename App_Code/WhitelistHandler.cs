using System;
using System.Collections.Generic;
using System.Web;

namespace UnlockedInsertService
{
    public class WhitelistHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.RequestType != "POST")
            {
                context.Response.StatusCode = 400;
                return;
            }

            System.Collections.Specialized.NameValueCollection form = context.Request.Form;

            string secret = form["Secret"];
            string requestedAssetId = form["AssetID"];
            ulong assetId = 0;

            if (secret == null || requestedAssetId == null || !ulong.TryParse(requestedAssetId, out assetId))
            {
                context.Response.StatusCode = 400;
                return;
            }
            if (secret != Secrets.ApiKey)
            {
                context.Response.StatusCode = 401;
                return;
            }

            ulong usableAssetId = AssetBot.WhitelistModel(assetId);

            context.Response.ContentType = "text/plain";
            context.Response.Write(usableAssetId.ToString());
            context.Response.Flush();
        }

        public bool IsReusable { get { return false; } }
    }
}
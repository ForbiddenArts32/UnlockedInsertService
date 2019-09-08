using System;
using System.Collections.Generic;

namespace UnlockedInsertService
{
    internal struct AssetIdCache
    {
        public ulong SourceAssetId;
        public ulong UsableAssetId;
        public DateTime ExpirationDate;

        public AssetIdCache(ulong sourceAssetId, ulong usableAssetId, DateTime expirationDate)
        {
            SourceAssetId = sourceAssetId;
            UsableAssetId = usableAssetId;
            ExpirationDate = expirationDate;
        }
    }
}
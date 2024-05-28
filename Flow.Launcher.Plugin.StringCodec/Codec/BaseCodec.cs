using System;
using System.Collections.Generic;

namespace Flow.Launcher.Plugin.StringCodec.Codec;

public interface BaseCodec
{
    string GetName();

    string GetSubTitle(IPublicAPI publicApi);

    bool MatchType(string query)
    {
        return GetName().Equals(query, StringComparison.OrdinalIgnoreCase);
    }

    // CodecResult GetResult(string second);

    List<Result> GetResult(IPublicAPI publicApi, string actionKeyword, string second, string third);
}
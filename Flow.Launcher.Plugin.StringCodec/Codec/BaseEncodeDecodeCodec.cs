using System.Collections.Generic;
using Flow.Launcher.Plugin.StringCodec.Model;

namespace Flow.Launcher.Plugin.StringCodec.Codec;

public interface BaseEncodeDecodeCodec : BaseCodec
{
    public const string Decode = "Decode";
    public const string Encode = "Encode";
    List<ResultItem> GetSubCommands(IPublicAPI publicApi, string second);

    List<Result> GetEncodeResult(IPublicAPI publicApi, string actionKeyword, string third);
    List<Result> GetDecodeResult(IPublicAPI publicApi, string actionKeyword, string third);
}
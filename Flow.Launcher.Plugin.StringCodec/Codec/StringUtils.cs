using System.Collections.Generic;

namespace Flow.Launcher.Plugin.StringCodec.Codec;

public class StringUtils
{
    public static List<BaseCodec> GetCoeecList()
    {
        return new List<BaseCodec>
        {
            new Base64Codec(),
            new UUIDCodec(),
            new UrlCodec()
        };
    }
}
using System;
using System.Collections.Generic;

namespace Flow.Launcher.Plugin.StringCodec.Codec;

public class UUIDCodec : BaseCodec
{
    public string GetName()
    {
        return "UUID";
    }

    public string GetSubTitle(IPublicAPI publicApi)
    {
        return publicApi.GetTranslation("uuid_generate");
    }


    public List<Result> GetResult(IPublicAPI publicApi, string actionKeyword, string second, string third)
    {
        var result = int.TryParse(second, out var cnt);
        if (!result) cnt = 1;


        var lines = new List<string>();

        for (var i = 0; i < cnt; i++)
        {
            lines.Add(Guid.NewGuid().ToString());
        }

        var text = string.Join("\r\n", lines);
        return new List<Result>
        {
            new()
            {
                IcoPath = StringCodec.IconPath,
                Title = string.Join(" ", lines),
                SubTitle = string.Format(publicApi.GetTranslation("uuid_generate_count"), $"{lines.Count}"),
                CopyText = text,
                Action = _ =>
                {
                    publicApi.CopyToClipboard(text, false, false);
                    return true;
                }
            }
        };
    }
}
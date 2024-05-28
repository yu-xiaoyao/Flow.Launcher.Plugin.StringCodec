using System;
using System.Collections.Generic;
using System.Text;
using Flow.Launcher.Plugin.StringCodec.Model;

namespace Flow.Launcher.Plugin.StringCodec.Codec;

public class Base64Codec : BaseEncodeDecodeCodec
{
    public string GetName()
    {
        return "B64";
    }

    public string GetSubTitle(IPublicAPI publicApi)
    {
        return publicApi.GetTranslation("base64_codec");
    }

    public List<Result> GetResult(IPublicAPI publicApi, string actionKeyword, string second, string third)
    {
        if (BaseEncodeDecodeCodec.Encode.Equals(second, StringComparison.OrdinalIgnoreCase))
        {
            return GetEncodeResult(publicApi, actionKeyword, third);
        }

        if (BaseEncodeDecodeCodec.Decode.Equals(second, StringComparison.OrdinalIgnoreCase))
        {
            return GetDecodeResult(publicApi, actionKeyword, third);
        }

        var subCommands = GetSubCommands(publicApi, second);
        // show match command list
        var list = new List<Result>();
        foreach (var command in subCommands)
        {
            if (!string.IsNullOrEmpty(second))
            {
                if (!command.Title.StartsWith(second, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }

            list.Add(new Result
            {
                IcoPath = StringCodec.IconPath,
                Title = command.Title,
                SubTitle = command.SubTitle,
                AutoCompleteText = $"{actionKeyword} {GetName()} {command.Title} ",
                Action = _ =>
                {
                    publicApi.ChangeQuery($"{actionKeyword} {GetName()} {command.Title} ");
                    return false;
                }
            });
        }

        return list;
    }


    public List<ResultItem> GetSubCommands(IPublicAPI publicApi, string second)
    {
        var list = new List<ResultItem>
        {
            new()
            {
                Title = BaseEncodeDecodeCodec.Encode,
                SubTitle = string.Format(publicApi.GetTranslation("encode_target"), GetName()),
            },
            new()
            {
                Title = BaseEncodeDecodeCodec.Decode,
                SubTitle = string.Format(publicApi.GetTranslation("decode_target"), GetName()),
            },
        };
        return list;
    }

    public List<Result> GetEncodeResult(IPublicAPI publicApi, string actionKeyword, string third)
    {
        if (string.IsNullOrEmpty(third))
            return GetInputEmptyResult(publicApi, actionKeyword);

        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(third));
        return new List<Result>
        {
            new Result()
            {
                IcoPath = StringCodec.IconPath,
                Title = string.Format(publicApi.GetTranslation("encode_result"), GetName()),
                SubTitle = base64,
                CopyText = base64,
                AutoCompleteText = $"{actionKeyword} {GetName()} {BaseEncodeDecodeCodec.Encode} {third}",
                Action = _ =>
                {
                    publicApi.CopyToClipboard(base64, false, false);
                    return true;
                }
            }
        };
    }

    public List<Result> GetDecodeResult(IPublicAPI publicApi, string actionKeyword, string third)
    {
        if (string.IsNullOrEmpty(third))
            return GetInputEmptyResult(publicApi, actionKeyword);

        try
        {
            var base64Bytes = Convert.FromBase64String(third);
            var origin = Encoding.UTF8.GetString(base64Bytes);

            return new List<Result>
            {
                new()
                {
                    IcoPath = StringCodec.IconPath,
                    Title = string.Format(publicApi.GetTranslation("decode_result"), GetName()),
                    SubTitle = origin,
                    CopyText = origin,
                    AutoCompleteText = $"{actionKeyword} {GetName()} {BaseEncodeDecodeCodec.Encode} {third}",
                    Action = _ =>
                    {
                        publicApi.CopyToClipboard(origin, false, false);
                        return true;
                    }
                }
            };
        }
        catch (FormatException e)
        {
            // base 64 异常
        }

        return new List<Result>
        {
            new()
            {
                IcoPath = StringCodec.IconPath,
                Title = publicApi.GetTranslation("decode_result_error"),
                AutoCompleteText = $"{actionKeyword} {GetName()} {BaseEncodeDecodeCodec.Encode} ",
                Action = _ => true
            }
        };
    }

    private List<Result> GetInputEmptyResult(IPublicAPI publicApi, string actionKeyword)
    {
        return new List<Result>
        {
            new()
            {
                IcoPath = StringCodec.IconPath,
                Title = publicApi.GetTranslation("input_empty"),
                AutoCompleteText = $"{actionKeyword} {GetName()} {BaseEncodeDecodeCodec.Encode} ",
                Action = _ => false
            }
        };
    }
}
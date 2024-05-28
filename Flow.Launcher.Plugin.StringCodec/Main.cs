using System;
using System.Collections.Generic;
using System.Linq;
using Flow.Launcher.Plugin.StringCodec.Codec;
using Flow.Launcher.Plugin.StringCodec.Model;

namespace Flow.Launcher.Plugin.StringCodec
{
    public class StringCodec : IPlugin, IPluginI18n
    {
        public static readonly string IconPath = "Images\\string-codec-icon.png";

        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }


        public List<Result> Query(Query query)
        {
            var firstSearch = query.FirstSearch;
            var supportCodecList = StringUtils.GetCoeecList();
            // 精确查找,
            var matchCodecList = supportCodecList.Where(codec => codec.MatchType(firstSearch)).ToList();

            if (!matchCodecList.Any())
            {
                // 找不到相应的, 返回相应的提示
                return GetSupportCodec(query.ActionKeyword, firstSearch, supportCodecList);
            }

            // find once, base64 or UUID ...
            var currentCodec = matchCodecList[0];

            return currentCodec.GetResult(_context.API, query.ActionKeyword, query.SecondSearch, query.ThirdSearch);
        }

        private List<Result> GetSupportCodec(string activeKey, string firstSearch, List<BaseCodec> codecList)
        {
            var result = new List<Result>();
            foreach (var codec in codecList)
            {
                var name = codec.GetName();

                if (!string.IsNullOrEmpty(firstSearch))
                {
                    if (name.Equals(firstSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!name.StartsWith(firstSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                result.Add(new Result
                {
                    IcoPath = IconPath,
                    Title = name,
                    SubTitle = codec.GetSubTitle(_context.API),
                    AutoCompleteText = $"{activeKey} {name} ",
                    Action = _ =>
                    {
                        _context.API.ChangeQuery($"{activeKey} {name} ");
                        return false;
                    }
                });
            }

            return result;
        }

        public string GetTranslatedPluginTitle()
        {
            return _context.API.GetTranslation("plugin_title");
        }

        public string GetTranslatedPluginDescription()
        {
            return _context.API.GetTranslation("plugin_desp");
        }
    }
}
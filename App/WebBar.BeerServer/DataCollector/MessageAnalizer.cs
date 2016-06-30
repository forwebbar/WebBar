using System.Collections.Generic;
using System.Globalization;
using Contracts;

namespace WebBar.BeerServer.DataCollector
{
    internal class MessageAnalizer : IMessageAnalizer
    {
        public MessageDto Process(MessageDto item)
        {
            item.Tags = ExpandArrayTags(item.Tags);
            return item;
        }

        private static Dictionary<string, string> ExpandArrayTags(Dictionary<string, string> tags)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var tag in tags)
            {
                dictionary.Add(tag.Key, tag.Value);
                if (tag.Key.Contains("[]"))
                {
                    var expanded = ExpandTag(tag.Key, tag.Value);
                    foreach (var expandedTag in expanded)
                        dictionary.Add(expandedTag.Key, expandedTag.Value);
                }
            }

            return dictionary;
        }

        private static Dictionary<string, string> ExpandTag(string prefix, string value)
        {
            var dictionary = new Dictionary<string, string>();
            var basePrefix = prefix.Substring(0, prefix.IndexOf('['));
            var vals = value.Split(';');
            var i = 0;
            foreach (var val in vals)
            {
                if (!string.IsNullOrEmpty(val))
                    dictionary.Add(basePrefix + (i).ToString(CultureInfo.InvariantCulture), val);

                i++;
            }

            return dictionary;
        }
    }
}
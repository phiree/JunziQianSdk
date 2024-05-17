using System.Collections.Generic;
using System.Dynamic;

namespace JunziQianSdk.Api.Sign
{
    public class TemplateParam
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public IDictionary<string, string> Adapt()
        {

            KeyValuePair<string, string> kv = new KeyValuePair<string, string>(Name, Value);

            var dict = new Dictionary<string, string>();
            dict.Add(Name, Value);
            return dict;
        }

    }

}

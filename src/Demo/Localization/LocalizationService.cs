using System.Collections.Concurrent;

namespace Demo.Localization
{
    public class LocalizationService
    {
        private ConcurrentDictionary<string, string[]> Texts;

        public LocalizationService()
        {
            Texts = new ConcurrentDictionary<string, string[]>();
            Texts.TryAdd("KEY:Test", new string[] { "Localized EN", "Localized FR", "Localized PL" });
        }

        public string Get(string Key, string Lang)
        {
            int Index = 0;
            switch (Lang)
            {
                case "fr":
                    Index = 1;
                    break;
                case "pl":
                    Index = 2;
                    break;
            }
            if (!Texts.ContainsKey(Key))
            {
                Texts.TryAdd(Key, new string[] { string.Format("{0} EN", Key), string.Format("{0} FR", Key), string.Format("{0} PL", Key) });
            }
            return Texts[Key][Index];
        }
    }
}

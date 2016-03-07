using System.Collections.Concurrent;

namespace Demo.Localization
{
    //This class is a small part of a bigger project.
    public class LocalizationService
    {
        /// <summary>
        /// This concurrent dictionary is a "cached" representation of localized strings.
        /// They are retrieved on demand from database to this dictionary
        /// </summary>
        private ConcurrentDictionary<string, string[]> Texts;

        public LocalizationService()
        {
            //For this example, we add manualy a small localized text for 3 Cultures (en, fr & pl).
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
                //If our dictionary doesn't contain our KEY, add it to the cache.
                //In the "full" project, we try to retrieve it from database, or add it if necessary.
                Texts.TryAdd(Key, new string[] { string.Format("{0} EN", Key), string.Format("{0} FR", Key), string.Format("{0} PL", Key) });
            }
            return Texts[Key][Index];
        }
    }
}

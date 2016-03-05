using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Demo.Localization
{
    public class DBStringLocalizer : IStringLocalizer
    {
        private CultureInfo _culture { get; set; }

        private readonly LocalizationService _loc;

        public DBStringLocalizer(LocalizationService loc) : this(loc, CultureInfo.CurrentUICulture)
        {
        }

        public DBStringLocalizer(LocalizationService loc, CultureInfo culture)
        {
            _loc = loc;
            if (culture != null)
                _culture = culture;
            else _culture = CultureInfo.CurrentUICulture;
        }

        public LocalizedString this[string name]
        {
            get
            {
                return new LocalizedString(name,
                    _loc.Get(name, _culture.TwoLetterISOLanguageName));
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var value = _loc.Get(name, _culture.TwoLetterISOLanguageName);

                return new LocalizedString(name, string.Format(value ?? name, arguments), value == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            return new DBStringLocalizer(_loc, culture);
        }
    }
}

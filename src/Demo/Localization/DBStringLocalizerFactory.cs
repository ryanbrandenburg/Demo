using Microsoft.Extensions.Localization;
using System;
using System.Globalization;

namespace Demo.Localization
{
    public class DBStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationService _loc;

        public DBStringLocalizerFactory(LocalizationService loc)
        {
            _loc = loc;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new DBStringLocalizer(_loc, CultureInfo.CurrentUICulture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new DBStringLocalizer(_loc, CultureInfo.CurrentUICulture);
        }
    }
}

// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Demo.Localization.EntityFramework
{
    public class EFStringLocalizer : IStringLocalizer
    {
        private readonly LocalizationDBContext _db;
	private readonly CultureInfo _culture;

        private CultureInfo Culture { get { 
            if(_culture != null)
            {
	        System.Console.WriteLine("CULTURE SAVED");
            }
            return _culture ?? CultureInfo.CurrentUICulture; } }

        public EFStringLocalizer(LocalizationDBContext db, CultureInfo culture = null)
        {
            _culture = culture;
            _db = db;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new EFStringLocalizer(_db, culture);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            return _db.Resources
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == Culture.Name)
                .Select(r => new LocalizedString(r.Key, r.Value, true));
        }

        private string GetString(string name)
        {
	    System.Console.WriteLine(name + " " + Culture.Name);
            return _db.Resources
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == Culture.Name)
                .FirstOrDefault(r => r.Key == name)?.Value;
        }
    }
}

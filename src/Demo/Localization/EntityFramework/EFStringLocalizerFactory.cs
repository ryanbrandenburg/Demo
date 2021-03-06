﻿// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Collections.Generic;
using Demo.Models;
using Microsoft.Extensions.Localization;
namespace Demo.Localization.EntityFramework
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationDBContext _db;

        public EFStringLocalizerFactory()
        {
            _db = new LocalizationDBContext();
            _db.AddRange(
                new Culture
                {
                    Name = "en",
                    Resources = new List<Resource>() { new Resource { Key = "Hello", Value = "Hello" } }
                },
                new Culture
                {
                    Name = "fr",
                    Resources = new List<Resource>() { new Resource { Key = "Hello", Value = "Bonjour" } }
                },
                new Culture
                {
                    Name = "pl",
                    Resources = new List<Resource>() { new Resource { Key = "Hello", Value = "Dzień dobry" } }
                }
                );
            _db.SaveChanges();
        }

        public IStringLocalizer Create(Type resourceSource)
        {

            return new EFStringLocalizer(_db);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(_db);
        }
    }
}

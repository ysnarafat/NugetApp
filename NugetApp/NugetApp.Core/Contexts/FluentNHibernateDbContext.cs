﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.AspNet.Identity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NugetApp.Core.Entities;
using System.Configuration;
using NugetApp.Core.Mappings;

namespace NugetApp.Core.Contexts
{
    public class FluentNHibernateDbContext
    {
        private static ISessionFactory _session;

        private static ISessionFactory CreateSession()
        {
            if (_session != null)
            {
                return _session;
            }

            var myEntities = new[] {
                typeof(ApplicationUser)
            };

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

            FluentConfiguration _config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PackageMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PackageVersionMapping>())
                .ExposeConfiguration(cfg =>
                {
                    cfg.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);
                    new SchemaUpdate(cfg).Execute(false, true);
                });

            _session = _config.BuildSessionFactory();

            return _session;
        }

        public static ISession SessionOpen()
        {
            return CreateSession().OpenSession();
        }
    }
}

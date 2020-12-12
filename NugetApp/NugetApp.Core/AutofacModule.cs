using Autofac;
using NHibernate;
using NugetApp.Core.Contexts;
using NugetApp.Core.Repositories;
using NugetApp.Core.Services;
using NugetApp.Core.UnitofWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PackageRepository>().As<IPackageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PackageUnitofWork>().As<IPackageUnitofWork>().InstancePerLifetimeScope();
            builder.RegisterType<PackageService>().As<IPackageService>().InstancePerLifetimeScope();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.Register(s => FluentNHibernateDbContext.SessionOpen()).As<ISession>().InstancePerLifetimeScope();
            //builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();
            //builder.RegisterType<UserManager<ApplicationUser>>();

            base.Load(builder);
        }
    }
}

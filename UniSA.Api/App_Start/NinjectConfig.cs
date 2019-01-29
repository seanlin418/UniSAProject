using Ninject;
using System;
using System.Reflection;
using Ninject.Web.Common;
using UniSA.Api.Repos;
using UniSA.Api.Services;
using UniSA.Data;
using AutoMapper;

namespace UniSA.Api
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<ApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IClientRepository>().To<ClientRepository>().InRequestScope();
            kernel.Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
            kernel.Bind<IAuthService>().To<AuthService>().InRequestScope();
            kernel.Bind<IMapper>().To<Mapper>().InRequestScope();
        }
    }
}
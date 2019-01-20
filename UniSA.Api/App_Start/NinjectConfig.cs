using Ninject;
using System;
using System.Reflection;
using Ninject.Web.Common;
using UniSA.Api.Repos;

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
            kernel.Bind<UniDBContext>().To<UniDBContext>().InRequestScope();
            kernel.Bind<IAuthRepository>().To<AuthRepository>().InRequestScope();
            kernel.Bind<IClientRepository>().To<ClientRepository>().InRequestScope();
        }
    }
}
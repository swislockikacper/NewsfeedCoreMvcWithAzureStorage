using Autofac;
using NewsfeedCoreMVC.Abstract;
using NewsfeedCoreMVC.Services;

namespace NewsfeedCoreMVC.DIConfiguration
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TableStorageService>()
                .As<ITableStorageService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BlobStorageService>()
                .As<IBlobStorageService>()
                .InstancePerLifetimeScope();
        }
    }
}

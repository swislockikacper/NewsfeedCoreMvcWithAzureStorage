using Autofac;

namespace NewsfeedCoreMVC.DIConfiguration
{
    public static class DIConfiguration
    {
        public static ContainerBuilder Configure(ContainerBuilder builder)
        {
            builder.RegisterModule<ServicesModule>();

            return builder;
        }
    }
}

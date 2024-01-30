using System;
using Autofac;
using LearningXamarin.Services.APIClientService;
using LearningXamarin.Services.NavigationService;
using LearningXamarin.Services.PopupNavigationService;
using LearningXamarin.ViewModels;

namespace LearningXamarin.Helpers
{
    public class ServiceLocator
    {
        private static IContainer container;

        public ServiceLocator()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterServices(ref builder);
            RegisterViewModels(ref builder);

            container = builder.Build();
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        private void RegisterServices(ref ContainerBuilder builder)
        {
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<APIClientService>().As<IAPIClientService>().SingleInstance();
            builder.RegisterType<PopupNavigationService>().As<IPopupNavigationService>().SingleInstance();
        }

        private void RegisterViewModels(ref ContainerBuilder builder)
        {
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<IKEAItemsViewModel>().SingleInstance();
            builder.RegisterType<IKEAItemDetailedViewModel>().SingleInstance();
        }
    }
}


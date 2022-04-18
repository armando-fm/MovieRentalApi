using Autofac;
using Core.Interfaces;
using Core.Services;

namespace Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    // builder.RegisterType<ToDoItemSearchService>()
    //     .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}

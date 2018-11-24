namespace Infrastructure.DependencyContainerBuilder
{
    using Infrastructure.DependencyContainerBuilder.Contract;
    using Infrastructure.Interception.Contract;

    public class TypeRegistrar : ITypeRegistrar
    {
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
            typeRegistrarService.RegisterTypeSingleton<IApplicationDependecyResolver, ApplicationDependecyResolver>();
        }
    }
}
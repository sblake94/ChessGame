using Library.Services;

namespace Library.Attributes.ServiceAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class SingletonService : Attribute
{
    public SingletonService()
    {
        // Ensure that the attribute can only be applied to classes that inherit from ServiceBase
        //if (!typeof(ServiceBase<>).IsAssignableFrom(this.GetType()))
        //{
        //    throw new InvalidOperationException($"The {nameof(SingletonService)} Attribute can only be applied to classes that inherit from ServiceBase.");
        //}
    }
}

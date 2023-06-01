using Library.Services;

namespace Library.Attributes.ServiceAttributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class SingletonService : Attribute
{
    public SingletonService()
    {
        
    }
}

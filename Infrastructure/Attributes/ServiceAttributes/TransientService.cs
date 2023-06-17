using System;

namespace Infrastructure.Attributes.ServiceAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientService : Attribute
    {
        public TransientService()
        {
            // Ensure that the attribute can only be applied to classes that inherit from ServiceBase
            //if (!typeof(ServiceBase<>).IsAssignableFrom(this.GetType()))
            //{
            //    throw new InvalidOperationException($"The {nameof(TransientService)} Attribute can only be applied to classes that inherit from ServiceBase.");
            //}
        }
    }
}
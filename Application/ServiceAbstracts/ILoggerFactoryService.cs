namespace Application.ServiceAbstracts
{
    public interface ILoggerFactoryService : IServiceBase
    {
        void InsertServiceLogger(IServiceBase service);
    }
}
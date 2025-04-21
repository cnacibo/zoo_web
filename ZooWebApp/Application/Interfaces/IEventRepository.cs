namespace ZooWebApp.Application.Interfaces;
public interface IEventRepository
{
    void Publish<TEvent>(TEvent @event) where TEvent : class;
}
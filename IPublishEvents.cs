namespace ClassLibrary4
{
    public interface IPublishEvents
    {
        void Publish<T>(T message) where T : Message;
    }
}
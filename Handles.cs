namespace ClassLibrary4
{
    public interface Handles<in T> where T: Message
    {
        void Handle(T message);
    }
}
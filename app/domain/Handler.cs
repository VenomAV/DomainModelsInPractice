namespace app.domain
{
    public interface Handler<in T>
    {
        void Handle(T message);
    }
}
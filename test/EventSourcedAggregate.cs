namespace test
{
    public interface EventSourcedAggregate
    {
        Event[] UnpublishedEvents { get; }
    }
}
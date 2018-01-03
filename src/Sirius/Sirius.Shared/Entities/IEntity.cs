namespace Sirius.Shared.Entities
{
    public interface IEntity
    {
        long Id { get; set; }

        bool IsDeleted { get; set; }
    }
}

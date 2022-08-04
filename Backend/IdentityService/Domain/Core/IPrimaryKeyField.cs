namespace Domain.Core
{
    public interface IPrimaryKeyField<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}

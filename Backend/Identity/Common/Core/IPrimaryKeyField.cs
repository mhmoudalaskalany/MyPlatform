namespace Common.Core
{
    public interface IPrimaryKeyField<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}

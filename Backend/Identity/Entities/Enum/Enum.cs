namespace Entities.Enum
{
    public enum UnitType
    {
        Sector = 1,
        Directorate,
        Department,
        Section,
        Team
    }

    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }

    public enum StorageType
    {
        LocalStorage = 1,
        BlobStorage
    }
}

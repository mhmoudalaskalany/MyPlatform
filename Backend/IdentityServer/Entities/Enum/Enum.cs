namespace Entities.Enum
{
    public  enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum UnitType
    {
        Sector = 1,
        Directorate,
        Department,
        Section,
        Team
    }

    public enum MaritalStatus
    {
        Single = 1 ,
        Married,
        Divorced,
        Widow
    }
    public enum DoseStatus
    {
        FirstDose = 1,
        SecondDose,
        ThirdDose,
        Exception,
        NonVaccinated
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

    public enum CardType
    {
        Plain = 1,
        Word
    }
}

using System;

namespace Entities.Entities.Views.Murasalat
{
    public class OrganizationHierarchyView
    {
        public Guid? Id { get; set; }
        public Guid? PartyRelationId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ParentPartyRoleId { get; set; }
        public string ParentCode { get; set; }
        public Guid? ChildId { get; set; }
        public string ChildCode { get; set; }
        public Guid? ChildPartyRoleId { get; set; }
        public int? Level { get; set; }
        public string HierarchyPath { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        

    }
}

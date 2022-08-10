using System;

namespace Domain.DTO.Identity.User.Parameters
{
    public class UserSearchCriteriaFilter
    {
        public string SearchCriteria { get; set; }
        public Guid? AppId { get; set; }
    }
}

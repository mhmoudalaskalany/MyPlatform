using System.Collections.Generic;
using Domain.Extensions;

namespace Domain.DTO.Base
{
    public class BaseParam<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public T Filter { get; set; }
        public IEnumerable<SortModel> OrderByValue { get; set; }
    }
}

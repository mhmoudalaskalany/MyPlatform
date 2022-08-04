using System;

namespace Entities.Entities.Views.Oracle
{
    public class EmployeeLeaveView
    {
        public long ID { get; set; }
        public string EMPLOYEE_NUMBER { get; set; }
        public int LEAVE_TYPE_ID { get; set; }
        public string ELEMENT_NAME_BY_ARABIC { get; set; }
        public string ELEMANT_NAME_BY_ENGLISH { get; set; }
        public DateTime? DATE_START { get; set; }
        public DateTime? DATE_END { get; set; }
        public int NO_OF_DAYS { get; set; }
      
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.DTO.Base;

namespace Domain.DTO.Hr.Card
{
    public class GeneralReportFilter : MainFilter
    {
        public Guid? EmployeeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string JobTitle { get; set; }
    }
}

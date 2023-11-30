using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourDiary.Models
{
    public class TourData
    {
        public String UserName { get; set; }
        public String Designation { get; set; }
        public String HeadQuarters { get; set; }
        public String ActualPay { get; set; }
        public String Dep_Station { get; set; }
        public String Dep_Date { get; set; }
        public String Dep_Hour { get; set; }
        public String Arv_Station { get; set; }
        public String Arv_Date { get; set; }
        public String Arv_Hour { get; set; }
        public String JourneyPurpose { get; set; }
        public String JourneyKind { get; set; }
        public String NoOfMiles { get; set; }
        public String RoadRate { get; set; }
        public String RoadAmount { get; set; }
        public String NoOfDays { get; set; }
        public String AllowanceRate { get; set; }
        public String AllowanceAmount { get; set; }
        public String Particulars { get; set; }
        public String ExpenseAmount { get; set; }
    }
}
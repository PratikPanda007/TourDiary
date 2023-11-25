using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace TourDiary.Models
{
    public class User
    {
        public String UserName { get; set; }
        public String UserRole { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }
    }
}
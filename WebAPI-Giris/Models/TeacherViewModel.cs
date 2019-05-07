using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_Giris.Models
{
    public class TeacherViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Nullable<int> StandardId { get; set; }
        public Nullable<int> TeacherType { get; set; }
    }
}
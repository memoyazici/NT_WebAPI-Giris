﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_Giris.Models
{
    public class CoursesViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public System.Data.Entity.Spatial.DbGeography Location { get; set; }
        public Nullable<int> TeacherId { get; set; }
    }
}
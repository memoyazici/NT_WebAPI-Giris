using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Giris.Models;

namespace WebAPI_Giris.Controllers.api
{
    public class StudentController : ApiController
    {
        public IHttpActionResult GetStudent(int id)
        {
            SchoolDBEntities db = new SchoolDBEntities();

            StudentViewModel svm = null;
            svm = db.Students
                .Where(x => x.StudentID == id)
                .Select(x => new StudentViewModel()
                {
                    StudentID = x.StudentID,
                    StudentName = x.StudentName,
                    StudentAddress = x.StudentAddress
                    
                }).FirstOrDefault<StudentViewModel>();

            if (svm == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(svm);
            }

            
        }

        public IHttpActionResult GetAllStudents()
        {
            IList<StudentViewModel> students = null;
            SchoolDBEntities db = new SchoolDBEntities();

            students = db.Students.Select(x => new StudentViewModel()
            {
                StudentID = x.StudentID,
                StudentName = x.StudentName
            }
            ).ToList<StudentViewModel>();

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);

            //http: /api/student
            // http : /api/student?includeAddress=true
        }

        public IHttpActionResult PostNewStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            else
            {
                using (var db = new SchoolDBEntities())
                {
                    db.Students.Add(new Student()
                    {
                        StudentID = student.StudentID,
                        StudentName = student.StudentName
                    });
                    db.SaveChanges();

                }

                return Ok();
            }

           
        }

        public IHttpActionResult Put(StudentViewModel student) {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            else
            {
                using (var db = new SchoolDBEntities())
                {
                    var existingStudent = db.Students
                        .Where(x => x.StudentID == student.StudentID)
                        .FirstOrDefault<Student>();

                    if (existingStudent != null)
                    {
                        existingStudent.StudentName = student.StudentName;
                        db.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return Ok();
            }
            
            
        }

        public IHttpActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Bad request");
            }
            else
            {
                using (var db = new SchoolDBEntities())
                {
                    var student = db.Students
                        .Where(x => x.StudentID == id)
                        .FirstOrDefault();

                    db.Students.Remove(student);
                    db.SaveChanges();
                }


            }
            return Ok();
        }
        
    }
}

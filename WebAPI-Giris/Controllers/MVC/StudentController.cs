using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPI_Giris.Models;

namespace WebAPI_Giris.Controllers.MVC
{
    public class StudentController : Controller
    {
        // GET: Student
        //public ActionResult Index()
        //{
        //    IEnumerable<StudentViewModel> students = null;
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:53923/api/");
        //    //client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));
            
        //    var responseTask = client.GetAsync(client.BaseAddress);
        //    responseTask.Wait();

        //    var result = responseTask.Result;

        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<List<StudentViewModel>>();
        //        readTask.Wait();
        //        students = readTask.Result;
        //    }
        //    else //web api error response
        //    {
        //        students = Enumerable.Empty<StudentViewModel>();
        //        ModelState.AddModelError(string.Empty,"Server error");
        //    }

        //    return View(students);
        //}

        public ActionResult Index()
        {
            IEnumerable<StudentViewModel> students = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53923/api/Student");
            //client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));

            var responseTask = client.GetAsync(client.BaseAddress);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
                readTask.Wait();
                students = readTask.Result;
            }
            else //web api error response
            {
                students = Enumerable.Empty<StudentViewModel>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return View(students);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(StudentViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/student");

                var postTask = client.PostAsJsonAsync<StudentViewModel>("student",student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error");

                    return View(student);
                }
            }
            
        }

        public ActionResult Edit(int id)
        {
            StudentViewModel student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/Student");

                var responseTask = client.GetAsync("student?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<StudentViewModel>();
                    readTask.Wait();
                    student = readTask.Result;
                }
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/student");

                var postTask = client.PutAsJsonAsync<StudentViewModel>("student", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
               
            }
            return View(student);

        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/student");

                var deleteTask = client.DeleteAsync("student/"+ id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
        }

    }
}
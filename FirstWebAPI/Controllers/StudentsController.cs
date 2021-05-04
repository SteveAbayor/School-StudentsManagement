using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentsDataAccess;
using System.Threading;
using System.Web.Mvc;

namespace FirstWebAPI.Controllers
{
    public class StudentsController : ApiController
    {
        [BasicAuthentication]
        public IHttpActionResult Get(string schoolClass, string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (StudentsDBEntities entities = new StudentsDBEntities())
            {

                if (!(gender.ToLower() == "all" || gender.ToLower() == "male" || gender.ToLower() == "female"))
                    return BadRequest();

                switch (schoolClass.ToLower())
                {
                    case "all":
                        return Ok(entities.Students.ToList());
                    case "jss1":
                        return Ok(entities.Students.Where(e => e.Class.ToLower() == schoolClass.ToLower() && e.Gender.ToLower() == gender.ToLower()).ToList());
                    case "jss2":
                        return Ok(entities.Students.Where(e => e.Class.ToLower() == schoolClass.ToLower() && e.Gender.ToLower() == gender.ToLower()).ToList());
                    case "jss3":
                        return Ok(entities.Students.Where(e => e.Class.ToLower() == schoolClass.ToLower() && e.Gender.ToLower() == gender.ToLower()).ToList());
                    case "ss1":
                        return Ok(entities.Students.Where(e => e.Class.ToLower() == schoolClass.ToLower() && e.Gender.ToLower() == gender.ToLower()).ToList());
                    case "ss2":
                        return Ok(entities.Students.Where(e => e.Class.ToLower() == schoolClass.ToLower() && e.Gender.ToLower() == gender.ToLower()).ToList());
                    case "ss3":
                        return Ok(entities.Students.Where(e => e.Class.ToLower() == schoolClass.ToLower() && e.Gender.ToLower() == gender.ToLower()).ToList());
                    default:
                        return BadRequest();
                }
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (StudentsDBEntities entities = new StudentsDBEntities())
            {
                var entity = entities.Students.FirstOrDefault(e => e.ID == id);

                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with Id = " + id.ToString() + " not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Student student)
        {
            try { 
                using(StudentsDBEntities entities = new StudentsDBEntities())
                {
                    entities.Students.Add(student);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, student);
                    message.Headers.Location = new Uri(Request.RequestUri + student.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
         try { 
                using(StudentsDBEntities entities = new StudentsDBEntities())
                {
                    var entity = entities.Students.FirstOrDefault(e => e.ID == id);

                    if(entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Students.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage PUT([FromBody]int id, Student student)
        {
          try { 
            using(StudentsDBEntities entities = new StudentsDBEntities())
                {
                    var entity = entities.Students.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with Id = " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = student.FirstName;
                        entity.LastName = student.LastName;
                        entity.Gender = student.Gender;
                        entity.YearAdmitted = student.YearAdmitted;
                        entity.Class = student.Class;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

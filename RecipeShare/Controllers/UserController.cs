using BLL.DTOS;
using BLL.Services;
using OpenAI_API;
using RecipeShare.Auth;
using RecipeShare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace RecipeShare.Controllers
{
    public class UserController : ApiController
    {
        [AdminAccess]
        [HttpGet]
        [Route("api/user/getall")]
        public HttpResponseMessage GetAllUsers()
        {
            try
            {
                var users = UserServices.Get();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [AdminAccess]
        [HttpGet]
        [Route("api/user/get/{id}")]
        public HttpResponseMessage GetUserById(string id)
        {
            try
            {
                var user = UserServices.GetById(id);
                if (user != null)
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/user/create")]
        public IHttpActionResult CreateUser(UserDTO userDTO)
        {
            try
            {
                var success = UserServices.Create(userDTO);
                if (success)
                    return Created(Request.RequestUri + "/" + userDTO.Uname, "User created successfully");
                else
                    return InternalServerError(new Exception("Failed to create User"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [AdminAccess]
        [HttpPut]
        [Route("api/user/update/{id}")]
        public HttpResponseMessage UpdateUser(string id, UserDTO userDTO)
        {
            try
            {
                userDTO.Uname = id;
                var updatedUser = UserServices.Update(userDTO);
                return Request.CreateResponse(HttpStatusCode.OK, updatedUser);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [AdminAccess]
        [HttpDelete]
        [Route("api/user/delete/{id}")]
        public HttpResponseMessage DeleteUser(string id)
        {
            try
            {
                var operation = UserServices.Delete(id);
                if (operation)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "User deleted successfully." });
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "User not found." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }


        /*
        private string CreateEmailBody(string email, string password)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/template/forgot-password.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{email}", email);
                body = body.Replace("{password}", password);
                body = body.Replace("{frontUrl}");

                return body;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        [HttpPost]
        [Route("api/user/forgotPassword")]
        public async Task<HttpResponseMessage> ForgotPassword([FromBody] LoginModel user)
        {
            var response = new { Message = "Password sent successfully to your email" };

            var userObj = UserServices.GetByEmailAndPassword(user.Email, user.Password);
            if (userObj == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "User not found" });
            }

            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));
            message.Subject = "Password by Management System";
            message.Body = CreateEmailBody(user.Email, userObj.Password);
            message.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }

        */



       
    }
}
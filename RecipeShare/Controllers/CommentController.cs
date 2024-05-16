using BLL.DTOS;
using BLL.Services;
using RecipeShare.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecipeShare.Controllers
{
    public class CommentController : ApiController
    {
        // Comments

        [AdminAccess]
        [HttpGet]
        [Route("api/comment/getall")]
        public HttpResponseMessage GetAllComments()
        {
            try
            {
                var comments = CommentServices.Get();
                return Request.CreateResponse(HttpStatusCode.OK, comments);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }






        [AdminAccess]
        [HttpPost]
        [Route("api/user/comment/create")]
        public IHttpActionResult CreateComment(CommentDTO commentDTO)
        {
            try
            {
                var success = CommentServices.CreateComment(commentDTO);
                if (success)
                    return Created(Request.RequestUri + "/" + commentDTO.Id, "Comment created successfully");
                else
                    return InternalServerError(new Exception("Failed to create comment"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [AdminAccess]
        [HttpPut]
        [Route("api/user/comment/update/{id}")]
        public HttpResponseMessage UpdateComment(int id, CommentDTO commentDTO)
        {
            try
            {
                commentDTO.Id = id;
                var updatedComment = CommentServices.UpdateComment(commentDTO);
                return Request.CreateResponse(HttpStatusCode.OK, updatedComment);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }


        [HttpDelete]
        [Route("api/user/comment/delete/{id}")]
        public HttpResponseMessage DeleteComment(int id)
        {
            try
            {
                var operation = CommentServices.DeleteComment(id);
                if (operation)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Comment deleted successfully." });
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Comment not found." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }


        [AdminAccess]
        [HttpGet]
        [Route("api/comment/getwithuser/{id}")]
        public HttpResponseMessage GetCommentWithUser(int id)
        {
            try
            {
                var commentWithUser = CommentServices.GetwithUser(id);
                if (commentWithUser != null)
                    return Request.CreateResponse(HttpStatusCode.OK, commentWithUser);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Comment not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
        

    }
}

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class CommentController : ApiController
    {
        /// <summary>
        /// API Method to get all comments in a course project
        /// </summary>
        /// <param name="commentInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<List<Comment>> GetAll(CommentInfo commentInfo)
        {
            var cmtMngr = new CommentManager();
            var retVal = cmtMngr.GetAllComments(commentInfo);
            if(retVal == null)
            {
                retVal = new List<Comment>();
            }
            return Json(retVal);
        }
        /// <summary>
        /// /// API Method to add a comment to a university project
        /// </summary>
        /// <param name="newComment"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewComment newComment)
        {
            var cmtMngr = new CommentManager();
            var retVal = cmtMngr.CreateComment(newComment,"");
            return Json(retVal);
        }
        /// <summary>
        /// API Method to add a comment to a university project (uploading a file to the user's user file repository's service through MyLearn)
        /// </summary>
        /// <param name="newComment"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> CreateWithFile(NewCommentWithFile newComment)
        {
            var cmtMngr = new CommentManager();
            var retVal = cmtMngr.CreateCommentWithFile(newComment);
            return Json(retVal);
        }
    }
}

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
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewComment newComment)
        {
            var cmtMngr = new CommentManager();
            var retVal = cmtMngr.CreateComment(newComment,"");
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> CreateWithFile(NewCommentWithFile newComment)
        {
            var cmtMngr = new CommentManager();
            var retVal = cmtMngr.CreateCommentWithFile(newComment);
            return Json(retVal);
        }
    }
}

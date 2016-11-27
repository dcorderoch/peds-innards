using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class CommentManager
    {
        public List<Comment> GetAllComments(CommentInfo commentInfo)
        {
            List<Comment> allComments = new List<Comment>();

            return allComments;

        }

        public ReturnCode CreateComment(NewComment newComment)
        {
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }
    }
}
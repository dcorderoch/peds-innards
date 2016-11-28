using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class CommentManager
    {
        public List<Comment> GetAllComments(CommentInfo commentInfo)
        {   
            ProjectCommentRepository commentRepo = new ProjectCommentRepository();
            ProjectRepository projectRepo = new ProjectRepository();
            Project project = projectRepo.GetProjectByStudentAndCourseId(new Guid(commentInfo.StudentId), new Guid(commentInfo.CourseId));
            List<Comment> allComments = new List<Comment>();
            List<MyLearnDAL.Models.ProjectComment> projectComments = commentRepo.GetProjectCommentByProjectId(project.ProjectId);

            foreach (ProjectComment projectComment in projectComments)
            {
                Comment comment = new Comment();
                comment.CommentContent = projectComment.Comment;
                comment.CommentId = projectComment.CommentId.ToString();
                comment.ParentId = projectComment.ParentId.ToString();
                comment.IsFromStudent = IsStudent(projectComment.UserId);
                
            }
            return allComments;

        }

        public ReturnCode CreateComment(NewComment newComment)
        {
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }

        private int IsStudent(Guid userId)
        {
         UserRepository userRepo = new UserRepository();
            int retVal =0;
            User user = userRepo.GetUserById(userId);
            if (user.RoleId == 1)
            {
                retVal = 1;
            }
            return retVal;
        }

        private List<List<Comment>> ObtainNestedComments(List<MyLearnDAL.Models.ProjectComment> projectComments)
        {
            List<List<Comment>> resultComments = new List<List<Comment>>();
            List<Comment> parentComments = new List<Comment>();

            foreach (ProjectComment projectComment in projectComments)
            {
                Comment comment = new Comment();
                if (projectComment.ParentId == Guid.Empty)
                {
                    comment.CommentId = projectComment.CommentId.ToString();
                    comment.CommentContent = projectComment.Comment;
                    

                    //parentComments.Add();
                }
            }    
        }
    }
}
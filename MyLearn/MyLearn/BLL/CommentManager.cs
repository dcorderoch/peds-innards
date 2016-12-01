using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class CommentManager
    {
        public List<Comment> GetAllComments(CommentInfo commentInfo)
        {
            using (var context = new MyLearnContext())
            {
                ProjectCommentRepository commentRepo = new ProjectCommentRepository(context);
                ProjectRepository projectRepo = new ProjectRepository(context);
                Project project = projectRepo.GetProjectByStudentAndCourseId(new Guid(commentInfo.StudentUserId), new Guid(commentInfo.CourseId));
                List<Comment> allComments = new List<Comment>();
                List<MyLearnDAL.Models.ProjectComment> projectComments = (project == null)? new List<MyLearnDAL.Models.ProjectComment>(): commentRepo.GetProjectCommentByProjectId(project.ProjectId);
                List<List<Comment>> splitComments = ObtainNestedComments(projectComments);
                List<Comment> parentComments = splitComments[0];
                List<Comment> childComments = splitComments[1];

                foreach (Comment childComment in childComments)
                {
                    Comment parentComment = parentComments.Find(x => x.CommentId == childComment.ParentId);
                    parentComment.NestedComments.Add(childComment);
                    allComments.Add(parentComment);
                }

                commentRepo.Dispose();
                projectRepo.Dispose();
                return allComments;
            }

        }

        public ReturnCode CreateComment(NewComment newComment)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode success = new ReturnCode();
                ProjectComment newProjectComment = new ProjectComment();
                ProjectRepository projectRepository = new ProjectRepository(context);
                ProjectCommentRepository projectCommentRepo = new ProjectCommentRepository(context);
                Project project = projectRepository.GetProjectByStudentAndCourseId(new Guid(newComment.StudentId), new Guid(newComment.CourseId));

                if (newComment.Comment != null)
                {
                    newProjectComment.CommentId = Guid.NewGuid();
                    newProjectComment.Comment = newComment.Comment;
                    newProjectComment.Date = DateTime.Now;
                    newProjectComment.ParentId = new Guid(newComment.ParentId);
                    if (newComment.Commenter == 1)
                    {
                        newProjectComment.UserId = new Guid(newComment.StudentId);
                    }
                    else
                    {
                        newProjectComment.UserId = new Guid(newComment.ProfUserId);
                    }
                    newProjectComment.ProjectId = project.ProjectId;
                    projectCommentRepo.Add(newProjectComment);
                    success.ReturnStatus = 1;
                }
                projectCommentRepo.SaveChanges();
                projectCommentRepo.Dispose();
                projectRepository.Dispose();
                return success;
            }
        }

        private int IsStudent(Guid userId)
        {
            using (var context = new MyLearnContext())
            {
                UserRepository userRepo = new UserRepository(context);
                int retVal = 0;
                User user = userRepo.GetUserById(userId);
                if (user.RoleId == 1)
                {
                    retVal = 1;
                }
                return retVal;
            }
        }

        private List<List<Comment>> ObtainNestedComments(List<MyLearnDAL.Models.ProjectComment> projectComments)
        {
            List<List<Comment>> resultComments = new List<List<Comment>>();
            List<Comment> parentComments = new List<Comment>();
            List<Comment> childComments = new List<Comment>();

            foreach (ProjectComment projectComment in projectComments)
            {
                Comment comment = new Comment();
                if (projectComment.ParentId == Guid.Empty)
                {
                    comment.CommentId = projectComment.CommentId.ToString();
                    comment.CommentContent = projectComment.Comment;
                    comment.Date = projectComment.Date.ToString();
                    comment.IsFromStudent = IsStudent(projectComment.UserId);
                    comment.ParentId = projectComment.ParentId.ToString();
                    comment.NestedComments = new List<Comment>();
                    parentComments.Add(comment);

                }
                else
                {
                    comment.CommentId = projectComment.CommentId.ToString();
                    comment.CommentContent = projectComment.Comment;
                    comment.Date = projectComment.Date.ToString();
                    comment.IsFromStudent = IsStudent(projectComment.UserId);
                    comment.ParentId = projectComment.ParentId.ToString();
                    comment.NestedComments = null;
                    childComments.Add(comment);
                }
            }
            resultComments.Add(parentComments);
            resultComments.Add(childComments);
            return resultComments;    
        }
    }
}
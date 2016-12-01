using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using JobOfferComment = MyLearn.Models.JobOfferComment;

namespace MyLearn.BLL
{
    public class JobCommentManager
    {
        public List<JobOfferComment> GetAllComments(string jobOfferId)
        {/*
            using (var context = new MyLearnContext())
            {
                ProjectCommentRepository commentRepo = new ProjectCommentRepository(context);
                ProjectRepository projectRepo = new ProjectRepository(context);
                Project project = projectRepo.GetProjectByStudentAndCourseId(new Guid(commentInfo.StudentUserId), new Guid(commentInfo.CourseId));
                List<Comment> allComments = new List<Comment>();
                List<MyLearnDAL.Models.ProjectComment> projectComments = (project == null) ? new List<MyLearnDAL.Models.ProjectComment>() : commentRepo.GetProjectCommentByProjectId(project.ProjectId);
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
            }*/
            return new List<JobOfferComment>();
        }

        public ReturnCode CreateComment(NewJobComment newJobComment)
        {
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }
        public ReturnCode CreateCommentWithFile(NewJobCommentWithFile newJobComment)
        {
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }
    }
}
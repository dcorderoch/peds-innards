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
        {
            using (var context = new MyLearnContext())
            {
                var commentRepo = new JobOfferCommentRepository(context);
                var jobOfferRepo = new JobOfferRepository(context);
                var jobOffer = jobOfferRepo.GetJobOfferById(new Guid(jobOfferId));
                List<JobOfferComment> allComments = new List<JobOfferComment>();
                List<MyLearnDAL.Models.JobOfferComment> jobOfferComments = (jobOffer == null) ? new List<MyLearnDAL.Models.JobOfferComment>() : commentRepo.GetJobOfferCommentsByJobOfferId(Guid.Parse(jobOfferId));
                List<List<JobOfferComment>> splitComments = ObtainNestedComments(jobOfferComments);
                List<JobOfferComment> parentComments = splitComments[0];
                List<JobOfferComment> childComments = splitComments[1];

                foreach (JobOfferComment childComment in childComments)
                {
                    JobOfferComment parentComment = parentComments.Find(x => x.CommentId == childComment.ParentId);
                    parentComment.NestedComments.Add(childComment);
                }
                allComments.AddRange(parentComments);
                commentRepo.Dispose();
                jobOfferRepo.Dispose();
                return allComments;
            }
            }
        

        public ReturnCode CreateComment(NewJobComment newJobComment, string link)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode success = new ReturnCode();
                var newJobOfferComment = new MyLearnDAL.Models.JobOfferComment();
                var jobOfferRepo = new JobOfferRepository(context);
                var jobCommentRepo = new JobOfferCommentRepository(context);
                var jobOffer = jobOfferRepo.GetJobOfferById(Guid.Parse(newJobComment.JobOfferId));
                
                if (newJobComment.JobOfferComment != null)
                {
                    newJobOfferComment.CommentId = Guid.NewGuid();
                    newJobOfferComment.Comment = newJobComment.JobOfferComment;
                    newJobOfferComment.Date = DateTime.Now;
                    newJobOfferComment.ParentId = newJobComment.ParentId == "-1" ? Guid.Empty : new Guid(newJobComment.ParentId);
                    newJobOfferComment.UserId = newJobComment.Commenter == 1 ? new Guid(newJobComment.StudentUserId) : new Guid(newJobComment.EmployerUserId);
                    newJobOfferComment.JobOfferId = jobOffer.JobOfferId;
                    newJobOfferComment.File = link;
                    jobCommentRepo.Add(newJobOfferComment);
                    jobCommentRepo.SaveChanges();
                    success.ReturnStatus = 1;
                    
                }
                jobCommentRepo.Dispose();
                jobOfferRepo.Dispose();


                success.ReturnStatus = 1;
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

        private List<List<JobOfferComment>> ObtainNestedComments(List<MyLearnDAL.Models.JobOfferComment> projectComments)
        {
            List<List<JobOfferComment>> resultComments = new List<List<JobOfferComment>>();
            List<JobOfferComment> parentComments = new List<JobOfferComment>();
            List<JobOfferComment> childComments = new List<JobOfferComment>();

            foreach (var projectComment in projectComments)
            {
                var comment = new JobOfferComment();
                if (projectComment.ParentId == Guid.Empty)
                {
                    comment.CommentId = projectComment.CommentId.ToString();
                    comment.CommentContent = projectComment.Comment;
                    comment.Date = projectComment.Date.ToString();
                    comment.IsFromStudent = IsStudent(projectComment.UserId);
                    comment.ParentId = projectComment.ParentId.ToString();
                    comment.NestedComments = new List<JobOfferComment>();
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
        public ReturnCode CreateCommentWithFile(NewJobCommentWithFile newJobComment)
        {
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }
    }
}
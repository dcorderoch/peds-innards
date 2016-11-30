using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Badge = MyLearn.Models.Badge;
using Course = MyLearn.Models.Course;

namespace MyLearn.BLL
{
    public class CourseManager
    {
        public ReturnCode CreateCourse(NewCourse newCourse)
        {
            ReturnCode retVal = new ReturnCode();
            CourseRepository courseRepo = new CourseRepository();
            MyLearnDAL.Models.Course course = new MyLearnDAL.Models.Course();
            List<MyLearnDAL.Models.Course> currentCourses = courseRepo.GetUniversityCourses(new Guid(newCourse.UniversityId));
            MyLearnDAL.Models.Course verifyCourse = currentCourses.Find(x => x.Name == newCourse.CourseName && x.Group == Convert.ToInt32(newCourse.Group));
            if (verifyCourse == null)
            {
                course.CourseId = Guid.NewGuid();
                course.Name = newCourse.CourseName;
                course.Description = newCourse.CourseDescription;
                course.Group = Convert.ToInt32(newCourse.Group);
                course.ProfessorId = new Guid(newCourse.ProfUserId);
                course.UniversityId = new Guid(newCourse.UniversityId);
                course.MinScore = newCourse.MinGrade;
                course.IsActive = 1;

                List<Badge> badges = newCourse.Badges;
                var achievements = new List<Achievement>();
                foreach (var badge in badges)
                {
                    var achievement = new Achievement();
                    achievement.AchievementId = new Guid(badge.BadgeId);
                    achievement.CourseId = course.CourseId;
                    achievement.Description = badge.BadgeDescription;
                    achievement.Score = badge.Value;
                    achievements.Add(achievement);
                }
                course.Achievements = achievements;
                course.Students = new List<Student>();
                courseRepo.Add(course);
                courseRepo.SaveChanges();
                retVal.ReturnStatus = 1;
            }
            courseRepo.Dispose();
            return retVal;
        }

        public ReturnCode CloseCourse(string courseId)
        {
            ReturnCode returnCode = new ReturnCode();
            CourseRepository courseRepo = new CourseRepository();
            var course = courseRepo.GetCoursebyId(new Guid(courseId));
            if (course != null)
            {
                course.IsActive = 0;
                courseRepo.SaveChanges();
                returnCode.ReturnStatus = 1;
            }
            courseRepo.Dispose();
            return returnCode;
        }

        public Course GetCourseAsProfessor(string courseId)
        {
            CourseRepository courseRepo = new CourseRepository();
            var course = courseRepo.GetCoursebyId(new Guid(courseId));
            Course retCourse = new Course();
            if (course != null)
            {
                retCourse.CourseId = course.CourseId.ToString();
                retCourse.CourseName = course.Name;
                retCourse.UniversityId = course.UniversityId.ToString();
                retCourse.Group = course.Group;
                retCourse.CourseDescription = course.Description;
                retCourse.MinGrade = course.MinScore;
                List<StudentInCourse> resultList = new List<StudentInCourse>();
                List<Student> listOfStudents = course.Students;
                foreach (var student in listOfStudents)
                {
                    var resStudent = new StudentInCourse();
                    resStudent.StudentUserId = student.UserId.ToString();
                    resStudent.Nombre = student.Name;
                    resultList.Add(resStudent);
                }
            }
            courseRepo.Dispose();
            return retCourse;
        }

        public SpecificCourse GetSpecificCourse(SharedAreaCredentials credentials)
        {
            ProjectRepository projectRepository = new ProjectRepository();
            SpecificCourse specificCourse = new SpecificCourse();
            var project = projectRepository.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId),
                new Guid(credentials.CourseId));
            if (project != null)
            {
                specificCourse.NombreContacto = project.Student.Name;
                specificCourse.ApellidoContacto = project.Student.LastName;
                specificCourse.Grade = project.Score;
                var listOfBadges = project.Badges;
                List<Badge> resultBadge = new List<Badge>();
                foreach (var badge in listOfBadges)
                {
                    var newbadge = new Badge();
                    newbadge.BadgeId = badge.AchievementId.ToString();
                    newbadge.BadgeDescription = badge.Achievement.Description;
                    newbadge.Value = badge.Achievement.Score;
                    newbadge.Alardeado = badge.Bragged;
                    newbadge.Awarded = 1;
                    resultBadge.Add(newbadge);
                }
                specificCourse.Badges = resultBadge;
            }
            projectRepository.Dispose();
            return specificCourse;   
        }

        public CourseAsStudent GetCourseAsStudent(CourseAsStudentCredentials credentials)
        {
            CourseAsStudent courseAsStudent = new CourseAsStudent();
            CourseRepository courseRepo = new CourseRepository();
            var projectRepo = new ProjectRepository();
            var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));
            var studentCourses = courseRepo.GetStudentCourses(new Guid(credentials.StudentUserId));
            var course = studentCourses.Find(x => x.CourseId == new Guid(credentials.CourseId));
            if (course != null)
            {
                courseAsStudent.CourseName = course.Name;
                courseAsStudent.StudentUserId =credentials.StudentUserId;
                courseAsStudent.ProfUserId = course.ProfessorId.ToString();
                courseAsStudent.ProfessorName = course.Professor.Name + " " + course.Professor.Lastname;
                courseAsStudent.UniversityId = course.UniversityId.ToString();
                courseAsStudent.Grade = course.MinScore;

                List<MyLearnDAL.Models.Badge> listOfBadges = project.Badges;
                List<Badge> resBadges = new List<Badge>();
                foreach (MyLearnDAL.Models.Badge newBadge in listOfBadges)
                {
                    var badge = new Badge();
                    badge.BadgeId = newBadge.AchievementId.ToString();
                    badge.BadgeDescription = newBadge.Achievement.Description;
                    badge.Alardeado = newBadge.Bragged;
                    badge.Awarded = 1;
                    badge.Value = newBadge.Achievement.Score;
                    resBadges.Add(badge);
                }

                courseAsStudent.Badges = resBadges;
                courseAsStudent.CourseId = course.CourseId.ToString();
                courseAsStudent.CourseDescription = course.Description;
                courseAsStudent.Group = course.Group;
                courseAsStudent.CourseState = course.IsActive;
            }
            courseRepo.Dispose();
            projectRepo.Dispose();
            return courseAsStudent;
                
        }

        public AllProfessorsCourses GetAllByProfessor(string professorId)
        {
            AllProfessorsCourses allCourses = new AllProfessorsCourses();
            //get all professor's courses
            List<ActiveCourse> activeCourses = new List<ActiveCourse>();
            List<FinishedCourse> finishedCourses = new List<FinishedCourse>();
            //get from db and add them here, once the repositories are up and running

            //activeCourses = ;
            //finishedCourses = ;

            allCourses.ActiveCourses = activeCourses;
            allCourses.FInishedCourses = finishedCourses;

            return allCourses;
        }

        public List<CourseShort> GetAllByUniversity(string universityId)
        {
         List<CourseShort> listOfCourses = new List<CourseShort>();
            //get courses by universityId
            //listOfCourses= dbobject(universityId);
            return listOfCourses;
        }

        public ReturnCode Join(StudentJoinsCourse joiningStudent)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }

        public ReturnCode Propose(ProjectProposal proposal)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }
    }
}
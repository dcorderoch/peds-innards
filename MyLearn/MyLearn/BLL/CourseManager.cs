using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL;
using MyLearn.Utils;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Badge = MyLearn.Models.Badge;
using Course = MyLearn.Models.Course;

namespace MyLearn.BLL
{
    public class CourseManager
    {
        private ModelMapper mapper;

        public CourseManager()
        {
            mapper = new ModelMapper();
        }

        public CourseManager(ModelMapper mapper)
        {
            this.mapper = mapper;
        }

        public ReturnCode CreateCourse(NewCourse newCourse)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode retVal = new ReturnCode();
                CourseRepository courseRepo = new CourseRepository(context);
                MyLearnDAL.Models.Course course = new MyLearnDAL.Models.Course();
                List<MyLearnDAL.Models.Course> currentCourses =
                    courseRepo.GetUniversityCourses(new Guid(newCourse.UniversityId));
                MyLearnDAL.Models.Course verifyCourse =
                    currentCourses.Find(
                        x => x.Name == newCourse.CourseName && x.Group == Convert.ToInt32(newCourse.Group));
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
                    course.Achievements = mapper.BadgeToAchievementListMap(newCourse.Badges, course);
                    course.Students = new List<Student>();
                    courseRepo.Add(course);
                    courseRepo.SaveChanges();
                    retVal.ReturnStatus = 1;
                }
                courseRepo.Dispose();
                return retVal;
            }
        }

        public ReturnCode CloseCourse(string courseId)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode returnCode = new ReturnCode();
                CourseRepository courseRepo = new CourseRepository(context);
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
        }
        
        public Course GetCourseAsProfessor(string courseId)
        {
            using (var context = new MyLearnContext())
            {
                CourseRepository courseRepo = new CourseRepository(context);
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
                    retCourse.Students = mapper.StudentInCourseMap(course.Students);
                }
                courseRepo.Dispose();
                return retCourse;
            }
        }

        public SpecificCourse GetSpecificCourse(SharedAreaCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                ProjectRepository projectRepository = new ProjectRepository(context);
                SpecificCourse specificCourse = new SpecificCourse();
                var project = projectRepository.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId),
                    new Guid(credentials.CourseId));
                if (project != null)
                {
                specificCourse.NombreContacto = project.Student.Name;
                specificCourse.ApellidoContacto = project.Student.LastName;
                specificCourse.Grade = project.Score;
                var listOfBadges = project.Badges;
                List<Badge> resultBadge = mapper.BadgeListMap(listOfBadges);
                specificCourse.Badges = resultBadge;
                }
                projectRepository.Dispose();
                return specificCourse;
            }
        }

        public CourseAsStudent GetCourseAsStudent(CourseAsStudentCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                CourseAsStudent courseAsStudent = new CourseAsStudent();
                CourseRepository courseRepo = new CourseRepository(context);
                var projectRepo = new ProjectRepository(context);
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
                    List<Badge> resBadges = mapper.BadgeListMap(listOfBadges);

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
                
        }

        public AllProfessorsCourses GetAllByProfessor(string professorId)
        {
            using (var context = new MyLearnContext())
            {
                CourseRepository courseRepo = new CourseRepository(context);
                var allCourses = new AllProfessorsCourses();
                var activeCourses = courseRepo.GetProfessorActiveCourses(new Guid(professorId));
                var inactiveCourses = courseRepo.GetProfessorInctiveCourses(new Guid(professorId));
                if (activeCourses != null && inactiveCourses != null)
                {
                    List<ActiveCourse> activeCoursesList = mapper.ActiveCourseListMap(activeCourses);
                    List<FinishedCourse> finishedCoursesList = mapper.FinishedCourseListMap(inactiveCourses);
                    allCourses.ActiveCourses = activeCoursesList;
                    allCourses.FInishedCourses = finishedCoursesList;
                }
                courseRepo.Dispose();
                return allCourses;
            }
            
        }

        public List<CourseShort> GetAllByUniversity(string universityId)
        {
            using (var context = new MyLearnContext())
            {
                CourseRepository courseRepo = new CourseRepository(context);

                List<CourseShort> listOfCourses = new List<CourseShort>();
                var coursesByUniversity = courseRepo.GetUniversityCourses(new Guid(universityId));
                if (coursesByUniversity != null)
                {
                    listOfCourses = mapper.CourseShortListMap(coursesByUniversity);
                }
                courseRepo.Dispose();
                return listOfCourses;
            }

        }

        public ReturnCode Join(StudentJoinsCourse joiningStudent)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var studentRepo = new StudentRepository(context);
                var retVal = new ReturnCode();
                var student = studentRepo.GetStudentById(new Guid(joiningStudent.StudentUserId));
                var course = courseRepo.GetCoursebyId(new Guid(joiningStudent.CourseId));

                if (course != null && student != null)
                {
                    course.Students.Add(student);
                    courseRepo.SaveChanges();
                    retVal.ReturnStatus = 1;
                }

                courseRepo.Dispose();
                studentRepo.Dispose();
                return retVal;
            }
        }

        public ReturnCode Propose(ProjectProposal proposal)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var studentRepo = new StudentRepository(context);
                var projectRepo = new ProjectRepository(context);
                var course = courseRepo.GetCoursebyId(new Guid(proposal.CourseId));
                var student = studentRepo.GetStudentById(new Guid(proposal.StudentUserId));
                var retVal = new ReturnCode();

                if (course != null && student != null)
                {
                    var project = new Project();
                    project.ProjectId = Guid.NewGuid();
                    project.CourseId = new Guid(proposal.CourseId);
                    project.UserId = new Guid(proposal.StudentUserId);
                    project.IsActive = 1;
                    project.Description = proposal.Description;
                    project.Score = 0;
                    project.Badges = new List<MyLearnDAL.Models.Badge>();
                    project.ProjectComments = new List<ProjectComment>();
                    projectRepo.Add(project);
                    projectRepo.SaveChanges();
                    retVal.ReturnStatus = 1;
                }
                courseRepo.Dispose();
                studentRepo.Dispose();
                projectRepo.Dispose();
                return retVal;
            }
        }
    }
}
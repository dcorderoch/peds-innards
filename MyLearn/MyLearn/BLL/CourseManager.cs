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
                var retVal = new ReturnCode();
                var courseRepo = new CourseRepository(context);
                var course = new MyLearnDAL.Models.Course();
                var currentCourses =courseRepo.GetUniversityCourses(new Guid(newCourse.UniversityId));
                var verifyCourse =
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
                else
                {
                    retVal.ReturnStatus = 0;
                }
                courseRepo.Dispose();
                return retVal;
            }
        }

        public ReturnCode CloseCourse(string courseId)
        {
            using (var context = new MyLearnContext())
            {
                var returnCode = new ReturnCode();
                var courseRepo = new CourseRepository(context);
                var course = courseRepo.GetCoursebyId(new Guid(courseId));
                if (course != null)
                {
                    course.IsActive = 0;
                    courseRepo.SaveChanges();
                    returnCode.ReturnStatus = 1;
                }
                else
                {
                    returnCode.ReturnStatus = 0;
                }
                courseRepo.Dispose();
                return returnCode;
            }
        }
        
        public Course GetCourseAsProfessor(string courseId)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var course = courseRepo.GetCoursebyId(new Guid(courseId));
                Course retCourse = null;
                if (course != null)
                {
                    retCourse = new Course
                    {
                        CourseId = course.CourseId.ToString(),
                        CourseName = course.Name,
                        UniversityId = course.UniversityId.ToString(),
                        Group = course.Group,
                        CourseDescription = course.Description,
                        MinGrade = course.MinScore,
                        Students = mapper.StudentInCourseMap(course.Students)
                    };
                }
                courseRepo.Dispose();
                return retCourse;
            }
        }

        public SpecificCourse GetSpecificCourse(SharedAreaCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                var projectRepository = new ProjectRepository(context);
                SpecificCourse specificCourse = null;
                var project = projectRepository.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId),
                    new Guid(credentials.CourseId));
                if (project != null)
                {
                    var listOfBadges = project.Badges;
                    var resultBadge = mapper.BadgeListMap(listOfBadges);
                    specificCourse = new SpecificCourse
                    {
                        NombreContacto = project.Student.Name,
                        ApellidoContacto = project.Student.LastName,
                        Grade = project.Score,
                        Badges = resultBadge
                    };
                }
                projectRepository.Dispose();
                return specificCourse;
            }
        }

        public CourseAsStudent GetCourseAsStudent(CourseAsStudentCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                CourseAsStudent courseAsStudent = null;
                var courseRepo = new CourseRepository(context);
                var projectRepo = new ProjectRepository(context);
                var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));
                var studentCourses = courseRepo.GetStudentCourses(new Guid(credentials.StudentUserId));
                var course = studentCourses.Find(x => x.CourseId == new Guid(credentials.CourseId));
                if (course != null)
                {
                    var listOfBadges = project.Badges;
                    var resBadges = mapper.BadgeListMap(listOfBadges);
                    courseAsStudent = new CourseAsStudent
                    {
                        CourseName = course.Name,
                        StudentUserId = credentials.StudentUserId,
                        ProfUserId = course.ProfessorId.ToString(),
                        ProfessorName = course.Professor.Name + " " + course.Professor.Lastname,
                        UniversityId = course.UniversityId.ToString(),
                        Grade = course.MinScore,
                        Badges = resBadges,
                        CourseId = course.CourseId.ToString(),
                        CourseDescription = course.Description,
                        Group = course.Group,
                        CourseState = course.IsActive
                    };
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
                var courseRepo = new CourseRepository(context);
                AllProfessorsCourses allCourses = null;
                var activeCourses = courseRepo.GetProfessorActiveCourses(new Guid(professorId));
                var inactiveCourses = courseRepo.GetProfessorInctiveCourses(new Guid(professorId));
                if (activeCourses != null && inactiveCourses != null)
                {
                    var activeCoursesList = mapper.ActiveCourseListMap(activeCourses);
                    var finishedCoursesList = mapper.FinishedCourseListMap(inactiveCourses);
                    allCourses = new AllProfessorsCourses
                    {
                        ActiveCourses = activeCoursesList,
                        FInishedCourses = finishedCoursesList
                    };
                }
                courseRepo.Dispose();
                return allCourses;
            }
            
        }

        public List<CourseShort> GetAllByUniversity(string universityId)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);

                List<CourseShort> listOfCourses = null;
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
                else
                {
                    retVal.ReturnStatus = 0;
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
                    var project = new Project
                    {
                        ProjectId = Guid.NewGuid(),
                        CourseId = new Guid(proposal.CourseId),
                        UserId = new Guid(proposal.StudentUserId),
                        IsActive = 1,
                        Description = proposal.Description,
                        Score = 0,
                        Badges = new List<MyLearnDAL.Models.Badge>(),
                        ProjectComments = new List<ProjectComment>()
                    };
                    projectRepo.Add(project);
                    projectRepo.SaveChanges();
                    retVal.ReturnStatus = 1;
                }
                else
                {
                    retVal.ReturnStatus = 0;
                }
                courseRepo.Dispose();
                studentRepo.Dispose();
                projectRepo.Dispose();
                return retVal;
            }
        }
    }
}
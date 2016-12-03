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
    /// <summary>
    /// Class built to manage everything related to courses and academic projects.
    /// </summary>
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

        /// <summary>
        /// Method in charge of creating a new course.
        /// </summary>
        /// <param name="newCourse"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
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
        /// <summary>
        /// Method that closes the given course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
        public ReturnCode CloseCourse(string courseId)
        {
            using (var context = new MyLearnContext())
            {
                var returnCode = new ReturnCode();
                var courseRepo = new CourseRepository(context);
                var projectRepo = new ProjectRepository(context);
                var studentRepo = new StudentRepository(context);
                var course = courseRepo.GetCoursebyId(new Guid(courseId));
                

                if (course != null)
                {
                    var studentsInCourse = course.Students;
                    foreach (var student in studentsInCourse)
                    {
                        var project = projectRepo.GetProjectByStudentAndCourseId(student.UserId, Guid.Parse(courseId));
                        if (project == null) {
                            continue;
                        }
                        if (project.Score >= course.MinScore) {
                            student.AvgCourses = CalculateAverage(project.Score, student.NumSuceedCourses + student.NumFailedCourses, student.AvgCourses);
                            student.NumSuceedCourses += 1;
                        } else {
                            student.AvgCourses = CalculateAverage(project.Score, student.NumSuceedCourses + student.NumFailedCourses, student.AvgCourses);
                            student.NumFailedCourses += 1;
                        }
                        studentRepo.Update(student);
                    }
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
        /// <summary>
        /// Auxiliary method that calculates the average grade for a student.
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="totalProjects"></param>
        /// <param name="currentAverage"></param>
        /// <returns>Average grade.</returns>
        private decimal CalculateAverage(decimal grade, int totalProjects, decimal currentAverage)
        {
            var val = currentAverage * totalProjects + grade;
            var retVal = val / (totalProjects + 1);
            return retVal;
        }

        /// <summary>
        /// Gets a given course for a professor.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Specific course.</returns>
        public Course GetCourseAsProfessor(string courseId)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var projectRepo = new ProjectRepository(context);
                List<bool> hasproject = new List<bool>();
                var course = courseRepo.GetCoursebyId(new Guid(courseId));
                foreach(Student student in course.Students) {
                    if(projectRepo.GetProjectByStudentAndCourseId(student.UserId, new Guid(courseId)) == null) {
                        hasproject.Add(false);
                    } else {
                        hasproject.Add(true);
                    }
                }
                var simplestudents = mapper.StudentInCourseMap(course.Students);
                for(int i = 0; i < simplestudents.Count;i++) {
                    simplestudents[i].ProposedProject = (hasproject[i] == true) ?1:0;
                }
                Course retCourse = null;
                if (course != null) {
                    retCourse = new Course {
                        CourseId = course.CourseId.ToString(),
                        CourseName = course.Name,
                        UniversityId = course.UniversityId.ToString(),
                        Group = course.Group,
                        CourseDescription = course.Description,
                        MinGrade = course.MinScore,
                        Students = simplestudents
                    };
                }
                projectRepo.Dispose();
                courseRepo.Dispose();
                return retCourse;
            }
        }
        /// <summary>
        /// Obtains specific course according to the credentials given.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>Specific course.</returns>
        public SpecificCourse GetSpecificCourse(SharedAreaCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                var projectRepository = new ProjectRepository(context);
                var courseRepo = new CourseRepository(context);
                
                SpecificCourse specificCourse = null;
                var project = projectRepository.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId),
                    new Guid(credentials.CourseId));
                if (project != null)
                {
                    var course = courseRepo.GetCoursebyId(project.CourseId);
                    var achievements = course.Achievements;
                    var listOfBadges = project.Badges;

                    List<Badge> resultList = new List<Badge>();
                    foreach (var newBadge in achievements)
                    {
                        var badge = new Badge
                        {
                            BadgeId = newBadge.AchievementId.ToString(),
                            BadgeDescription = newBadge.Description,
                            Alardeado = (listOfBadges.Find(b => b.AchievementId.Equals(newBadge.AchievementId)) != null)?listOfBadges.Find(b => b.AchievementId.Equals(newBadge.AchievementId)).Bragged:0,
                            Awarded = (listOfBadges.Count > 0 && listOfBadges.Find(b => b.AchievementId.Equals(newBadge.AchievementId)) != null) ? 1 : 0,
                            Value = newBadge.Score
                        };
                        resultList.Add(badge);
                    }
                    specificCourse = new SpecificCourse
                    {
                        NombreContacto = project.Student.Name,
                        ApellidoContacto = project.Student.LastName,
                        Grade = project.Score,
                        Badges = resultList
                    };
                }
                courseRepo.Dispose();
                projectRepository.Dispose();
                return specificCourse;
            }
        }

        /// <summary>
        /// Obtain a course for a given student.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>Course as student.</returns>
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
                    var achievements = course.Achievements;
                    var listOfBadges = (project == null) ? new List<MyLearnDAL.Models.Badge>() : project.Badges;
                    List<Badge> resultList = new List<Badge>();
                    foreach (var newBadge in achievements)
                    {
                        var badge = new Badge
                        {
                            BadgeId = newBadge.AchievementId.ToString(),
                            BadgeDescription = newBadge.Description,
                            Alardeado = (listOfBadges.Find(b => b.AchievementId.Equals(newBadge.AchievementId)) != null) ? listOfBadges.Find(b => b.AchievementId.Equals(newBadge.AchievementId)).Bragged : 0,
                            Awarded = (listOfBadges.Count > 0 && listOfBadges.Find(b => b.AchievementId.Equals(newBadge.AchievementId)) != null) ? 1 : 0,
                            Value = newBadge.Score
                        };
                        resultList.Add(badge);
                    }
                    courseAsStudent = new CourseAsStudent
                    {
                        CourseName = course.Name,
                        StudentUserId = credentials.StudentUserId,
                        ProfUserId = course.ProfessorId.ToString(),
                        ProfessorName = course.Professor.Name + " " + course.Professor.Lastname,
                        UniversityId = course.UniversityId.ToString(),
                        Grade = (project == null)?0:project.Score,
                        Badges = resultList,
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

        /// <summary>
        /// Obtains all courses for a given professor.
        /// </summary>
        /// <param name="profUserId"></param>
        /// <returns>All professor's courses.</returns>
        public AllProfessorsCourses GetAllByProfessor(string profUserId)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var projecRepo = new ProjectRepository(context);
                var allProjects = projecRepo.GetAll();
                AllProfessorsCourses allCourses = null;
                var activeCourses = courseRepo.GetProfessorActiveCourses(new Guid(profUserId));
                var inactiveCourses = courseRepo.GetProfessorInctiveCourses(new Guid(profUserId));
                if (activeCourses != null && inactiveCourses != null)
                {
                    var activeCoursesList = mapper.ActiveCourseListMap(activeCourses);
                    for(int i = 0;i<activeCoursesList.Count;i++)
                    {
                        activeCoursesList[i].Accepted = (allProjects.Find(p => p.CourseId.ToString().Equals(activeCoursesList[i].CourseId)) != null) ? 1 : 0;
                    }

                    var finishedCoursesList = mapper.FinishedCourseListMap(inactiveCourses);
                    for (int i = 0; i < finishedCoursesList.Count; i++)
                    {
                        finishedCoursesList[i].Accepted = (allProjects.Find(p => p.CourseId.ToString().Equals(finishedCoursesList[i].CourseId)) != null) ? 1 : 0;
                    }
                    allCourses = new AllProfessorsCourses
                    {
                        ActiveCourses = activeCoursesList,
                        FinishedCourses = finishedCoursesList
                    };
                }
                projecRepo.Dispose();
                courseRepo.Dispose();
                return allCourses;
            }
            
        }
        /// <summary>
        /// Obtains all courses for a certain student.
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns>All student's courses.</returns>
        public StudentCourses GetAllByStudent(string studentUserId)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var projRepo = new ProjectRepository(context);
                StudentCourses allCourses = null;
                var activeCourses = courseRepo.GetActiveStudentCourses(new Guid(studentUserId));
                var inactiveCourses = courseRepo.GetInactiveStudentCourses(new Guid(studentUserId));
                if(activeCourses != null && inactiveCourses != null)
                {
                    var theCoursesThatAreActive = mapper.ActiveCourseListMap(activeCourses);
                    foreach(ActiveCourse course in theCoursesThatAreActive)
                    {
                        course.Accepted = (projRepo.GetProjectByStudentAndCourseId(new Guid(studentUserId), new Guid(course.CourseId)) == null)?0:1;
                    }
                    var theCoursesThatAreInactive = mapper.FinishedCourseListMap(inactiveCourses);
                    foreach (FinishedCourse course in theCoursesThatAreInactive)
                    {
                        course.Accepted = (projRepo.GetProjectByStudentAndCourseId(new Guid(studentUserId), new Guid(course.CourseId)) == null)?0:1;
                    }
                    allCourses = new StudentCourses
                    {
                        ActiveCourses = theCoursesThatAreActive,
                        FinishedCourses = theCoursesThatAreInactive
                    };
                }
                projRepo.Dispose();
                courseRepo.Dispose();
                return allCourses;
            }
        }
        /// <summary>
        /// Gets all courses offered by a University.
        /// </summary>
        /// <param name="universityId"></param>
        /// <returns>All courses offered by given university.</returns>
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

        /// <summary>
        /// Method in charge of joining a student into a specific course.
        /// </summary>
        /// <param name="joiningStudent"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
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
                    if (student.Courses.Contains(course))
                    {
                        //The user is already taking this course
                        retVal.ReturnStatus = -1;
                    }
                    else
                    {
                        course.Students.Add(student);
                        courseRepo.SaveChanges();
                        retVal.ReturnStatus = 1;
                    }
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

        /// <summary>
        /// Method in charge of proposing a new academic project for a specific course.
        /// </summary>
        /// <param name="proposal"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
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
                        Name = proposal.ProjectName,
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
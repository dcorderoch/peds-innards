(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies', 'naif.base64'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider

            .when('/studentprofile', {
            controller: 'StudentProfileController',
            templateUrl: 'studentProfile/studentProfile.view.html',
            controllerAs: 'vm'
        })
            .when('/login', {
            controller: 'LoginController',
            templateUrl: 'login/login.view.html',
            controllerAs: 'vm'
        })
            .when('/register', {
            controller: 'RegisterController',
            templateUrl: 'register/register.view.html',
            controllerAs: 'vm'
        })
            .when('/register2', {
            controller: 'Register2Controller',
            templateUrl: 'register2/register2.view.html',
            controllerAs: 'vm'
        })
            .when('/register3', {
            controller: 'Register3Controller',
            templateUrl: 'register3/register3.view.html',
            controllerAs: 'vm'
        })
            .when('/notifications', {
            controller: 'NotificationsController',
            templateUrl: 'notifications/notifications.view.html',
            controllerAs: 'vm'
        })
            .when('/coursearea', {
            controller: 'CourseAreaController',
            templateUrl: 'courseArea/courseArea.view.html',
            controllerAs: 'vm'
        })
            .when('/sharedarea', {
            controller: 'SharedAreaController',
            templateUrl: 'sharedArea/sharedArea.view.html',
            controllerAs: 'vm'
        }) 
            .when('/sharedareaprofessor', {
            controller: 'SharedAreaProfessorController',
            templateUrl: 'sharedAreaProfessor/sharedAreaProfessor.view.html',
            controllerAs: 'vm'
        }) 
            .when('/unicourses', {
            controller: 'UniCoursesController',
            templateUrl: 'uniCourses/uniCourses.view.html',
            controllerAs: 'vm'
        }) 
            .when('/searcher', {
            controller: 'SearcherController',
            templateUrl: 'searcher/searcher.view.html',
            controllerAs: 'vm'
        })            
            .when('/offering', {
            controller: 'OfferingController',
            templateUrl: 'offering/offering.view.html',
            controllerAs: 'vm'
        }) 
            .when('/workprofile',{
            controller:'WorkProfileController',
            templateUrl: 'workProfile/workProfile.view.html',
            controllerAs: 'vm'
        })
            .when('/admin',{
            controller:'AdminController',
            templateUrl: 'admin/admin.view.html',
            controllerAs: 'vm'
        }) 
            .when('/professorprofile',{
            controller:'ProfessorProfileController',
            templateUrl: 'professorProfile/professorProfile.view.html',
            controllerAs: 'vm'
        })  
            .when('/employerprofile',{
            controller:'EmployerProfileController',
            templateUrl: 'employerProfile/employerProfile.view.html',
            controllerAs: 'vm'
        }) 
            .when('/newcourse',{
            controller:'NewCourseController',
            templateUrl: 'newCourse/newCourse.view.html',
            controllerAs: 'vm'
        })   
            .when('/newproject',{
            controller:'NewProjectController',
            templateUrl: 'newProject/newProject.view.html',
            controllerAs: 'vm'
        })  
            .when('/auction',{
            controller:'AuctionController',
            templateUrl: 'auction/auction.view.html',
            controllerAs: 'vm'
        })  
            .when('/courseoverview',{
            controller:'CourseOverviewController',
            templateUrl: 'courseOverview/courseOverview.view.html',
            controllerAs: 'vm'
        })  
            .when('/sharedareaemployer',{
            controller:'SharedAreaEmployerController',
            templateUrl: 'sharedAreaEmployer/sharedAreaEmployer.view.html',
            controllerAs: 'vm'
        }) 
            .when('/sharedstudentemployer',{
            controller:'SharedStudentEmployerController',
            templateUrl: 'sharedStudentEmployer/sharedStudentEmployer.view.html',
            controllerAs: 'vm'
        }) 
            .otherwise({ redirectTo: '/login' });
    }


    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];
    function run($rootScope, $location, $cookieStore, $http) {
        // keep user logged in after page refresh
        $rootScope.globals = $cookieStore.get('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {

            $rootScope.SERVER = "url";
            // redirect to login page if not logged in and trying to access a restricted page
            //    var restrictedPage = $.inArray($location.path(), ['/login', '/register','/']) === -1;
            //   var loggedIn = $rootScope.globals.currentUser;
            //        if (restrictedPage && !loggedIn) {
            //              $location.path('/login');
            //            }
        });
    }

})();
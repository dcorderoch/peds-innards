(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
        
          .when('/',{
            controller: 'homeController',
            templateUrl: 'home/homeController.view.html',
            controllerAs: 'vm'
        })
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
            .when('/unicourses', {
            controller: 'uniCoursesController',
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
        //    var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
         //   var loggedIn = $rootScope.globals.currentUser;
    //        if (restrictedPage && !loggedIn) {
  //              $location.path('/login');
//            }
        });
    }

})();
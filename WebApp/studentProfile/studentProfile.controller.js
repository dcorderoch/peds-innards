(function () {
    'use strict';

    angular
        .module('app')
        .controller('StudentProfileController', StudentProfileController);

    StudentProfileController.$inject = ['$location', 'FlashService',  '$rootScope', 'CourseService', 'UserService', 'ProfileCourseService' ];
    function StudentProfileController($location, FlashService, $rootScope, CourseService, UserService, ProfileCourseService) {
        var vm = this;

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;

        initController();

        function initController(){

            vm.userData = $rootScope.userData;
            console.log(vm.userData);
            
            vm.userData.Foto = "data:image/jpg;base64,"+vm.userData.Foto
            
            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};  

            console.log( vm.courseAverageWidth );
            console.log( vm.projectAverageWidth );
            
            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            var currentCourseData={
                "status":"true",
                "CourseName":"algo",
                "StudentUserId":"algo",
                "ProfUserId":"algo",
                "ProfessorName":"algo",
                "UniversityId":"algo",
                "Grade":13,
                "Badges":[
                    {
                        "BadgeDescription":"algo",
                        "Value":80,
                        "Alardeado":0
                    },
                    {
                        "BadgeDescription":"Aprendió a usar Java y Do",
                        "Value":20,
                        "Alardeado":1
                    }
                ],
                "CourseId":"algo",
                "CourseDescription":"algo",
                "Group":5,
                "CourseState":0
            };

            ProfileCourseService.SetCourseData(currentCourseData);

        }

        function goCourseFinished(id){
            $location.path('/sharedarea');    
            return;
            CourseService.GetCourseAsStudent(id)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status = false;

                ProfileCourseService.SetCourseData(currentCourseData);

                $location.path('/sharedarea');    

            }, function(response){
                console.log("no sirvio")
            });
        }

        function goCourseActive(id, status){
            $location.path('/coursearea');    
            return;
            CourseService.GetCourseAsStudent(id)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status=true;

                ProfileCourseService.SetCourseData(currentCourseData);

                if(status===0){
                    $location.path('/coursearea');  
                }
                else{
                    $location.path('/sharedarea');
                }

            }, function(response){
                console.log("no sirvio")
            });
        }

        function disableAccount(){

            console.log(vm.userData.userId);
            console.log(vm.userData.Active);
            
            UserService.Disable(vm.userData.UserId)
                .then(function(response){

                if (vm.userData.Active == "1"){
                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("La cuenta ha sido deshabilitada");
                        $location.path("/login")

                    }
                    else{
                        FlashService.Error("No se pudo deshabilitar la cuenta");
                    }
                }
                if (vm.userData.Active == "0"){

                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("Cuenta habilitada");
                        vm.toggleEnable =true;
                        vm.userData.Active = "1";
                    }
                    else{
                        FlashService.Error("No se pudo habilitar la cuenta");
                    }
                }

            }, function(response){
                if (vm.userData.Active == "0"){ 
                    FlashService.Error("No se pudo habilitar la cuenta");
                } 
                if (vm.userData.Active == "1"){ 
                    FlashService.Error("No se pudo deshabilitar la cuenta");
                }

                console.log("no funcó");
            })
        }

    }
})();

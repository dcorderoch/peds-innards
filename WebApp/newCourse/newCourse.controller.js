(function () {
    'use strict';

    angular
        .module('app')
        .controller('NewCourseController', NewCourseController);

    NewCourseController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'UserService' ];
    function NewCourseController($location,  FlashService, $rootScope, CourseService, UserService) {
        var vm = this;

        initController();
        vm.newEvaluation = newEvaluation;
        vm.createCourse = createCourse;
        vm.disableAccount =disableAccount;
        vm.removeEval = removeEval;

        function initController(){

     
            vm.userData = $rootScope.userData;
            vm.evaluations=[];

        }

        function newEvaluation(){
            var value = vm.porcentaje.toString();
            vm.evaluations.push({BadgeDescription:vm.nombreEval,Value: value});
            vm.nombreEval="";
            vm.porcentaje="";
        }

        function createCourse(){

            if ( !checkEvaluation(vm.evaluations)){

                FlashService.Error("La evaluación debe sumar un 100%");
                vm.evaluations=[];

                return;
            }
            
            var group = vm.Group.toString();
            var minGrade = vm.MinGrade.toString();
            

            var enviar = {CourseName:vm.CourseName, CourseDescription: vm.CourseDescription,
                          Group: group, MinGrade: minGrade, ProfUserId: "id", 
                          UniversityId: "123", Badges: vm.evaluations}

            console.log(enviar);
            CourseService.CreateCourse(enviar)
                .then( function(response){
                if (reponse.sucess){
                    FlashService.Success("Curso creado");
                }
            }, function(response){
                FlashService.Error("No se pudo crear el curso");
                console.log(enviar);
            })

            vm.nombreEval="";
            vm.porcentaje ="";
            vm.CourseName="";
            vm.CourseDescription="";
            vm.Group="";
            vm.MinGrade="";
            vm.evaluations=[];
        }

        function checkEvaluation(evaluations){

            var suma=0;
            var i;
            for (i=0; i<evaluations.length; i++){

                suma+= parseInt(evaluations[i].Value);
                if (suma>100){ 
                    console.log(suma);
                    return false;
                }
            }
            if (suma==100){ 
                console.log(suma);
                return true;
            }
            else{
                console.log(suma);
                return false;
            }
        }

        function disableAccount(){

            UserService.Disable(vm.UserId)
                .then(function(response){

                FlashService.Success("Cuenta deshabilitada");
                $location.path("/login")

            }, function(response){
                console.log("no funcó");
            })
        }   

        function removeEval(evaluation){

            var index = vm.evaluations.indexOf(evaluation);
            if (index > -1) {
                vm.evaluations.splice(index, 1);
            }
        }


    }
})();

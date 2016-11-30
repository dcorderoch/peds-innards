(function () {
    'use strict';

    angular
        .module('app')
        .controller('NewCourseController', NewCourseController);

    NewCourseController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'UserService', '$localStorage' ];
    function NewCourseController($location,  FlashService, $rootScope, CourseService, UserService, $localStorage) {
        var vm = this;

        initController();
        vm.newEvaluation = newEvaluation;
        vm.createCourse = createCourse;
        vm.disableAccount =disableAccount;
        vm.removeEval = removeEval;

        function initController(){

            vm.userData = $rootScope.userData;
            vm.evaluations=[];


            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.photo = "data:image/jpg;base64," + $localStorage.Foto
            console.log($localStorage);
            console.log(vm.userData);


            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }
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
                //vm.evaluations=[];

                return;
            }

            var group = vm.Group.toString();
            var minGrade = vm.MinGrade.toString();


            var enviar = {CourseName:vm.CourseName, CourseDescription: vm.CourseDescription,
                          Group: group, MinGrade: minGrade, ProfUserId: vm.userData.UserId, 
                          UniversityId: vm.userData.UniversityId , Badges: vm.evaluations}

            console.log(enviar);
            CourseService.CreateCourse(enviar)
                .then( function(response){

                if (response.data.ReturnStatus == "1"){
                    FlashService.Success("Curso creado");
                }
                else{
                    FlashService.Error("No se pudo crear el curso");
                }
            }, function(response){
                FlashService.Error("No se pudo crear el curso");
                vm.nombreEval="";
                vm.porcentaje ="";
                vm.CourseName="";
                vm.CourseDescription="";
                vm.Group="";
                vm.MinGrade="";
                vm.evaluations=[];
            })


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


        function removeEval(evaluation){

            var index = vm.evaluations.indexOf(evaluation);
            if (index > -1) {
                vm.evaluations.splice(index, 1);
            }
        }


    }
})();

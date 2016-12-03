(function () {
    'use strict';

    angular
        .module('app')
        .controller('WorkProfileController', WorkProfileController);

    WorkProfileController.$inject = ['$location',  'FlashService', 'JobService', 'UserService', 'ProfileCourseService', '$localStorage'];
    function WorkProfileController($location,  FlashService, JobService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;

        vm.goWorkActive = goWorkActive;
        vm.goWorkFinished = goWorkFinished;
        vm.disableAccount =disableAccount;

        initController();

        function initController(){

            vm.userData = ProfileCourseService.GetProfileData();

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;
            console.log(vm.userData);

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            jobOfferGetByStudent();
        }

        function jobOfferGetByStudent(){

            var send = {StudentUserId: vm.userData.UserId}
            console.log(send)
            JobService.JobOfferGetByStudent(send)
                .then(function(response){

                vm.userData.FinishedJobOffersList = response.data.FinishedJobOffers;
                vm.userData.ActiveJobOffersList = response.data.ActiveJobOffers;
                console.log(response)
            }, function(response){

                FlashService.Error("Error al traer los trabajos del estudiante")

            });

        }


        function goWorkFinished(id){

            console.log("entro")
            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.status = false;
                currentworkData.JobOfferId =id;

                ProfileCourseService.SetWorkData(currentworkData);

                $location.path('/sharedstudentemployer');    

            }, function(response){
                console.log("no sirvio")
            });
        }
        function goWorkActive(id){

            JobService.GetById(id)
                .then(function(response){
                console.log(response)
                var currentworkData = response.data;
                currentworkData.status = true;
                ProfileCourseService.SetWorkData(currentworkData);
                $location.path('/sharedstudentemployer');    

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

                        FlashService.Success("La cuenta ha sido deshabilitada",true);
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
                        ProfileCourseService.SetProfileData(vm.userData);

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
(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployerProfileController', EmployerProfileController);

    EmployerProfileController.$inject = ['$location',  'FlashService', 'JobService', 'UserService', 'ProfileCourseService', '$localStorage'];
    function EmployerProfileController($location,  FlashService, JobService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;

        initController();

        vm.goWorkActive = goWorkActive;
        vm.goWorkFinished = goWorkFinished;
        vm.disableAccount =disableAccount;


        // Starts the controller, called at start and refresh
        // Gets profile data from cookies
        // toggle enables is for enabling/disabling an account
        function initController(){

            vm.userData = ProfileCourseService.GetProfileData();

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;
            var userId = vm.userData.UserId;

            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }
            loadWork(userId);
        }

        //Loads all the  user's active and finished work projects.
        function loadWork(userId){

            JobService.GetByEmployer(userId)
                .then(function(response){

                vm.userData.FinishedJobOffersList = response.data.FinishedJobOffers;
                vm.userData.ActiveJobOffersList = response.data.ActiveJobOffers;

            }, function(response){

                FlashService.Error("Error al traer los trabajos del empleador")
            });
        }

        // Go to a finished shared area (if it has a shared area), otherwise 
        // will stay in the same place
        function goWorkFinished(id){

            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.JobOfferId = id;
                currentworkData.status = false;
                currentworkData.JobOfferId =id;

                ProfileCourseService.SetWorkData(currentworkData);

                $location.path('/sharedareaemployer');    

            }, function(response){
            });
        }

        // Go to an active courses, if there is a proposed project, the user
        //  will go to the shared area, otherwise to the course area. 
        function goWorkActive(id){

            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.JobOfferId = id;
                currentworkData.status = true;
                ProfileCourseService.SetWorkData(currentworkData);

                if(currentworkData.State =="0") 
                    $location.path('/auction'); 
                else
                    $location.path('/sharedareaemployer'); 

            }, function(response){
            });
        }

        //  Disables or enables an account, depending on the user state
        //  stores the state on cookies;
        function disableAccount(){

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
                        ProfileCourseService.SetProfileData(vm.userData)

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

            })
        }

    }
})();
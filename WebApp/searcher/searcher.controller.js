(function () {
    'use strict';

    angular
        .module('app')
        .controller('SearcherController', SearcherController);

    SearcherController.$inject = ['$location', 'FlashService', '$scope', 'JobService', 'SearchOfferingService', 'UserService', '$localStorage', 'ProfileCourseService'];
    function SearcherController($location, FlashService, $scope, JobService, SearchOfferingService, UserService, $localStorage, ProfileCourseService) {
        var vm = this;

        vm.goOffering = goOffering;
        vm.search = search;
        vm.results=[];

        vm.disableAccount =disableAccount;
        vm.checkState= checkState;

        initController();

        // starts the controller, gets data from cookies and server
        // allows or blocks interaction based on userData.Active
        function initController(){

            vm.userData = ProfileCourseService.GetProfileData();

            console.log(vm.userData);


            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   

            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;

            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }
        }


        //performs a search by name XOR technology
        function search(query,parameter){

            if (parameter === "0" ) {

                JobService.GetByName(query)
                    .then(function(response){

                    vm.results = response.data;
                    console.log(response.data)

                },function(response){

                    FlashService.Error("Fallo en traer resultados para búsqueda por nombre");
                });                
            }

            if (parameter === "1"){ 
                JobService.GetByTechnology(query)
                    .then(function(response){

                    vm.results = response.data;
                    console.log(response.data)

                },function(response){

                    FlashService.Error("Fallo en traer resultados para búsqueda por tecnología");
                });
            }

        }

        // goes to a job offering to make a bid
        function goOffering( jobData){
            SearchOfferingService.SetSearchData(jobData);
            $location.path("/offering");

        }

        //toggles an account from enabled to diabled or vice versa
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

            })
        }

        //checks if work is over
        function checkState(number){
            console.log(number)
            if (number ===2 || number === 1){
                return true;
            }
            console.log(number)
            return false;
        }

    }
})();


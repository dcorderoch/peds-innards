(function () {
    'use strict';

    angular
        .module('app')
        .controller('AuctionController', AuctionController);

    AuctionController.$inject = ['$location',  'FlashService', 'JobService', 'UserService', 'ProfileCourseService', '$localStorage'];
    function AuctionController($location,  FlashService, JobService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;

        initController();
        vm.goProfile = goProfile;
        vm.approve = approve;


        // Inicializa el controlador, llamado al comienzo y en refresh
        // Obtiene datos del perfil y del trabajo
        function initController(){

            vm.listOfBids=[];
            vm.userData = ProfileCourseService.GetProfileData();
            vm.workData= ProfileCourseService.GetWorkData();
            getBids();

        }

        // Gets all the bids from students
        // Saves them in vm.listOfBids
        function getBids(){

            JobService.GetBidsById(vm.workData.JobOfferId)
                .then( function(response){

                vm.listOfBids = response.data;

            }, function(response){

                FlashService.Error("No se pudieron cargar los ofertantes");
            });
        }

        // Visits a student profile to know about his projects and work.
        // Gets his personal data, photo and list of active && inactiva work && projects
        // That info is stored in differnt cookies and localstorage
        function goProfile(id){

            var send= { StudentUserId: id};
            JobService.GetProfile(send)
                .then(function(response){

                console.log(response);

                $localStorage.Foto2 = response.data.Foto

                var data = response.data;
                delete data.Foto;

                ProfileCourseService.SetProfileData2(data);
                $location.path('/viewprofile'); 

            }, function(response){

                FlashService.Error("Error en la carga del perfil de estudiante" )
            });
        }

        // Approves a bid of a particular student.
        // Assigns the work to that student and redirects to their shared area
        function approve(bid){

            var send = {JobOfferId:vm.workData.JobOfferId, StudentUserId:bid.StudentUserId}
            console.log(send)
            JobService.Assign(send)
                .then(function(response){

                if (response.data.ReturnStatus == "1"){ 

                    vm.workData.studentInfo = bid;
                    ProfileCourseService.SetWorkData(vm.workData);
                    FlashService.Success("Subasta ganada por "+ bid.StudentName, true);
                    $location.path('/sharedareaemployer');
                }
                else{
                    FlashService.Error("Error en la subasta");
                }

            }, function(response){

                FlashService.Error("Error en la subasta")
            });
        }



    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .controller('AuctionController', AuctionController);

    AuctionController.$inject = ['$location',  'FlashService', '$rootScope', 'JobService', 'UserService', 'ProfileCourseService'];
    function AuctionController($location,  FlashService, $rootScope, JobService, UserService, ProfileCourseService) {
        var vm = this;

        initController();
        vm.goArea = goArea;
        vm.goProfile = goProfile;

        function initController(){

            vm.listOfBids=[];
            vm.userData = $rootScope.userData;
            vm.workData= ProfileCourseService.GetWorkData();
            vm.approve = approve;
            console.log(vm.workData);
            getBids();

        }

        function getBids(){

            JobService.GetBidsById(vm.workData.JobOfferId)
                .then( function(response){

                vm.listOfBids = response.data;

            }, function(response){

                FlashService.Error("No se pudieron cargar los ofertantes");
            });
        }

        function goArea(bid){
            console.log(bid)

        }


        function goProfile(bid){

            var send;
            JobService.GetProfile(send)
                .then(function(response){

                console.log(response);

                var userFoto = $localStorage.$default({
                    Foto : response.data.Foto
                });

                var data = response.data;
                delete data.Foto;

                $rootScope.userData= data;
                $location.path('/viewprofile'); 

            }, function(response){

                FlashService.Error("Error en la carga del perfil de estudiante" )
            });
        }

        function approve(bid){

            var send = {JobOfferId:vm.workData.JobOfferId, StudentUserId:bid.StudentUserId}
            console.log(send)
            JobService.Assign(send)
                .then(function(response){

                if (response.data.ReturnStatus == "1"){ 
                    
                    vm.workData.studentInfo = bid;
                    ProfileCourseService.SetWorkData(vm.workData);
                    FlashService.Success("Subasta ganada por "+ bid.StudentName, true);
                    $location.path("sharedareaemployer");
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
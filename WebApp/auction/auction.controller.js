(function () {
    'use strict';

    angular
        .module('app')
        .controller('AuctionController', AuctionController);

    AuctionController.$inject = ['$location',  'FlashService', 'JobService', 'UserService', 'ProfileCourseService', '$localStorage'];
    function AuctionController($location,  FlashService, JobService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;

        initController();
        vm.goArea = goArea;
        vm.goProfile = goProfile;

        function initController(){

            vm.listOfBids=[];
            vm.userData = ProfileCourseService.GetProfileData();
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


        function goProfile(id){

            var send= { StudentUserId: id};
            JobService.GetProfile(send)
                .then(function(response){

                console.log(response);

                var userFoto = $localStorage.$default({
                    Foto2 : response.data.Foto
                });

                var data = response.data;
                delete data.Foto;

                ProfileCourseService.SetProfileData2(data);
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
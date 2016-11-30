(function () {
    'use strict';

    angular
        .module('app')
        .controller('SearcherController', SearcherController);

    SearcherController.$inject = ['$location', 'FlashService', '$scope', '$rootScope', 'JobService', 'SearchOfferingService', 'UserService'];
    function SearcherController($location, FlashService, $scope, $rootScope, JobService, SearchOfferingService, UserService) {
        var vm = this;

        initController();

        vm.goOffering = goOffering;
        vm.search = search;
        vm.disableAccount =disableAccount;

        //        vm.results=[];


        function goOffering(){
            $location.path('/offering');
        }

        function initController(){

            vm.results=[];

            vm.userData = $rootScope.userData;
            console.log(vm.userData);

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   

            $rootScope.currentCourseData={};

            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            vm.inferiorLimit = 0;
            vm.superiorLimit = 0;

            $scope.listShow = [];
            vm.currentPage = 1;
            vm.numPerPage = 20;
            vm.maxSize = 5;
        }

        $scope.$watch('currentPage + numPerPage', function() {


            vm.inferiorLimit = vm.currentPage + vm.superiorLimit
            vm.superiorLimit = vm.currentPage + 19;
            setTimeout(function(){ 

                var begin = (( vm.currentPage - 1) * vm.numPerPage)
                , end = begin + vm.numPerPage;
                $scope.listShow = vm.results.slice(begin, end); 
            }           , 
                       10000
                      );

            console.log($scope.listShow);
        });


        function search(query,parameter){

            if (parameter == 0) {

                JobService.GetByName(query)
                    .then(function(response){

                    vm.results = response.data;
                    //                    $scope.$apply(); 

                },function(response){

                    FlashService.Error("Fallo en traer resultados para búsqueda por nombre");
                    console.log( "no sirvió");
                });                
            }

            if (parameter ==1){ 
                JobService.GetByTechnology(query)
                    .then(function(response){

                    vm.results = response.data;
                    //                    $scope.$apply(); 

                },function(response){

                    FlashService.Error("Fallo en traer resultados para búsqueda por tecnología");
                    console.log("no sirvió");
                });
            }
            else{
                console.log("No sirvió");
                FlashService.Error("Escoge un parámetro por el cual buscar")
            }
        }

        function goOffering( jobData){


            SearchOfferingService.SetSearchData(jobData);
            $location.path("/offering");

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


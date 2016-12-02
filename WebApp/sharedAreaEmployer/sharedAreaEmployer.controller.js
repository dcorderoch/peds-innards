(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaEmployerController', SharedAreaEmployerController);

    SharedAreaEmployerController.$inject = ['$location',  'FlashService', '$scope', '$rootScope', 'ProfileCourseService', 'JobService'];
    function SharedAreaEmployerController($location,  FlashService, $scope, $rootScope, ProfileCourseService, JobService) {
        var vm = this;

        initController();

        vm.sendReply = sendReply;
        vm.replyMessage ="";
        vm.comments =[];

        vm.replyaMessage = replyaMessage;
        vm.sendComment = sendComment;
        vm.closeProject = closeProject;

        function initController(){




            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();

            console.log(vm.workData);

            getComments();

            processComments();
        }

        function sendReply( replyMessage, parentId){

            var send={Commenter:"0", ParentId:parentId, JobOfferComment:replyMessage, JobOfferId:vm.workData.JobOfferId}
            console.log(send);
            JobService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();
                processComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.writeReply=false;;
        }

        function processComments(){

            var i;
            if (vm.comments == null){
                return;
            }
            for (i=0; i<vm.comments.length; i++){

                if (vm.comments[i].IsFromStudent==="0"){
                    vm.comments[i].IsFromStudent = true;
                    vm.comments[i].Author = "Estudiante"
                }
                else{
                    vm.comments[i].IsFromStudent = false;
                    vm.comments[i].Author = "Empleador"

                }
                var j;
                for (j=0; j<vm.comments[i].Nested.length; j++){
                    if (vm.comments[i].Nested[j].IsFromStudent==="0"){
                        vm.comments[i].Nested[j].IsFromStudent = true;
                        vm.comments[i].Nested[j].Author = "Estudiante"
                    }
                    else{
                        vm.comments[i].Nested[j].IsFromStudent = false;
                        vm.comments[i].Nested[j].Author = "Empleador"
                    }                 
                }
            }
        }

        function replyaMessage(commentId){
            vm.writeReply=true;
        }  

        function getComments (){
            JobService.GetAllComments( vm.workData.JobOfferId)
                .then( function(response){

                vm.comments = response.data;

            }, function(response){
                console.log("no sirvió")
            })
        }

        function sendComment( comment, dataUpload ){

            var send={Commenter:"0", ParentId:"-1", JobOfferComment:comment, JobOfferId:vm.workData.JobOfferId};
            console.log(send);
            JobService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();
                processComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
        }

        function closeProject(finishProject,stars,status){

            var state = (status=="Exitoso") ? "2" : "3"; 

            var send = {JobOfferId:vm.workData.JobOfferId, State:state, 
                        StateDescription: finishProject, Stars: stars.toString()};
            console.log(send)
            JobService.CloseJob(send)
                .then(function(response){

                if (response.data.ReturnStatus ===1){ 
                    FlashService.Success("El proyecto se ha cerrado");
                    $location.path("/employerprofile")
                    console.log(response);
                }
                else{
                    FlashService.Error("No se pudo cerrar el proyecto")
                    console.log(response);
                }
            }, function(response){
                FlashService.Error("No se pudo cerrar el proyecto")
                console.log(response);
            })
        }


    }
})();
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
        vm.replyaMessage = replyaMessage;
        vm.sendComment = sendComment;
        vm.closeProject = closeProject;

        function initController(){


            vm.comments =[];

            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();

            console.log(vm.workData);

            getComments();

            vm.comments.push({CommentId:"123",ParentId:"123", Date:"2016-11-24T01:55:01+00:00", IsFromStudent:"0",
                              Comment:"Bla bla", Nested:[{ "CommentId":"algo",
                                                          "ParentId":"algo",
                                                          "Date":"fecha en ISO8601",
                                                          "IsFromStudent":"0",
                                                          "Comment":"algo"}]});

            vm.comments.push({CommentId:"123",ParentId:"123", Date:"1994-11-24T01:55:01+00:00", IsFromStudent:"1",
                              Comment:"na na na", Nested:[{ "CommentId":"algo",
                                                           "ParentId":"algo",
                                                           "Date":"fecha en ISO8601",
                                                           "IsFromStudent":"1",
                                                           "Comment":"algo"},
                                                          { "CommentId":"algo",
                                                           "ParentId":"algo",
                                                           "Date":"fecha en ISO8601",
                                                           "IsFromStudent":"1",
                                                           "Comment":"algo"}
                                                         ]});

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

                comments = response.data;

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

            var send = {JobOffer:vm.workData.JobOfferId, State:state, 
                        StateDescription: finishProject, Stars: stars.toString()};
            console.log(send)
            JobService.CloseJob(send)
                .then(function(response){
                FlashService.Error("El proyecto se ha cerrado");
                $location.path("/employerprofile")
                console.log(response);

            }, function(response){
                FlashService.Error("No se pudo cerrar el proyecto")
                console.log(response);
            })
        }


    }
})();
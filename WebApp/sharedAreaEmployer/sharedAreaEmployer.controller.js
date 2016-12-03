(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaEmployerController', SharedAreaEmployerController);

    SharedAreaEmployerController.$inject = ['$location',  'FlashService', '$scope',  'ProfileCourseService', 'JobService'];
    function SharedAreaEmployerController($location,  FlashService, $scope,  ProfileCourseService, JobService) {
        var vm = this;

        initController();

        vm.sendReply = sendReply;
        vm.replyMessage ="";
        vm.comments =[];

        vm.replyaMessage = replyaMessage;
        vm.sendComment = sendComment;
        vm.closeProject = closeProject;
        vm.checkFile = checkFile;


        function initController(){

            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();
            vm.userData = ProfileCourseService.GetProfileData();

            console.log(vm.workData);

            getComments();
            processComments();
        }

        function sendReply( replyMessage, parentId){

            var send={Commenter:"0", ParentId:parentId, JobOfferComment:replyMessage, JobOfferId:vm.workData.JobOfferId, 
                      StudentUserId: vm.workData.StudentUserId, EmployerUserId: vm.userData.UserId};
            console.log(send);
            JobService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();

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
            console.log(vm.comments);
            for (i=0; i<vm.comments.length; i++){

                if (vm.comments[i].IsFromStudent =="1"){
                    vm.comments[i].IsFromStudent = true;
                    vm.comments[i].Author = "Estudiante"
                }
                else{
                    vm.comments[i].IsFromStudent = false;
                    vm.comments[i].Author = "Empleador"

                }
                var j;
                for (j=0; j<vm.comments[i].NestedComments.length; j++){
                    if (vm.comments[i].NestedComments[j].IsFromStudent=="1"){
                        vm.comments[i].NestedComments[j].IsFromStudent = true;
                        vm.comments[i].NestedComments[j].Author = "Estudiante"
                    }
                    else{
                        vm.comments[i].NestedComments[j].IsFromStudent = false;
                        vm.comments[i].NestedComments[j].Author = "Empleador"
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
                console.log(vm.comments)
                processComments();

            }, function(response){
                console.log("no sirvió")
            })
        }

        function sendComment( dataUpload ){

            if ( typeof dataUpload === "undefined" ){

                var send={Commenter:"0", ParentId:"-1", JobOfferComment:vm.comment, JobOfferId:vm.workData.JobOfferId,
                          StudentUserId: vm.workData.StudentUserId, EmployerUserId: vm.userData.UserId};
                console.log(send);
                JobService.CommentCreate(send)
                    .then(function(response){

                    console.log(response);
                    getComments();
                    vm.comment="";

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })
            }
            else{
                var send={Commenter:"0", ParentId:"-1", JobOfferComment:vm.comment, JobOfferId:vm.workData.JobOfferId,
                          StudentUserId: vm.workData.StudentUserId, EmployerUserId: vm.userData.UserId, FileName: dataUpload.filename, File: dataUpload.base64, RefreshToken: vm.userData.RefreshToken};
                console.log(send)
                JobService.CommentCreateWithFile(send)
                    .then(function(response){

                    console.log(response);
                    getComments();
                    vm.comment="";

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })

            }
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
        function checkFile( file){

            if (file == "0"){

                return false;
            }
            return true;
        }

    }
})();
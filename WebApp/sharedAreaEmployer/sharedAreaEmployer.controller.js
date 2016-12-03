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

        //Gets data from cookies
        //Gets all comments and precess them
        function initController(){

            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();
            vm.userData = ProfileCourseService.GetProfileData();

            console.log(vm.workData);

            getComments();
            processComments();

            // initialize with defaults
            $("#input-id").rating();

            // with plugin options
            $("#input-id").rating({min:1, max:10, step:2, size:'lg'});

        }

        //Sends a nested message, specifies a parent, a message and won't have a file
        function sendReply( replyMessage, parentId){

            var send={Commenter:"0", ParentId:parentId, JobOfferComment:replyMessage, JobOfferId:vm.workData.JobOfferId, 
                      StudentUserId: vm.workData.StudentUserId, EmployerUserId: vm.userData.UserId};
            JobService.CommentCreate(send)
                .then(function(response){

                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.writeReply=false;;
        }

        //tags the comments with their authors
        function processComments(){

            var i;
            if (vm.comments == null){
                return;
            }
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

        //Toggles an input box for reply messages (nested)
        function replyaMessage(commentId){
            vm.writeReply=true;
        }  

        //gets all comments from server
        function getComments (){
            JobService.GetAllComments( vm.workData.JobOfferId)
                .then( function(response){

                vm.comments = response.data;
                processComments();

            }, function(response){
                console.log("no sirvió")
            })
        }

        // sends a comment (not nested) with a file or without a file.
        // Set the comment input empty if works
        function sendComment( dataUpload ){

            if ( typeof dataUpload === "undefined" ){

                var send={Commenter:"0", ParentId:"-1", JobOfferComment:vm.comment, JobOfferId:vm.workData.JobOfferId,
                          StudentUserId: vm.workData.StudentUserId, EmployerUserId: vm.userData.UserId};
                JobService.CommentCreate(send)
                    .then(function(response){

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
                JobService.CommentCreateWithFile(send)
                    .then(function(response){

                    getComments();
                    vm.comment="";

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })

            }
        }


        //closes a project with a reason and qualification
        function closeProject(finishProject,stars,status){

            var state = (status=="Exitoso") ? "2" : "3"; 

            var send = {JobOfferId:vm.workData.JobOfferId, State:state, 
                        StateDescription: finishProject, Stars: stars.toString()};
            JobService.CloseJob(send)
                .then(function(response){

                if (response.data.ReturnStatus ===1){ 
                    FlashService.Success("El proyecto se ha cerrado");
                    $location.path("/employerprofile")
                }
                else{
                    FlashService.Error("No se pudo cerrar el proyecto")
                    console.log(response);
                }
            }, function(response){
                FlashService.Error("No se pudo cerrar el proyecto")
            })
        }

        //Checks if file is empty
        function checkFile( file){

            if (file == ""){

                return false;
            }
            return true;
        }

    }
})();
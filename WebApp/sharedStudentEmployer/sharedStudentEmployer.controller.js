(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedStudentEmployerController', SharedStudentEmployerController);

    SharedStudentEmployerController.$inject = ['$location','FlashService',   'JobService', 'ProfileCourseService'];
    function SharedStudentEmployerController($location,  FlashService,  JobService, ProfileCourseService) {
        var vm = this;

        initController();

        vm.sendReply = sendReply;
        vm.replyMessage ="";
        vm.comments =[];

        vm.replyaMessage = replyaMessage;
        vm.sendComment = sendComment;
        vm.checkFile = checkFile;

        initController();
        //Gets data from cookies
        //Gets all comments and precess them
        function initController(){

            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();
            vm.userData = ProfileCourseService.GetProfileData();
            vm.workData.Carnet = ProfileCourseService.GetProfileData().Carnet;      


            getComments();
            processComments();

        }

        //Sends a nested message, specifies a parent, a message and won't have a file
        function sendReply( replyMessage, parentId){

            var send={Commenter:"1", ParentId:parentId, JobOfferComment:replyMessage, JobOfferId:vm.workData.JobOfferId, 
                      StudentUserId: vm.userData.UserId, EmployerUserId: vm.workData.EmployerUserId};
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

        //loads all the shared area comments from server (nested and not)
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
        function sendComment(  dataUpload ){

            if ( typeof dataUpload === "undefined" ){

                var send={Commenter:"1", ParentId:"-1", JobOfferComment:vm.comment, JobOfferId:vm.workData.JobOfferId, 
                          StudentUserId: vm.userData.UserId, EmployerUserId: vm.workData.EmployerUserId};
                JobService.CommentCreate(send)
                    .then(function(response){

                    getComments();
                    vm.comment="";

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })
                vm.comment="";
            }
            else{
                var send={Commenter:"1", ParentId:"-1", JobOfferComment:vm.comment, JobOfferId:vm.workData.JobOfferId, 
                          StudentUserId: vm.userData.UserId, EmployerUserId: vm.workData.EmployerUserId, FileName: dataUpload.filename, File: dataUpload.base64, RefreshToken: vm.userData.RefreshToken};
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

        //Checks if file is empty
        function checkFile( file){
            console.log(file)
            if (file == ""){

                return false;
            }
            return true;
        }


    }
})();
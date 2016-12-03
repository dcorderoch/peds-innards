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
        function initController(){

            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();
            vm.userData = ProfileCourseService.GetProfileData();
            vm.workData.Carnet = ProfileCourseService.GetProfileData().Carnet;      


            getComments();
            processComments();

        }

        function sendReply( replyMessage, parentId){

            var send={Commenter:"1", ParentId:parentId, JobOfferComment:replyMessage, JobOfferId:vm.workData.JobOfferId, 
                      StudentUserId: vm.userData.UserId, EmployerUserId: vm.workData.EmployerUserId};
            console.log(send);
            JobService.CommentCreate(send)
                .then(function(response){

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
                processComments();

            }, function(response){
                console.log("no sirvió")
            })
        }

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


        function checkFile( file){

            if (file == "0"){

                return false;
            }
            return true;
        }


    }
})();
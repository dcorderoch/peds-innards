(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedStudentEmployerController', SharedStudentEmployerController);

    SharedStudentEmployerController.$inject = ['$location','FlashService', '$rootScope',  'JobService', 'ProfileCourseService'];
    function SharedStudentEmployerController($location,  FlashService, $rootScope, JobService, ProfileCourseService) {
        var vm = this;

        initController();

        vm.sendReply = sendReply;
        vm.replyMessage ="";
        vm.comments =[];

        vm.replyaMessage = replyaMessage;
        vm.sendComment = sendComment;

        initController();
        function initController(){

            vm.writeReply= false;

            vm.workData ={};
            vm.workData = ProfileCourseService.GetWorkData();
            vm.userData = $rootScope.userData;
            vm.workData.Carnet = $rootScope.userData.Carnet;            

            console.log(vm.workData);

            getComments();
            processComments();

        }

        function sendReply( replyMessage, parentId){

            var send={Commenter:"1", ParentId:parentId, JobOfferComment:replyMessage, JobOfferId:vm.workData.JobOfferId, 
                      StudentUserId: vm.userData.UserId, EmployerUserId: vm.workData.EmployerUserId};
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

        function sendComment(  dataUpload ){

            var send={Commenter:"1", ParentId:"-1", JobOfferComment:vm.comment, JobOfferId:vm.workData.JobOfferId, 
                      StudentUserId: vm.userData.UserId, EmployerUserId: vm.workData.EmployerUserId};
            console.log(send);
            JobService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.comment="";
        }




    }
})();
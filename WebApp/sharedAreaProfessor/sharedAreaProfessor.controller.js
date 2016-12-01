(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaProfessorController', SharedAreaProfessorController);

    SharedAreaProfessorController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'ProfileCourseService'];
    function SharedAreaProfessorController( $location,  FlashService, $rootScope, CourseService, ProfileCourseService) {
        var vm = this;

        initController();

        vm.sendReply = sendReply; 
        vm.replyMessage = replyaMessage;
        vm.sendComment = sendComment;
        vm.assignBadge = assignBadge;

        function initController(){

            vm.comments = [];
            vm.writeReply=false;
            vm.courseData = {};

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = $rootScope.userData.Carnet;
            vm.userData = $rootScope.userData;
            //            vm.courseData.status=true;
            console.log(vm.courseData);
            console.log(vm.userData);

            getComments();
            processComments();
        }

        function sendReply( replyMessage,  commentId){

            var send={Commenter:"0", ParentId:commentId, Comment:replyMessage, StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.ProfUserId, CourseId: vm.courseData.CourseId};

            CourseService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.writeReply=false;;
        }

        function sendComment(  dataUpload ){

            var send={Commenter:"0", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.ProfUserId, CourseId: vm.courseData.CourseId};
            vm.comment =""

            CourseService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
        }

        function processComments(){

            var i;
            console.log(vm.comments);
            for (i=0; i<vm.comments.length; i++){

                if (vm.comments[i].IsFromStudent =="1"){
                    vm.comments[i].IsFromStudent = true;
                    vm.comments[i].Author = "Estudiante"
                }
                else{
                    vm.comments[i].IsFromStudent = false;
                    vm.comments[i].Author = "Profesor"

                }
                var j;
                for (j=0; j<vm.comments[i].NestedComments.length; j++){
                    if (vm.comments[i].NestedComments[j].IsFromStudent=="1"){
                        vm.comments[i].NestedComments[j].IsFromStudent = true;
                        vm.comments[i].NestedComments[j].Author = "Estudiante"
                    }
                    else{
                        vm.comments[i].NestedComments[j].IsFromStudent = false;
                        vm.comments[i].NestedComments[j].Author = "Profesor"
                    }                 
                }
            }
        }       

        function replyaMessage(commentId){


            vm.writeReply=true;
        }

        function getComments (){

            var send= {StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, CourseId: vm.courseData.CourseId};
            CourseService.GetComments(send)
                .then( function(response){

                vm.comments = response.data;
                processComments();

            }, function(response){
                console.log("no sirvió")
            })
        }


        function assignBadge(badgeId){

            var send={StudentUserId: vm.courseData.StudentUserId, 
                      ProfUserId: vm.userData.IdProfesor,
                      Group: vm.courseData.Group,
                      CourseId: vm.courseData.CourseId}

            console.log(send);
            CourseService.GiveBadge(send)
                .then(function(response){

                if (response.data.ReturnStatus=="0"){
                    FlashService.Error("No se pudo asignar el badge")
                }
                else{
                    FlashService.Success("Se ha asignado el badge");

                    var dataSend = {StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, UniversityId: vm.courseData.UniversityId, 
                                    Group: vm.courseData.Group, CourseId: vm.courseData.CourseId}

                    CourseService.GetSpecificCourse(dataSend)
                        .then(function(response){

                        vm.courseData.NombreContacto = response.data.NombreContacto;
                        vm.courseData.ApellidoContacto = response.data.ApellidoContacto;
                        vm.courseData.Grade = response.data.Grade;
                        vm.courseData.Badges = response.data.Badges;
                        vm.courseData.StudentUserId = vm.courseData.StudentUserId;
                        ProfileCourseService.SetCourseData(vm.courseData);
                        initController();

                    }, function(response){
                        console.log("no sirvió")
                    })

                }

            }, function(response){

                FlashService.Error("No se pudo asignar el badge");
            });
        }

    }
})();
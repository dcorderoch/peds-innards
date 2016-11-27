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
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};  
            console.log(vm.courseData);

            getComments();

            vm.comments.push({CommentId:"123",ParentId:"123", Date:"2016-11-24T01:55:01+00:00", IsFromStudent:"0",
                              Comment:"Bla bla", Nested:[{ "CommentId":"algo",
                                                          "ParentId":"algo",
                                                          "Date":"fecha en ISO8601",
                                                          "IsFromStudent":"0",
                                                          "Comment":"algo"}]});
            vm.comments.push({CommentId:"123",ParentId:"125", Date:"1994-11-24T01:55:01+00:00", IsFromStudent:"1",
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

            var send={Commenter:"1", ParentId:parentId, Comment:replyMessage, StudentUserId: vm.userData.StudentUserId, ProfUserId: vm.userData.ProfUserId, CourseId: vm.courseData.CourseId};
            console.log(send);
            CourseService.CommentCreate(send)
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
                    vm.comments[i].Author = "Profesor"

                }
                var j;
                for (j=0; j<vm.comments[i].Nested.length; j++){
                    if (vm.comments[i].Nested[j].IsFromStudent==="0"){
                        vm.comments[i].Nested[j].IsFromStudent = true;
                        vm.comments[i].Nested[j].Author = "Estudiante"
                    }
                    else{
                        vm.comments[i].Nested[j].IsFromStudent = false;
                        vm.comments[i].Nested[j].Author = "Profesor"
                    }                 
                }
            }
        }        

        function replyaMessage(commentId){


            vm.writeReply=true;
        }

        function getComments (){

            var send= {StudentUserId: vm.userData.StudentUserId, ProfUserId: vm.userData.ProfUserId, CourseId: vm.courseData.CourseId};
            CourseService.GetComments(send)
                .then( function(response){

                comments = response.data;

            }, function(response){
                console.log("no sirvió")
            })
        }

        function sendComment(  dataUpload ){

            var send={Commenter:"0", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.userData.StudentUserId, ProfUserId: vm.userData.ProfUserId, CourseId: vm.courseData.CourseId};
            console.log(send)
            vm.comment =""

            CourseService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();
                processComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
        }
        
        function assignBadge(badgeId){
         
            
        }

    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaController', SharedAreaController);

    SharedAreaController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'ProfileCourseService'];
    function SharedAreaController($location,  FlashService, $rootScope, CourseService, ProfileCourseService) {
        var vm = this;

        vm.comments = [];
        vm.sendReply = sendReply; 
        vm.replyMessage ="";
        vm.replyaMessage = replyaMessage;

        initController();
        function initController(){

            vm.writeReply=false;
            vm.courseData = {};

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = $rootScope.userData.Carnet;
            //            vm.courseData.status=true;
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};  
            console.log(vm.gradeWidth);

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

        function sendReply(commentId){

            //Write reply
            vm.replyMessage="";
            setTimeout( function(){ 
                vm.writeReply=false}, 100);
            //            vm.writeReply=false;
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

            CourseService.GetComments()
                .then( function(response){

                comments = response.data;

            }, function(response){
                console.log("no sirvió")
            })
        }


    }

})();

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
        vm.replyaMessage =replyaMessage;
        vm.sendComment = sendComment;
        vm.brag = brag;

        initController();
        function initController(){

            vm.writeReply=false;
            vm.courseData = {};

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = $rootScope.userData.Carnet;
            vm.userData = $rootScope.userData;
            //            vm.courseData.status=true;
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};  
            console.log(vm.gradeWidth);

            getComments();

            processComments();
        }

        function sendReply( replyMessage, parentId){

            var send={Commenter:"1", ParentId:parentId, Comment:replyMessage, StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, CourseId: vm.courseData.CourseId};
            
            console.log(send);
            CourseService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();

                var dataSend = {StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, UniversityId: vm.courseData.UniversityId, 
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


            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.writeReply=false;;
        }

        function brag(bragId){
            console.log(bragId)
            CourseService.Brag(bragId)
                .then(function(response){

                if (response.ReturnStatus =="0"){
                    FlashService.Error("No se pudo alardear, intentalo más tarde")
                }
                else{
                    FlashService.Success("Se ha alardeado en Twitter exitsamente!");
                    getComments();

                }

            }, function(response){
                FlashService.Error("No se pudo alardear, intentalo más tarde")
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

            var send= {StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, CourseId: vm.courseData.CourseId};
            CourseService.GetComments(send)
                .then( function(response){

                vm.comments = response.data;
                console.log(vm.comments)
                processComments();

            }, function(response){
                console.log("no sirvió")
            })
        }

        function sendComment(  dataUpload ){

            var send={Commenter:"1", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, CourseId: vm.courseData.CourseId};
            console.log(send)
            CourseService.CommentCreate(send)
                .then(function(response){

                console.log(response);
                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.comment="";
        }

        function getCourseData(id){


            CourseService.GetCourseAsStudent(id)
                .then(function(response){

                var currentCourseData = response.data;

                currentCourseData.status = true;

                ProfileCourseService.SetCourseData(currentCourseData);
                initController();


            }, function(response){
                console.log("no sirvio")
            });
        }


    }
})();

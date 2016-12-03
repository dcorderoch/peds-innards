(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaController', SharedAreaController);

    SharedAreaController.$inject = ['$location',  'FlashService',  'CourseService', 'ProfileCourseService'];
    function SharedAreaController($location,  FlashService,  CourseService, ProfileCourseService) {
        var vm = this;

        vm.comments = [];
        vm.sendReply = sendReply; 
        vm.replyaMessage =replyaMessage;
        vm.sendComment = sendComment;
        vm.checkFile = checkFile;
        vm.brag = brag;

        initController();
        //Gets data from cookies
        //Gets all comments and precess them
        function initController(){

            vm.writeReply=false;
            vm.courseData = {};

            vm.courseData =ProfileCourseService.GetCourseData();

            vm.courseData.Carnet = ProfileCourseService.GetProfileData().Carnet

            vm.userData = ProfileCourseService.GetProfileData();
            //            vm.courseData.status=true;
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};

            console.log(vm.userData);

            console.log(vm.courseData);

            getComments();
            processComments();
            getAllBadges();
        }



        //gets all the badges of the shared area 
        function getAllBadges(){

            var send ={ StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, UniversityId:vm.courseData.UniversityId, 
                       Group:vm.courseData.Group, CourseId: vm.courseData.CourseId};
            CourseService.GetAllBadges(send)
                .then(function(response){

                vm.courseData.Badges = response.data;
            }, function(response){

                FlashService.Error("No se pudieron traer los badges");
            });
        }


        function sendReply( replyMessage, parentId){

            var send={Commenter:"1", ParentId:parentId, Comment:replyMessage, StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, CourseId: vm.courseData.CourseId};

            CourseService.CommentCreate(send)
                .then(function(response){

                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.writeReply=false;;
        }

        function brag(bragId){

            var send = {  BadgeId:bragId, StudentName:vm.userData.NombreContacto, StudentLastName:vm.userData.ApellidoContacto };

            CourseService.Brag(send)
                .then(function(response){

                if (response.data.ReturnStatus ===0){
                    FlashService.Error("No se pudo alardear, intentalo más tarde")
                    getAllBadges();
                }
                if (response.data.ReturnStatus ===1){
                    FlashService.Success("Se ha alardeado en Twitter exitosamente!");
                    getAllBadges();
                }
                getAllBadges();

            }, function(response){
                FlashService.Error("No se pudo alardear, intentalo más tarde")
            })
        }



        function processComments(){

            var i;
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
                processComments();
                getAllBadges();

            }, function(response){
            })
        }


        // sends a comment (not nested) with a file or without a file.
        // Set the comment input empty if works
        function sendComment(  dataUpload ){

            if ( typeof dataUpload === "undefined" ){
                var send={Commenter:"1", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, CourseId: vm.courseData.CourseId};
                console.log(send)
                CourseService.CommentCreate(send)
                    .then(function(response){

                    getComments();

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })
                vm.comment="";                
            }
            else{
                var send={Commenter:"1", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.userData.UserId, ProfUserId: vm.courseData.ProfUserId, CourseId: vm.courseData.CourseId, FileName: dataUpload.filename, File: dataUpload.base64, RefreshToken: vm.userData.RefreshToken};

                CourseService.CreateWithFile(send)
                    .then(function(response){

                    getComments();

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })
                vm.comment="";
            }
        }

        //Checks if file is empty
        function checkFile( file){

            if (file == "0"){

                return false;
            }
            return true;
        }

    }
})();

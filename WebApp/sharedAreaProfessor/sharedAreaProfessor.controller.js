(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaProfessorController', SharedAreaProfessorController);

    SharedAreaProfessorController.$inject = ['$location',  'FlashService',  'CourseService', 'ProfileCourseService'];
    function SharedAreaProfessorController( $location,  FlashService,  CourseService, ProfileCourseService) {
        var vm = this;

        initController();

        vm.sendReply = sendReply; 
        vm.replyaMessage = replyaMessage;
        vm.sendComment = sendComment;
        vm.assignBadge = assignBadge;
        vm.checkFile = checkFile;

        //Gets data from cookies
        //Gets all comments and precess them
        function initController(){

            vm.comments = [];
            vm.writeReply=false;
            vm.courseData = {};

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = ProfileCourseService.GetProfileData.Carnet;
            vm.userData = ProfileCourseService.GetProfileData();

            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};

            getComments();
            processComments();
            getAllBadges();
        }

        //gets all the badges of the shared area 
        function getAllBadges(){

            var send ={ StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, UniversityId:vm.courseData.UniversityId, 
                       Group:vm.courseData.Group, CourseId: vm.courseData.CourseId};

            CourseService.GetAllBadges(send)
                .then(function(response){

                vm.courseData.Badges = response.data;
            }, function(response){

                FlashService.Error("No se pudieron traer los badges");
            });
        }


        //Sends a nested message, specifies a parent, a message and won't have a file
        function sendReply( replyMessage,  commentId){

            var send={Commenter:"0", ParentId:commentId, Comment:replyMessage, StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, CourseId: vm.courseData.CourseId};

            CourseService.CommentCreate(send)
                .then(function(response){

                getComments();

            }, function(response){
                //                console.log(response);
                FlashService.Error("No se pudo enviar el comentario"); 
            })
            vm.writeReply=false;;
        }

        // sends a comment (not nested) with a file or without a file.
        // Set the comment input empty if works
        function sendComment(  dataUpload ){

            if ( typeof dataUpload === "undefined" ){
                var send={Commenter:"0", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, CourseId: vm.courseData.CourseId};
                vm.comment =""

                CourseService.CommentCreate(send)
                    .then(function(response){

                    getComments();

                }, function(response){
                    //                console.log(response);
                    FlashService.Error("No se pudo enviar el comentario"); 
                })
            }
            else{

                var send={Commenter:"0", ParentId:"-1", Comment:vm.comment, StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, CourseId: vm.courseData.CourseId, FileName: dataUpload.filename, File: dataUpload.base64, RefreshToken: vm.userData.RefreshToken};
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

        //tags the comments with their authors
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

        //Toggles an input box for reply messages (nested)
        function replyaMessage(commentId){

            vm.writeReply=true;
        }

        //loads all the shared area comments from server (nested and not)
        function getComments (){

            var send= {StudentUserId: vm.courseData.StudentUserId, ProfUserId: vm.userData.UserId, CourseId: vm.courseData.CourseId};
            CourseService.GetComments(send)
                .then( function(response){

                vm.comments = response.data;
                processComments();

            }, function(response){
            })
        }


        //assigns a badge to a student, only can do it once for each
        function assignBadge(badgeId){

            var send={StudentUserId: vm.courseData.StudentUserId, 
                      ProfUserId: vm.userData.UserId,
                      Group: vm.courseData.Group,
                      CourseId: vm.courseData.CourseId,
                      AchievementId: badgeId}

            CourseService.GiveBadge(send)
                .then(function(response){

                if (response.data.ReturnStatus=="0"){
                    FlashService.Error("No se pudo asignar el badge");
                    getAllBadges();
                }
                else{
                    FlashService.Success("Se ha asignado el badge");
                    getAllBadges();

                }

            }, function(response){

                FlashService.Error("No se pudo asignar el badge");
            });
            getAllBadges();

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
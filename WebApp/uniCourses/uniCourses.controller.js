(function () {
    'use strict';

    angular
        .module('app')
        .controller('UniCoursesController', UniCoursesController);

    UniCoursesController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService'];
    function UniCoursesController($location,  FlashService, $rootScope, CourseService) {
        var vm = this;

        initController();

        vm.courses=[];
        
        function initController(){

            vm.NombreContacto = $rootScope.userData.NombreContacto;
            vm.ApellidoContacto = $rootScope.userData.ApellidoContacto;
            vm.Ubicacion = $rootScope.userData.Ubicacion;
            vm.Email = $rootScope.userData.Email;
            vm.Telefono = $rootScope.userData.Telefono;
            vm.Fecha_Registro = $rootScope.userData.Fecha_Registro;
            vm.Password = $rootScope.userData.Password;
            vm.TipoRepositorioArchivos = $rootScope.userData.TipoRepositorioArchivos;
            //  vm.Foto = $rootScope.userData.Foto;
            vm.Foto= "data:image/png;base64,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"
            vm.Carnet = $rootScope.userData.Carnet;
            vm.Universidad = $rootScope.userData.Universidad;
            vm.EnlaceRepositorioCodigo = $rootScope.userData.EnlaceRepositorioCodigo;
            vm.EnlaceACurriculum = $rootScope.userData.EnlaceACurriculum;
            vm.PromedioProyectos = $rootScope.userData.PromedioProyectos;
            vm.PromedioCursos = $rootScope.userData.PromedioCursos;
            vm.Idiomas = $rootScope.userData.Idiomas;
            vm.CursosAprobados = $rootScope.userData.CursosAprobados;
            vm.CursosReprobados = $rootScope.userData.CursosReprobados;
            vm.ProyectosExitisos = $rootScope.userData.ProyectosExitisos;
            vm.ProyectosFallidos = $rootScope.userData.ProyectosFallidos;
            vm.Tecnologias = $rootScope.userData.Tecnologias;

            vm.courseAverageWidth = {'width': vm.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.PromedioProyectos+'%'};      
            $rootScope.currentCourseData={};
            loadCourses();

        }
        
        function loadCourses(){
            
            CourseService.GetAllByUniversity(vm.UniversidadId)
                .then(function(response){
                    
                    vm.courses = response.data; 
                   
            }, function(response){
                console.log("No funcó")
            });
        }
    }

})();
            

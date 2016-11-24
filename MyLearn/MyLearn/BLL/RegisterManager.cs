using System;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class RegisterManager
    {
        public InfoEstudiante StudentRegister(RegisterEstudianteInfo newStudent)
        {
            InfoEstudiante retStudent = new InfoEstudiante();
            AccountManager accountSession = new AccountManager();
            AddStudentToDB(newStudent);
            retStudent = accountSession.StudentLogin(newStudent.Email, newStudent.Password);
            return retStudent;
         }

        public InfoProfesor ProfessorRegister(RegisterProfessorInfo newProfessor)
        {
            InfoProfesor retProfessor = new InfoProfesor();
            AccountManager accountSession = new AccountManager();
            AddProfessorToDB(newProfessor);
            retProfessor = accountSession.ProfessorLogin(newProfessor.Email, newProfessor.Password);
            return retProfessor;

        }

        public InfoEmpleador EmployerRegister(RegisterEmployerInfo newEmployer)
        {
            InfoEmpleador retEmployer = new InfoEmpleador();
            AccountManager accountSession = new AccountManager();
            AddEmployerToDB(newEmployer);
            retEmployer = accountSession.EmployerLogin(newEmployer.Email, newEmployer.Password);
            return retEmployer;

        }

        private void AddStudentToDB(RegisterEstudianteInfo newStudent)
        {
            //Add new student to DB
                /*dbobject.Add(newStudent.NombreContacto);
                dbobject.Add(newStudent.ApellidoContacto);
                dbobject.Add(newStudent.Ubicacion);
                dbobject.Add(newStudent.Email);
                dbobject.Add(newStudent.Telefono);
                dbobject.Add(newStudent.Password);
                dbobject.Add(newStudent.TipoRepositorioArchivos);
                dbobject.Add(newStudent.Foto);
                dbobject.Add(newStudent.Carnet);
                dbobject.Add(newStudent.Universidad);
                dbobject.Add(newStudent.EnlaceACurriculum);
                dbobject.Add(newStudent.Idiomas);
                dbobject.Add(newStudent.Tecnologias);*/
        }

        private void AddProfessorToDB(RegisterProfessorInfo newProfessor)
        {
            //Add new professor to DB
            /*dbobject.Add(newProfessor.NombreContacto);
            dbobject.Add(newProfessor.ApellidoContacto);
            dbobject.Add(newProfessor.Ubicacion);
            dbobject.Add(newProfessor.Email);
            dbobject.Add(newProfessor.Telefono);
            dbobject.Add(newProfessor.Password);
            dbobject.Add(newProfessor.TipoRepositorioArchivos);
            dbobject.Add(newProfessor.Foto);
            dbobject.Add(newProfessor.Universidad);
            dbobject.Add(newProfessor.HorarioAtencion);*/        
    }

        private void AddEmployerToDB(RegisterEmployerInfo newEmployer)
        {
            //Add new employer to DB
            /*dbobject.Add(newEmployer.NombreContacto);
            dbobject.Add(newEmployer.ApellidoContacto);
            dbobject.Add(newEmployer.Ubicacion);
            dbobject.Add(newEmployer.Email);
            dbobject.Add(newEmployer.Telefono);
            dbobject.Add(newEmployer.Password);
            dbobject.Add(newEmployer.TipoRepositorioArchivos);
            dbobject.Add(newEmployer.Foto);
            dbobject.Add(newEmployer.NombreEmpresarial);
            dbobject.Add(newEmployer.EnlaceSitioWeb);*/
        }



    }
}

 
 
 
 
 
 
 
 
 
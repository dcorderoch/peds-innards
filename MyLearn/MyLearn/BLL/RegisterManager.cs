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
            //AddProfessorToDB(newProfessor);
            retProfessor = accountSession.ProfessorLogin(newProfessor.Email, newProfessor.Password);
            return retProfessor;
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
    }
}
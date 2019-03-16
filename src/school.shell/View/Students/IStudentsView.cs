using System;

namespace school.shell.View.Students
{
    public interface IStudentsView
    {
        event EventHandler OnLoadStudents;
        void AddStudents(double mark, params StudentModel[] studentModel);
    }
}
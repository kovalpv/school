using System;

namespace school.shell.View.Students
{
    public class StudentsController
    {
        private readonly IStudentsService studentService;
        private readonly IStudentsView view;
        private readonly IStudentsModel model;

        public StudentsController(IStudentsService studentService, IStudentsView view, IStudentsModel model)
        {
            this.studentService = studentService;
            this.view = view;
            this.model = model;
            this.view.OnLoadStudents += View_OnLoadStudents;
            this.model.AvgPointsChanged += Model_OnAvgPointsChanged;
        }

        private void Model_OnAvgPointsChanged(object sender, AvgPointsEventArg e)
        {
            this.view.AddAvgPoints(e.AvgPoints);
        }

        private void View_OnLoadStudents(object sender, System.EventArgs e)
        {
            LoadStudents();
        }

        internal void AddStudents(params StudentModel[] students)
        {
            model.AddStudents(students);
            view.AddStudents(model.AvgPoints, students);
        }

        internal void LoadStudents()
        {
            var students = studentService.LoadStudents();
            AddStudents(students);
        }
    }
}

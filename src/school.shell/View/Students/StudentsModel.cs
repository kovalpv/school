using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace school.shell.View.Students
{
    public class StudentsModel : IStudentsModel
    {
        public List<StudentModel> Students { get; private set; }
        public double AvgPoints { get; internal set; }
        public event EventHandler<AvgPointsEventArg> AvgPointsChanged;

        public StudentsModel()
        {
            Students = new List<StudentModel>();
        }

        public void AddStudents(params StudentModel[] studentModel)
        {
            Students.Clear();
            studentModel.ToList().ForEach(AddStudent);
            AvgPointsCalculate();
        }

        private void AvgPointsCalculate()
        {
            AvgPoints = Students.Sum(x => x.Points) / Students.Count;
        }

        private void AddStudent(StudentModel student)
        {
            student.PropertyChanged += StudentAvgPointsChanged;
            Students.Add(student);
        }

        private void StudentAvgPointsChanged(object sender, PropertyChangedEventArgs e)
        {
            AvgPointsCalculate();
            OnAvgPointsChanged(this.AvgPoints);
        }

        private void OnAvgPointsChanged(double avgPoints)
        {
            if (AvgPointsChanged == null)
                return;
            AvgPointsChanged(this, new AvgPointsEventArg(avgPoints));
        }
    }
}

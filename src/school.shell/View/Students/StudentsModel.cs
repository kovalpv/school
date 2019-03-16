using System.Collections.Generic;
using System.Linq;

namespace school.shell.View.Students
{
    public class StudentsModel : IStudentsModel
    {
        public List<StudentModel> Students { get; private set; }
        public double AvgPoints { get; internal set; }

        public StudentsModel()
        {
            Students = new List<StudentModel>();
        }

        public void AddStudents(params StudentModel[] studentModel)
        {
            Students.Clear();
            studentModel.ToList().ForEach(AddStudent);
            AvgPoints = Students.Sum(x => x.Points) / Students.Count;
        }

        private void AddStudent(StudentModel student)
        {
            Students.Add(student);
        }
    }
}

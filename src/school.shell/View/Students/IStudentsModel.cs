namespace school.shell.View.Students
{
    public interface IStudentsModel
    {
        void AddStudents(params StudentModel[] studentModel);
        double AvgPoints { get; }
    }
}
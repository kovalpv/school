using NUnit.Framework;
using school.shell.View.Students;

namespace school.shell.tests.View.Students
{
    [TestFixture]
    public class StudentsModelTests
    {
        private StudentsModel viewModel;

        [SetUp]
        public void Setup()
        {
            viewModel = new StudentsModel();
        }

        [Test]
        public void WhenAddOneStudent_ThenStudentsCountIsOne()
        {
            viewModel.AddStudents(new StudentModel { Points = 100 });
            Assert.AreEqual(1, viewModel.Students.Count);
            Assert.AreEqual(100, viewModel.AvgPoints);
        }

        [Test]
        public void WhenAddStudentsAvgPoint78dot4_ThenSuccess()
        {
            viewModel.AddStudents(
                new StudentModel { Points = 78.4 },
                new StudentModel { Points = 78.4 }
             );
            Assert.AreEqual(2, viewModel.Students.Count);
            Assert.AreEqual(78.4, viewModel.AvgPoints);
        }
    }
}

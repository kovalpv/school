using Moq;
using NUnit.Framework;
using school.shell.View.Students;
using System;

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

        [Test]
        public void WhenStudentPointsIsChanged_ThenAvgChanged()
        {
            var students = new[]
            {
                new StudentModel { Points = 70 },
                new StudentModel { Points = 75 }
            };
            viewModel.AddStudents(
                students
             );
            Assert.AreEqual(2, viewModel.Students.Count);
            Assert.AreEqual(72.5, viewModel.AvgPoints);
            students[1].Points = 80;
            Assert.AreEqual(2, viewModel.Students.Count);
            Assert.AreEqual(75, viewModel.AvgPoints);
        }

        [Test]
        public void WhenStudentPointsIsChanged_ThenEventRaise()
        {
            var students = new[]
            {
                new StudentModel { Points = 70 },
                new StudentModel { Points = 75 }
            };

            var AvgPointsChangedMock = new Mock<Action<object, AvgPointsEventArg>>(MockBehavior.Strict);
            AvgPointsChangedMock.Setup(x => x(It.IsAny<object>(), It.IsAny<AvgPointsEventArg>()));
            viewModel.AvgPointsChanged += new EventHandler<AvgPointsEventArg>(AvgPointsChangedMock.Object);


            viewModel.AddStudents(
                students
             );
            students[1].Points = 80;

            AvgPointsChangedMock.Verify(x => x(It.IsAny<object>(), It.IsAny<AvgPointsEventArg>()), Times.Once);
        }
    }
}

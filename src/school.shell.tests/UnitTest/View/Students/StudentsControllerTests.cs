using Moq;
using NUnit.Framework;
using school.shell.View.Students;
using System;

namespace school.shell.tests.View.Students
{
    [TestFixture]
    public class StudentsControllerTests
    {
        private Mock<IStudentsView> viewMock;
        private Mock<IStudentsModel> modelMock;
        private Mock<IStudentsService> serviceMock;
        private StudentsController controller;

        [SetUp]
        public void Setup()
        {
            viewMock = new Mock<IStudentsView>(MockBehavior.Strict);
            modelMock = new Mock<IStudentsModel>(MockBehavior.Strict);
            serviceMock = new Mock<IStudentsService>(MockBehavior.Strict);
            controller = new StudentsController(serviceMock.Object, viewMock.Object, modelMock.Object);
        }

        [Test]
        public void WhenAddStudent_Then()
        {
            var student = new StudentModel();
            modelMock.Setup(x => x.AddStudents(It.IsAny<StudentModel>()));
            modelMock.Setup(x => x.AvgPoints).Returns(24.783);
            viewMock.Setup(x => x.AddStudents(It.IsAny<double>(), It.IsAny<StudentModel>()));

            controller.AddStudents(student);


        }

        [Test]
        public void WhenOnLoad_Then()
        {
            var view = viewMock.Object;
            StudentModel[] students = new StudentModel[]
            {
                new StudentModel (),
                new StudentModel ()
            };

            serviceMock.Setup(x => x.LoadStudents()).Returns(students);
            modelMock.Setup(x => x.AddStudents(It.IsAny<StudentModel[]>()));
            modelMock.Setup(x => x.AvgPoints).Returns(95);
            viewMock.Setup(x => x.AddStudents(It.IsAny<double>(), It.IsAny<StudentModel[]>()));

            viewMock.Raise(x => x.OnLoadStudents += null, EventArgs.Empty);

            serviceMock.Verify(x => x.LoadStudents(), Times.Once);
            modelMock.Verify(x => x.AddStudents(It.IsAny<StudentModel[]>()), Times.Once);
            modelMock.Verify(x => x.AvgPoints, Times.Once);
            viewMock.Verify(x => x.AddStudents(It.IsAny<double>(), It.IsAny<StudentModel[]>()), Times.Once);

        }

    }
}

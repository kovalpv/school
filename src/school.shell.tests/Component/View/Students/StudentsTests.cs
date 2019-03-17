using Moq;
using NUnit.Framework;
using school.shell.View.Students;
using System;

namespace school.shell.tests.View.Students
{
    [TestFixture]
    [SetCulture(Constants.Culture)]
    [SetUICulture(Constants.UICulture)]
    public class StudentsTests
    {
        private StudentsView view;
        private StudentsModel model;
        private Mock<IStudentsService> service;
        private StudentsController controller;

        [SetUp]
        public void Setup()
        {
            view = new StudentsView();
            model = new StudentsModel();
            service = new Mock<IStudentsService>(MockBehavior.Strict);
            controller = new StudentsController(service.Object, view, model);
        }

        [Test]
        public void WhenLoadStudentAll100Points_ThenAvgIs100()
        {
            ScenarioCalculateAvg("100", new StudentModel[]
                {
                    new StudentModel{Points=100},
                    new StudentModel{Points=100},
                    new StudentModel{Points=100},
                    new StudentModel{Points=100},
                    new StudentModel{Points=100}
                });
        }

        private void ScenarioCalculateAvg(string expectAvg, StudentModel[] students)
        {
            var button = view.BtnLoad;

            service.Setup(x => x.LoadStudents()).Returns(students);

            button.PerformClick();

            service.Verify(x => x.LoadStudents(), Times.Once);
            Assert.AreEqual(expectAvg, view.LabelPoints.Text);
        }

        [Test]
        public void WhenLoadStudentFirst100PointsAndSecod50_ThenAvgIs75()
        {
            ScenarioCalculateAvg("75",
                new StudentModel[]
                {
                    new StudentModel{Points=100},
                    new StudentModel{Points=50}
                });
        }

        [Test]
        public void WhenLoadStudentIsNull_ThenThrowArgumentNullException()
        {
            var button = view.BtnLoad;
            var students = new StudentModel[]
                {
                    null
                };
            service.Setup(x => x.LoadStudents()).Returns(students);

            var exception = Assert.Throws<NullReferenceException>(() => button.PerformClick());

            Assert.AreEqual("Object reference not set to an instance of an object.", exception.Message);
        }

        [Test]
        public void WhenStudentPointsIsChanged_ThenAvgChanged()
        {
            var students = new StudentModel[]
                {
                    new StudentModel{Points=100},
                    new StudentModel{Points=50}
                };
            ScenarioCalculateAvg("75", students);
            students[1].Points = 80;
            Assert.AreEqual("90", view.LabelPoints.Text);
        }
    }
}

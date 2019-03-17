using Moq;
using NUnit.Framework;
using school.shell.View.Students;
using System;

namespace school.shell.tests.View.Students
{
    [TestFixture]
    [SetCulture(Constants.Culture)]
    [SetUICulture(Constants.UICulture)]
    public class StudentsViewTests
    {
        private StudentsView view;

        [SetUp]
        public void Setup()
        {
            view = new StudentsView();
        }

        [TearDown]
        public void TearDown()
        {
            view.Dispose();
        }

        [Test]
        public void WhenLoadButtonClick_ThenEventTrigger()
        {
            var btnLoad = view.BtnLoad;

            var eventClick = new Mock<EventHandler>(MockBehavior.Strict);
            eventClick.Setup(x => x(It.IsAny<object>(), It.IsAny<EventArgs>()));

            view.OnLoadStudents += eventClick.Object;

            btnLoad.PerformClick();
            eventClick.Verify(x => x(It.IsAny<object>(), It.IsAny<EventArgs>()), Times.Once());
        }

        [Test]
        public void WhenAddStudent_ThenCoutEqualsOne()
        {
            var listStudents = view.ListStudents;
            view.AddStudents(9.645, new StudentModel());
            Assert.AreEqual(1, listStudents.Items.Count);
            Assert.AreEqual("9.65", view.LabelPoints.Text);
        }

        [Test]
        public void WhenAddFiveStudent_ThenCoutEqualsFive()
        {
            var listStudents = view.ListStudents;
            view.AddStudents(0, new[] {
                new StudentModel(),
                new StudentModel(),
                new StudentModel(),
                new StudentModel(),
                new StudentModel()
            });
            Assert.AreEqual(5, listStudents.Items.Count);
            Assert.AreEqual("0", view.LabelPoints.Text);
        }

        [Test]
        public void WhenStudentIsNull_ThenTrowException()
        {
            var listStudents = view.ListStudents;
            var exception = Assert.Throws<ArgumentNullException>(() => view.AddStudents(0, null));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: source", exception.Message);
        }

        [Test]
        public void WhenDefault_ThenAvgPoint0()
        {
            var labelPoints = view.LabelPoints;
            Assert.AreEqual("0", labelPoints.Text);
        }

        [Test]
        public void WhenFiveStudentAvgPoints100_ThenAvgPoint100()
        {
            var students = new[]
            {
                new StudentModel() { Points = 100 },
                new StudentModel() { Points = 100 },
                new StudentModel() { Points = 100 },
                new StudentModel() { Points = 100 },
            };
            view.AddStudents(8.7342, students);
            var labelPoints = view.LabelPoints;
            Assert.AreEqual("8.73", view.LabelPoints.Text);
        }

        [Test]
        public void WhenLoadButtonClickAndEventRemove_ThenEventNotTrigger()
        {
            var btnLoad = view.BtnLoad;

            var eventClick = new Mock<EventHandler>(MockBehavior.Strict);
            eventClick.Setup(x => x(It.IsAny<object>(), It.IsAny<EventArgs>()));

            view.OnLoadStudents += eventClick.Object;

            btnLoad.PerformClick();
            eventClick.Verify(x => x(It.IsAny<object>(), It.IsAny<EventArgs>()), Times.Once());
            eventClick.Reset();

            view.OnLoadStudents -= eventClick.Object;
            btnLoad.PerformClick();
            eventClick.Verify(x => x(It.IsAny<object>(), It.IsAny<EventArgs>()), Times.Never());
        }

    }
}

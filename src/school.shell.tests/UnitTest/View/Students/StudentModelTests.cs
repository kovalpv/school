using Moq;
using NUnit.Framework;
using school.shell.View.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.shell.tests.UnitTest.View.Students
{
    [TestFixture]
    public class StudentModelTests
    {
        public Mock<Action<object, PropertyChangedEventArgs>> PropertyChangedMock { get; private set; }

        private StudentModel model;

        [SetUp]
        public void Setup()
        {
            PropertyChangedMock = new Mock<Action<object, PropertyChangedEventArgs>>(MockBehavior.Strict);
            model = new StudentModel();
            model.PropertyChanged += new PropertyChangedEventHandler(PropertyChangedMock.Object);
        }

        [Test]
        public void WhenPointTryChangeEqualValue_ThenNothing()
        {
            model.Points = 0;
            PropertyChangedMock.Verify(x => x(It.IsAny<object>(), It.IsAny<PropertyChangedEventArgs>()), Times.Never);
        }

        [Test]
        public void WhenPointChangeEqualValue_ThenEventRaise()
        {
            PropertyChangedMock.Setup(x => x(It.IsAny<object>(), It.IsAny<PropertyChangedEventArgs>()));

            model.Points = 10;
            PropertyChangedMock.Verify(x => x(It.IsAny<object>(), It.IsAny<PropertyChangedEventArgs>()), Times.Once);
        }
    }
}

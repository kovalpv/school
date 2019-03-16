using System;
using System.Linq;
using System.Windows.Forms;

namespace school.shell.View.Students
{
    public partial class StudentsView : UserControl, IStudentsView
    {

        public event EventHandler OnLoadStudents
        {
            add { BtnLoad.Click += value; }
            remove { BtnLoad.Click -= value; }
        }

        public StudentsView()
        {
            InitializeComponent();
        }

        public void AddStudents(double mark, params StudentModel[] studentModel)
        {
            ListStudents.Items.Clear();

            LabelPoints.Text = (Math.Round(mark, 2, MidpointRounding.AwayFromZero)).ToString();
            studentModel.ToList().ForEach(AddStudent);
        }

        private void AddStudent(StudentModel x)
        {
            ListStudents.Items.Add(x.ToString());
        }

    }
}

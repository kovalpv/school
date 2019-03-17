using System.ComponentModel;

namespace school.shell.View.Students
{
    public class StudentModel : INotifyPropertyChanged
    {
        private double points;
        public double Points
        {
            get { return points; }
            internal set
            {
                if (value == points)
                    return;
                points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

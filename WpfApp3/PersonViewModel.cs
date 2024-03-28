using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp3;

namespace WpfApp3
{
    internal class PersonViewModel
    {
        private PersonModel _person;
        private MainWindow _window;

        public PersonViewModel(MainWindow mainWindow)
        {
            _person = new PersonModel("", "", "", DateTime.Now);
            _window = mainWindow;
            ProceedCommand = new RelayCommand(Proceed, CanProceed);
        }

        public ICommand ProceedCommand { get; }

        public string Name
        {
            get { return _person.Name; }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get { return _person.Surname; }
            set { _person.Surname = value; }
        }

        public string Mail
        {
            get { return _person.Mail; }
            set { _person.Mail = value; }
        }

        public DateTime Birthdate
        {
            get { return _person.Birthdate; }
            set { _person.Birthdate = value; }
        }

        public bool IsAdult
        {
            get { return _person.IsAdult; }
        }

        public string SunSign
        {
            get { return _person.SunSign; }
        }

        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }

        public bool IsBirthday
        {
            get { return _person.IsBirthday; }
        }

        private bool CanProceed(object parameter)
        {
            if (string.IsNullOrEmpty(_window.NameInput.Text)) return false;
            if (string.IsNullOrEmpty(_window.SurnameInput.Text)) return false;
            if (string.IsNullOrEmpty(_window.MailInput.Text)) return false;
            if (!_window.BirthdatePicker.SelectedDate.HasValue) return false;
            return true;
        }

        private async void Proceed(object parameter)
        {
            _window.PersonData.IsEnabled = false;
            _window.NameOutput.Text = "";
            _window.SurnameOutput.Text = "";
            _window.MailOutput.Text = "";
            _window.DateOfBirthOutput.Text = "";
            _window.IsAdultOutput.Text = "";
            _window.SunSignOutput.Text = "";
            _window.ChineseSignOutput.Text = "";
            _window.HappyBirthdayGreeting.Content = "";
            try
            {
                string name = _window.NameInput.Text;
                string surname = _window.SurnameInput.Text;
                string mail = _window.MailInput.Text;
                if (!CheckMail(mail)) throw new InvalidMail();
                DateTime date = _window.BirthdatePicker.SelectedDate.Value;
                if (CheckAge(date) < 0) throw new FutureBirthdayException();
                if (CheckAge(date) > 135) throw new FarInPastBirthday();
                await Task.Run(() => { _person = new PersonModel(name, surname, mail, date); });
                _window.NameOutput.Text = Name;
                _window.SurnameOutput.Text = Surname;
                _window.MailOutput.Text = Mail;
                _window.DateOfBirthOutput.Text = Birthdate.ToShortDateString();
                _window.IsAdultOutput.Text = IsAdult ? "Yes" : "No";
                _window.SunSignOutput.Text = SunSign;
                _window.ChineseSignOutput.Text = ChineseSign;
                _window.HappyBirthdayGreeting.Content = IsBirthday ? "Happy birthday!" : "";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally { _window.PersonData.IsEnabled = true; }
        }

        private bool CheckMail(string mail)
        {
            if (!mail.Contains("@")) return false;
            if (!mail.EndsWith(".com")) return false;
            return true;
        }

        public int CheckAge(DateTime dateOfBirth)
        {
            DateTime current = DateTime.Now;
            int age = current.Year - dateOfBirth.Year;

            if (current.Month < dateOfBirth.Month || (current.Month == dateOfBirth.Month && current.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }
    }
}

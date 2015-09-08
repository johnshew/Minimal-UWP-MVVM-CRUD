using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using Models;

namespace ViewModels
{
    public class OrganizationViewModel : NotificationBase
    {
        Organization organization;

        public OrganizationViewModel(String name)
        {
            organization = new Organization(name);
            _SelectedIndex = -1;
            // Load the database
            foreach (var person in organization.People)
            {
                var np = new PersonViewModel(person);
                np.PropertyChanged += Person_OnNotifyPropertyChanged;
                _People.Add(np);
            }
        }

        ObservableCollection<PersonViewModel> _People = new ObservableCollection<PersonViewModel>();
        public ObservableCollection<PersonViewModel> People
        {
            get { return _People; }
            set { SetProperty(ref _People, value); }
        }

        String _Name;
        public String Name
        {
            get { return organization.Name; }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { if (SetProperty(ref _SelectedIndex, value)) { RaisePropertyChanged("SelectedPerson"); } }
        }

        public PersonViewModel SelectedPerson
        {
            get { return (_SelectedIndex >= 0) ? _People[_SelectedIndex] : null; }
        }

        public void Add()
        {
            var person = new PersonViewModel();
            person.PropertyChanged += Person_OnNotifyPropertyChanged;
            People.Add(person);
            organization.Add(person);
            SelectedIndex = People.IndexOf(person);
        }

        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var person = People[SelectedIndex];
                People.RemoveAt(SelectedIndex);
                organization.Delete(person);
            }
        }

        void Person_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            organization.Update((PersonViewModel)sender);
        }
    }
}
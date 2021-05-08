using System;
using System.Windows.Input;
using IMAP.Model;
using IMap.ViewModels;
using IMAP.View;

namespace IMAP.ViewModels
{
    public class AreaViewModel : ViewModel
    {
        private Area area;

        private double latitude;
        private double longitude;

        public AreaViewModel()
        {
            area = new Area();
            
            SaveCommand = new IMap.ViewModels.Command(SaveMethod, canExecuteMethod);
            DeleteCommand = new IMap.ViewModels.Command(DeleteMethod, canExecuteMethod);
            CancelCommand = new IMap.ViewModels.Command(CancelMethod, canExecuteMethod);
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Id
        {
            get => area.Id.ToString();
            set
            {
                area.Id = Guid.Parse(value);
                OnPropertyChange();
            }
        }

        public string Label
        {
            get => area.Label;
            set
            {
                area.Label = value;
                OnPropertyChange();
            }
        }

        public string Description
        {
            get => area.Description;
            set
            {
                area.Description = value;
                OnPropertyChange();
            }
        }

        public string Latitude
        {
            get => latitude.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.latitude = Convert.ToDouble(value);
                    OnPropertyChange();
                }
            }
        }

        public string Longitude
        {
            get => longitude.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.longitude = Convert.ToDouble(value);
                    OnPropertyChange();
                }
            }
        }

        public string Radius
        {
            get => area.Radius.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    area.Radius = Convert.ToInt32(value);
                    OnPropertyChange();
                }
            }
        }

        private async void SaveMethod(object parameters) //TODO Проверку на пустые строки
        {
            area.setPosition(latitude, longitude);

            await area.SaveFromDB();
            await App.NavigateMasterDetail(new AreasPage()); //this wrong
            //await Application.Current.MainPage.Navigation.PopAsync(); //this true
        }

        private async void DeleteMethod(object parameters)
        {
            await area.DeleteFromDB();
            await App.NavigateMasterDetail(new AreasPage());
          //  await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void CancelMethod(object parameters)
        {
            await App.NavigateMasterDetail(new AreasPage());
          //  await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}




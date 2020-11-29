using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using IMap.ViewModel;

namespace IMAP.Model
{
    public class DailyWeather : ViewModel
    {
        public int DayOfYear { get; set; }

        private ObservableCollection<HourlyWeather> hourlyWeather { get; set; }
        public ObservableCollection<HourlyWeather> HourlyWeather { 
            get => hourlyWeather;
            set
            {
                hourlyWeather = value;
                OnPropertyChange();
            }
        }

    }
}

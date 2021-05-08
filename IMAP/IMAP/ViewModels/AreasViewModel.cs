using System.Collections.ObjectModel;
using System.Windows.Input;
using IMAP.Model;
using Xamarin.Forms;
using IMAP.View;
using IMap.ViewModels;

namespace IMAP.ViewModels
{
    public class AreasViewModel : ViewModel
    {
        private Area selectedArea; //хранение выбранного item из списка
        private TableDataModel<Area> areasModel; //Модель для работы с бд
        private ObservableCollection<Area> areasCollection; //коллекция Компаний

        public AreasViewModel()
        {
            selectedArea = new Area();
            areasModel = new TableDataModel<Area>();
            areasCollection = new ObservableCollection<Area>();

            OnAppearing();

            SelectAreaCommand = new IMap.ViewModels.Command(SelectAreaMethod, canExecuteMethod);
            AddAreaCommand = new IMap.ViewModels.Command(AddAreaMethod, canExecuteMethod);
            UpdateAreaCommand = new IMap.ViewModels.Command(UpdateData, canExecuteMethod);
        }

        public ICommand SelectAreaCommand { get; set; }
        public ICommand AddAreaCommand { get; set; }
        public ICommand UpdateAreaCommand { get; set; }

        public Area SelectedArea
        {
            get => selectedArea;
            set
            {
                if (value != null)
                {
                    selectedArea = value;
                    OnPropertyChange();
                }
            }
        }

        public ObservableCollection<Area> AreasCollection
        {
            get => areasCollection;
        }

        protected async void OnAppearing()
        {
            await areasModel.CreateTable();
            UpdateAreaCommand.Execute(null);    //Обновляем данные
        }

        private async void SelectAreaMethod(object parameters)
        {
            SelectedArea = (Area)parameters;    //Принимаем параметры с фронта о выбранном сотруднике
            AreaPage areaPage = new AreaPage(selectedArea); //Передаем данные в форму сотрудника

            await App.NavigateMasterDetail(areaPage);
        }

        private async void AddAreaMethod(object parameters)
        {
            await App.NavigateMasterDetail(new AreaPage());
        }

        private async void UpdateData(object parameters)
        {
            areasCollection.Clear(); //TODO сделать добавление новых данных, а не очистку всей коллекции

            foreach (var item in await areasModel.GetItems())
            {
                areasCollection.Add(item);
            }                

            OnPropertyChange();
        }
    }
}

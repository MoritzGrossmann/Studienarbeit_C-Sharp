internal class MainViewModel : BaseViewModel
{
    private BaseViewModel _currentViewModel;

    /// <summary>
    /// Das Aktuell angezeigte ViewMdoel
    /// </summary>
    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
    }

    private void ToBaseData()
    {
        CurrentViewModel = new LoadingViewModel(); // Ladeansicht wird angezeigt
        TaskAwaiter<BaseDataManagementViewModel> awaiter = GetBaseDataManagementViewModel().GetAwaiter();

        awaiter.OnCompleted(() =>
        {
            CurrentViewModel = awaiter.GetResult(); //Stammdatenverwaltung wird angezeigtm wenn alle Daten geladen wurden
        });

    }

    private Task<BaseDataManagementViewModel> GetBaseDataManagementViewModel()
    {
        //_bookingSystemDataPersistence = Kontext zur Datenbank
        return Task.Run(() => new BaseDataManagementViewModel(_bookingSystemDataPersistence));
    }
}
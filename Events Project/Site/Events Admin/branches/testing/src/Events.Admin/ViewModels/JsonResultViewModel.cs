namespace Aafp.Events.Admin.ViewModels
{
    public class JsonResultViewModel<T> : ViewModelBase
    {
        public T Data { get; set; }
    }
}
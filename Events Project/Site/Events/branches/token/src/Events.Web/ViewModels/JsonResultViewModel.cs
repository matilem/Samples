namespace Aafp.Events.Web.ViewModels
{
    public class JsonResultViewModel<T> : ViewModelBase
    {
        public T Data { get; set; }
    }
}
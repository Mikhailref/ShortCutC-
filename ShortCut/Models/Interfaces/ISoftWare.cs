using ShortCut.Models.Apps;

namespace ShortCut.Models.Interfaces
{
    public interface ISoftWare
    {
        public string GroupName { get; set; }    
        public List<App> AppsCollection { get; set; }
        void AddNewAppToTheList(App NewApp);

    }
}

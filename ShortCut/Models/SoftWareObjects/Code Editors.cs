using ShortCut.Models.Apps;
using ShortCut.Models.Interfaces;

namespace ShortCut.Models.SoftWareGroups
{
    public class CodeEditors : ISoftWare
    {
        public string GroupName { get; set; }
        public List<App> AppsCollection { get; set; }
        public CodeEditors()
        {
            AppsCollection = new List<App>();
        }
        public void AddNewAppToTheList(App NewApp)
        {
            AppsCollection.Add(NewApp);
        }
    }
}

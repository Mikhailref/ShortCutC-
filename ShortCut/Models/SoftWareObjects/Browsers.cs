using ShortCut.Models.Apps;
using ShortCut.Models.Interfaces;

namespace ShortCut.Models.SoftWareGroups
{
    public class Browsers:ISoftWare
    {
        public string GroupName { get ; set ; }
        public List<App> AppsCollection { get ; set ; }

        public Browsers()
        {
            AppsCollection = new List<App>();
        }

        public void AddNewAppToTheList(App NewApp )
        {
            AppsCollection.Add(NewApp);
        }
    }
}


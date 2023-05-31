using ShortCut.Models.Interfaces;

namespace ShortCut.Models.Apps
{
    public class App
    {
        public string Name { get; set; }
        public List<string[]> ShortCutsWithDescription { get; set; }


        public App(string name)
        {
            Name = name;
            ShortCutsWithDescription = new List<string[]>();
        }

  
        public void CreateAndAddArrayOfShortCutToTheList(string description, string shortCut)
        {
            string[] data = new string[] { description, shortCut };
            ShortCutsWithDescription.Add(data);
        }

    }
}

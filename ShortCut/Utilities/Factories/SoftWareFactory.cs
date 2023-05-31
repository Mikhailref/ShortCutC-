using ShortCut.Models.Interfaces;
using ShortCut.Models.SoftWareGroups;

namespace ShortCut.Utilities.Factories
{
    public class SoftWareFactory : ISoftWareFactory
    {
        public ISoftWare CreateSoftWare(int Id)
        {
            switch(Id) 
            {
                case 1:
                    return new OperatingSystems();
                case 2:
                    return new Browsers();
                case 3:
                    return new OfficeSuites();
                case 4:
                    return new CodeEditors();
                case 5:
                    return new GraphicDesigners();
                default:
                    throw new ArgumentException("Invalid SoftWareType");
            }
        }
    }
}

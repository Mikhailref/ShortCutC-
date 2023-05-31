using ShortCut.Models.Interfaces;

namespace ShortCut.Utilities.Factories
{
    public interface ISoftWareFactory
    {
        public ISoftWare CreateSoftWare(int Id);
    }
}

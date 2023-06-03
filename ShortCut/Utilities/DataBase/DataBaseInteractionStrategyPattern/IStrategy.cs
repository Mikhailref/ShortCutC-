using ShortCut.Models.Interfaces;

namespace ShortCut.Utilities.DataBase.DataBaseInteractionStrategyPattern
{
    public interface IStrategy
    {
        ISoftWare FetchSoftWare(int id);
    }
}

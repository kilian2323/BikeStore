using Core.Classes;

namespace UI.Interfaces
{
    public interface IResultsRequiredBySearch
    {
        public string GetTableAlias();

        public void DoSearch(Search search);
    }
}

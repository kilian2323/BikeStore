using Core.Classes;

namespace UI.Interfaces
{
    public interface IResultsRequiredBySearch
    {
        public string GetTableAlias();

        public int DoSearch(Search search);
    }
}

using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Base
{
    public class Search
    {
        public Dictionary<string, string> SearchItems = new Dictionary<string, string>(3);
        public bool CombineAnd = false;
        public bool ExactMatch = false;
        public List<string> ColDisplayNames;
        private List<List<string>> colSelected = new List<List<string>>(3);

        public Search(Dictionary<string, string> si, bool ca, bool em)
        {
            SearchItems = si;
            CombineAnd = ca;
            ExactMatch = em;
            PopulateComboBoxes();
        }

        public DataTable GetSearchResults(Dictionary<string, string> searchItems, bool combineAnd, bool exactMatch)
        {

        }

        private List<string> getColDisplayNames(string tableAlias)
        {
            return BLLAccessor.GetTableColumns(tableAlias);
        }

        private void PopulateComboBoxes()
        {

        }

        public void DisplayResults()
        {

        }

        


    }
}

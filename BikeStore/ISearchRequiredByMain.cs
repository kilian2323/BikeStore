using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore
{
    /**
     * Methods required by Main to interface the Search panel.
     */
    interface ISearchRequiredByMain
    {
        public void SetResultsPage(IResultsRequiredBySearch _page, string schemaName, string tableName);
        public void Clear();
    }
}

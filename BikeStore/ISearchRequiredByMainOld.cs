using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore
{
    /**
     * Methods required by Main to interface the Search panel.
     */
    interface ISearchRequiredByMainOld
    {
        public void SetResultsPage(IResultsRequiredBySearchOld _page, string schemaName, string tableName);
        public void Clear();
    }
}

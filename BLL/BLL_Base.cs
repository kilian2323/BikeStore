using Core.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace BLL
{
    public class BLL_Base
    {
        /* To be called from GUI level */
        public List<string> GetTableColumns(string tableAlias)        {

            Type t = Core.Models.TableTypes.GetTypeFromAlias(tableAlias);
            Debug.WriteLine("Type is " + t.ToString());
            MemberInfo[] members = t.GetMembers();
            List<string> attributeNames = new List<string>();
            foreach (MemberInfo member in members)
            {                
                var memberAttributeName = member.GetCustomAttribute<ColumnViewNameAttribute>();
                if (memberAttributeName != null)
                {
                    Debug.WriteLine("Member is " + member.ToString());
                    Debug.WriteLine("Adding attribute " + memberAttributeName.Name);
                    attributeNames.Add(memberAttributeName.Name);
                }                
            }
            return attributeNames;
        }
    }
}

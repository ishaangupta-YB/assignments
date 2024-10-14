using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.util
{
    // defined functions to dynamically build SQL queries (reduces code repitition and is also easier for me to implement)
    public class QueryBuilder
    {
        private StringBuilder query;       // Stores the query being built
        private List<string> conditions;   // List of where conditions.
        private List<string> columns;      // List of columns for INSERT/UPDATE
        private List<string> values;       // List of values for INSERT/UPDATE

        public QueryBuilder()
        {
            query = new StringBuilder();
            conditions = new List<string>();
            columns = new List<string>();
            values = new List<string>();
        }

        // method to build SELECT query (optional columns to retrieve)
        public QueryBuilder Select(string table, params string[] cols)
        {
            query.Clear();
            query.Append("SELECT ");
            if (cols.Length > 0)
            {
                query.Append(string.Join(", ", cols));
            }
            else
            {
                query.Append("*");
            }
            query.Append(" FROM ").Append(table);
            return this;
        }

        // method to INSERT query with specified columns and values
        public QueryBuilder Insert(string table, Dictionary<string, object> colVals)
        {
            query.Clear();
            columns.Clear();
            values.Clear();

            foreach (var column in colVals)
            {
                columns.Add(column.Key);
                values.Add($"@{column.Key}");
            }

            query.Append("INSERT INTO ").Append(table)
                .Append(" (").Append(string.Join(", ", columns)).Append(") ")
                .Append("VALUES (").Append(string.Join(", ", values)).Append(")");

            return this;
        }

        // method to build UPDATE query with specified columns and values
        public QueryBuilder Update(string table, Dictionary<string, object> colVals)
        {
            query.Clear();
            columns.Clear();

            foreach (var column in colVals)
            {
                columns.Add($"{column.Key} = @{column.Key}");
            }

            query.Append("UPDATE ").Append(table).Append(" SET ")
                .Append(string.Join(", ", columns));

            return this;
        }
        // method to build a DELETE query for the specified table
        public QueryBuilder Delete(string table)
        {
            query.Clear();
            query.Append("DELETE FROM ").Append(table);
            return this;
        }

        // method to append a WHERE clause condition
        public QueryBuilder Where(string condition)
        {
            conditions.Add(condition);
            return this;
        }
        // Finalizes the query and returns the complete SQL string
        public string Build()
        {
            if (conditions.Count > 0)
            {
                query.Append(" WHERE ").Append(string.Join(" AND ", conditions));
            }
            return query.ToString();
        }

        // Resets the builder for reuse (iam not using for now)
        public void Reset()
        {
            query.Clear();
            conditions.Clear();
            columns.Clear();
            values.Clear();
        }
    }
}

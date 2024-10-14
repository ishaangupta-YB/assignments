using System.Collections.Generic;
using System.Text;

namespace CarConnect.util
{
    public class QueryBuilder
    {
        private StringBuilder query;
        private List<string> conditions;
        private List<string> columns;
        private List<string> values;

        public QueryBuilder()
        {
            query = new StringBuilder();
            conditions = new List<string>();
            columns = new List<string>();
            values = new List<string>();
        }

        public QueryBuilder Select(string table, params string[] cols)
        {
            query.Clear();
            conditions.Clear();
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

        public QueryBuilder Update(string table, Dictionary<string, object> colVals)
        {
            query.Clear();
            conditions.Clear();
            columns.Clear();

            foreach (var column in colVals)
            {
                columns.Add($"{column.Key} = @{column.Key}");
            }

            query.Append("UPDATE ").Append(table).Append(" SET ")
                .Append(string.Join(", ", columns));

            return this;
        }

        public QueryBuilder Delete(string table)
        {
            query.Clear();
            conditions.Clear();
            query.Append("DELETE FROM ").Append(table);
            return this;
        }

        public QueryBuilder Where(string condition)
        {
            conditions.Add(condition);
            return this;
        }

        public string Build()
        {
            if (conditions.Count > 0)
            {
                query.Append(" WHERE ").Append(string.Join(" AND ", conditions));
            }
            return query.ToString();
        }

        public void Reset()
        {
            query.Clear();
            conditions.Clear();
            columns.Clear();
            values.Clear();
        }
    }
}

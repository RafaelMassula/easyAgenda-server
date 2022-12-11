using System.Reflection;

namespace EasyAgendaService
{
    public abstract class DmlService<T>
    {
        public static string GetQueryInsert(T entity, string tableName)
        {
            string props = $"INSERT INTO {tableName}(";
            string values = "VALUES(";
            PropertyInfo[] propertyInfos = GetPropertyInfo(entity);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    if (propertyInfo == propertyInfos[propertyInfos.Length - 1])
                    {
                        props += String.Concat("[", propertyInfo.Name, "]", ")");
                        values += String.Concat("@", propertyInfo.Name, ")");
                    }
                    else
                    {
                        props += String.Concat("[", propertyInfo.Name, "]", ",");
                        values += String.Concat("@", propertyInfo.Name, ", ");
                    }
                }
            }
            return String.Concat(props, values);
        }
        public static string GetQueryInsert(IList<T> entitys, string tableName)
        {
            string props = $"INSERT INTO {tableName}(";
            string values = "VALUES(";
            int i = 0;

            PropertyInfo[] propertyInfos = GetPropertyInfo(entitys[i]);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    if (propertyInfo == propertyInfos[propertyInfos.Length - 1])
                    {
                        props += String.Concat("[", propertyInfo.Name, "]", ")");
                        values += String.Concat("@", propertyInfo.Name, ")");
                    }
                    else
                    {
                        props += String.Concat("[", propertyInfo.Name, "]", ",");
                        values += String.Concat("@", propertyInfo.Name, ", ");
                    }
                }
            }
            string valuesReplace = values.Replace("VALUES", ",");
            while (i < (entitys.Count - 1))
            {
                values += valuesReplace;
                i++;
            }
            return String.Concat(props, values);
        }
        public static string GetQueryInsertReturn(T entity, string tableName)
        {
            string props = $"INSERT INTO {tableName}(";
            string values = "VALUES(";
            PropertyInfo[] propertyInfos = GetPropertyInfo(entity);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    if (propertyInfo == propertyInfos[propertyInfos.Length - 1])
                    {
                        props += String.Concat(propertyInfo.Name, ") OUTPUT INSERTED.ID ");
                        values += String.Concat("@", propertyInfo.Name, ")");
                    }
                    else
                    {
                        props += String.Concat(propertyInfo.Name, ",");
                        values += String.Concat("@", propertyInfo.Name, ", ");
                    }
                }
            }
            return String.Concat(props, values);

        }
        public static string GetQueryUpdate(T entity, string tableName)
        {
            string query = $"UPDATE {tableName} SET ";
            string values = String.Empty;
            string filter = $" WHERE ";

            List<PropertyInfo> propertyInfos = GetPropertyInfo(entity).ToList();

            propertyInfos.Remove(propertyInfos.First(p => p.Name.ToLower() == "tablename"));
        
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.Name.ToLower() != "tablename")
                {
                    if (propertyInfo.Name.ToLower() == "id")
                    {
                        filter += String.Concat(propertyInfo.Name, " = ", "@", propertyInfo.Name);
                        continue;
                    }
                    if (propertyInfo == propertyInfos[propertyInfos.Count - 1])
                    {
                        values += String.Concat(propertyInfo.Name, " = ", "@", propertyInfo.Name);
                    }
                    else
                    {
                        values += String.Concat(propertyInfo.Name, " = ", "@", propertyInfo.Name, ", ");
                    }
                }
            }
            return String.Concat(query, values, filter);
        }
        private static PropertyInfo[] GetPropertyInfo(T entity)
        {
            if (entity == null)
                throw new NullReferenceException("Reference null");
            return entity.GetType().GetProperties();
        }
    }
}

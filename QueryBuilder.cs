﻿using CSCI2910_Lab5.models;
using Microsoft.Data.Sqlite;
using System.Reflection;
using System.Text;

namespace CSCI2910_Lab5
{
    internal class QueryBuilder : IDisposable
    {
        private SqliteConnection connection;

        public QueryBuilder(string databasePath)
        {
            connection = new SqliteConnection("Data Source=" + databasePath);
            connection.Open();
        }

        public void Insert<T>(T obj)
        {
            // get the property names of obj
            PropertyInfo[] properties = typeof(T).GetProperties();

            // get the values of the properties
            var values = new List<string>();
            var names = new List<string>();
            PropertyInfo property;


            // loop though collection of properties -- pulling out the name and
            // value of each and adding it to the corresponding list of strings
            for(int i = 0; i < properties.Length; i++)
            {
                property = properties[i];
                values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                names.Add(property.Name);
            }

            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

            for (int i = 0; i < values.Count; i++)
            {
                if(i == values.Count - 1)
                {
                    sbValues.Append($"{values[i]}");
                    sbNames.Append($"{names[i]}");
                }
                else
                {
                    sbValues.Append($"{values[i]}, ");
                    sbNames.Append($"{names[i]}");
                }
            }

            var command = connection.CreateCommand();

            // optional -- help speed or prevent lockout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 0;

            // not optional
            command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues})";
            command.ExecuteNonQuery();
        }

        public T Read<T>(int id) where T : new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name} WHERE Id = {id}";
            using(var reader = command.ExecuteReader())
            {
                T data = new T();

                while(reader.Read())
                {
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        var propertyType = typeof(T).GetProperty(reader.GetName(i)).PropertyType;
                        var propertyName = typeof(T).GetProperty(reader.GetName(i));

                        // need to convert integer values -- sqlite will try to
                        // store them in our objects as Int64 -- we need Int32
                        if(propertyType == typeof(int))
                        {
                            propertyName.SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                        }
                        else
                        {
                            propertyName.SetValue(data, reader.GetValue(i));
                        }
                    }
                }
                return data;
            }
        }

        public List<T> ReadAll<T>() where T : new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            using (var reader = command.ExecuteReader())
            {
                T data;
                var list = new List<T>();

                while (reader.Read())
                {
                    data = new T();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var propertyType = typeof(T).GetProperty(reader.GetName(i)).PropertyType;
                        var propertyName = typeof(T).GetProperty(reader.GetName(i));

                        // need to convert integer values -- sqlite will try to
                        // store them in our objects as Int64 -- we need Int32
                        if (propertyType == typeof(int))
                        {
                            propertyName.SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                        }
                        else
                        {
                            propertyName.SetValue(data, reader.GetValue(i));
                        }
                    }
                    list.Add(data);
                }
                return list;
            }
        }

        public void Create<T>(T obj)
        {
            //SqliteCommand command = connection.CreateCommand();
            //command.CommandText = $"CREATE {typeof(T).Name} ({column1} {datatype}, {column2} {datatype});";
            //command.ExecuteNonQuery();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"CREATE TABLE Test (TestId INTEGER PRIMARY KEY, TestName TEXT, TestNumber INTEGER);";
            command.ExecuteNonQuery();
        }

        public void Update<T>(T obj)
        {
            //string column1 = string.Empty;
            //string value1 = string.Empty;
            //string condition = string.Empty;

            //SqliteCommand command = connection.CreateCommand();
            //command.CommandText = $"update {typeof(T).Name} set {column1} = {value1} where {condition};";
            //command.ExecuteNonQuery();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Test SET TestId = 123, TestName = 'Jacob', TestNumber = 123 WHERE TestId = 1;";
            command.ExecuteNonQuery();
        }

        // method to delete based on Id -- MUST implement the
        // IClassModel interface to ensure the Id is present
        public void Delete<T>(int id) where T : IClassModel
        {
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE Id = {id};";
            command.ExecuteNonQuery();
        }

        public void DeleteAll<T>()
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {typeof(T).Name};";
            command.ExecuteNonQuery();
        }

        // closes connection at end of 'using' directive
        public void Dispose()
        {
            connection.Close();
        }
    }
}

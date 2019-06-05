using AutoGarden.Models.Interfaces;
using System.Collections.Generic;

namespace AutoGarden.Database
{
    using System.Data;

    using MySql.Data.MySqlClient;

    using Newtonsoft.Json;

    using Utility;

    public class MySqlDatabaseConnector<T> : IRepository<T>
    {
        private readonly string connectionString;
        private readonly MySqlCommandFactory commandFactory;

        public MySqlDatabaseConnector(string connectionString, MySqlCommandFactory commandFactory)
        {
            this.connectionString = connectionString;
            this.commandFactory = commandFactory;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            using (MySqlCommand command = commandFactory.CreateGetAllCommand())
            {
                DataSet dataSet = new DataSet();
                DataTable data = new DataTable();
                dataSet.Load(command.ExecuteReader(), LoadOption.OverwriteChanges, data);

                string xmlData = dataSet.GetXml();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(xmlData);
            }
        }

        public Maybe<T> GetOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T value)
        {
            throw new System.NotImplementedException();
        }
    }
}
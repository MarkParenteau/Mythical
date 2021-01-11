using System;
using System.Collections.Generic;
using System.Text;

namespace Mythical
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string ServiceTypeCollectionName { get; set; }
        public string ServiceStatusCollectionName { get; set; }
        public string ServiceCollectionName { get; set; }
        public string RoleCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ServiceTypeCollectionName { get; set; }
        string ServiceStatusCollectionName { get; set; }
        string ServiceCollectionName { get; set; }
        string RoleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

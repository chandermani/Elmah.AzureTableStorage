using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace Elmah.AzureTableStorage
{
    public class AzureTableStorageRollingErrorLog : AzureTableStorageErrorLog
    {
        private readonly CloudTable _cloudTableNextCyleLog;

        private string CurrentCycleTableNameSuffix
        {
            get
            {
                return DateTime.UtcNow.ToString("yyyyMM");
            }
        }

        private string NextCycleTableNameSuffix
        {
            get
            {
                return DateTime.UtcNow.AddMonths(1).ToString("yyyyMM");
            }
        }

        public AzureTableStorageRollingErrorLog(IDictionary config) : base(config)
        {
            
            var tableClient = this.storageAccount.CreateCloudTableClient();

            _cloudTableNextCyleLog = tableClient.GetTableReference(base.GetElmahLogTableName() + NextCycleTableNameSuffix);
            _cloudTableNextCyleLog.CreateIfNotExists();
        }

        protected override void AddEntity(ElmahEntity elmahEntity)
        {
            base.AddEntity(elmahEntity);

            _cloudTableNextCyleLog.Execute(TableOperation.Insert(elmahEntity));
        }

        protected override string GetElmahLogTableName()
        {
            return base.GetElmahLogTableName() + CurrentCycleTableNameSuffix;
        }
    }
}

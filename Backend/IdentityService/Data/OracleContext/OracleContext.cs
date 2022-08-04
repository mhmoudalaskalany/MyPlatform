using System;
using Oracle.ManagedDataAccess.Client;

namespace Data.OracleContext
{
    public static class OracleContext 
    {

        public static  void ConfigureConnection()
        {
            try
            {
                // This sample demonstrates how to use ODP.NET Core Configuration API

                // Add connect descriptors and net service names entries.
                OracleConfiguration.OracleDataSources
                    .Add("wali", "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.233.4.2)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=wali)))");
                

                // Set default statement cache size to be used by all connections.
                OracleConfiguration.StatementCacheSize = 25;

                // Disable self tuning by default.
                OracleConfiguration.SelfTuning = false;

                // Bind all parameters by name.
                OracleConfiguration.BindByName = true;

                // Set default timeout to 60 seconds.
                OracleConfiguration.CommandTimeout = 60;

                // Set default fetch size as 1 MB.
                OracleConfiguration.FetchSize = 1024 * 1024;

                // Set tracing options
                OracleConfiguration.TraceOption = 1;
                OracleConfiguration.TraceFileLocation = @"D:\traces";
                // Uncomment below to generate trace files
                OracleConfiguration.TraceLevel = 7;

                // Set network properties
                OracleConfiguration.SendBufferSize = 8192;
                OracleConfiguration.ReceiveBufferSize = 8192;
                OracleConfiguration.DisableOOB = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using GOSTechnology.Providers.CryptoProvider.LIB;

namespace GOSTechnology.Providers.LoggerProvider.LIB
{
    /// <summary>
    /// LoggerExtension.
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// ConfigureElasticsearch.
        /// </summary>
        /// <param name="indexApplication"></param>
        /// <returns></returns>
        public static ILogger ConfigureElasticsearch(String indexApplication = ConstantsLoggerProvider.INDEX_DEFAULT, String key = ConstantsLoggerProvider.EMPTY, String iv = ConstantsLoggerProvider.EMPTY)
        {
            ILogger result = default(ILogger);

            try
            {
                String dateTimeFormat = GetDateTimeFormat();
                Uri uriElasticsearch = new Uri(GetUriElasticsearch(key, iv));

                ElasticsearchSinkOptions optionsElasticsearch = new ElasticsearchSinkOptions(uriElasticsearch);
                optionsElasticsearch.IndexFormat = $"{indexApplication}-{dateTimeFormat}";
                optionsElasticsearch.AutoRegisterTemplate = true;

                result = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Elasticsearch(optionsElasticsearch).CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.ToString());
                throw new Exception(ConstantsLoggerProvider.MSG_REQUIRED_CONNECTION_STRING, ex);
            }

            return result;
        }

        /// <summary>
        /// GetDateTimeFormat.
        /// </summary>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static String GetDateTimeFormat(String timeZone = ConstantsLoggerProvider.TIMEZONE_DEFAULT)
        {
            var result = DateTime.UtcNow.AddHours(-3).ToString(ConstantsLoggerProvider.DATE_FORMAT);

            try
            {
                result = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(timeZone)).ToString(ConstantsLoggerProvider.DATE_FORMAT);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.ToString());
            }

            return result;
        }

        /// <summary>
        /// GetUriElasticsearch.
        /// </summary>
        /// <returns></returns>
        public static String GetUriElasticsearch(String key = ConstantsLoggerProvider.EMPTY, String iv = ConstantsLoggerProvider.EMPTY)
        {
            String result = default(String);

            try
            {
                IEnumerable<String> elasticsearchConnectionString = GetEnvironmentVariable(key, iv)?.Split(ConstantsLoggerProvider.CONFIG_SPLIT);

                if (elasticsearchConnectionString != null && elasticsearchConnectionString.Count() == 5)
                {
                    String host = elasticsearchConnectionString.FirstOrDefault(_ => _.Contains(ConstantsLoggerProvider.CONFIG_HOST)).Replace(ConstantsLoggerProvider.CONFIG_HOST, String.Empty);
                    Int32.TryParse(elasticsearchConnectionString.FirstOrDefault(_ => _.Contains(ConstantsLoggerProvider.CONFIG_PORT)).Replace(ConstantsLoggerProvider.CONFIG_PORT, String.Empty), out Int32 port);
                    String password = elasticsearchConnectionString.FirstOrDefault(_ => _.Contains(ConstantsLoggerProvider.CONFIG_PASSWORD)).Replace(ConstantsLoggerProvider.CONFIG_PASSWORD, String.Empty);
                    String scheme = elasticsearchConnectionString.FirstOrDefault(_ => _.Contains(ConstantsLoggerProvider.CONFIG_SCHEME)).Replace(ConstantsLoggerProvider.CONFIG_SCHEME, String.Empty);

                    if (!String.IsNullOrEmpty(host) && port > 0)
                    {
                        result = $"{scheme}://{host}:{port}/";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.ToString());
            }

            return result;
        }

        /// <summary>
        /// GetEnvironmentVariable.
        /// </summary>
        /// <param name="environmentVariable"></param>
        /// <returns></returns>
        public static String GetEnvironmentVariable(String key = ConstantsLoggerProvider.EMPTY, String iv = ConstantsLoggerProvider.EMPTY)
        {
            String result = null;

            try
            {
                var defaultEnvironment = Environment.GetEnvironmentVariable(ConstantsLoggerProvider.CONFIG_ELASTICSEARCH_CONNECTION_STRING);
                var userEnvironment = Environment.GetEnvironmentVariable(ConstantsLoggerProvider.CONFIG_ELASTICSEARCH_CONNECTION_STRING, EnvironmentVariableTarget.User);
                var processEnvironment = Environment.GetEnvironmentVariable(ConstantsLoggerProvider.CONFIG_ELASTICSEARCH_CONNECTION_STRING, EnvironmentVariableTarget.Process);
                var machineEnvironment = Environment.GetEnvironmentVariable(ConstantsLoggerProvider.CONFIG_ELASTICSEARCH_CONNECTION_STRING, EnvironmentVariableTarget.Machine);

                if (!String.IsNullOrWhiteSpace(defaultEnvironment))
                {
                    result = defaultEnvironment;
                }
                else if (!String.IsNullOrWhiteSpace(userEnvironment))
                {
                    result = userEnvironment;
                }
                else if (!String.IsNullOrWhiteSpace(processEnvironment))
                {
                    result = processEnvironment;
                }
                else if (!String.IsNullOrWhiteSpace(machineEnvironment))
                {
                    result = machineEnvironment;
                }

                if (!String.IsNullOrWhiteSpace(key) && !String.IsNullOrWhiteSpace(iv))
                {
                    result = CryptoExtension.DecryptAES(result, key, iv);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.ToString());
            }

            return result;
        }
    }
}

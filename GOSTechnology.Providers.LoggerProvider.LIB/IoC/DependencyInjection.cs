using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace GOSTechnology.Providers.LoggerProvider.LIB
{
    /// <summary>
    /// DependencyInjection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// AddLoggerProvider.
        /// </summary>
        /// <param name="builder">Builder service collection extension for initialize dependency injection of LoggerProvider.</param>
        /// <param name="indexApplication">Index for application in Elasticsearch, default "logstash".</param>
        /// <param name="hasElasticsearch">Flag for enable logging in Elasticsearch, default "true".</param>
        /// <returns></returns>
        public static IServiceCollection AddLoggerProvider(this IServiceCollection builder, String indexApplication = ConstantsLoggerProvider.INDEX_DEFAULT, Boolean hasElasticsearch = true, String key = ConstantsLoggerProvider.EMPTY, String iv = ConstantsLoggerProvider.EMPTY)
        {
            try
            {
                builder.AddLogging(configure => { configure.AddConsole(); });

                if (hasElasticsearch)
                {
                    Log.Logger = LoggerExtension.ConfigureElasticsearch(indexApplication, key, iv);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.ToString());
                throw ex;
            }

            return builder;
        }
    }
}

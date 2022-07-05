using System;

namespace GOSTechnology.Providers.LoggerProvider.LIB
{
    /// <summary>
    /// ConstantsLoggerProvider.
    /// </summary>
    public static class ConstantsLoggerProvider
    {
        #region "CONFIGURATIONS"

        public const String CONFIG_HOST = "host=";
        public const String CONFIG_PORT = "port=";
        public const String CONFIG_PASSWORD = "password=";
        public const String CONFIG_SCHEME = "scheme=";
        public const String CONFIG_ELASTICSEARCH_CONNECTION_STRING = "ElasticsearchConnectionString";
        public const String CONFIG_EMPTY = "";
        public const String TIMEZONE_DEFAULT = "E. South America Standard Time";
        public const String INDEX_DEFAULT = "logstash";
        public const String DATE_FORMAT = "yyyy-MM-dd";
        public const String EMPTY = "";
        public const Char CONFIG_SPLIT = ';';

        #endregion

        #region "MESSAGES"

        public const String MSG_REQUIRED_CONNECTION_STRING = "Check the Elasticsearch Connection String in the Environment Variables. E.g: ElasticsearchConnectionString = \"host=x.x.x.x;port=9200;password=mypassword;scheme=http;\"";

        #endregion
    }
}

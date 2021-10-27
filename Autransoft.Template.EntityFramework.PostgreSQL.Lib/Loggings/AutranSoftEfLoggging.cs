using Autransoft.Template.EntityFramework.PostgreSQL.Lib.Exceptions;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Loggings
{
    internal static class AutranSoftEfLoggging
    {
        public static string LogError(this AutranSoftEfException ex)
        {
            var log = Logging.GetErrorHeader(typeof(AutranSoftEfException));

            if(!string.IsNullOrEmpty(ex.Query))
                log.Append($"Query:{ex.Query}|");

            if(!string.IsNullOrEmpty(ex.JsonEntity))
                log.Append($"JsonEntity:{ex.JsonEntity}|");

            Logging.GetExceptionMessage(log, ex);

            return log.ToString().Substring(0, log.ToString().Length - 1);
        }
    }
}
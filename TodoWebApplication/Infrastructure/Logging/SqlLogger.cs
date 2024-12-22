namespace TodoWebApplication.Infrastructure.Logging
{
    public class SqlLogger
    {
        public static void LogQuery(ILogger logger, string sql, object parameters)
        {
            if (parameters == null)
            {
                logger.LogInformation("Executing SQL: {Sql}", sql);
                return;
            }
            logger.LogInformation("Executing SQL: {Sql}\nParameters: {@Params}", sql, parameters);
        }
    }
}

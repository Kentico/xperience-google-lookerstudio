using CMS.Core;
using CMS.Helpers;
using CMS.Scheduler;

using Kentico.Xperience.Google.DataStudio.Services;
using Kentico.Xperience.Google.DataStudio.Services.Implementations;

using System;

namespace Kentico.Xperience.Google.DataStudio.Tasks
{
    /// <summary>
    /// An Xperience scheduled task which generates the physical report file.
    /// </summary>
    public class DataStudioReportTask : ITask
    {
        public string Execute(TaskInfo task)
        {
            try
            {
                CacheHelper.TouchKey(DefaultDataStudioReportProvider.CACHE_DEPENDENCY);
                Service.Resolve<IDataStudioReportGenerator>().GenerateReport().ConfigureAwait(false).GetAwaiter().GetResult();

                return String.Empty;
            }
            catch(Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(DataStudioReportTask), nameof(Execute), ex);
                return "Errors occurred, please check the Event Log.";
            }
        }
    }
}

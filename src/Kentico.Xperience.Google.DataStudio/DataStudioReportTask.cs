using CMS.Core;
using CMS.Scheduler;

using Kentico.Xperience.Google.DataStudio.Services;

using System;

namespace Kentico.Xperience.Google.DataStudio
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
                Service.Resolve<IDataStudioReportGenerator>().GenerateReport();
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

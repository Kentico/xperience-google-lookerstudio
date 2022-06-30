using CMS.Core;
using CMS.Scheduler;

using Kentico.Xperience.Google.DataStudio.Services;

using System;

namespace Kentico.Xperience.Google.DataStudio
{
    public class DataStudioReportTask : ITask
    {
        public string Execute(TaskInfo task)
        {
            try
            {
                return Service.Resolve<IDataStudioReportGenerator>().GenerateReport();
            }
            catch(Exception ex)
            {
                Service.Resolve<IEventLogService>().LogException(nameof(DataStudioReportTask), nameof(Execute), ex);
                return "Errors occurred, please check the Event Log.";
            }
        }
    }
}

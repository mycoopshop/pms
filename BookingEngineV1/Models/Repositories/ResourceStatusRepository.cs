using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class ResourceStatusRepository : IResourceStatusRepository
    {
        private readonly DataContext context;
        public ResourceStatusRepository(DataContext ctx) => context = ctx;


        public bool AddResourceStatusChange(int resourceID, int resourceStatusID, string userID, string comment)
        {
            ResourceStatusLog rsChange = new ResourceStatusLog()
            {
                ResourceID = resourceID,
                UserID = userID,
                Comment = comment,
                ResourceStatusID = resourceStatusID,
                ChangeDate = System.DateTime.Now
            };

            context.ResourceStatusLogs.Add(rsChange);
            context.SaveChanges();
            return true;
        }

        public ResourceStatusLog GetCurrentResourceStatus(int resourceID)
        {
            return context.ResourceStatusLogs.Where(x => x.ResourceID == resourceID).OrderByDescending(y => y.ResourceStatusLogId).FirstOrDefault();
        }

        public List<CurrentResourceStatusViewModel> GetAllResourceStatuses()
        {
            List<CurrentResourceStatusViewModel> allCurrentStatuses = new List<CurrentResourceStatusViewModel>();
            string statusSql = $@"select ResourceID, ResourceName,  ResourceTypeID, ResourceTypeName, 
            ResourceStatusID, ResourceStatusName, ChangeDate, UserID, Comment
            From vwResourceCurrentStatus";
            allCurrentStatuses = context.CurrentResourceStatuses.FromSql(statusSql).AsNoTracking().ToList();
            return allCurrentStatuses;
        }

        public List<ResourceStatus> GetResourceStatuses()
        {
            return context.ResourceStatuses.ToList();
        }
    }
}

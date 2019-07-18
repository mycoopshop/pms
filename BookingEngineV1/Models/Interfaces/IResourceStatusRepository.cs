using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IResourceStatusRepository
    {
        bool AddResourceStatusChange(int resourceID, int newStatusID, string userID, string comment);
        ResourceStatusLog GetCurrentResourceStatus(int resourceID);
        List<CurrentResourceStatusViewModel> GetAllResourceStatuses();
        List<ResourceStatus> GetResourceStatuses();
    }
}

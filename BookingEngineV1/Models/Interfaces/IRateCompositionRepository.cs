using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IRateCompositionRepository
    {
        IEnumerable<RateComposition> RateCompositions { get; }
    }
}

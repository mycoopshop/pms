using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class RateCompositionRepository : IRateCompositionRepository
    {
        private DataContext context;

        public RateCompositionRepository(DataContext ctx) => context = ctx;

        public IEnumerable<RateComposition> RateCompositions => context.RateCompositions.ToArray();
    }
}

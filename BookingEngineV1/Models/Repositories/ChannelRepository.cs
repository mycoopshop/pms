using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingEngineV1.Models.Repositories
{
    public class ChannelRepository : IChannelRepository
    {

        private DataContext context;

        public ChannelRepository(DataContext ctx) => context = ctx;

        public IEnumerable<Channel> Channels => context.Channels.ToArray();
    }
}

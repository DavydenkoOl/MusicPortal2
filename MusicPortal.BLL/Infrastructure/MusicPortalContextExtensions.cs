﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Infrastructure
{
    public static class MusicPortalContextExtensions
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;

namespace TeamTaskManagement.Application.Interfaces.Services
{

    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();
    }
}

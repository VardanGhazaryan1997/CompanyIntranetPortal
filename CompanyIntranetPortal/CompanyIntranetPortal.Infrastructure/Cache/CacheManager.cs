using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyIntranetPortal.Infrastructure
{
    public class CacheManager
    {
        private IUserService _userService;
        private IMemoryCache _memoryCache;

        public CacheManager(IUserService? userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        public async Task CacheInit()
        {
            await GetJubilees();
        }

        private async Task GetJubilees()
        {
            List<User> users = _userService.GetJubilees().Result;

          
            _memoryCache.Set("Users", users);

            //todo: Add timer per starting day

            //try
            //{
            //    var time = DateTime.Now;
            //    const int fourHoursInMilliseconds = 24 * 3600 * 1000;

            //    var fourHourTimer = new System.Timers.Timer() { Interval = fourHoursInMilliseconds };
            //    fourHourTimer.Elapsed += (sender, e) =>
            //    {
            //        List<User> users = _userService.GetJubilees().Result;
            //        _memoryCache.Set<List<User>>("Users", users);
            //    };

            //    var span = DateTime.Now - DateTime.Now;
            //    var timer = new System.Timers.Timer { Interval = span.TotalMilliseconds, AutoReset = false };
            //    timer.Elapsed += (sender, e) => { fourHourTimer.Start(); };
            //    timer.Start();

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }
    }
}

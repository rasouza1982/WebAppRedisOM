using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redis.OM;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAppRedisOM.Domain;

namespace WebAppRedisOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheaterController : ControllerBase
    {
        private readonly RedisConnectionProvider _distributedCache;
        private readonly ILogger<WeatherForecastController> _logger;

        public TheaterController(ILogger<WeatherForecastController> logger, RedisConnectionProvider redisConnectionProvider)
        {
            _logger = logger;
            _distributedCache = redisConnectionProvider;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTheatersById(string id)
        {
            var theaters = _distributedCache.RedisCollection<Theater>();

            var result = theaters.Where(p => p.BoxOfficeId == id);

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetTheatersByName(string name)
        {
            var theaters = _distributedCache.RedisCollection<Theater>();

            var result = theaters.Where(p => p.Name == name);

            if(result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _distributedCache.RedisCollection<Theater>();

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var theaters = _distributedCache.RedisCollection<Theater>();            

            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6080", Name = "Test1", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6025", Name = "Test2", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6025", Name = "Test2", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6025", Name = "Test2", Datetime = DateTime.Now, ErrorDescription = "Timeout" });
            await theaters.InsertAsync(new Theater() { BoxOfficeId = "6015", Name = "Test3", Datetime = DateTime.Now, ErrorDescription = "Timeout" });

            return Ok();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(string id)
        {
            var theaters = _distributedCache.RedisCollection<Theater>();

            var result = theaters.Where(p => p.BoxOfficeId == id).ToList();

            if (result.Count() == 0)
                return NotFound();

            foreach (var item in result)
            {
                theaters.Delete(item);
            }

            return Ok($"{theaters.Count()} records deleted." );
        }
    }
}

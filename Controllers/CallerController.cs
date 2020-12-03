using System;
using System.Net.Http;
using System.Threading.Tasks;
using KubernetesTestClusterIP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KubernetesTestClusterIP.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CallerController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CallerController> _logger;
        private readonly string url;
        private readonly string age;

        public CallerController(ILogger<CallerController> logger, IOptions<KubeTestUrl> joko)
        {
            _logger = logger;
            url = joko.Value.Url;
           
        }

        [HttpGet]
        public async  Task<IActionResult> Get()
        {

            //We will make a GET request to a really cool website...

            string baseUrl = url;
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            using (HttpClient client = new HttpClient())

            //Setting up the response...         

            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    return Ok(new { data });
                }
               

            }

            return Ok();
        }
    }
}

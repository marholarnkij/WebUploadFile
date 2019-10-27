using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Common.Lib.Services;
using Common.Lib.Entities.ReturnModels;
using Common.Lib.Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace WebUploadFile.Controllers
{
    [Produces("application/json")]
    [Route("api/UploadFile")]
    public class UploadFileController : Controller
    {
        private readonly JournalService journalService;
        private IConfiguration Configuration { get; set; }
        private readonly IMapper _mapper;
        private readonly ILogger<UploadFileController> _logger;

        public UploadFileController(IConfiguration configuration, IMapper mapper, ILogger<UploadFileController> logger)
        {
            Configuration = configuration;
            _mapper = mapper;
            _logger = logger;

            journalService = new JournalService(Configuration);
        }

         
        
        
        // POST: api/UploadFile
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        
    }
}

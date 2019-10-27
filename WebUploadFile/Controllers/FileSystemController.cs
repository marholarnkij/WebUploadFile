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
using System.Threading.Tasks;
using Common.Lib.Entities.InputModels;

namespace WebUploadFile.Controllers
{
    [Produces("application/json")]
    [Route("api/FileSystem")]
    public class FileSystemController : Controller
    {
        private readonly JournalService journalService;
        private IConfiguration Configuration { get; set; }
        private readonly IMapper _mapper;
        private readonly ILogger<FileSystemController> _logger;

        public FileSystemController(IConfiguration configuration, IMapper mapper, ILogger<FileSystemController> logger)
        {
            Configuration = configuration;
            _mapper = mapper;
            _logger = logger;

            journalService = new JournalService(Configuration);
        }




        [HttpPost]
        [Route("import")]
        public IActionResult Import([FromBody]List<JournalInput> value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                return Ok();
            }
        }


    }
}

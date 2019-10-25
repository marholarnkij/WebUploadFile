using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Common.Lib.Services;
using Common.Lib.Entities.ReturnModels;
using Common.Lib.Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using AutoMapper;

namespace WebUploadFile.Controllers
{
    [Produces("application/json")]
    [Route("api/UploadFile")]
    public class UploadFileController : Controller
    {
        private readonly JournalService journalService;
        private IConfiguration Configuration { get; set; }
        private readonly IMapper _mapper;
        //private readonly ILogger<UploadFileController> _logger;

        public UploadFileController(IConfiguration configuration, IMapper mapper)//, ILogger<InventoryController> logger)
        {
            Configuration = configuration;
            _mapper = mapper;
            //_logger = logger;

            journalService = new JournalService(Configuration);
        }
        // GET: api/UploadFile
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UploadFile/USD
        [HttpGet("{Currency}", Name = "GetByCurrency")]
        public IActionResult Get(string currency)
        {
            var results = new List<JournalReturnModel>();
            try
            {
                var list = journalService.GetByCurrency(currency);
                foreach (var item in list)
                {
                    var result = new JournalReturnModel();
                    result = _mapper.Map<JournalDetails, JournalReturnModel>(item);
                    results.Add(result);
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            if (ModelState.ErrorCount > 0)
            {
                //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                //foreach (var error in allErrors)
                //{
                //    _logger.LogError(error.ErrorMessage);
                //}
                //return new ValidationFailedResult(ModelState);
            }
            return new ObjectResult(results);
        }
        
        // POST: api/UploadFile
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/UploadFile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

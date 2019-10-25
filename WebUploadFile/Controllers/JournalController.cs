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
    [Route("api/Journal")]
    public class JournalController : Controller
    {
        private readonly JournalService journalService;
        private IConfiguration Configuration { get; set; }
        private readonly IMapper _mapper;
        //private readonly ILogger<UploadFileController> _logger;

        public JournalController(IConfiguration configuration, IMapper mapper)//, ILogger<InventoryController> logger)
        {
            Configuration = configuration;
            _mapper = mapper;
            //_logger = logger;

            journalService = new JournalService(Configuration);
        }
        // GET: api/Journal 
        [HttpGet(Name = "GetAll")]
        public IActionResult Get()
        {
            var results = new List<JournalReturnModel>();
            try
            {
                var list = journalService.GetAll();
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

        // GET: api/Journal/Currency/USD
        [HttpGet("Currency/{currency}", Name = "GetByCurrency")]
        [Route("Currency")]
        public IActionResult GetByCurrency(string currency)
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

        // GET: api/Journal/Status/A
        [HttpGet("Status/{status}", Name = "GetByStatus")]
        [Route("Status")]
        public IActionResult GetByStatus(string status)
        {
            var results = new List<JournalReturnModel>();
            try
            {
                var list = journalService.GetByStatus(status);
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
    }
}

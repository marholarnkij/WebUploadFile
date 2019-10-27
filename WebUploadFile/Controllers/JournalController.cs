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
using WebUploadFile.Filters;
using System;
using Common.Lib.Entities.InputModels;
using System.ComponentModel.DataAnnotations;

namespace WebUploadFile.Controllers
{
    [Produces("application/json")]
    [Route("api/Journal")]
    public class JournalController : Controller
    {
        private readonly JournalService journalService;
        private IConfiguration Configuration { get; set; }
        private readonly IMapper _mapper;
        private readonly ILogger<JournalController> _logger;

        public JournalController(IConfiguration configuration, IMapper mapper, ILogger<JournalController> logger)
        {
            Configuration = configuration;
            _mapper = mapper;
            _logger = logger;

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
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in allErrors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return new ValidationFailedResult(ModelState);
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
                if (results.Count == 0)
                {
                    _logger.LogInformation("Not found currency :" + currency.ToString());
                    return NotFound();
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            if (ModelState.ErrorCount > 0)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in allErrors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return new ValidationFailedResult(ModelState);
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
                string mapstatus = "";
                if (status.ToLower() == "approved" || status.ToLower() == "a")
                {
                    mapstatus = "A";
                }

                if (status.ToLower() == "failed" || status.ToLower() == "rejected" || status.ToLower() == "r")
                {
                    mapstatus = "R";
                }

                if (status.ToLower() == "finished" || status.ToLower() == "done" || status.ToLower() == "d")
                {
                    mapstatus = "D";
                }

                var list = journalService.GetByStatus(mapstatus);
                foreach (var item in list)
                {
                    var result = new JournalReturnModel();
                    result = _mapper.Map<JournalDetails, JournalReturnModel>(item);
                    results.Add(result);
                }
                if (results.Count == 0)
                {
                    _logger.LogInformation("Not found status :" + status.ToString());
                    return NotFound();
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            if (ModelState.ErrorCount > 0)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in allErrors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return new ValidationFailedResult(ModelState);
            }
            return new ObjectResult(results);
        }


        // GET: api/Journal/GetByDateRange/
        [HttpGet("DateRange/{fromdate}/{todate}", Name = "GetByDateRange")]
        [Route("DateRange")]
        public IActionResult GetByDateRange(DateTime fromdate, DateTime todate)
        {
            var results = new List<JournalReturnModel>();
            try
            {
                var list = journalService.GetByDateRange(fromdate, todate);
                foreach (var item in list)
                {
                    var result = new JournalReturnModel();
                    result = _mapper.Map<JournalDetails, JournalReturnModel>(item);
                    results.Add(result);
                }
                if (results.Count == 0)
                {
                    _logger.LogInformation("Not found date range :" + fromdate.ToString() + " and " + todate.ToString());
                    return NotFound();
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            if (ModelState.ErrorCount > 0)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in allErrors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return new ValidationFailedResult(ModelState);
            }
            return new ObjectResult(results);
        }

        [HttpPost]
        [Route("CreateJournal")]
        public IActionResult CreateJournal([FromBody]List<JournalInput> items)
        {
            int counter = 0;
            if (items != null)
            {
                foreach (var item in items)
                {
                    counter++;
                    var t = _mapper.Map<JournalInput, JournalDetails>(item);
                    if (ValidateItem(item))
                    {
                        journalService.Createjournal(t);
                        _logger.LogInformation(string.Format("{0}:{1} - Journal was created.", counter.ToString(), t.TransactionId));
                    }
                }
            }
            if (ModelState.ErrorCount > 0)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in allErrors)
                {
                    _logger.LogError(error.ErrorMessage == "" ? error.Exception.Message : error.ErrorMessage);
                }
                return new ValidationFailedResult(ModelState);
            }
            return StatusCode(200);
        }
    
        private bool ValidateItem(object p)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            System.ComponentModel.DataAnnotations.ValidationContext validateContexts = new System.ComponentModel.DataAnnotations.ValidationContext(p, null, null);
            bool IsValid = Validator.TryValidateObject(p, validateContexts, res, true);
            return IsValid;
        }
    }
}

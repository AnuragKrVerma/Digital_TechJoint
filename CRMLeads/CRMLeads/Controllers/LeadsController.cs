using CRMLeads.Data;
using CRMLeads.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CRMLeads.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ILogger<LeadsController> _logger;
        private readonly LeadsRepository _leadsRepository;

        public LeadsController(ILogger<LeadsController> logger, LeadsRepository leadsRepository)
        {
            _logger = logger;
            _leadsRepository = leadsRepository;
        }

        public IActionResult Index()
        {
            List<LeadsEntity> leads = _leadsRepository.GetAllLeads();
            
            return View(leads);
        }

        public IActionResult EditLead(int Id)
        {
            LeadsEntity leads = _leadsRepository.GetLeadsById(Id);

            return View(leads);
        }

        [HttpPost]
        public IActionResult EditLead(int Id, LeadsEntity leadsEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if (_leadsRepository.EditLead(Id, leadsEntity))
                    {
                        return RedirectToAction("Index");
                    }

                    return View();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding lead");
                return View();
            }

            return View();
        }

        public ActionResult AddLead()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLead(LeadsEntity lead)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if (_leadsRepository.AddLead(lead))
                    {
                        return RedirectToAction("Index");
                    }

                    return View();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding lead");
                return View();
            }

            return View();
        }

        public ActionResult DeleteLead(int Id)
        {
            LeadsEntity leads = _leadsRepository.GetLeadsById(Id);

            return View(leads);
        }

        [HttpPost]
        [ActionName("DeleteLead")]
        public IActionResult DeleteLeadById(int Id)
        {
          
            
            try
            {
                    if (_leadsRepository.DeleteLead(Id))
                    {
                        return RedirectToAction("Index");
                    }

                    return View();
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting lead");
                return View();
            }

            
            return View();
        }

    }
}

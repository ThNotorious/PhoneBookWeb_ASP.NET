using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWeb.Interfaces;
using PhoneBookWeb.Models;

namespace PhoneBookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataContact _dataContact;

        public HomeController(ILogger<HomeController> logger, IDataContact dataContact)
        {
            _logger = logger;
            _dataContact = dataContact;
        }

        [HttpGet]
        public async Task<IActionResult> FilterContacts(string secondName)
        {
            ViewBag.Contacts = await _dataContact.SearchByLastName(secondName);
            return View("SearchContacts", ViewBag.Contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Contacts = await _dataContact.GetAll();

            return View(ViewBag.Contacts);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddContact(string secondName, string firstName, string middleName, string phoneNumber, string description)
        {
            bool result = await _dataContact.Add(secondName, firstName, middleName, phoneNumber, description);

            if (!result)
            {
                return View("Add");
            }
            else
            {
                return Redirect("~/");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> More(int idContact)
        {
            ViewBag.Contact = await _dataContact.GetOneElement(idContact);

            return View(ViewBag.Contact);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Changed(int idContact, string newSecondName, string newFirstName, string newMiddleName, string newPhoneNumber, string newDescription)
        {
            await _dataContact.Changed(idContact, newSecondName, newFirstName, newMiddleName, newPhoneNumber, newDescription);
            return Redirect("~/");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int idContact)
        {
           await _dataContact.Delete(idContact);

            return Redirect("~/");
        }
    }
}
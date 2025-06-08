using Microsoft.AspNetCore.Mvc;

namespace E_commerse_study.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
           
        {
            List<string> name = new List<string>() { "Ali ","Salman","Manran"};
            return View(model: name);
        }
    }
}

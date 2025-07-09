using Microsoft.AspNetCore.Mvc;

namespace E_commerse_study.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Success()
        {
           
            return View();
        }

        public IActionResult Cancel()
        {
            
            return View();
        }
    }
}

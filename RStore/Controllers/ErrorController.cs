using Microsoft.AspNetCore.Mvc;

namespace RStore.Controllers {
    public class ErrorController : Controller {
        public ViewResult Error() => View();
    }
}
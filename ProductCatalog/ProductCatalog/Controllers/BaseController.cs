using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}

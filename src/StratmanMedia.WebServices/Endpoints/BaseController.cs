using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace StratmanMedia.WebServices.Endpoints;

public class BaseController : ControllerBase
{
    public BaseController()
    {
    }

    protected IActionResult InternalServerError()
    {
        return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
    }
}
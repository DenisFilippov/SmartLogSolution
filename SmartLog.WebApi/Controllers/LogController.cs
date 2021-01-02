using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartLog.Domain.Interfaces;

namespace SmartLog.WebApi.Controllers
{
  [Route("log")]
  [ApiController]
  public class LogController : ControllerBase
  {
    public LogController()
    {

    }

    [HttpGet("service-info")]
    public ActionResult ServiceInfo()
    {
      return Ok($"Ping {DateTime.Now.ToLongTimeString()}");
    }

    [HttpGet("debug")]
    public ActionResult Debug(string uid, string methodName, string createDate, string message)
    {
      return Ok();
    }

    [HttpGet("info")]
    public ActionResult Info(string uid, string methodName, string createDate, string message)
    {
      return Ok();
    }

    [HttpGet("warning")]
    public ActionResult Warning(string uid, string methodName, string createDate, string message)
    {
      return Ok();
    }

    [HttpGet("error")]
    public ActionResult Error(string uid, string methodName, string createDate, string message)
    {
      return Ok();
    }

    [HttpGet("critical")]
    public ActionResult Critical(string uid, string methodName, string createDate, string message)
    {
      return Ok();
    }
  }
}

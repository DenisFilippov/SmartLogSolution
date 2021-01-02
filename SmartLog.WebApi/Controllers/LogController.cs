using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;

namespace SmartLog.WebApi.Controllers
{
  [Route("log")]
  [ApiController]
  public class LogController : ControllerBase
  {
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
      _logService = logService ?? throw new ArgumentNullException(nameof(logService));
    }

    [HttpGet("service-info")]
    public IActionResult ServiceInfo()
    {
      var result = _logService.GetServiceInfo();
      return Ok(result);
    }

    [HttpGet("debug")]
    public async Task<IActionResult> Debug(string uid, string methodName, string createDate, string message)
    {
      var result = await _logService.CreateLogAsync(LogTypes.DEBUG, uid, methodName, createDate, message);
      return Ok(result);
    }

    [HttpGet("info")]
    public async Task<IActionResult> Info(string uid, string methodName, string createDate, string message)
    {
      var result = await _logService.CreateLogAsync(LogTypes.INFO, uid, methodName, createDate, message);
      return Ok(result);
    }

    [HttpGet("warning")]
    public async Task<IActionResult> Warning(string uid, string methodName, string createDate, string message)
    {
      var result = await _logService.CreateLogAsync(LogTypes.WARNING, uid, methodName, createDate, message);
      return Ok(result);
    }

    [HttpGet("error")]
    public async Task<IActionResult> Error(string uid, string methodName, string createDate, string message)
    {
      var result = await _logService.CreateLogAsync(LogTypes.ERROR, uid, methodName, createDate, message);
      return Ok(result);
    }

    [HttpGet("critical")]
    public async Task<IActionResult> Critical(string uid, string methodName, string createDate, string message)
    {
      var result = await _logService.CreateLogAsync(LogTypes.CRITICAL, uid, methodName, createDate, message);
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Log(SmartLogRequest request)
    {
      var result = await _logService.CreateLogAsync(request);
      return Ok(result);
    }
  }
}
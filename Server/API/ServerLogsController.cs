﻿using Microsoft.AspNetCore.Mvc;
using Rimot.Server.Auth;
using Rimot.Server.Services;
using System.Text;
using System.Text.Json;

namespace Rimot.Server.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerLogsController : ControllerBase
    {

        public ServerLogsController(IDataService dataService)
        {
            DataService = dataService;
        }
        public IDataService DataService { get; set; }

        [ServiceFilter(typeof(ApiAuthorizationFilter))]
        [HttpGet("Download")]
        public ActionResult Download()
        {
            Request.Headers.TryGetValue("OrganizationID", out var orgID);
            var logs = DataService.GetAllEventLogs(orgID);
            var fileBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(logs));
            return File(fileBytes, "application/octet-stream", "ServerLogs.json");
        }
    }
}

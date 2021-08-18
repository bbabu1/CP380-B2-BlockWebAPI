using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B2_BlockWebAPI.Models;
using CP380_B1_BlockList.Models;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    
    
    
    public class PendingPayloadsController : ControllerBase
    {
     
        private readonly PendingPayloads _pendingPayloads;
        public List<PendingPayloads> pendingPayloadsList { get; set; }

        
        
        
        public PendingPayloadsController(PendingPayloads pendingPayloads)
       
        
        
        
        {
            _pendingPayloads = pendingPayloads;
        }

      
        
        
        [HttpGet]
       
        
        public ActionResult<List<Payload>> Get()
        {
            return _pendingPayloads.payloads.ToList();
        }


        [HttpPost]
       
        
        public void Post(Payload payload)
        {
            _pendingPayloads.payloads.Add(payload);

        }
}
}
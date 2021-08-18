﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;



namespace CP380_B2_BlockWebAPI.Controllers



{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    
    
    public class BlocksController : ControllerBase
    
    
    {
      
        private readonly BlockList _blockList;
        public List<Payload> payloadList { get; set; }

        public BlocksController(BlockList blockList)
        
        
        {
            _blockList = blockList;
        }


        [HttpGet]
       
        public ActionResult<List<BlockSummary>> Get()
       
        
        {
            List<Block> blocks = _blockList.Chain.ToList();

            List<BlockSummary> blockSummaryList = new List<BlockSummary>();
            foreach (var bl in blocks)
            {
                _blockList.AddBlock(bl);
                blockSummaryList.Add(new BlockSummary()
                {
                    Hash = bl.Hash,
                    PreviousHash = bl.PreviousHash,
                    TimeStamp = bl.TimeStamp,
                });
            }
            return blockSummaryList;

        }


        [HttpGet("/{hash}")]
        
        
        public ActionResult<Block> Get(string hash)
        
        
        {
            var bl = _blockList.Chain.Where(tempBl => tempBl.Hash == hash);
            int i = bl.Count();
            if (i > 0)
                return bl.First();
            else
                return NotFound();
        }

       
        
        [HttpGet("/{hash}/Payloads")]
        
        public ActionResult<List<Payload>> GetPayload(string hash)
        
        
        {

            var bl = _blockList.Chain.Where(tempBl => tempBl.Hash == hash);
            int count = bl.Count();
            if (count > 0)
            {
                var fv = bl.Select(vl => vl.Data)
                    .First().ToList();
                return fv;

            }
            else
            {
                return NotFound();
            }
        }

        
        
       


}
}

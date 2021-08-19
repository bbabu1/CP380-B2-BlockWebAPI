using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using CP380_B2_BlockWebAPI.Services;



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
            var i = 0;
            List<BlockSummary> blockSummaryList = new List<BlockSummary>();
            foreach (var bl in blocks)
            {
                _blockList.AddBlock(bl);
                if (i != 0)
                {
                    blockSummaryList.Add(new BlockSummary()
                    {
                        Hash = bl.Hash,
                        PreviousHash = bl.PreviousHash,
                        TimeStamp = bl.TimeStamp,
                    });
                }
                else
                {
                    blockSummaryList.Add(new BlockSummary()
                    {
                        Hash = bl.Hash,
                        PreviousHash = null,
                        TimeStamp = bl.TimeStamp,
                    });
                }
                i++;
            }
            return blockSummaryList;

        }


        [HttpGet("/block/{hash}")]
        
        
        public ActionResult<Block> Get(string hash)
        
        
        {
            var bl = _blockList.Chain.Where(tempBl => tempBl.Hash == hash);
            int i = bl.Count();
            if (i > 0)
                return bl.First();
            else
                return NotFound();
        }



        [HttpGet("/blocklist/{hash}/Payloads")]

        public ActionResult<List<Payload>> GetPayload(string hash)


        {

            var bl = _blockList.Chain.Where(tempBl => tempBl.Hash == hash);
            return bl.Select(tempBl => tempBl.Data).First();
        }
        [HttpPost]
        public void Post(Block block)
        {
            payloadList = new PendingPayloads().payloads;
            var tmpBlock = _blockList.Chain[_blockList.Chain.Count - 1];
            var block1 = new Block(tmpBlock.TimeStamp, tmpBlock.PreviousHash, payloadList);
            if (block1.CalculateHash() == block1.Hash)
            {
                _blockList.Chain.Add(block);

            }

            
            else
            {
                BadRequest();
            }
        }

        
        
       


}
}


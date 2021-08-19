using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.Extensions.Configuration;

namespace CP380_B2_BlockWebAPI.Services
{
    public class BlockListService
    {

        private readonly IConfiguration _config;

        public BlockList blockList { get; set; }
        public PendingPayloads pendingPayloads { get; set; }

        public List<Payload> payloadList { get; set; }

        public BlockListService(IConfiguration configuration, BlockList blockList, PendingPayloads pendingPayloads)
        {
            _config = configuration;
            this.blockList = blockList;
            this.pendingPayloads = pendingPayloads;
        }

        public Block SubmitNewBlock(string hash, int nonce, DateTime timestamp)
        {
            payloadList = pendingPayloads.payloads;
            var tmpBlockList = blockList.Chain[blockList.Chain.Count - 1];
            var blk = new Block(tmpBlockList.TimeStamp, tmpBlockList.PreviousHash, payloadList);
            if(blk.CalculateHash() == hash)
            {
                blockList.Chain.Add(blk);
                payloadList.Clear();
                return blk;
            }
            else
            {
                return null;
            }
        }
    }
}

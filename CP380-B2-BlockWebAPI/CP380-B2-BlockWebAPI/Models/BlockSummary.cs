using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;

namespace CP380_B2_BlockWebAPI.Models
{
    public class BlockSummary
    {
       
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }

       
        
        
        public BlockSummary()
       
        
        
        {
           
            List<Payload> data12 = new()
            {
                new Payload("user", TransactionTypes.GRANT, 10, null),
                new Payload("user", TransactionTypes.BUY, 10, "10C"),
            };
          
            
            var block = new Block(DateTime.Now, "", data12);
            block.Mine(2);

            TimeStamp = block.TimeStamp;
            PreviousHash = block.PreviousHash;
            Hash = block.Hash;
        }
}

}
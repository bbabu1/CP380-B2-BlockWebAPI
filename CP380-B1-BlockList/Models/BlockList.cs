using System;
using System.Collections.Generic;

namespace CP380_B1_BlockList.Models
{
    public class BlockList
    {
        public IList<Block> Chain { get; set; }

        public int Difficulty { get; set; } = 2;

        public BlockList()
        {
            Chain = new List<Block>();
            MakeFirstBlock();
        }

        public void MakeFirstBlock()
        {
            var block = new Block(DateTime.Now, null, new List<Payload>());
            block.Mine(Difficulty);
            Chain.Add(block);
        }

       
        
        
public void AddBlock(Block block)
        



{
            block.PreviousHash = Chain[Chain.Count-1].Hash;
            block.Mine(Difficulty);
            Chain.Add(block);

            
}

        
        
public bool IsValid()
        
         
           {
            int flag = 0;
            string hashValidation = new String('C', Difficulty);

            for (int i = 1; i < Chain.Count; i++)    //each block except the first block
           
               {
                string hashedString=Chain[i].Hash;
                if (Chain[i].PreviousHash!=Chain[i-1].Hash)
                    {
                    flag++;
                    }
               
                if(hashedString.Substring(0, Difficulty)!=hashValidation)
                    {
                    flag++;
                    }
              }
                 if(flag==0)
              {
                     return true;
              }
          
                    return false;
              }
}
}

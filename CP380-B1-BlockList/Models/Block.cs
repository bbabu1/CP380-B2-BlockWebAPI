using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace CP380_B1_BlockList.Models
{
    public class Block
    {
        public int Nonce { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Payload> Data { get; set; }

        public Block(DateTime timeStamp, string previousHash, List<Payload> data)
        {
            Nonce = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        
        
public string CalculateHash()
        {
            var sha256 = SHA256.Create();
            var json = JsonSerializer.Serialize(Data);
            var inputString = $"{TimeStamp.Date:yyyy-MM-dd hh:mm:ss tt}-{PreviousHash}-{Nonce}-{json}"; 

            var inputBytes = Encoding.ASCII.GetBytes(inputString);
            var outputBytes = sha256.ComputeHash(inputBytes);

            return Base64UrlEncoder.Encode(outputBytes);
        }

       
        
public void Mine(int difficulty)
      

       {
            string hashValidation = new String('C', difficulty); 
            string hashedString = CalculateHash();  
            while(hashedString.Substring(0, difficulty) != hashValidation) 
        {
                Nonce++;                     
                hashedString = CalculateHash();             
        }                                             
           
                 Hash = hashedString;
                    
}
}
}

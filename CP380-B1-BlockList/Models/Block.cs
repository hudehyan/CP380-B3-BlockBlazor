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
        public int difval()
        {
            var diff = 0;

            while (Hash[diff] == 'C')
            {
                diff++;
            }

            return diff;
        }



        //
        // JSON serialisation:
        //   https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
        //
        public string CalculateHash()
        {
            var sha256 = SHA256.Create();
            var data = JsonSerializer.Serialize(Data);

            var inputString = $"{TimeStamp}-{PreviousHash}-{Nonce}-{data}"; 

            var inputBytes = Encoding.ASCII.GetBytes(inputString);
            var outputBytes = sha256.ComputeHash(inputBytes);

            return Base64UrlEncoder.Encode(outputBytes);
        }

        public void Mine(int d)
        {
            int currentDifficulty = difval();

            if (currentDifficulty == d)
            {
                return;
            }

            Nonce++;
            Hash = CalculateHash();
            Mine(d);
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


namespace Tweet4PemkotBDG2.Models
{
    public class LocationExtractor
    {
        

        public String extractLocation(String text)
        {
            Regex pattern = new Regex(@"(di|jl|jln|simp.|jl.|jln.)\s\w*\s\w*");
            Match result = pattern.Match(text);
            if (result.Success)
            {
                return result.Value;

            }

            return "na";


        }


    }
}
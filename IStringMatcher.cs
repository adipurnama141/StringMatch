using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweet4PemkotBDG2.Models
{
    public interface IStringMatcher
    {
    
        int search(string text, string pattern);
        bool b_search(string text, string pattern);
        
     
    }
}
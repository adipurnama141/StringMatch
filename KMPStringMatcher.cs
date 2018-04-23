using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweet4PemkotBDG2.Models
{
    class KMPStringMatcher : IStringMatcher
    {
        private static int[] fail; //array fail

        //membuat array fail untuk digunakan di KMP.search
        private void compute_fail(string pattern)
        {
            fail = new int[pattern.Length];
            fail[0] = 0;
            int m = pattern.Length;
            int j = 0;
            int i = 1;
            while (i < m)
            {
                if (pattern[j] == pattern[i])
                {
                    fail[i] = j + 1;
                    i++; j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    fail[i] = 0;
                    i++;
                }
            }
        }

        //lakukan search berdasarkan KMP
        public int search_kata(string text, string pattern)
        {
            //ubah text ke huruf kecil terlebih dahulu agar case-insensitive
            text = text.ToLower();
            pattern = pattern.ToLower();
            //hitung fail
            compute_fail(pattern);
            int n = text.Length;
            int m = pattern.Length;
            int i = 0;
            int j = 0;
            if (m > n)
            {
                //panjang pattern > panjang text
                return -1;
            }
            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    if (j == m - 1)
                    {
                        return (i - m + 1);
                    }
                    i++; j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        public bool b_search_kata(string text, string pattern)
        {
            if (search_kata(text, pattern) == -1)
                return false;
            return true;
        }

        //search untuk mencari kemunculan pattern
        //mungkin berupa frasa
        public int search(string text, string pattern)
        {
            string[] delim = { " " };
            string[] pattern_split = pattern.Split(delim, StringSplitOptions.None);
            int pattern_length = pattern_split.Length;
            if (pattern_length == 1)
            {
                //kasus 1: pattern 1 kata, langsung kembalikan hasil search kata
                return search_kata(text, pattern);
            }
            else
            {
                //kasus 2: pattern merupakan frasa, kembalikan hanya jika semua kata
                //ada dalam frasa, kembalikan nilai terkecil kata yang ditemukan
                int idx_min = -1;
                int[] result = new int[pattern_length];
                //hitung dan simpan hasil pencarian setiap kata
                for (int i = 0; i < pattern_length; i++)
                {
                    result[i] = search_kata(text, pattern_split[i]);
                }
                //cek apakah semua kata pada frasa ada
                bool found_all = true;
                for (int i = 0; i < pattern_length; i++)
                {
                    if (result[i] == -1)
                    {
                        found_all = false;
                        break;
                    }
                }
                //jika tidak lengkap, langsung return
                if (!found_all)
                    return -1;
                //cari indeks minimum untuk dikembalikan
                idx_min = result[0];
                for (int i = 1; i < pattern_length; i++)
                {
                    if (idx_min > result[i])
                        idx_min = result[i];
                }
                return idx_min;
            }
        }

        public bool b_search(string text, string pattern)
        {
            if (search(text, pattern) == -1)
            {
                return false;
            }
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweet4PemkotBDG2.Models
{
    class BMStringMatcher : IStringMatcher
    {
        private static int[] last;

        //array last, last[i] berisi kemunculan terakhir char i

        //cari last untuk masing-masing karakter
        private void build_last(string pattern)
        {
            last = new int[128];

            for (int i = 0; i < 128; i++)
            {
                last[i] = -1;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                last[pattern[i]] = i;
            }
        }

        //search untuk pattern satu kata
        public int search_kata(string text, string pattern)
        {
            text = text.ToLower();
            pattern = pattern.ToLower();
            build_last(pattern);
            int n = text.Length;
            int m = pattern.Length;
            int i = m - 1;

            if (i > n - 1)
                return -1; //pattern lebih panjang dari text
            int j = m - 1;
            do
            {
                if (pattern[j] == text[i])
                {
                    if (j == 0)
                        return i;
                    else
                    {
                        i--; j--;
                    }
                }
                else
                {
                    if (text[i] < 128)
                    {
                        //kasus 1: karakter normal
                        int last_occur = last[text[i]];
                        i = i + m - Math.Min(j, last_occur + 1);
                        j = m - 1;
                    }
                    else
                    {
                        //kasus 2: karakter tidak normal (extended ASCII), diabaikan
                        i += m - 1;
                        j = m - 1;
                    }
                }
            } while (i <= n - 1);
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
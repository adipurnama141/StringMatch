using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using Tweet4PemkotBDG2.Models;
using Tweetinvi;


namespace Tweet4PemkotBDG2.Controllers
{
    public class HomeController : Controller
    {
       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string txtTwitterName,string key1, string key2,string key3,string key4,
            string key5, string searchMethod)
        {
            ViewData.Clear();

            //Masuk ke twitter API
            var token = "61724143-ZCI8HoClO2gj2FmWISvONBPQOLLKFxNIcJxwPMbvl";
            var tokenSecret = "rxoni8jRDNbj4SX9EJSx6ZzrIUFDPf1o8OqZq3U3P3THF";

            var consumerSecret = "AZUV6yoFt9XkBmXGuDbdShzKhcUBWvCDE3YrhUGJB1cOwrozEd";
            var consumerKey = "ygR3omnRosi5p9Mm1nfPPJfEs";


            /*
            TweetinviConfig.CurrentThreadSettings.ProxyURL = "http://adipurnama:romegostailor141!@cache.itb.ac.id:8080";
            Auth.SetUserCredentials(consumerKey, consumerSecret, token , tokenSecret);
            var cred = new Tweetinvi.Core.Authentication.TwitterCredentials(consumerKey, consumerSecret, token, tokenSecret);
            Auth.SetCredentials(cred);
            var tweets2 = Search.SearchTweets(txtTwitterName);
            */
         



            var service = new TwitterService(consumerKey, consumerSecret);
            service.AuthenticateWith(token, tokenSecret);

            //Minta Query Search Tweet
           ViewBag.searchQuery = txtTwitterName;
           ViewBag.Key1 = key1;
           ViewBag.Key2 = key2;
           ViewBag.Key3 = key3;
           ViewBag.Key4 = key4;
           ViewBag.Key5 = key5;

           Dictionary<string, int> dic = new Dictionary<string, int>();

            txtTwitterName = txtTwitterName.Replace(",", " OR ");
            var options = new SearchOptions { Q = txtTwitterName , Count=100 };
           
            
            var tweets = service.Search(options);
            ViewBag.Tweets = tweets;

            IStringMatcher StringMatcher;

            if (searchMethod == "KMP")
            {
                StringMatcher = new KMPStringMatcher();
            }
            else
            {
                StringMatcher = new BMStringMatcher();
            }


            var keyword1 = key1.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
            var keyword2 = key2.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var keyword3 = key3.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var keyword4 = key4.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var keyword5 = key5.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int[] tweetCategorize = new int[100];
            string[] locationExtracted = new string[100];
            int[] numberOfCategorizedTweet = new int[6];

            int i = 0;
            foreach (var tweet in tweets.Statuses)
            {
                string[] source = tweet.Text.Split(' ');
             
                foreach (string s in source)
                {
                    if (dic.Keys.Contains(s))
                        dic[s] = dic[s]++;
                    else
                        dic.Add(s, 1);
                }

                LocationExtractor locationextractor = new LocationExtractor();
                locationExtracted[i] = locationextractor.extractLocation(tweet.Text);

                foreach (var kword in keyword1)
                {
                    if ( (tweetCategorize[i] == 0) && StringMatcher.b_search(tweet.Text, kword.Trim()) ) {
                        tweetCategorize[i] = 1;
                        numberOfCategorizedTweet[1]++;
                    }
                }
                foreach (var kword in keyword2)
                {
                    if ((tweetCategorize[i] == 0) && StringMatcher.b_search(tweet.Text, kword.Trim()))
                    {
                        tweetCategorize[i] = 2;
                        numberOfCategorizedTweet[2]++;
                    }
                }
                foreach (var kword in keyword3)
                {
                    if ((tweetCategorize[i] == 0) && StringMatcher.b_search(tweet.Text, kword.Trim()))
                    {
                        tweetCategorize[i] = 3;
                        numberOfCategorizedTweet[3]++;
                    }
                }
                foreach (var kword in keyword4)
                {
                    if ((tweetCategorize[i] == 0) && StringMatcher.b_search(tweet.Text, kword.Trim()))
                    {
                        tweetCategorize[i] = 4;
                        numberOfCategorizedTweet[4]++;
                    }
                }
                foreach (var kword in keyword5)
                {
                    if ((tweetCategorize[i] == 0) && StringMatcher.b_search(tweet.Text, kword.Trim()))
                    {
                        tweetCategorize[i] = 5;
                        numberOfCategorizedTweet[5]++;
                    }
                }
                i++;
            }

            numberOfCategorizedTweet[0] = 100;
            for (int j = 1; j <= 5; j++)
            {
                numberOfCategorizedTweet[0] -= numberOfCategorizedTweet[j];
            }

            ViewBag.TweetCategorize = tweetCategorize;
            ViewBag.NumberOfCategorizedTweet = numberOfCategorizedTweet;
            ViewBag.Location = locationExtracted;
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.TwitterPoster
{
    // Class that contains all constants used in the Twitter Service Module
    public static class Constants
    {
        public const string _TwitterAPIURI = "https://api.twitter.com/1.1/statuses/update.json";
        public const string _MyLearnConsumerKey = "6mjsdenmJfW30AwA30JCFwG4y";
        public const string _MyLearnConsuerSecret = "ecvASOVKNuwhVKTEI8j5skzH6bf3ZMgRBiPVuFvQ56DgNxq4XT";
        public const string _MyLearnAccessToken = "779106829108379648-i0Q7zca2NDDYWpTuLjosoSKtQO65oNP";
        public const string _MyLearnAccessSecret = "FCcl9Qg0QqUkwLCAHIz4Ufli5VdolfvIRZlxRIWAEZIkQ";
        // Request Builder's Constants.
        public const string VERSION = "1.0";
        public const string SIGNATURE_METHOD = "HMAC-SHA1";
    }
}
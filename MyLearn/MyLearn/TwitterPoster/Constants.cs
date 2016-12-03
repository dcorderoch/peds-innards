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
        public const string _MyLearnConsumerKey = "5hyyWgcMxckpmSn3f9AGWX3QT";
        public const string _MyLearnConsuerSecret = "YRSCNxMc6qmJWcnh7g7SUJcm4pkYncF65NL0Q6dchhx5hIZTov";
        public const string _MyLearnAccessToken = " 805155331621920768-VF47JGNn98917iObUvFPPmKgU6flr55";
        public const string _MyLearnAccessSecret = "kvQlB8bMEyyLWVWV4qPKUF6pA0JxoxDNATassuoG5uO9i";
        // Request Builder's Constants.
        public const string VERSION = "1.0";
        public const string SIGNATURE_METHOD = "HMAC-SHA1";
    }
}
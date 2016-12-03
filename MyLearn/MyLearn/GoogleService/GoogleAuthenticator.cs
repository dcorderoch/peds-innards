namespace MyLearn.GoogleService
{
    // Class that obtains authentication for Google user accounts
    public class GoogleAuthenticator
    {
        private AuthResponse _Authenticator;

        public GoogleAuthenticator() { }

        public string GetRefreshToken(string codeFromUser, string clientId, string clientSecret, string redirectURI) {
            this._Authenticator = AuthResponse.Exchange(codeFromUser, clientId, clientSecret, redirectURI);
            if(this._Authenticator == null) {
                return "FAIL";
            } else {
                return this._Authenticator.refresh_token;
            }
        }
    }
}
namespace MyLearn.GoogleService
{
    // Class that obtains authentication for Google user accounts
    public class GoogleAuthenticator
    {
        private AuthResponse _Authenticator;

        public GoogleAuthenticator() { }

        public string GetRefreshToken(string codeFromUser) {
            this._Authenticator = AuthResponse.Exchange(codeFromUser, Constants._MyLearnClientId, Constants._MyLearnClientSecret, Constants._MyLearnRedirectURI);
            if(this._Authenticator == null) {
                return "FAIL";
            } else {
                return this._Authenticator.refresh_token;
            }
        }
    }
}
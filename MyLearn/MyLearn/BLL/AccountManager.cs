
using System;
using System.Collections.Generic;


namespace MyLearn.BLL
{
    /// <summary>
    /// Class whose intention is to validate a user's log in.
    /// </summary>
    public class AccountManager
    {
        public int GetUserTypeCode(string username, string password)
        {
            //if user does not exist return -1.
            int userTypeCode =0;





            return userTypeCode;
        }
        /// <summary>
        /// Method that returns to the REST API the corresponding user's information if and only if the parameters given match a user in the DB.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>User information.</returns>
        public List<string> AuthorizeLogin(int username, string password)
        {
            List<string> retVal = new List<string>();
       //     DBUser dbUserInstance = new DBUser();
            try
            {
             
            }
            catch (Exception)
            {
                
            }
            return retVal;
        }
    }
}

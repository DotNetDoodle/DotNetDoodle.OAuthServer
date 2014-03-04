using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetDoodle.OAuthServer.Infrastructure.Config
{
    public class UnifiedConfigurationManager : IConfigurationManager
    {
        private const int DefaultAccessTokenTimeoutInSeconds = 900; // 15 Minutes

        private readonly int _accessTokenTimeoutInSeconds;

        protected const string AccessTokenTimeoutInSecondsConfigKey = "OAuthServer:AccessTokenTimeoutInSeconds";

        public UnifiedConfigurationManager()
        {
            Nullable<int> accessTokenTimeoutInSeconds = GetValue<int>(AccessTokenTimeoutInSecondsConfigKey);
            _accessTokenTimeoutInSeconds = (accessTokenTimeoutInSeconds != null) ? accessTokenTimeoutInSeconds.Value : DefaultAccessTokenTimeoutInSeconds;
        }

        public int AccessTokenTimeoutInSeconds
        {
            get { return _accessTokenTimeoutInSeconds; }
        }

        // Privates

        private Nullable<TValue> GetValue<TValue>(string key) where TValue : struct, IConvertible
        {
            Nullable<TValue> returnValue = null;
            string valueAsString = CloudConfigurationManager.GetSetting(key);
            if (string.IsNullOrEmpty(valueAsString) == false)
            {
                returnValue = (TValue)Convert.ChangeType(valueAsString, typeof(TValue));
            }

            return returnValue;
        }
    }
}
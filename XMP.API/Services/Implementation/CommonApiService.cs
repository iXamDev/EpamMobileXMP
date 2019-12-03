using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using XMP.API.Services.Abstract;
namespace XMP.API.Services.Implementation
{
    public abstract class CommonApiService
    {
        public CommonApiService(IWebConnectionService webConnectionService, IApiSettingsService apiSettingService)
        {
            WebConnectionService = webConnectionService;

            ApiSettingsService = apiSettingService;
        }

        protected IApiSettingsService ApiSettingsService { get; }

        protected IWebConnectionService WebConnectionService { get; }

        public StringContent ToStringContent(object data)
        {
            return new StringContent(
                JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }) ?? string.Empty,
                Encoding.UTF8,
                "application/json"
            );
        }

    }
}

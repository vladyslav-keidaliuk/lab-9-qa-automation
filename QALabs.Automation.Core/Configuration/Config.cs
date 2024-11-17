using Microsoft.Extensions.Configuration;

namespace QALabs.Automation.Core.Configuration
{
    public static class Config
    {
        public static ConfigModel Model { get; }

        static Config()
        {
            Model = new ConfigModel();
            new ConfigurationBuilder()
                .AddJsonFile(@$"./Configuration/config.json")
                .Build()
                .Bind(Model);
        }
    }

}

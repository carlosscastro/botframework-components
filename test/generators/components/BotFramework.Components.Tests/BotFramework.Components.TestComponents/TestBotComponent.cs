using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFramework.Components.TestComponents
{
    public class TestBotComponent : BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DeclarativeType>(new DeclarativeType<EchoActivity>(EchoActivity.Kind));
            services.AddSingleton<IBotFrameworkHttpAdapter, SimpleHttpAdapter>();
        }
    }
}

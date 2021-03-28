using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotFramework.Components.TestComponents
{
    internal class EchoActivity : Dialog
    {
        public const string Kind = "Test.EchoActivity";

        public async override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            var incomingActivity = dc.Context.Activity;

            if (incomingActivity.Type != ActivityTypes.Message)
            {
                return await dc.EndDialogAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            var responseActivity = Activity.CreateMessageActivity();
            responseActivity.Text = $"EchoActivity: {dc.Context.Activity.Text}";

            var sendResult = await dc.Context.SendActivityAsync(responseActivity, cancellationToken).ConfigureAwait(false);
            return await dc.EndDialogAsync(sendResult, cancellationToken).ConfigureAwait(false);
        }
    }
}

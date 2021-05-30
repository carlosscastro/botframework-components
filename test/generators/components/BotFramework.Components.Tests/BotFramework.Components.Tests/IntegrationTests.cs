using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BotFramework.Components.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public async Task TestCustomAction_MessagesEndpoint()
        {
            await TestCustomAction("http://localhost:3978/api/messages");
        }

        [Fact]
        public async Task TestCustomAction_CustomAdapterEndpoint()
        {
            await TestCustomAction("http://localhost:3978/api/adaptertest");
        }

        private async Task TestCustomAction(string endpoint)
        {
            var conversationUpdate = Activity.CreateMessageActivity();
            conversationUpdate.ServiceUrl = "http://localhost:3979";
            conversationUpdate.ChannelId = "emulator";
            conversationUpdate.Conversation = new ConversationAccount(id: Guid.NewGuid().ToString());
            conversationUpdate.From = new ChannelAccount(id: "Ash");
            conversationUpdate.Text = "hello";

            (conversationUpdate as Activity).DeliveryMode = DeliveryModes.ExpectReplies;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(conversationUpdate)));

                Assert.True(response.IsSuccessStatusCode);
                var body = await response.Content.ReadAsStringAsync();
                var invokeResponse = JsonConvert.DeserializeObject<ExpectedReplies>(body);
                Assert.Equal("EchoActivity: hello", invokeResponse.Activities.First(a => a.Type == ActivityTypes.Message).Text);
            }
        }
    }
}

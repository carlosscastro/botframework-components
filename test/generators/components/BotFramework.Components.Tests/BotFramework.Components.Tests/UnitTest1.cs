using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BotFramework.Components.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            //var conversationUpdate = Activity.CreateConversationUpdateActivity();
            var conversationUpdate = Activity.CreateMessageActivity();
            conversationUpdate.ServiceUrl = "http://localhost:3979";
            conversationUpdate.ChannelId = "emulator";
            conversationUpdate.Conversation = new ConversationAccount(id: Guid.NewGuid().ToString());
            conversationUpdate.From = new ChannelAccount(id: "Ash");
            conversationUpdate.Text = "hello";
            //conversationUpdate.MembersAdded = new ChannelAccount[]
            //{
            //    new ChannelAccount(Guid.NewGuid().ToString(), "Bender")
            //};

            (conversationUpdate as Activity).DeliveryMode = DeliveryModes.ExpectReplies;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("http://localhost:3978/api/messages", new StringContent(JsonConvert.SerializeObject(conversationUpdate)));

                Assert.True(response.IsSuccessStatusCode);
                var body = await response.Content.ReadAsStringAsync();
                var invokeResponse = JsonConvert.DeserializeObject<InvokeResponse<ExpectedReplies>>(body);
                Assert.Equal("EchoActivity: hello", invokeResponse.Body.Activities.First(a => a.Type == ActivityTypes.Message).Text);
            }
        }
    }
}

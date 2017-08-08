using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;

namespace MicrosoftBotApp.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly ILifetimeScope _scope;

        public MessagesController(ILifetimeScope scope)
        {
            SetField.NotNull(out _scope, nameof(scope), scope);
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity, CancellationToken token)
        {
            if (activity != null)
            {
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        using (var scope = DialogModule.BeginLifetimeScope(_scope, activity))
                        {
                            var postToBot = scope.Resolve<IPostToBot>();
                            await postToBot.PostAsync(activity, token);
                        }

                        break;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
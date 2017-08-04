using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftBotApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace MicrosoftBotApp.Tests
{
    [TestClass]
    public class MessagesControllerTests
    {
        [TestMethod]
        public void PostTest()
        {
            var controller = new MessagesController
            {
                Request = new HttpRequestMessage()
            };

            Activity activity = new Activity();

            var result = controller.Post(activity).Result;

            Assert.Fail("Sample assert.");
        }
    }
}
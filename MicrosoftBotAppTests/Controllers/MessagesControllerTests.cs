using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftBotApp.Dialogs;
using MicrosoftBotAppTests.Base;

namespace MicrosoftBotAppTests.Controllers
{
    [TestClass]
    public class MessagesControllerTests : DialogTestBase
    {
        [TestMethod]
        public async Task MessagesCountTest()
        {
            //Arrange
            var toBot = MakeTestMessage();
            toBot.From.Id = Guid.NewGuid().ToString();
            toBot.Text = "Hello, world!";

            var rootDialog = new RootDialog();

            IDialog<object> Root() => rootDialog;

            using (new FiberTestBase.ResolveMoqAssembly(rootDialog))
            using (var container = Build(Options.MockConnectorFactory | Options.ScopedQueue, rootDialog))
            {
                //Act
                var toUser = await GetResponse(container, Root, toBot);

                //Assert
                Assert.IsNotNull(toUser);
                Assert.IsTrue(toUser.Text.Contains(toBot.Text.Length.ToString()));
            }
        }
    }
}
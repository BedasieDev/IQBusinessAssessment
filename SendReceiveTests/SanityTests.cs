using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Receive.ReceiveCommands;
using Send.SendCommands;
using SendReceiveLib.Interfaces;
using SendReceiveLib.Models;
using System.Text;

namespace SendReceiveTests
{
    [TestClass]
    public class SanityTests
    {
        public string TestMessage = "Test Message";
        public string TestFormattedMessage = "Test {0} Format";
        public string TestPromptMessage = "Test Prompt";
        public string TestInputMessage = "Test Input Message";
        IDisplay fakeDisplay;
        IPublisherConfig fakeConfig;

        [TestInitialize]
        public void TestInitialize()
        {
            fakeDisplay = A.Fake<IDisplay>();
            fakeConfig = A.Fake<IPublisherConfig>();
            A.CallTo(() => fakeConfig.FormattedMessageToSend).Returns(TestFormattedMessage);
            A.CallTo(() => fakeConfig.PromptMessage).Returns(TestPromptMessage);
            A.CallTo(() => fakeDisplay.PromptMessage(TestPromptMessage)).Returns(TestMessage);
        }

        [TestMethod]
        public void TestSendCommand()
        {
            var expectedResponse = string.Format(TestFormattedMessage, TestMessage);

            var response = new IQBAssessmentSendCommand<IQBAssessmentMessage>()
            {
                OnComplete = response => response
            }.Invoke(fakeConfig, fakeDisplay);

            Assert.AreEqual(response.InputMessage, TestMessage);
            Assert.AreEqual(response.MessageToSend, expectedResponse);
        }

        [TestMethod]
        public void TestReceiveCommand()
        {
            var messageReceived = Encoding.Default.GetBytes(JsonConvert.SerializeObject(new IQBAssessmentMessage() { MessageToSend = TestMessage, InputMessage = TestInputMessage }));

            Assert.IsTrue(new IQBAssessmentReceiveCommand<bool>
            {
                OnComplete = falseResponse => falseResponse
            }.Invoke(messageReceived, fakeDisplay));

            messageReceived = Encoding.Default.GetBytes(JsonConvert.SerializeObject(new IQBAssessmentMessage() { MessageToSend = string.Empty, InputMessage = string.Empty}));

            Assert.IsFalse(new IQBAssessmentReceiveCommand<bool>
            {
                OnComplete = falseResponse => falseResponse
            }.Invoke(messageReceived, fakeDisplay));

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Alword.Telegram.Logic.Controllers
{
	class HelloController
	{
		private readonly ITelegramBotClient _botClient;
		public HelloController(ITelegramBotClient botClient)
		{
			_botClient = botClient;
		}
		public async void Handle(object sender, MessageEventArgs e)
		{
			if (e.Message.Text != null)
			{
				Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

				await _botClient.SendTextMessageAsync(
				  chatId: e.Message.Chat,
				  text: "You said:\n" + e.Message.Text
				);
			}
		}
	}
}

using Alword.Telegram.Logic.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Alword.Telegram.Logic
{
	public static class BotRegistrator
	{
		public static IServiceCollection AddTelegramBotApi(this IServiceCollection services)
		{
			// for each controller in controllers
			services.AddSingleton<HelloController>();

			services.AddSingleton<ITelegramBotClient>((app) =>
			{
				var configuration = app.GetService<IConfiguration>();
				var section = nameof(TelegramBotClient);
				var key = Environment.GetEnvironmentVariable(section);
				if (string.IsNullOrWhiteSpace(key))
				{
					key = configuration.GetSection(nameof(TelegramBotClient)).Value;
					if (string.IsNullOrWhiteSpace(key)) 
						throw new ArgumentNullException(Strings.ApiKeyNotFoundException);
				}

				var botClient = new TelegramBotClient(key);
				return botClient;
			});
			return services;
		}

		public static IApplicationBuilder UseTelegramBotApi(this IApplicationBuilder app)
		{
			var botClient = app.ApplicationServices.GetService<ITelegramBotClient>();
			// for each controller in controllers
			var hello = app.ApplicationServices.GetService<HelloController>();
			botClient.OnMessage += hello.Handle;

			botClient.StartReceiving();
			return app;
		}
	}
}

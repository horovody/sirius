using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace Sirius.Sandbox
{
    class Program
    {
        private const int ApiId = 0;
        private const string ApiHash = "";

        private const string DevUrl = "";

        static void Main(string[] args)
        {
            var ct = new CancellationTokenSource();
            MainAsync(ct.Token).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(CancellationToken cancellationToken)
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(ApiId, ApiHash, store);

            if (await client.ConnectAsync() && await AuthorizeIfNot(client))
            {
                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
                var channels = dialogs.Chats?.Where(x => x is TLChannel).ToList();
                if (channels != null && channels.Any())
                {
                    var channel = channels.First() as TLChannel;
                    var peer = new TLInputPeerChannel() {ChannelId = channel.Id, AccessHash = channel.AccessHash.Value};
                    var msg = await client.GetHistoryAsync(peer, 0, 0, 20);
                }
            }
        }

        private static async Task<bool> AuthorizeIfNot(TelegramClient client)
        {
            if (!client.IsUserAuthorized())
            {
                Console.WriteLine("Введите номер телефона");
                var phone = Console.ReadLine();
                var hash = await client.SendCodeRequestAsync(phone);
                Console.WriteLine("Введите Код");
                var code = Console.ReadLine();
                var user = await client.MakeAuthAsync(phone, hash, code);
                return true;
            }
            return true;
        }
    }
}

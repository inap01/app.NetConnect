using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot
{
    public class Log
    {
        public string id { get; set; }
        public string title { get; set; }
        public string owner { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string zone { get; set; }
    }


    public class Bot
    {
        DiscordClient client;
        Channel channel;

        public Bot()
        {
            client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = log;
            });

            client.ServerAvailable += async (s, e) =>
            {
                channel = e.Server.FindChannels("logs", ChannelType.Text).FirstOrDefault();

                while (true)
                {
                    List<Log> logs = getNewLogs();

                    for (int i = 0; i < logs.Count; i++)
                    {
                        string msg = logs[i].title;
                        msg += "\t https://www.warcraftlogs.com/reports/";
                        msg += logs[i].id;
                        await channel.SendMessage(msg);
                        System.Threading.Thread.Sleep(1000);
                    }

                    System.Threading.Thread.Sleep(1000 * 15);
                }
            };

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MzA3NjY3MwDAyMDExNTQ5dwaNzAy.C-Vo4Q.djWCD1xfgvRqhIk-jNv4GoJnXuzL6Q", TokenType.Bot);
            });
        }

        private string buildRequestURL()
        {
            string url = "https://www.warcraftlogs.com:443/v1/reports/user/chrises5?start=";
            url += System.IO.File.ReadAllText(@"lastDate.txt");
            url += "&api_key=b271d78553408bc8e6c89eq8aa51a6f8755";
            return url;
        }

        private List<Log> getNewLogs()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(buildRequestURL());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = "";
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                Console.WriteLine(data);
                response.Close();
                readStream.Close();
            }

            List<Log> logs = js.Deserialize<List<Log>>(data);

            if (logs.Count > 0)
            {
                long end = Int64.Parse(logs[logs.Count - 1].end) + 1;
                System.IO.File.WriteAllText(@"lastDate.txt", end.ToString());
            }

            return logs;
        }

        private void log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
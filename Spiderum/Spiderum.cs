using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spiderum
{
    public partial class Spiderum : Form
    {
        string status = string.Empty;

        List<string> users = new List<string>();
        List<string> emails = new List<string>();

        private delegate void DelegateUpdate();

        bool isRunning = false;

        string header = "_id \t name \t email \t created_at \t post_score \t salt \t hashed_password \n";

        public Spiderum()
        {
            InitializeComponent();
        }

        private void Spiderum_Load(object sender, EventArgs e)
        {
            if (File.Exists("emails.txt"))
            {
                emails = File.ReadAllLines("emails.txt").ToList();
            }
            if (File.Exists("users.txt"))
            {
                users = File.ReadAllLines("users.txt").ToList();
            }
        }

        private void UpdateStatus(string status)
        {
            this.status = status;
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (labelStatus.InvokeRequired)
            {
                DelegateUpdate d = new DelegateUpdate(UpdateStatus);
                this.Invoke(d);
            }
            else
            {
                labelStatus.Text = status;
            }
        }

        private void UpdateStatus2(string status)
        {
            this.status = status;
            UpdateStatus2();
        }
        public void UpdateStatus2()
        {
            if (buttonCrawlUser.InvokeRequired)
            {
                DelegateUpdate d = new DelegateUpdate(UpdateStatus2);
                this.Invoke(d);
            }
            else
            {
                buttonCrawlUser.Text = status;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            isRunning = false;
        }

        private void Crawl(string name)
        {
            for (int page = 1; page < 100; page++)
            {
                try
                {
                    UpdateStatus(name + " : " + page);

                    string text = new WebClient().DownloadString("https://spiderum.com/api/v2/user/getUserComments?profile_name=" + name + "&page=" + page);

                    JObject obj = JObject.Parse(text);

                    if (obj["userComments"].Count() == 0)
                    {
                        break;
                    }

                    string data = "";

                    foreach (var item in obj["userComments"])
                    {
                        string email = string.Empty;

                        if (item["user_id"].ToString().Contains("email"))
                        {
                            email = item["user_id"]["email"].ToString();
                            if (!emails.Contains(email))
                            {
                                data += item["user_id"]["_id"].ToString() + "\t";
                                data += item["user_id"]["name"].ToString() + "\t";
                                data += item["user_id"]["email"].ToString() + "\t";
                                data += item["user_id"]["created_at"].ToString() + "\t";
                                data += item["user_id"]["post_score"].ToString() + "\t";
                                data += item["user_id"]["salt"].ToString() + "\t";
                                data += item["user_id"]["hashed_password"].ToString() + "\t";
                                data += Environment.NewLine;
                                emails.Add(email);
                            }
                        }
                        email = item["post_id"]["creator_id"]["email"].ToString();
                        if (!emails.Contains(email))
                        {
                            data += item["post_id"]["creator_id"]["_id"].ToString() + "\t";
                            data += item["post_id"]["creator_id"]["name"].ToString() + "\t";
                            data += item["post_id"]["creator_id"]["email"].ToString() + "\t";
                            data += item["post_id"]["creator_id"]["created_at"].ToString() + "\t";
                            data += item["post_id"]["creator_id"]["post_score"].ToString() + "\t";
                            data += item["post_id"]["creator_id"]["salt"].ToString() + "\t";
                            data += item["post_id"]["creator_id"]["hashed_password"].ToString() + "\t";
                            data += Environment.NewLine;
                            emails.Add(email);
                        }

                        if (!item.ToString().Contains("parent_id"))
                        {
                            continue;
                        }
                        if (item["parent_id"].ToString() == "")
                        {
                            continue;
                        }
                        if (!item["parent_id"]["user_id"].ToString().Contains("email"))
                        {
                            continue;
                        }

                        email = item["parent_id"]["user_id"]["email"].ToString();
                        if (!emails.Contains(email))
                        {
                            data += item["parent_id"]["user_id"]["_id"].ToString() + "\t";
                            data += item["parent_id"]["user_id"]["name"].ToString() + "\t";
                            data += item["parent_id"]["user_id"]["email"].ToString() + "\t";
                            data += item["parent_id"]["user_id"]["created_at"].ToString() + "\t";
                            data += item["parent_id"]["user_id"]["post_score"].ToString() + "\t";
                            data += item["parent_id"]["user_id"]["salt"].ToString() + "\t";
                            data += item["parent_id"]["user_id"]["hashed_password"].ToString() + "\t";
                            data += Environment.NewLine;
                            emails.Add(email);
                        }
                    }

                    File.AppendAllText("data.txt", data);
                }
                catch (Exception ex)
                {
                    UpdateStatus(ex.Message);
                    File.AppendAllText("errors.txt", name + " : " + ex.Message + Environment.NewLine);
                    break;
                }
                Thread.Sleep(100);
            }
        }

        private void buttonSpiderum_Click(object sender, EventArgs e)
        {
            UpdateStatus("Start...");
            string typeFeed = comboSpiderum.Text;
            int index = 1;
            int totalItems = 9999;

            isRunning = true; //https://spiderum.com/api/v2/user/getUserSavedPosts?page=1

            Task task = new Task(() =>
            {
                for (int i = index; i < totalItems && isRunning; i++)
                {
                    string text = new WebClient().DownloadString("https://spiderum.com/api/v1/feed/getPostsInFeed?type=" + typeFeed + "&page=" + i);

                    JObject array = JObject.Parse(text);

                    totalItems = Convert.ToInt32(array["posts"]["totalItems"]) / 20;

                    UpdateStatus(i + " / " + totalItems);

                    foreach (var item in array["posts"]["items"])
                    {
                        string name = item["creator_id"]["name"].ToString();

                        if (users.Contains(name))
                        {
                            continue;
                        }

                        UpdateStatus(i + " : " + name);
                        users.Add(name);

                        Crawl(name);

                        File.WriteAllLines("emails.txt", emails);
                        File.WriteAllLines("users.txt", users);
                    }
                    Thread.Sleep(100);
                }

                UpdateStatus("Done");
            });
            task.Start();
        }


        private void buttonCrawlUser_Click(object sender, EventArgs e)
        {
            UpdateStatus("Start...");
            isRunning = true;

            var lines = File.ReadAllLines("data.txt");

            Task task = new Task(() =>
            {
                for (int i = 0; i < lines.Length && isRunning; i++)
                {
                    string name = lines[i].Split('\t')[1];

                    if (users.Contains(name))
                    {
                        continue;
                    }

                    UpdateStatus2(i + "/" + lines.Length + " : " + name);
                    users.Add(name);

                    Crawl(name);

                    File.WriteAllLines("emails.txt", emails);
                    File.WriteAllLines("users.txt", users);
                }
                UpdateStatus("Done");
            });
            task.Start();
        }

    }
}

﻿using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

namespace JsonApiClient
{
	public class GitHubUser 
	{
		public string login { get; set; }
		public int id { get; set; }
		public string avatar_url { get; set; }
		public string gravatar_id { get; set; }
		public string url { get; set; }
		public string html_url { get; set; }
		public string followers_url { get; set; }
		public string following_url { get; set; }
		public string gists_url { get; set; }
		public string starred_url { get; set; }
		public string subscriptions_url { get; set; }
		public string organizations_url { get; set; }
		public string repos_url { get; set; }
		public string events_url { get; set; }
		public string received_events_url { get; set; }
		public string type { get; set; }
		public bool site_admin { get; set; }
		public string name { get; set; }
		public object company { get; set; }
		public object blog { get; set; }
		public object location { get; set; }
		public string email { get; set; }
		public object hireable { get; set; }
		public object bio { get; set; }
		public int public_repos { get; set; }
		public int public_gists { get; set; }
		public int followers { get; set; }
		public int following { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string zen_url = @"https://api.github.com/zen";
		public ArrayList urls
		{
			get { return new ArrayList (new string[] {zen_url, avatar_url, url, html_url, followers_url, following_url, gists_url, subscriptions_url, repos_url, events_url, received_events_url}); }
		}
	}
}

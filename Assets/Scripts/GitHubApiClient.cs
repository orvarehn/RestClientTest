using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;

namespace JsonApiClient
{
	public class GitHubApiClient : MonoBehaviour 
	{
		private readonly string GIT_HUB_API_BASE_URI = @"https://api.github.com/";
		private readonly string GITHUB_ZEN = @"https://api.github.com/zen";
		private Texture2D avatar;
		public  InputField inputField;
		public Image avatarImage;
		public GameObject label;

		private GameObject getChildGameObject(GameObject fromGameObject, string withName) {
			Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
			foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
			return null;
		}

		public void StartGetGithubUser() {
			string gitHubNick = inputField.text;
			if (string.IsNullOrEmpty(gitHubNick) ) { 
				Debug.Log ("Please enter a nickname");
				return;
			}
			StartCoroutine(GetGitHubUser(gitHubNick, (gitHubUser) => { 
				loadingIsDone(gitHubUser); 	
			}));
		}

		private void loadingIsDone(GitHubUser gitHubUser) {
			Debug.Log ("Name = " + gitHubUser.name + "\n" +
				"AvatarURL = " + gitHubUser.avatar_url);
//			StartCoroutine(GetAvatar(gitHubUser.avatar_url, (message) => { 
//				Debug.Log(message);
//			} ));
			StartCoroutine(loadStuff(gitHubUser.urls, (message) => { 
				Debug.Log(message);
			}));

		}

		IEnumerator loadStuff(ArrayList urls, System.Action<string> callback) {
			IEnumerator iter = urls.GetEnumerator();
			while (iter.MoveNext()) {
				Debug.Log(iter.Current);
				WWW www = new WWW (iter.Current.ToString());
				yield return www;
				string value;
				if (www.responseHeaders.TryGetValue ("CONTENT-TYPE", out value)) {
					if (value.Contains("image")) {
						SetAvatar (www);
					} else if (value.Contains("json")) {
						Debug.Log (www.text);
					} else if (value.Contains("text/plain")) {
						GameObject gameObject = getChildGameObject (label, "text");
						Text text = gameObject.GetComponent<Text> ();
						text.text = www.text;
						label.SetActive (true);
					}
				} else {
					Debug.Log ("CONTENT-TYPE not found");
				}
			}
			callback ("DONE PARSING URLS");					
		}

		private void SetAvatar(WWW www) {
			avatar = (Texture2D) www.texture;
			Sprite sprite = Sprite.Create (avatar, 
				new Rect (0, 0, avatar.width, avatar.height), 
				new Vector2 (0.5f, 0.5f), 
				40f);
			avatarImage.sprite = sprite;
			LayoutElement layOutElement = avatarImage.GetComponent<LayoutElement> ();
			layOutElement.preferredWidth = 200;
			layOutElement.preferredHeight = 200;
		}

		IEnumerator GetAvatar(string url, System.Action<string> callback) {
			// Get supplementary data.
			WWW www2 = new WWW(url);
			yield return www2;
			avatar = (Texture2D) www2.texture;
			Sprite sprite = Sprite.Create (avatar, 
				new Rect (0, 0, avatar.width, avatar.height), 
				new Vector2 (0.5f, 0.5f), 
				40f);
			avatarImage.sprite = sprite;
			LayoutElement layOutElement = avatarImage.GetComponent<LayoutElement> ();
			layOutElement.preferredWidth = 200;
			layOutElement.preferredHeight = 200;
			callback ("AVATAR LOADED");

		}
			
		IEnumerator GetGitHubUser(string username, System.Action<GitHubUser> callback) {
			string url = GIT_HUB_API_BASE_URI + "users/" + username;
			WWW www = new WWW(url);
			yield return www;
			string content = www.text;
			GitHubUser gitHubUser = JsonConvert.DeserializeObject<GitHubUser>(content);
			callback (gitHubUser);
		}
			
	}
}
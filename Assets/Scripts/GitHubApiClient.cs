using UnityEngine;
using System.Collections;
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
		private readonly string API_KEY = "bdd60ef65b25f9bf0292b59fdc90cea3";
		private readonly string GIT_HUB_API_BASE_URI = @"https://api.github.com/";
		private readonly string OAuthToken = "ab6cb2b0c9aa6986ce19df75fd9695f80d669c68";
		private Texture2D avatar;


		public  InputField inputField;
		public Image avatarImage;

		// Use this for initialization
//		void Start() {
//			Debug.Log ("Start()");
//			StartCoroutine(GetGitHubUser("orvarehn"));
//		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void StartGetGithubUser() {
			
			Debug.Log ("Start()");
//			StartCoroutine(GetGitHubUser("orvarehn", (ret) => { if (ret = true) {
//					Debug.Log ("DONE " + ret);
//
//				}}));
			string gitHubNick = inputField.text;
			if (string.IsNullOrEmpty(gitHubNick) ) { 
				Debug.Log ("Please enter a nickname");
				return;
			}
			Debug.Log (gitHubNick);
			StartCoroutine(GetGitHubUser(gitHubNick, (gitHubUser) => { 

				loadingIsDone(gitHubUser); 
				
			} ));
		}

		private void loadingIsDone(GitHubUser gitHubUser) {
			Debug.Log ("Name = " + gitHubUser.name + "\n" +
				"AvatarURL = " + gitHubUser.avatar_url);
			StartCoroutine(GetAvatar(gitHubUser.avatar_url, (isDone) => { 

				Debug.Log("AVATAR LOADED");

			} ));
			
		}

		IEnumerator GetAvatar(string url, System.Action<bool> callback) {
			// Get supplementary data.
			WWW www2 = new WWW(url);
			yield return www2;
			avatar = (Texture2D) www2.texture;
			Debug.Log("setting image");

			Sprite sprite = Sprite.Create (avatar, 
				new Rect (0, 0, avatar.width, avatar.height) , 
				new Vector2 (0.5f, 0.5f), 
				40f);
			avatarImage.sprite = sprite;
			LayoutElement layOutElement = avatarImage.GetComponent<LayoutElement> ();
			layOutElement.preferredWidth = 200;
			layOutElement.preferredHeight = 200;
			callback (true);

		}

		void OnGUI() {
			if (avatar) {
				
				//GUI.DrawTexture(new Rect(0.0f, 0.0f, avatar.width, avatar.height),  avatar, ScaleMode.ScaleToFit, true, 0);

			}
		
		}

		IEnumerator GetGitHubUser(string username, System.Action<GitHubUser> callback) {
			string url = GIT_HUB_API_BASE_URI + "users/" + username;
			WWW www = GetJson(url);
			yield return www;
//			Renderer renderer = GetComponent<Renderer>();
//			renderer.material.mainTexture = www.texture;
			// Syncronious consuming
//			ServicePointManager.CertificatePolicy = new GitApiClient ();
//			WebClient webClient = new WebClient();
			//webClient.Headers.Add ("Authorization", "token " + OAuthToken);
//			string content = webClient.DownloadString (url);
			string content = www.text;
//			Renderer renderer = GetComponent<Renderer>();
//			renderer.material.mainTexture = www.texture;
			GitHubUser gitHubUser = JsonConvert.DeserializeObject<GitHubUser>(content);
			callback (gitHubUser);
		}

		private WWW GetJson(string url) {
			Debug.Log ("Getting data fron url = " + url);
			return new WWW(url);
		}
	}
}
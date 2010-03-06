using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;

namespace PayPalEmulator
{
	/// <summary>
	/// Submits post data to a url.
	/// </summary>
	public class PostSubmitter
	{
		/// <summary>
		/// determines what type of post to perform.
		/// </summary>
		public enum PostTypeEnum
		{
			/// <summary>
			/// Does a get against the source.
			/// </summary>
			Get,
			/// <summary>
			/// Does a post against the source.
			/// </summary>
			Post
		}

		/// <summary>
		/// Gets or sets the url to submit the post to.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the items to post.
		/// </summary>
		public QueryString PostItems { get; set; }

		/// <summary>
		/// Gets or sets the type of action to perform against the url.
		/// </summary>
		public PostTypeEnum Type { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostSubmitter()
		{
		}

		/// <summary>
		/// Constructor that accepts a url as a parameter
		/// </summary>
		/// <param name="url">The url where the post will be submitted to.</param>
		public PostSubmitter(string url)
			: this()
		{
			Url = url;
		}

		/// <summary>
		/// Constructor allowing the setting of the url and items to post.
		/// </summary>
		/// <param name="url">the url for the post.</param>
		/// <param name="values">The values for the post.</param>
		public PostSubmitter(string url, QueryString postItems)
			: this(url)
		{
			PostItems = postItems;
		}

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <returns>a string containing the result of the post.</returns>
		public string Post()
		{
			string result = PostData(Url, PostItems.ToString());
			return result;
		}

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <param name="url">The url to post to.</param>
		/// <returns>a string containing the result of the post.</returns>
		public string Post(string url)
		{
			Url = url;
			return this.Post();
		}

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <param name="url">The url to post to.</param>
		/// <param name="values">The values to post.</param>
		/// <returns>a string containing the result of the post.</returns>
		public string Post(string url, QueryString postItems)
		{
			PostItems = postItems;
			return this.Post(url);
		}

		/// <summary>
		/// Posts data to a specified url. Note that this assumes that you have already url encoded the post data.
		/// </summary>
		/// <param name="postData">The data to post.</param>
		/// <param name="url">the url to post to.</param>
		/// <returns>Returns the result of the post.</returns>
		private string PostData(string url, string postData)
		{
			HttpWebRequest request = null;
			if (Type == PostTypeEnum.Post)
			{
				Uri uri = new Uri(url);
				request = (HttpWebRequest)WebRequest.Create(uri);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = postData.Length;
				using (Stream writeStream = request.GetRequestStream())
				{
					UTF8Encoding encoding = new UTF8Encoding();
					byte[] bytes = encoding.GetBytes(postData);
					writeStream.Write(bytes, 0, bytes.Length);
				}
			}
			else
			{
				Uri uri = new Uri(url + "?" + postData);
				request = (HttpWebRequest)WebRequest.Create(uri);
				request.Method = "GET";
			}
			string result = string.Empty;
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
					{
						result = readStream.ReadToEnd();
					}
				}
			}
			return result;
		}
	}
}

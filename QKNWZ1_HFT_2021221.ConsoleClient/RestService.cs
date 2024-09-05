using System;
using System.Collections.Generic;
using System.Net.Http;

namespace QKNWZ1_HFT_2021221.ConsoleClient
{
	public class RestService
	{
		private HttpClient httpClient;

		public RestService(string baseAddress) => this.Init(baseAddress);

		private void Init(string baseAddress)
		{
			this.httpClient = new()
			{
				BaseAddress = new Uri(baseAddress),
			};
			this.httpClient.DefaultRequestHeaders.Accept.Clear();
			this.httpClient.DefaultRequestHeaders.Accept
				.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
			try
			{
				_ = this.httpClient.GetAsync(default(Uri)).GetAwaiter().GetResult();
			}
			catch (HttpRequestException)
			{
				throw new ArgumentException("Endpoint unavailable!", nameof(baseAddress));
			}
		}

		public IList<T> GetMultiple<T>(string endpoint)
		{
			var resultList = new List<T>();
			var relativeUri = new Uri(endpoint, UriKind.Relative);
			var httpResponseMessage = this.httpClient.GetAsync(relativeUri).GetAwaiter().GetResult();
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				resultList = httpResponseMessage.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
			}
			return resultList;
		}

		public T GetById<T>(int id, string endpoint)
		{
			var relativeUri = new Uri(
				new Uri(endpoint, UriKind.Relative),
				id.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			var result = default(T);
			var httpResponseMessage = this.httpClient.GetAsync(relativeUri).GetAwaiter().GetResult();
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				result = httpResponseMessage.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
			}
			return result;
		}

		/// <summary>
		/// Fetch a single <typeparamref name="T"/> object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="endpoint"></param>
		/// <returns></returns>
		public T GetSingle<T>(string endpoint)
		{
			var result = default(T);
			var httpResponseMessage = this.httpClient.GetAsync(new Uri(endpoint, UriKind.Relative)).GetAwaiter().GetResult();
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				result = httpResponseMessage.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
			}
			return result;
		}

		/// <summary>
		/// Create and insert a(n) new <typeparamref name="T"/> object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item">The new object.</param>
		/// <param name="endpoint"></param>
		public void Post<T>(T item, string endpoint)
		{
			var httpResponseMessage = this.httpClient.PostAsJsonAsync<T>(endpoint, item)
				.GetAwaiter().GetResult();
			httpResponseMessage.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Delete an object.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="endpoint"></param>
		public void Delete(int id, string endpoint)
		{
			var relativeUri = new Uri(
				new Uri(endpoint, UriKind.Relative),
				id.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			var httpResponseMessage = this.httpClient.DeleteAsync(relativeUri)
				.GetAwaiter().GetResult();
			httpResponseMessage.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Update a(n) <typeparamref name="T"/> object.
		/// </summary>
		/// <typeparam name="T">The type of the object to be modified.</typeparam>
		/// <param name="item">The object to be modified.</param>
		/// <param name="endpoint">The address of the endpoint.</param>
		public void Put<T>(T item, string endpoint)
		{
			var relativeUri = new Uri(endpoint, UriKind.Relative);
			var httpResponseMessage = this.httpClient.PutAsJsonAsync<T>(relativeUri, item)
				.GetAwaiter().GetResult();
			httpResponseMessage.EnsureSuccessStatusCode();
		}
	}
}

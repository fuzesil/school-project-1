using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace QKNWZ1_HFT_2021221.WpfClient
{
	public class RestService : IDisposable
	{
		private HttpClient client;

		public RestService(string baseurl, string pingableEndpoint = "swagger")
		{
			bool isPingged = false;
			while (!isPingged)
			{
				isPingged = Ping(baseurl + pingableEndpoint);
			}
			this.Init(baseurl);
		}

		private static bool Ping(string url)
		{
			try
			{
				using WebClient wc = new();
				wc.DownloadData(url);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void Init(string baseurl)
		{
			this.client = new HttpClient() { BaseAddress = new Uri(baseurl) };
			this.client.DefaultRequestHeaders.Accept.Clear();
			this.client.DefaultRequestHeaders.Accept.Add(
				new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
				("application/json"));
			try
			{
				this.client.GetAsync("").GetAwaiter().GetResult();
			}
			catch (HttpRequestException)
			{
				throw new ArgumentException("Endpoint is not available!");
			}

		}

		public async Task<IList<T>> GetAsync<T>(string endpoint)
		{
			HttpResponseMessage response = await this.client.GetAsync(endpoint);
			List<T> items;
			if (response.IsSuccessStatusCode)
			{
				items = await response.Content.ReadAsAsync<List<T>>();
			}
			else
			{
				var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
				throw new ArgumentException(error.Message);
			}
			return items;
		}

		public IList<T> Get<T>(string endpoint)
		{
			HttpResponseMessage response = this.client.GetAsync(endpoint).GetAwaiter().GetResult();
			List<T> items;
			if (response.IsSuccessStatusCode)
			{
				items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
			}
			else
			{
				var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
				throw new ArgumentException(error.Message);
			}
			return items;
		}

		public async Task<T> GetSingleAsync<T>(string endpoint)
		{
			HttpResponseMessage response = await this.client.GetAsync(endpoint);
			T item;
			if (response.IsSuccessStatusCode)
			{
				item = await response.Content.ReadAsAsync<T>();
			}
			else
			{
				var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
				throw new ArgumentException(error.Message);
			}
			return item;
		}

		public T GetSingle<T>(string endpoint)
		{
			HttpResponseMessage response = this.client.GetAsync(endpoint).GetAwaiter().GetResult();
			T item;
			if (response.IsSuccessStatusCode)
			{
				item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
			}
			else
			{
				var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
				throw new ArgumentException(error.Message);
			}
			return item;
		}

		public async Task<T> GetAsync<T>(int id, string endpoint)
		{
			HttpResponseMessage response = await this.client.GetAsync(endpoint + "/" + id.ToString());
			T item;
			if (response.IsSuccessStatusCode)
			{
				item = await response.Content.ReadAsAsync<T>();
			}
			else
			{
				var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
				throw new ArgumentException(error.Message);
			}
			return item;
		}

		public T Get<T>(int id, string endpoint)
		{
			HttpResponseMessage response = this.client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
			T item;
			if (response.IsSuccessStatusCode)
			{
				item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
			}
			else
			{
				var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
				throw new ArgumentException(error.Message);
			}
			return item;
		}

		public async Task PostAsync<T>(T item, string endpoint)
		{
			HttpResponseMessage response =
				await this.client.PostAsJsonAsync(endpoint, item);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
				throw new ArgumentException(error.Message);
			}
			response.EnsureSuccessStatusCode();
		}

		public void Post<T>(T item, string endpoint)
		{
			HttpResponseMessage response =
				this.client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

			if (!response.IsSuccessStatusCode)
			{
				var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
				throw new ArgumentException(error.Message);
			}
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteAsync(int id, string endpoint)
		{
			HttpResponseMessage response =
				await this.client.DeleteAsync(endpoint + "/" + id.ToString());

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
				throw new ArgumentException(error.Message);
			}

			response.EnsureSuccessStatusCode();
		}

		public void Delete(int id, string endpoint)
		{
			HttpResponseMessage response =
				client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

			if (!response.IsSuccessStatusCode)
			{
				var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
				throw new ArgumentException(error.Message);
			}

			response.EnsureSuccessStatusCode();
		}

		public async Task PutAsync<T>(T item, string endpoint)
		{
			HttpResponseMessage response =
				await this.client.PutAsJsonAsync(endpoint, item);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsAsync<RestExceptionInfo>();
				throw new ArgumentException(error.Message);
			}

			response.EnsureSuccessStatusCode();
		}

		public void Put<T>(T item, string endpoint)
		{
			HttpResponseMessage response =
				this.client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

			if (!response.IsSuccessStatusCode)
			{
				var error = response.Content.ReadAsAsync<RestExceptionInfo>().GetAwaiter().GetResult();
				throw new ArgumentException(error.Message);
			}

			response.EnsureSuccessStatusCode();
		}

		public void Dispose()
		{
			this.client.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

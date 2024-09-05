using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QKNWZ1_HFT_2021221.WpfClient
{
	public class RestCollection<T> : INotifyCollectionChanged, IEnumerable<T>, IDisposable
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		private NotifyService notify;
		private RestService rest;
		private ICollection<T> items;
		private bool hasSignalR;
		private Type type = typeof(T);
		private string initUrl;

		public RestCollection(string baseurl, string endpoint, string hub = "")
		{
			this.initUrl = baseurl + endpoint;
			this.rest = new RestService(baseurl);
			this.hasSignalR = !string.IsNullOrWhiteSpace(hub);
			if (this.hasSignalR)
			{
				NotifyService ns = new(baseurl + hub);

				ns.AddHandler<T>($"Post{this.type.Name}", item =>
				{
					this.items.Add(item);
					this.OnCollectionChanged();
				});

				ns.AddHandler<T>($"Delete{this.type.Name}", item => this.Delete(item));

				ns.AddHandler<T>($"Put{this.type.Name}", item => this.Init());

				ns.Init();
				this.notify = ns;
			}
			this.Init();
		}

		private async Task Init()
		{
			this.items = await this.rest.GetAsync<T>($"{this.initUrl}Get{this.type.Name}Table");
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public IEnumerator<T> GetEnumerator() => this.items is not null ? this.items.GetEnumerator() : new List<T>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.items is not null ? this.items.GetEnumerator() : new List<T>().GetEnumerator();

		public void Add(T item)
		{
			string endpoint = $"Admin/Post{this.type.Name}";

			if (this.hasSignalR) this.rest.PostAsync(item, endpoint);
			else
			{
				this.rest.PostAsync(item, endpoint)
				.ContinueWith(t =>
					this.Init()
					.ContinueWith(task => Application.Current.Dispatcher.Invoke(() =>
						CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)))));
			}
		}

		public void Update(T item)
		{
			string endpoint = $"Admin/Put{this.type.Name}";

			if (this.hasSignalR) this.rest.PutAsync(item, endpoint);
			else
			{
				this.rest.PutAsync(item, endpoint)
				.ContinueWith(t =>
					this.Init()
					.ContinueWith(task => Application.Current.Dispatcher.Invoke(() =>
						CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)))));
			}
		}

		public void Delete(int id)
		{
			string endpoint = $"Admin/Delete{this.type.Name}";

			if (this.hasSignalR) this.rest.DeleteAsync(id, endpoint);
			else
			{
				this.rest.DeleteAsync(id, endpoint)
				.ContinueWith(t =>
					this.Init()
					.ContinueWith(task => Application.Current.Dispatcher.Invoke(() =>
						CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)))));
			}
		}

		private void OnCollectionChanged() => this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		private void Delete(T item)
		{
			T element = this.items.FirstOrDefault(t => t.Equals(item));
			if (element is null) throw new NullReferenceException("Element was Not Found");

			if (this.items.Remove(item)) this.OnCollectionChanged();
			else this.Init();

		}
		public void Dispose()
		{
			this.rest.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace QKNWZ1_HFT_2021221.WpfClient
{
	public class ImmutableRestCollection<T> : INotifyCollectionChanged, IEnumerable<T>, IDisposable
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		private NotifyService notify;
		private RestService rest;
		private IList<T> items;
		private bool hasSignalR;
		private string controller;
		private Type type = typeof(T);

		public ImmutableRestCollection(string baseurl, string controller, string hub = "", string specialAction = null)
		{
			this.rest = new RestService(baseurl);
			this.controller = controller;
			this.hasSignalR = !string.IsNullOrWhiteSpace(hub);
			if (this.hasSignalR)
			{
				this.notify = new NotifyService(baseurl + hub);
				/*
				this.notify.AddHandler<T>(this.type.Name + "Created", (T item) =>
				{
					this.items.Add(item);
					CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
				});
				this.notify.AddHandler<T>(this.type.Name + "Deleted", (T item) =>
				{
					var element = this.items.FirstOrDefault(t => t.Equals(item));
					if (element is not null)
					{
						this.items.Remove(item);
						CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
					}
					else
					{
						this.Init(endpoint);
					}

				});
				this.notify.AddHandler<T>(type.Name + "Updated", (T item) => this.Init());
				*/

				this.notify.Init();
			}
			this.Init(specialAction ?? $"Get{this.type.Name}Table");
		}

		private async Task Init(string endpoint)
		{
			this.items = await this.rest.GetAsync<T>(this.controller + endpoint);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public IEnumerator<T> GetEnumerator() => this.items is not null ? this.items.GetEnumerator() : new List<T>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.items is not null ? this.items.GetEnumerator() : new List<T>().GetEnumerator();

		public void GetSingle(string url)
		{
			this.items.Clear();
			this.items.Add(this.rest.GetSingle<T>(this.controller + url));
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void Get(string url)
		{
			this.items = this.rest.Get<T>(this.controller + url);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void ReInit()
		{
			this.items = this.rest.Get<T>($"{this.controller}Get{this.type.Name}Table");
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		/*
		public void Add(T item)
		{
			if (this.hasSignalR)
			{
				this.rest.PostAsync(item, endpoint);
			}
			else
			{
				this.rest.PostAsync(item, typeof(T).Name)
					.ContinueWith((t) => Init("")
					.ContinueWith(task => Application.Current.Dispatcher.Invoke(() =>
					CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)))));
			}
		}

		public void Update(T item)
		{
			if (this.hasSignalR)
			{
				this.rest.PutAsync(item, typeof(T).Name);
			}
			else
			{
				this.rest.PutAsync(item, typeof(T).Name).ContinueWith((t) =>
				{
					this.Init().ContinueWith(z =>
					{
						Application.Current.Dispatcher.Invoke(() =>
						{
							CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
						});
					});
				});
			}
		}

		public void Delete(int id)
		{
			if (this.hasSignalR)
			{
				this.rest.DeleteAsync(id, typeof(T).Name);
			}
			else
			{
				this.rest.DeleteAsync(id, typeof(T).Name).ContinueWith((t) =>
				{
					Init().ContinueWith(z =>
					{
						Application.Current.Dispatcher.Invoke(() =>
						{
							CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
						});
					});
				});
			}
		}
		*/
		public void Dispose()
		{
			this.rest.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}

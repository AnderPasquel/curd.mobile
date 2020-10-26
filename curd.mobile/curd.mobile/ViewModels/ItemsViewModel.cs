using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using curd.mobile.Models;
using curd.mobile.Views;

namespace curd.mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Productos";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;           
                await DataStore.AddItemAsync(newItem);
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<UpdateItemPage, Item>(this, "UpdateItem", async (obj, item) =>
            {
                var newItem = item as Item;
                await DataStore.UpdateItemAsync(newItem);
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Remove(newItem);
                await DataStore.DeleteItemAsync(newItem.Id);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {                
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
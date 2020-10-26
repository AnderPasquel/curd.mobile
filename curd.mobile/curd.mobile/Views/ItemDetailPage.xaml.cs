using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using curd.mobile.Models;
using curd.mobile.ViewModels;

namespace curd.mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Name = "",
                Price = 0.0
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        async void Delete_Clicked(object sender, EventArgs e)
        {            
            var page = new ItemsPage();
            MessagingCenter.Send(this, "DeleteItem", this.viewModel.Item);
            await Navigation.PushModalAsync(new NavigationPage(page));
        }
        async void UpdateItem_Clicked(object sender, EventArgs e)
        {         
            var page = new UpdateItemPage(viewModel.Item);
            await Navigation.PushModalAsync(new NavigationPage(page));
        }
    }
}
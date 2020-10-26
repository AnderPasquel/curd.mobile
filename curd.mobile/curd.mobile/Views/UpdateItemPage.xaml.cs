using curd.mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace curd.mobile.Views
{  
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class UpdateItemPage : ContentPage
    {
        public Item Item { get; set; }

        public UpdateItemPage() {
            InitializeComponent();
        }
        public UpdateItemPage(Item viewModel)
        {
            InitializeComponent();

            BindingContext = this.Item = viewModel;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            var page = new ItemsPage();
            MessagingCenter.Send(this, "UpdateItem", Item);         
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
using MauiContentHub.Models;
using MauiContentHub.Services;

namespace MauiContentHub
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            getProducts();
        }
        private async Task getProducts()
        {
            if (await MConnector.CheckConnection())
            {
                List<HomeProduct> products = new List<HomeProduct>();
                products = await ProductService.GetHomes();
                productsListView.ItemsSource = products;
            }
        }
    }
}
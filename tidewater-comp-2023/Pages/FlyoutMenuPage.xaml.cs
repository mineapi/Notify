using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tidewater_comp_2023.Pages;

namespace tidewater_comp_2023;

public partial class FlyoutMenuPage : ContentPage
{
    public NavigationPage page;

    public FlyoutMenuPage()
    {
        InitializeComponent();

        //Select first item.
        collectionView.SelectedItem = collectionView.ItemsSource.Cast<object>().FirstOrDefault();
    }

    /// <summary>
    /// Toggle menu
    /// </summary>
    private void Btn_menu_OnClicked(object sender, EventArgs e) =>
        GlobalPages.FlyoutPage.IsPresented = !GlobalPages.FlyoutPage.IsPresented;
}
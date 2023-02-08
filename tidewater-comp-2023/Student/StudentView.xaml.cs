namespace tidewater_comp_2023;

public partial class StudentView : FlyoutPage
{
	public StudentView()
	{
		InitializeComponent();

        //Disable navigation page bar.
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetBackButtonTitle(this, null);

        GlobalPages.FlyoutPage = this;

        //Add handler for selecting from flyout.
        flyoutPage.collectionView.SelectionChanged += OnSelectionChanged;
    }

    //Flyout selection change.
    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            GlobalPages.FlyoutPage.IsPresented = !GlobalPages.FlyoutPage.IsPresented;
            await ((NavigationPage)Detail).PushAsync((Page)Activator.CreateInstance(item.TargetType));
        }
    }
}
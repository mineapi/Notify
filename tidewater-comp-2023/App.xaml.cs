namespace tidewater_comp_2023;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        NavigationPage page = new NavigationPage(new RoleSelectionPage());

        MainPage = page;
    }
}

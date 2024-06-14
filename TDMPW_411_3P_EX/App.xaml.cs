using TDMPW_411_3P_EX.Views;
namespace TDMPW_411_3P_EX;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppView();
	}
}

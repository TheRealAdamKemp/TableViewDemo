using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace TableViewDemo
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        private UINavigationController _navigationController;

        private static readonly IEnumerable<string> _demoItems = Enumerable.Range(0, 100).Select(i => i.ToString()).ToList();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var mainViewController = new SingleItemSelectionViewController(new [] { "Checks", "Switches" })
                {
                    Title = "Demos"
                };
            mainViewController.ItemSelected += HandleDemoSelected;

            _navigationController = new UINavigationController(mainViewController);

            // If you have defined a root view controller, set it here:
            Window.RootViewController = _navigationController;

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }

        private void HandleDemoSelected(object sender, ItemSelectedEventArgs e)
        {
            UIViewController demoViewController = null;
            switch (e.Index)
            {
                case 0:
                    demoViewController = CreateAccessoryDemoViewController();
                    break;
                case 1:
                    demoViewController = CreateSwitchesDemoViewController();
                    break;
                default:
                    throw new NotImplementedException();
            }

            _navigationController.PushViewController(demoViewController, animated: true);
        }

        private UIViewController CreateAccessoryDemoViewController()
        {
            var viewController = new MultipleItemSelectionWithChecksViewController(_demoItems) { Title = "Accessories" };
            AddItemsSelectedEventHandler(viewController);

            return viewController;
        }

        private UIViewController CreateSwitchesDemoViewController()
        {
            var viewController = new MultipleItemSelectionWithSwitchesViewController(_demoItems) { Title = "Switches" };
            AddItemsSelectedEventHandler(viewController);

            return viewController;
        }

        private void AddItemsSelectedEventHandler(MultipleItemSelectionViewControllerBase viewController)
        {
            viewController.ItemsSelected += (s, e) => _navigationController.PopViewController(animated: true);
        }
    }
}



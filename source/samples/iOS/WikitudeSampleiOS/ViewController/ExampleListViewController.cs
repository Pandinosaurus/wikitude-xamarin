using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using System.IO;
using System.Json;

namespace WikitudeSample
{
	public partial class ExampleListViewController : DialogViewController
	{
		ARViewController arController;
		RecentUrlsViewController urlsController;

		UIBarButtonItem buttonUrls;

		public ExampleListViewController () : base (UITableViewStyle.Grouped, null)
		{

			Root = new RootElement ("Examples");

			var indexList = Path.Combine (NSBundle.MainBundle.BundlePath, "IndexList.json");
			var json = JsonArray.Parse (File.ReadAllText (indexList));
			var titleList = new string[] {
				"ImageRecognition",
				"3dAndImageRecognition",
				"PointOfInterest",
				"ObtainPoiData",
				"BrowsingPois",
				"Video",
				"Demo"
			};


			for (int i = 0; i < titleList.Length; i++)
			{
				var title = titleList [i];

				var indexArr = (JsonArray)json [i];

				var section = new Section (title);

				foreach (var jobj in indexArr)
				{
					var elem = new StyledStringElement (jobj["Title"].ToString().Trim('"'), () =>
						{
							var path = jobj["Path"].ToString().Trim('"');
							var exampleVCName = jobj["ViewController"].ToString().Trim('"');
												
							arController = this.getViewControllerForExample(exampleVCName, path);
							NavigationController.PushViewController(arController, true);
						});
					section.Add (elem);
				}

				Root.Add (section);
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			buttonUrls = new UIBarButtonItem ("URL's", UIBarButtonItemStyle.Bordered, new EventHandler((s, e) =>
			{
				urlsController = new RecentUrlsViewController();
				NavigationController.PushViewController(urlsController, true);
			}));

			NavigationItem.RightBarButtonItem = buttonUrls;
		}

		private ARViewController getViewControllerForExample(String exampleVCName, String examplePath)
		{
			if ( exampleVCName.Equals("StandardARViewController") )
			{
				return new ARViewController (examplePath, false);
			}
			else if ( exampleVCName.Equals("ApplicationModelARViewController") ) 
			{
				return new ApplicationModelARViewController (examplePath);
			}
			else if ( exampleVCName.Equals("PresentingPoiDetailsARViewController") ) 
			{
				return new PresentingPoiDetailsARViewController (examplePath);
			}
			else if ( exampleVCName.Equals("ScreenshotARViewController") ) 
			{
				return new ScreenshotARViewController (examplePath);
			}

			return null;
		}
	}
}

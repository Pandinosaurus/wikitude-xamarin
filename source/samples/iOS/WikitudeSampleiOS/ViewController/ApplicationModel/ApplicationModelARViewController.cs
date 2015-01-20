using System;
using System.Json;
using CoreLocation;
using Wikitude.Architect;

namespace WikitudeSample
{
	public class ApplicationModelARViewController : ARViewController
	{
		CLLocationManager locationManager;
		protected JsonArray poiData;

		public ApplicationModelARViewController (string worldOrUrl) : base (worldOrUrl, false)
		{
		}

		override public void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear (animated);

			locationManager = new CLLocationManager ();
			locationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;

			locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) => {

				poiData = GeoUtils.GetPoiInformation(e.Locations [e.Locations.Length -1], 20);
				var js = "World.loadPoisFromJsonData(" + this.poiData.ToString() + ")";
				this.arView.CallJavaScript(js);

				locationManager.StopUpdatingLocation();
				locationManager.Delegate = null;
			};
			locationManager.StartUpdatingLocation ();
		}
	}
}

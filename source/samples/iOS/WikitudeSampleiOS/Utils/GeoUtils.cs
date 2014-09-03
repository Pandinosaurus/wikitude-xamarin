using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using MonoTouch.CoreLocation;

namespace WikitudeSample
{
	public class GeoUtils
	{
		public static JsonArray GetPoiInformation(CLLocation userLocation, int numberOfPlaces)
		{
			if (userLocation == null)
				return null;

			var pois = new List<JsonObject> ();

			for (int i = 0; i < numberOfPlaces; i++)
			{
				var loc = GetRandomLatLonNearby (userLocation.Coordinate.Latitude, userLocation.Coordinate.Longitude);

				var p = new Dictionary<string, JsonValue>(){
					{ "id", i.ToString() },
					{ "name", "POI#" + i.ToString() },
					{ "description", "This is the description of POI#" + i.ToString() },
					{ "latitude", loc[0] },
					{ "longitude", loc[1] },
					{ "altitude", 100f }
				};


				pois.Add (new JsonObject (p.ToList()));
			}

			var vals = from p in pois select (JsonValue)p;

			return new JsonArray (vals);
		}

		static double[] GetRandomLatLonNearby(double lat, double lon)
		{
			var rnd = new Random ();

			var newLat = lat + rnd.NextDouble () / 5 - 0.1;
			var newLng = lon + rnd.NextDouble () / 5 - 0.1;

			return new double[] { newLat, newLng };
		}
	}
}


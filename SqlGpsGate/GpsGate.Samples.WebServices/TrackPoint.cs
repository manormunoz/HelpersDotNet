using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	/// <summary>
	/// A TrackPoint contains a position, speed, heading in at a given time.
	/// </summary>
	[XmlRoot("trackPoint")]
	public class TrackPoint
	{
		private Position m_Position;
		private Velovity m_Velocity;
		private DateTime m_UTC;

		/// <summary>
		/// Position.
		/// </summary>
		[XmlElement("pos")]
		public Position Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}

		/// <summary>
		/// Speed and heading.
		/// </summary>
		[XmlElement("vel")]
		public Velovity Velocity
		{
			get { return m_Velocity; }
			set { m_Velocity = value; }
		}

		/// <summary>
		/// Time stamp in UTC
		/// </summary>
		[XmlElement("utc")]
		public DateTime UTC
		{
			get { return m_UTC; }
			set { m_UTC = value; }
		}
	}

	/// <summary>
	/// Represents a geographic position.
	/// </summary>
	[XmlRoot("pos")]
	public class Position
	{
		private double m_dblLat;
		private double m_dblLng;
		private double m_dblAlt;

		/// <summary>
		/// Latitude in decimal degrees
		/// </summary>
		[XmlElement("lat")]
		public double Latitude
		{
			get { return m_dblLat; }
			set { m_dblLat = value; }
		}

		/// <summary>
		/// Longitude in decimal degrees
		/// </summary>
		[XmlElement("lng")]
		public double Longitude
		{
			get { return m_dblLng; }
			set { m_dblLng = value; }
		}

		/// <summary>
		/// Altitude in meters
		/// </summary>
		[XmlElement("alt")]
		public double Altidude
		{
			get { return m_dblAlt; }
			set { m_dblAlt = value; }
		}

		#region Move, Bearing, Distance etc.

		/// <summary>
		/// Returns a new Position object at a specific distance and heading.
		/// </summary>
		/// <param name="dblDistance">Distance in meters to new position.</param>
		/// <param name="dblBearing">Heading in degrees to new position</param>
		/// <returns>New Position object.</returns>
		public Position Move(double dblDistance, double dblBearing)
		{
			dblDistance /= 6366710;

			double e1 = Longitude * Math.PI / 180.0;
			double n1 = Latitude * Math.PI / 180.0;

			dblBearing *= Math.PI / 180.0;

			double dblNewLat = Math.Asin(Math.Sin(n1) * Math.Cos(dblDistance) + Math.Cos(n1) * Math.Sin(dblDistance) * Math.Cos(dblBearing));
			double dblDeltaLon = Math.Atan2(Math.Sin(dblBearing) * Math.Sin(dblDistance) * Math.Cos(n1),
				Math.Cos(dblDistance) - Math.Sin(n1) * Math.Sin(dblNewLat));

			double dblLongitude = (e1 + dblDeltaLon) * 180.0 / Math.PI;
			double dblLatitude = dblNewLat * 180.0 / Math.PI;


			return new Position { Longitude = dblLongitude, Latitude = dblLatitude, Altidude = Altidude };
		}

		/// <summary>
		/// Bearing from "this" position object to the position object given as argument.
		/// </summary>
		/// <param name="posSecond">Position to calculate bearing to.</param>
		/// <returns>Bearing in degrees.</returns>
		public double Bearing(Position posSecond)
		{
			// http://williams.best.vwh.net/avform.htm#GCF

			double dblBearing = 0;
			double dblDistanceRads = DistanceRadians(posSecond);	// Distance in radians

			if (dblDistanceRads > 1e-16)
			{
				double e1 = Longitude / 180.0 * Math.PI;
				double n1 = Latitude / 180.0 * Math.PI;

				double e2 = posSecond.Longitude / 180.0 * Math.PI;
				double n2 = posSecond.Latitude / 180.0 * Math.PI;

				double cos_n1 = Math.Cos(n1);
				double sin_d = Math.Sin(dblDistanceRads);

				if (cos_n1 < 1e-16)   // EPS a small number ~ machine precision
				{
					// Pole is starting point
					if (n1 > 0)
					{
						dblBearing = 180.0;        //  starting from N pole
					}
					else
					{
						dblBearing = 0.0;         //  starting from S pole
					}
				}
				else
				{
					if (cos_n1 < 1e-16)
					{
						// 
					}
					else if (sin_d < 1e-16)
					{
						// 
					}
					else
					{
						double kvot = (Math.Sin(n2) - Math.Sin(n1) * Math.Cos(dblDistanceRads)) / (sin_d * cos_n1);

						// acos() can take a value from -1 to 1 but "kvot" sometimes
						// gets slightly outside the range causing an error

						if (kvot <= -1.0)
						{
							kvot = -1.0;
						}
						else if (kvot >= 1.0)
						{
							kvot = 1.0;
						}
						dblBearing = Math.Acos(kvot);
					}

					// For starting points other than the poles:
					if (Math.Sin(e1 - e2) >= 0.0)
					{
						// tc1=2*pi-acos((sin(lat2)-sin(lat1)*cos(d))/(sin(d)*cos(lat1)))
						dblBearing = 2.0 * Math.PI - dblBearing;
					}

					dblBearing *= 180.0 / Math.PI;
				}
			}
			else
			{
				// Same point
			}

			return dblBearing;
		}

		/// <summary>
		/// Distance between two position objects in radians.
		/// Great Circle Distance Calculations are used.
		/// </summary>
		/// <param name="posSecond">Position to calculate distance to.</param>
		/// <returns>Distance in radians</returns>
		public double DistanceRadians(Position posSecond)
		{
			// http://williams.best.vwh.net/avform.htm#GCF

			// Method 1
			// d=acos(sin(lat1)*sin(lat2)+cos(lat1)*cos(lat2)*cos(lon1-lon2))

			// Method 2
			// d=2*asin(sqrt((sin((lat1-lat2)/2))^2 + 
			//               cos(lat1)*cos(lat2)*(sin((lon1-lon2)/2))^2))

			double dblDistance = 0;

			double e1 = Longitude / 180.0 * Math.PI;
			double e2 = posSecond.Longitude / 180.0 * Math.PI;

			double n1 = Latitude / 180.0 * Math.PI;
			double n2 = posSecond.Latitude / 180.0 * Math.PI;

			double sin_lat = Math.Sin((n1 - n2) / 2.0);
			sin_lat *= sin_lat;

			double sin_lon = Math.Sin((e1 - e2) / 2.0);
			sin_lon *= sin_lon;

			dblDistance = 2.0 * Math.Asin(Math.Sqrt(sin_lat + Math.Cos(n1) * Math.Cos(n2) * sin_lon));

			return dblDistance;
		}

		/// <summary>
		/// Returns the distance between two <see cref="Position"/> objects.
		/// Great Circle Distance is used for the calculation. The returned value is in meters.
		/// </summary>
		/// <param name="posSecond">Position to calculate distance to.</param>
		/// <returns>Distance in meters</returns>
		public double Distance(Position posSecond)
		{
			return DistanceRadians(posSecond) * 6366710;
		}

		/// <summary>
		/// Take a longitude value in degrees, and returns a value between 180 and -180.
		/// E.g. -270 returns 90, which is the same longitude on Earth.
		/// todo: expose in public API (static)
		/// </summary>
		/// <param name="dblLongitude">None normalized longitude</param>
		/// <returns>Normalized longitude</returns>
		private double m_NormalizeLongitude(double dblLongitude)
		{
			if (dblLongitude > 180.0 || dblLongitude < -180.0)
			{
				// longitude MOD 360
				int iLong = (int)(dblLongitude / (360.0));
				dblLongitude -= iLong * 360.0;
			}

			if (dblLongitude > 180.0)
			{
				dblLongitude -= 360.0;
			}
			else if (dblLongitude < -180.0)
			{
				dblLongitude += 360.0;
			}

			return dblLongitude;
		}

		#endregion
	}

	/// <summary>
	/// Represents a velocity. This is the same as speed + direction.
	/// </summary>
	[XmlRoot("vel")]
	public class Velovity
	{
		private double m_dblSpeed;
		private double m_dblHeading;

		/// <summary>
		/// Speed over ground in M/S
		/// </summary>
		[XmlElement("speed")]
		public double Speed
		{
			get {return m_dblSpeed; }
			set { m_dblSpeed = value; }
		}

		/// <summary>
		/// Heading over ground.
		/// </summary>
		[XmlElement("heading")]
		public double Heading
		{
			get { return m_dblHeading; }
			set { m_dblHeading = value; }
		}
	}
}

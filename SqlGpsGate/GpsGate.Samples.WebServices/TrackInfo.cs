using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	/// <summary>
	/// Represents meta data for a track.
	/// </summary>
	[XmlRoot("trackInfo")]
	public class TrackInfo
	{
		private int m_iID;
		private int m_iOwnerID;
		private string m_strName;
		private double m_dblDistance;
		private BoundingBox m_Bounds;

		/// <summary>
		/// ID of the track info.
		/// </summary>
		[XmlElement("id")]
		public int ID
		{
			get { return m_iID; }
			set { m_iID = value; }
		}

		/// <summary>
		/// ID of the owner.
		/// </summary>
		[XmlElement("ownerId")]
		public int OwnerID
		{
			get { return m_iOwnerID; }
			set { m_iOwnerID = value; }
		}

		/// <summary>
		/// Name of the TrackInfo
		/// </summary>
		[XmlElement("name")]
		public string Name
		{
			get { return m_strName; }
			set { m_strName = value; }
		}

		/// <summary>
		/// The tracks total distance.
		/// </summary>
		[XmlElement("totalDistance")]
		public double Distance
		{
			get { return m_dblDistance; }
			set { m_dblDistance = value; }
		}

		/// <summary>
		/// The bounding box of the track.
		/// </summary>
		[XmlElement("bounds")]
		public BoundingBox Bounds
		{
			get { return m_Bounds; }
			set { m_Bounds = value; }
		}
	}

	/// <summary>
	/// Represents the bounds in 3D including time.
	/// </summary>
	[XmlRoot("bounds")]
	public class BoundingBox
	{
		private double m_dblMinX;
		private double m_dblMaxX;
		private double m_dblMinY;
		private double m_dblMaxY;
		private double m_dblMinZ;
		private double m_dblMaxZ;
		private DateTime m_dtMin;
		private DateTime m_dtMax;

		/// <summary>
		/// Max X value. Typically represents Longitude or Easting.
		/// </summary>
		[XmlElement("maxX")]
		public double MaxX
		{
			get { return m_dblMaxX; }
			set { m_dblMaxX = value; }
		}

		/// <summary>
		/// Minimum X value. Typically Longitude or Easting.
		/// </summary>
		[XmlElement("minX")]
		public double MinX
		{
			get { return m_dblMinX; }
			set { m_dblMinX = value; }
		}

		/// <summary>
		/// Max Y value. Typically represents Latitude or Northing.
		/// </summary>
		[XmlElement("maxY")]
		public double MaxY
		{
			get { return m_dblMaxY; }
			set { m_dblMaxY = value; }
		}

		/// <summary>
		/// Min Y value. Typically represents Latitude or Northing.
		/// </summary>
		[XmlElement("minY")]
		public double MinY
		{
			get { return m_dblMinY; }
			set { m_dblMinY = value; }
		}

		/// <summary>
		/// Max Z value. Typically represents Altitude.
		/// </summary>
		[XmlElement("maxZ")]
		public double MaxZ
		{
			get { return m_dblMaxZ; }
			set { m_dblMaxZ = value; }
		}

		/// <summary>
		/// Min Z value. Typically represents Altitude.
		/// </summary>
		[XmlElement("minZ")]
		public double MinZ
		{
			get { return m_dblMinZ; }
			set { m_dblMinZ = value; }
		}

		/// <summary>
		/// Max time. Typically represents when data bound by this box was ended in time.
		/// </summary>
		[XmlElement("maxTime")]
		public DateTime MaxTime
		{
			get { return m_dtMax; }
			set { m_dtMax = value; }
		}

		/// <summary>
		/// Min time. Typically represents when data bound by this box was started in time.
		/// </summary>
		[XmlElement("minTime")]
		public DateTime MinTime
		{
			get { return m_dtMin; }
			set { m_dtMin = value; }
		}
	}
}

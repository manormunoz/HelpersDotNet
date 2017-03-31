using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	/// <summary>
	/// A trip represents the traveling between fat points.
	/// </summary>
	[XmlRoot("trip")]
	public class Trip
	{
		#region Private Fields

		private double m_dblTotDistance;
		private double m_dblMaxSpeed;
		private TrackPoint m_tpStart;
		private TrackPoint m_tpEnd;
		private double m_dblIdlePrev;
		private List<GateMessage> m_messages;

		#endregion

		#region Public Properties

		/// <summary>
		/// The <see cref="TrackPoint" /> where this trip starts. 
		/// </summary>
		[XmlElement("startTrackPoint")]
		public TrackPoint StartTrackPoint
		{
			get { return m_tpStart; }
			set { m_tpStart = value; }
		}

		/// <summary>
		/// The <see cref="TrackPoint" /> where this trip ends. 
		/// </summary>
		[XmlElement("endTrackPoint")]
		public TrackPoint EndTrackPoint
		{
			get { return m_tpEnd; }
			set { m_tpEnd = value; }
		}

		/// <summary>
		/// The total distance of this trip in meters.
		/// </summary>
		[XmlElement("distance")]
		public double Distance
		{
			get { return m_dblTotDistance; }
			set { m_dblTotDistance = value; }
		}

		/// <summary>
		/// The Max speed of this trip in M/S.
		/// </summary>
		[XmlElement("maxSpeed")]
		public double MaxSpeed
		{
			get { return m_dblMaxSpeed; }
			set { m_dblMaxSpeed = value; }
		}

		/// <summary>
		/// Gert the idle time to previous trip in seconds.
		/// </summary>
		[XmlElement("idleSecondsToPrevTrip")]
		public double IdleSecondsToPrevTrip
		{
			get { return m_dblIdlePrev; }
			set { m_dblIdlePrev = value; }
		}

		/// <summary>
		/// Get the <see cref="GateMessage">Messages</see> for which this trip was built from.
		/// </summary>
		[XmlArray("messages")]
		[XmlArrayItem("gateMessage", typeof(GateMessage))]
		public List<GateMessage> Messages
		{
			get { return m_messages; }
			set { m_messages = value; }
		}

		#endregion
	}
}

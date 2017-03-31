using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	/// <summary>
	/// Represents a User (user can be a vehicle as well as an operator)
	/// </summary>
	[XmlRoot("user")]
	public class User
	{
		/// <summary>
		/// The attributes of a user. Information about icon color etc.
		/// </summary>
		[XmlRoot("attribute")]
		public class Attribute
		{
			private string m_strName;
			private string m_strType;
			private string m_strValue;

			/// <summary>
			/// Attribute name.
			/// </summary>
			[XmlElement("name")]
			public string Name
			{
				get { return m_strName; }
				set { m_strName = value; }
			}

			/// <summary>
			/// Attribute type.
			/// </summary>
			[XmlElement("type")]
			public string Type
			{
				get { return m_strType; }
				set { m_strType = value; }
			}

			/// <summary>
			/// The attribute value.
			/// </summary>
			[XmlElement("value")]
			public string Value
			{
				get { return m_strValue; }
				set { m_strValue = value; }
			}
		}

		private int m_iID;
		private string m_strName;
		private string m_strUsername;
		private TrackPoint m_TrackPoint;
		private string m_strProtocol;
		private DateTime m_dtDeiceActivity;
		private List<Attribute> m_listAttributes;

		/// <summary>
		/// ID of the user.
		/// </summary>
		[XmlElement("id")]
		public int ID
		{
			get { return m_iID; }
			set { m_iID = value; }
		}

		/// <summary>
		/// Name of the user.
		/// </summary>
		[XmlElement("name")]
		public string Name
		{
			get { return m_strName; }
			set {m_strName = value; }
		}

		/// <summary>
		/// Username.
		/// </summary>
		[XmlElement("username")]
		public string Username
		{
			get { return m_strUsername; }
			set { m_strUsername = value; }
		}

		/// <summary>
		/// The last position of the user.
		/// </summary>
		[XmlElement("trackPoint")]
		public TrackPoint TrackPoint
		{
			get { return m_TrackPoint; }
			set { m_TrackPoint = value; }
		}

		/// <summary>
		/// Last protocol for which the user has been connected over.
		/// </summary>
		[XmlElement("lastTransportProtocol")]
		public string LastTransportProtocol
		{
			get { return m_strProtocol; }
			set { m_strProtocol = value; }
		}

		/// <summary>
		/// The timestamp for the last connection activity of the user.
		/// </summary>
		[XmlElement("deviceActivity")]
		public DateTime DeviceActivity
		{
			get { return m_dtDeiceActivity; }
			set { m_dtDeiceActivity = value; }
		}
		
		/// <summary>
		/// List of attributes user attributes.
		/// </summary>
		[XmlArray("attributes")]
		[XmlArrayItem("attribute", typeof(Attribute))]
		public List<Attribute> Attributes
		{
			get { return m_listAttributes; }
			set { m_listAttributes = value; }
		}
	}
}

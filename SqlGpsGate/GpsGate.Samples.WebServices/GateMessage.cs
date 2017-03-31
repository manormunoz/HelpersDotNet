using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	/// <summary>
	/// Represents a GateMessage.
	/// 
	/// GateMessage will carry data in numeric, text, binary or/and bool data together with a position.
	/// The position as well as the dataContent is optional.
	/// 
	/// The data is stored in GateMessageRecords.
	/// </summary>
	[XmlRoot("gateMessage")]
	public class GateMessage
	{
		private TrackPoint m_TrackPoint;
		private List<GateMessageRecord> m_Records;

		/// <summary>
		/// The TrackPoint for which this message where reported.
		/// </summary>
		[XmlElement("trackPoint")]
		public TrackPoint TrackPoint
		{
			get { return m_TrackPoint; }
			set { m_TrackPoint = value; }
		}

		/// <summary>
		/// Records of data for this GateMessage.
		/// </summary>
		[XmlArray("records")]
		[XmlArrayItem("gateMessageRecord", typeof(GateMessageRecord))]
		public List<GateMessageRecord> Records
		{
			get { return m_Records; }
			set { m_Records = value; }
		}
	}

	/// <summary>
	/// Represents one data record in a GateMessage.
	/// </summary>
	[XmlRoot("gateMessageRecord")]
	public class GateMessageRecord
	{
		private Field m_Field;
		private RecordData m_Data;

		/// <summary>
		/// Defines the meta data and propertis of the data.
		/// </summary>
		[XmlElement("field")]
		public Field Field
		{
			get { return m_Field; }
			set { m_Field = value; }
		}

		/// <summary>
		/// The data.
		/// </summary>
		[XmlElement("recordData")]
		public RecordData Data
		{
			get { return m_Data; }
			set { m_Data = value; }
		}
	}

	/// <summary>
	/// A message field defines properties and meta information such as system type, unit, namespaces etc. of message data.
	/// </summary>
	[XmlRoot("field")]
	public class Field
	{
		private string m_strName;
		private string m_strUnit;

		[XmlElement("name")]
		public string Name
		{
			get { return m_strName; }
			set { m_strName = value; }
		}

		[XmlElement("unitName")]
		public string Unit
		{
			get { return m_strUnit; }
			set { m_strUnit = value; }
		}
	}

	/// <summary>
	/// The data container.
	/// </summary>
	[XmlRoot("recordData")]
	public class RecordData
	{
		private string m_strData;
		private string m_strType;

		/// <summary>
		/// The data.
		/// </summary>
		[XmlElement("data")]
		public string Data
		{
			get { return m_strData; }
			set { m_strData = value; }
		}

		/// <summary>
		/// Type of data.
		/// </summary>
		[XmlElement("dataType")]
		public string DataType
		{
			get { return m_strType; }
			set { m_strType = value; }
		}
	}
}

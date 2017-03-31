using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	/// <summary>
	/// Represents a view.
	/// 
	/// A view is built up by tags for which users can be tagged with along with other filters such as connection status filter.
	/// </summary>
	[XmlRoot("view")]
	public class View
	{
		private int m_iID;
		private string m_strName;
		private string m_strDescription;
		private bool m_bMatchAllTags;
		private string m_strStatusFilters;
		private List<int> m_Tags;

		/// <summary>
		/// ID of the view.
		/// </summary>
		[XmlElement("id")]
		public int ID
		{
			get { return m_iID; }
			set { m_iID = value; }
		}

		/// <summary>
		/// Name of the view.
		/// </summary>
		[XmlElement("name")]
		public string Name
		{
			get { return m_strName; }
			set { m_strName = value; }
		}

		/// <summary>
		/// Short description of the view.
		/// </summary>
		[XmlElement("description")]
		public string Description
		{
			get { return m_strDescription; }
			set { m_strDescription = value; }
		}

		/// <summary>
		/// Indicates if the users must be member of all tags or any tag.
		/// </summary>
		[XmlElement("matchAllTags")]
		public bool MatchAllTags
		{
			get { return m_bMatchAllTags; }
			set { m_bMatchAllTags = value; }
		}

		/// <summary>
		/// Comma sepparated list of connection status which this view is based on
		/// Possible values
		/// Connected, NeverBeenConnected, Offline and Sending.
		/// 
		/// None means ignore this filter.
		/// </summary>
		[XmlElement("statusFilter")]
		public string StatusFilter
		{
			get { return m_strStatusFilters; }
			set { m_strStatusFilters = value; }
		}

		/// <summary>
		/// List of tag IDs for which this View is based on.
		/// </summary>
		[XmlArray("tagIDs")]
		public List<int> Tags
		{
			get { return m_Tags; }
			set { m_Tags = value; }
		}
	}
}

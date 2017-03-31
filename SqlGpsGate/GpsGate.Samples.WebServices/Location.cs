using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
	[XmlRoot("loc")]
	public class Location
	{
		private Position m_Position;
		private string m_strCity;
		private string  m_strCountry;
		private string  m_strPostalCode;
		private string m_strStreetBox;
		private string m_strStreetNumber;
		private string m_strAddress;
		private string  m_strStreetName;
		private string m_strAdministrativeAreaName;
		private string m_strSubAdministrativeAreaName;
		private string m_strFormattedResult;

		[XmlElement("countryName")]
		public string  Country
		{
			get { return m_strCountry;}
			set { m_strCountry = value;}
		}

		[XmlElement("cityName")]
		public string City
		{
			get { return m_strCity;}
			set { m_strCity = value;}
		}

		[XmlElement("postalCodeNumber")]
		public string  PostalCode
		{
			get { return m_strPostalCode;}
			set { m_strPostalCode = value;}
		}

		[XmlElement("streetBox")]
		public string StreetBox
		{
			get { return m_strStreetBox;}
			set { m_strStreetBox = value;}
		}

		[XmlElement("streetName")]
		public string  Street
		{
			get { return m_strStreetName;}
			set { m_strStreetName = value;}
		}

		[XmlElement("streetNumber")]
		public string StreetNumber
		{
			get { return m_strStreetNumber;}
			set { m_strStreetNumber = value;}
		}

		[XmlElement("address")]
		public string Address
		{
			get { return m_strAddress; }
			set { m_strAddress = value; }
		}

		[XmlElement("administrativeAreaName")]
		public string AdministrativeArea
		{
			get { return m_strAdministrativeAreaName; }
			set { m_strAdministrativeAreaName = value; }
		}

		[XmlElement("subAdministrativeAreaName")]
		public string SubAdministrativeArea
		{
			get { return m_strSubAdministrativeAreaName; }
			set { m_strSubAdministrativeAreaName = value; }
		}

		[XmlElement("formattedResult")]
		public string FormattedResult
		{
			get { return m_strFormattedResult; }
			set { m_strFormattedResult = value; }
		}

		[XmlElement("pos")]
		public Position Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}
	}
}

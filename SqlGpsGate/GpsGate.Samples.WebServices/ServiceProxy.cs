using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GpsGate.Samples.SOAP
{
    /// <summary>
    /// Service proxy object used for communicating with the web services and transform response XML to object representations.	
    /// </summary>
    public class ServiceProxy
    {
        /// <summary>
        /// Web Service reference
        /// </summary>
        private DirectoryService.Directory m_Directory = new DirectoryService.Directory();
        private TracksService.Tracks m_Tracks = new TracksService.Tracks();
        private GeocoderService.Geocoder m_Geocoder = new GeocoderService.Geocoder();
        private ReportingService.Reporting m_Reporting = new ReportingService.Reporting();

        // Current Session ID
        private string m_strSessionID;

        // Current Application ID
        private int m_iApplicationID;

        // Simgleton Instance of the service Proxy
        private static ServiceProxy m_SimpleService = new ServiceProxy();

        /// <summary>
        /// Get the ServiceProxy object.
        /// </summary>
        /// <returns></returns>
        public static ServiceProxy GetSimpleService()
        {
            return m_SimpleService;
        }

        /// <summary>
        /// Private constructor.
        /// SimpleService is used as a singleton.
        /// </summary>
        private ServiceProxy()
        { }

        #region Directory Service

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="strUsername">Username</param>
        /// <param name="strPassword">Password</param>
        /// <param name="iApplicationID">ID of the application to logon to.</param>
        /// <returns>SessionID string used in following service calls.</returns>
        public string Login(string strUsername, string strPassword, int iApplicationID)
        {
            // Call the Directory WebService Login.
            XmlNode xmlResponse = m_Directory.Login(strUsername, strPassword, iApplicationID);

            // Check response for errors in response.
            CheckError(xmlResponse);

            // Get the session id returned by GpsGate Server.
            m_strSessionID = xmlResponse.InnerText;

            // Set internal application ID prtoperty.
            m_iApplicationID = iApplicationID;

            return m_strSessionID;
        }

        /// <summary>
        /// Get latest GateRecords for a user.
        /// </summary>
        /// <param name="iUserID">ID of the user.</param>
        /// <param name="bFilterNotUsed">Get only records created by the current device mapping.</param>
        /// <returns>List of XmlGateMessge representations.</returns>
        public List<GateMessage> GetLatestGateRecords(int iUserID, bool bFilterNotUsed)
        {
            List<GateMessage> retMessages = new List<GateMessage>();

            // Call Directory WebService GetLatestGateRecords
            XmlNode xmlResponse = m_Directory.GetLatestGateRecords(m_strSessionID, m_iApplicationID, iUserID, bFilterNotUsed);

            // Check Repsone for errors.
            CheckError(xmlResponse);

            // Deserialize the result into XmlGateMessage
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found a new gateMessage node to deserialize.
                    if (reader.Name == "gateMessage")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(GateMessage));

                        // Add gateMessage representation to return list.
                        retMessages.Add((GateMessage)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retMessages;
        }

        /// <summary>
        /// Get users for a given group with optional filtering by view.
        /// </summary>
        /// <param name="strGroupName">Name of group for which to receive users from.</param>
        /// <param name="iViewID">ID of the view to filter by.</param>
        /// <returns>List of XmlUser representations.</returns>
        public List<User> GetUsers(string strGroupName, int iViewID)
        {
            List<User> retUsers = new List<User>();

            // Call the Directory WebService GetUsersInGroup
            XmlNode xmlResponse = m_Directory.GetUsersInGroup(m_strSessionID, m_iApplicationID, strGroupName, iViewID);

            // Check response for errors.
            CheckError(xmlResponse);

            // Desserialize the result into XmlUsers
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found a new user node to deserialize.
                    if (reader.Name == "user")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(User));

                        // Add user representation to return list.
                        retUsers.Add((User)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retUsers;
        }

        /// <summary>
        /// Get users for a given group with optional filtering by view and who has been sending since the given timestamp.
        /// </summary>
        /// <param name="strGroupName">Name of group for which to receive users from.</param>
        /// <param name="iStartIndex">Start index to limit the result.</param>
        /// <param name="iEndIndex">End index to limit the result.</param>
        /// <param name="iViewID">ID of the view to filter by.</param>
        /// <param name="dtUtcSince">UTC TimeStamp</param>
        /// <returns>List of XmlUer representations.</returns>
        public List<User> GetUpdatedUsers(string strGroupName, int iStartIndex, int iEndIndex, int iViewID, DateTime dtUtcSince)
        {
            List<User> retUsers = new List<User>();

            // Call Directory WebService GetUpdatedUsersInGroup
            XmlNode xmlResponse = m_Directory.GetUpdatedUsersInGroup(
                m_strSessionID,
                m_iApplicationID,
                strGroupName,
                iStartIndex,
                iEndIndex,
                iViewID,
                dtUtcSince
            );

            // Chekc response for errors.
            CheckError(xmlResponse);

            // Desserialize the result into XmlUsers
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found a new user node to deserialize.
                    if (reader.Name == "user")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(User));

                        // Add user representation to return list.
                        retUsers.Add((User)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retUsers;
        }

		/// <summary>
		/// Get users for a given user template ID.
		/// </summary>
		/// <param name="iUserTemplateID">IDs of user template.</param>
		/// <returns>List of XmlUser representations.</returns>
		public List<User> GetUsersInUserTemplate(int iUserTemplateID)
		{
			List<User> retUsers = new List<User>();

			XmlNode xmlResponse = m_Directory.GetUsersInUserTemplate(m_strSessionID, m_iApplicationID, iUserTemplateID);

			// Check response for errors.
			CheckError(xmlResponse);

			// Desserialize the result into XmlUsers
			using (XmlReader reader = new XmlNodeReader(xmlResponse))
			{
				while (reader.Read())
				{
					// Found a new user node to deserialize.
					if (reader.Name == "user")
					{
						XmlSerializer serializer = new XmlSerializer(typeof(User));

						// Add user representation to return list.
						retUsers.Add((User)serializer.Deserialize(reader.ReadSubtree()));
					}
				}
			}

			return retUsers;
		}

        /// <summary>
        /// Get all views.
        /// </summary>
        /// <returns>List of XmlView representations.</returns>
        public List<View> GetViews()
        {
            List<View> retViews = new List<View>();

            // Call Directory WebService GetViews
            XmlNode xmlResponse = m_Directory.GetViews(m_strSessionID, m_iApplicationID);

            // Check response for errors.
            CheckError(xmlResponse);

            // Desserialize the result into XmlViews
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found a new view node to desserialize.
                    if (reader.Name == "view")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(View));

                        // Add view repsentation to return list.
                        retViews.Add((View)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retViews;
        }

        /// <summary>
        /// Get all gate messages for a given track info.
        /// </summary>
        /// <param name="iTrackInfoID">ID of the track info for which gate messages should be returned.</param>
        /// <param name="dtUtcStart">UTC start time stamp.</param>
        /// <param name="dtUtcEnd">UTC end time stamp.</param>
        /// <param name="iStartIndex">Start index.</param>
        /// <param name="iStopIndex">End index.</param>
        /// <param name="bFilterInvalid">Filter gateMessages with track points marked as invalid.</param>
        /// <param name="fieldNames">string array of field names. E.g {"Speed", "SOS", "Engine on", "Fuel level"}</param>
        /// <returns>List of XmlGateMessage representations.</returns>
        public List<GateMessage> GetGateMessageByTrackInfoId(int iTrackInfoID, DateTime dtUtcStart, DateTime dtUtcEnd, int iStartIndex, int iStopIndex, bool bFilterInvalid, string[] fieldNames)
        {
            List<GateMessage> retMessages = new List<GateMessage>();

            // Call Directory WebService GetGateMessagesByTrackInfoId
            XmlNode xmlResponse = m_Directory.GetGateMessagesByTrackInfoId(
                m_strSessionID,
                m_iApplicationID,
                iTrackInfoID,
                dtUtcStart,
                dtUtcEnd,
                iStartIndex,
                iStopIndex,
                bFilterInvalid,
                fieldNames

            );

            // Check response for errors.
            CheckError(xmlResponse);

            // Deserialize the response into gateMessage representations.
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found new gateMessage node to deserialize.
                    if (reader.Name == "gateMessage")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(GateMessage));

                        // Add gateMessage representation to return list.
                        retMessages.Add((GateMessage)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retMessages;
        }

		/// <summary>
		/// Get manageable device definitions.
		/// </summary>
		/// <returns>IEnumerable of DirectoryService.SoapDeviceDefinition representations.</returns>
		public IEnumerable<DirectoryService.SoapDeviceDefinition> GetManageableDeviceDefinitions()
		{
			return m_Directory.GetManageableDeviceDefinitions(m_strSessionID, m_iApplicationID);			
		}

		/// <summary>
		/// Get user templates.
		/// </summary>
		/// <returns>IEnumerable of DirectoryService.SoapUserTemplate representations.</returns>
		public IEnumerable<DirectoryService.SoapUserTemplate> GetUserTemplates()
		{
			return m_Directory.GetUserTemplates(m_strSessionID, m_iApplicationID);
		}

		/// <summary>
		/// Adds a user with the provided user template ID.
		/// </summary>
		/// <param name="iUserTemplateID"></param>
		/// <param name="strUsername"></param>
		/// <param name="strName"></param>
		/// <param name="strPassword"></param>
		/// <returns>ID of the new user</returns>
		public int AddUserWithUserTemplate(int iUserTemplateID, string strUsername, string strName, string strPassword)
		{
			return m_Directory.AddUserWithUserTemplate(m_strSessionID, m_iApplicationID, iUserTemplateID, strUsername, strName, strPassword);
		}

		/// <summary>
		/// Adds a device to the provided owner user ID.
		/// </summary>
		/// <param name="strSessionID"></param>
		/// <param name="iApplicationID"></param>
		/// <param name="iOwnerID">User ID of the user that will be the owner of the device.</param>
		/// <param name="strDeviceName"></param>
		/// <param name="strIMEI"></param>
		/// <param name="strMSISDN"></param>
		/// <param name="strEmail"></param>
		/// <param name="strIP"></param>
		/// <param name="strPort"></param>
		/// <param name="strAPN"></param>
		/// <param name="strGPRSUsername"></param>
		/// <param name="strGPRSPassword"></param>
		/// <param name="iDeviceDefinitionID"></param>
		/// <param name="iMobileNetworkID"></param>
		/// <param name="strDevicePassword"></param>
		/// <returns></returns>
		public void AddDevice(int iOwnerID, string strDeviceName, string strIMEI, string strMSISDN, string strEmail, string strIP, string strPort, string strAPN, string strGPRSUsername, string strGPRSPassword, int iDeviceDefinitionID, int iMobileNetworkID, string strDevicePassword)
		{
			XmlNode xmlResponse = m_Directory.AddDevice(m_strSessionID, m_iApplicationID, iOwnerID, strDeviceName, strIMEI, strMSISDN, strEmail, strIP, strPort, strAPN, strGPRSUsername, strGPRSPassword, iDeviceDefinitionID, iMobileNetworkID, strDevicePassword, new DirectoryService.OneWireBag[] {});

			// Check response for errors.
			CheckError(xmlResponse);
		}

		/// <summary>
		/// Get accumulator by name and user.
		/// </summary>
		/// <returns>DirectoryService.SoapAccumulator</returns>
		public DirectoryService.SoapAccumulator GetAccumulator(int iUserID, string strName)
		{
			return m_Directory.GetAccumulator(m_strSessionID, m_iApplicationID, iUserID, strName);
		}

		/// <summary>
		/// Get latest value for the given accumulator ID.
		/// </summary>
		/// <param name="iAccumulatorID"></param>
		/// <returns>DirectoryService.SoapAccumulatorData</returns>
		public DirectoryService.SoapAccumulatorData GetLatestAccumulatorValue(int iAccumulatorID)
		{
			return m_Directory.GetLatestAccumulatorValue(m_strSessionID, m_iApplicationID, iAccumulatorID);
		}

		/// <summary>
		/// Get value of the specified accumulator at the given point in time for a user.
		/// </summary>
		/// <param name="iAccumulatorID"></param>
		/// <param name="dtTime"></param>
		/// <returns></returns>
		public DirectoryService.SoapAccumulatorData GetAccumulatorValue(int iAccumulatorID, DateTime dtTime)
		{
			return m_Directory.GetAccumulatorValue(m_strSessionID, m_iApplicationID, iAccumulatorID, dtTime);
		}

        #endregion

        #region Tracks Service

        /// <summary>
        /// Return <see cref="Trip">Trips</see> for the given user and timespan.
        /// 
        /// The <see cref="Trip">Trips</see> returned are splitted based by <see cref="XmlFatPoint" /> and <see cref="TrackInfo" /> data.
        /// </summary>
        /// <param name="iUserID">DB id of the user for which to get trips.</param>
        /// <param name="dtUtcStart">Start UTC date and time.</param>
        /// <param name="dtUtcEnd">End UTC date and time.</param>
        /// <returns></returns>
        public List<Trip> GetTripsByUser(int iUserID, DateTime dtUtcStart, DateTime dtUtcEnd)
        {
            List<Trip> retTrips = new List<Trip>();

            // Call Track WebService GetTripsByUser
            XmlNode xmlResponse = m_Tracks.GetTripsByUser(m_strSessionID, m_iApplicationID, iUserID, dtUtcStart, dtUtcEnd);

            // Check response for errors.
            CheckError(xmlResponse);

            // Deserialize the result into XmlTrip representations.
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Trip));
                while (reader.Read())
                {
                    // Found a new trip node to deserialize.
                    if (reader.Name == "trip")
                    {
                        Trip trip = (Trip)serializer.Deserialize(reader.ReadSubtree());
                        retTrips.Add(trip);
                    }
                }
            }

            return retTrips;
        }

        /// <summary>
        /// Get track info for a list of users.
        /// </summary>
        /// <param name="userIDs">Array of user id's for which TrackInfo's should be returned.</param>
        /// <param name="dtUtcStart">Start UTC time stamp</param>
        /// <param name="dtUtcEnd">End UTC time stamp</param>
        /// <returns>List of XmlTrackInfo representations.</returns>
        public List<TrackInfo> GetTracksByUsers(int[] userIDs, DateTime dtUtcStart, DateTime dtUtcEnd)
        {
            List<TrackInfo> retTracks = new List<TrackInfo>();

            // Call Track WebService GetTracksByUsers
            XmlNode xmlResponse = m_Tracks.GetTracksByUsers(m_strSessionID, m_iApplicationID, userIDs, dtUtcStart, dtUtcEnd);

            // Check response for errors.
            CheckError(xmlResponse);

            // Deserialize the result into XmlTrackInfo representations.
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found a new trackInfo node to deserialize.
                    if (reader.Name == "trackInfo")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(TrackInfo));

                        // Add the XmlTrackInfo representation to return list.
                        retTracks.Add((TrackInfo)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retTracks;
        }

        #endregion

        #region Geocoder Service

        /// <summary>
        /// Address lookup an array of positions.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public List<Location> ReverseGeocode(Position[] positions)
        {
            List<Location> retLocations = new List<Location>();

            decimal[] arrLats = new decimal[positions.Length];
            decimal[] arrLngs = new decimal[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                arrLats[i] = (decimal)positions[i].Latitude;
                arrLngs[i] = (decimal)positions[i].Longitude;
            }

            // Call Geocoder WebService ReverseGeocode
            XmlNode xmlResponse = m_Geocoder.ReverseGeocode(m_strSessionID, m_iApplicationID, arrLats, arrLngs);

            // Check response for errors.
            CheckError(xmlResponse);

            // Deserialize the result into XmlLocation representations.
            using (XmlReader reader = new XmlNodeReader(xmlResponse))
            {
                while (reader.Read())
                {
                    // Found a new loc node to deserialize.
                    if (reader.Name == "loc")
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Location));

                        // Add the XmlLocation representation to return list.
                        retLocations.Add((Location)serializer.Deserialize(reader.ReadSubtree()));
                    }
                }
            }

            return retLocations;
        }

        #endregion

        #region Reporting Service
        /// <summary>
        /// Gets list of reports for selected application
        /// </summary>
        /// <returns>xml document with collection of report names and ids</returns>
        public XmlNode GetReports()
        {
            //call web service for current session and application
            return m_Reporting.GetReports(m_strSessionID, m_iApplicationID);
        }

        /// <summary>
        /// Start Generating report
        /// </summary>
        /// <param name="iReportID">report id</param>
        /// <param name="dtStartDate">start date of report data period</param>
        /// <param name="dtEndDate">end date of report data period</param>
        /// <returns>xml document with handle for report processing</returns>
        public XmlNode GenerateReport(int iReportID, DateTime dtStartDate, DateTime dtEndDate)
        {
            //call web service for current session, appication, report and period
            return m_Reporting.GenerateReport(m_strSessionID, iReportID, dtStartDate, dtEndDate);
        }

        /// <summary>
        /// Get report status <i>this is used to check if report finish processing</i>
        /// </summary>
        /// <param name="iHandleID">Handle id, returned by the generatereport method</param>
        /// <returns>xml document with new handle data</returns>
        public XmlNode GetReportStatus(int iHandleID)
        {
            return m_Reporting.GetReportStatus(m_strSessionID, iHandleID);
        }

        /// <summary>
        /// Fetch report xml document, this method should only be called when handle
        /// from GenerateReport and GetReportStatus has <b>Done</b> status
        /// </summary>
        /// <param name="iHandleID">Handle id, returned by the generatereport method</param>
        /// <returns>xml document with report data and style</returns>
        public XmlNode FetchReport(int iHandleID)
        {
            return m_Reporting.FetchReport(m_strSessionID, iHandleID);
        }

        /// <summary>
        /// gets list of currently processing reports
        /// </summary>
        /// <returns>a xml document with the list of handlers for reports, this
        /// list should typically have only at most 1 element</returns>
        public XmlNode GetProcessingReports()
        {
            return m_Reporting.GetProcessingReports(m_strSessionID, m_iApplicationID);
        }


        /// <summary>
        /// cancel processing report
        /// </summary>
        /// <param name="iHandleID">Handle id, returned by the generatereport method</param>
        /// <returns></returns>
        public XmlNode CancelReport(int iHandleID)
        {
            return m_Reporting.CancelReport(m_strSessionID, iHandleID);
        }

        #endregion

        /// <summary>
        /// Check the response for error.
        /// 
        /// If exception node found. this method throws a new Exception with the error message.
        /// </summary>
        /// <param name="xml"></param>
        private static void CheckError(XmlNode xml)
        {
            if (xml != null && xml.FirstChild != null && xml.FirstChild.Name == "exception")
            {
                throw new Exception(xml.SelectSingleNode("//exception/message").InnerText);
            }
        }
    }
}

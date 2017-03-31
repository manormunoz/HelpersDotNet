using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Diagnostics;
using Newtonsoft.Json;

namespace SqlGpsGate
{
    public partial class RealTime : Form
    {
        string vehicle_id;
        List<Models.RealTime> dataSource = new List<Models.RealTime>();
        System.Windows.Forms.Timer t;

        DateTime dt_f = DateTime.UtcNow.AddDays(-1);
        DateTime dt_t = DateTime.UtcNow.AddDays(1);
        int count = 0;

        public RealTime(string vehicle_id)
        {
            this.vehicle_id = vehicle_id;
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;

            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

        }

        private void RealTime_Load(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            this.toolStripProgressBar1.Visible = true;

            SqlConnection conn = new SqlConnection();
            using (MySqlConnection mysqlConnection = conn.Get())
            {
                string query = @"SELECT user_id, name, IMEI 
                                 FROM users 
                                 INNER JOIN device
                                    ON users.user_id = device.owner_id
                                 WHERE 
                                    active = true AND 
                                    user_id = " + vehicle_id + @" AND 
                                    IMEI IS NOT NULL 
                                 ORDER BY name ;";
                mysqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, mysqlConnection))
                {
                    //Create a data reader and Execute the command
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            this.label2.Text = dataReader["name"].ToString();
                            this.label3.Text = dataReader["IMEI"].ToString();

                        }

                        dataReader.Close();
                    }
                }
                mysqlConnection.Close();

                dt_t = DateTime.UtcNow.AddMinutes(1);
                BackgroundWorker worker = sender as BackgroundWorker;

                SqlConnection conn2 = new SqlConnection();

                using (MySqlConnection mysqlConnection2 = conn.Get())
                {
                    string query2 = @"  SELECT 
                                        td.track_info_id, 
                                        td.sequence_num, 
                                        td.latitude_mod AS latitude, 
                                        td.longitude_mod AS longitude, 
                                        td.altitude, 
                                        td.heading_mod AS heading, 
                                        td.ground_speed_mod AS ground_speed, 
                                        td.time_stamp, 
                                        td.milliseconds, 
                                        td.valid_mod AS valid,
                                        gmr.data_bool,
                                        gmr.data_double,
	                                    gmr.data_text,
                                        msg.name,
                                        msg.unit_name,
	                                    ti.owner_id
                                    FROM track_data_v6 td 
	                                INNER JOIN track_info ti 
	                                       ON ti.track_info_id = td.track_info_id
	                                LEFT JOIN gate_message_record_v6 gmr 
	                                       ON gmr.track_info_id = td.track_info_id AND gmr.sequence_num = td.sequence_num
	                                LEFT JOIN msg_field msg 
	                                       ON msg.msg_field_id = gmr.msg_field_id
                                    WHERE td.deleted_mod != 1
	                                     AND ti.owner_id = " + vehicle_id + @"
	                                     AND td.time_stamp BETWEEN '" + dt_f.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + dt_t.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + @"'
                                    ORDER BY td.time_stamp,
		                                    td.milliseconds,
		                                    td.sequence_num;";
                    mysqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query2, mysqlConnection2))
                    {
                        //Create a data reader and Execute the command
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            List<string[]> arrayList = new List<string[]>();
                            while (dataReader.Read())
                            {
                                string[] variables = { 
                                        dataReader["name"].ToString(), 
                                        dataReader["data_bool"].ToString() + dataReader["data_double"].ToString() + dataReader["data_text"].ToString(),
                                        dataReader["unit_name"].ToString() 
                                     };
                                if (arrayList.Any(x => x[0] == variables[0]))
                                {
                                    arrayList.RemoveAll(x => x[0] == variables[0]);

                                }
                                if (!string.IsNullOrWhiteSpace(variables[0]))
                                    arrayList.Add(variables);

                                string json = "";
                                foreach (var al in arrayList)
                                {
                                    var keyValues = new Dictionary<string, string>
                                   {
                                       { "name", al[0] },
                                       { "data", al[1] },
                                       { "unit", al[2] }
                                   };
                                    json += JsonConvert.SerializeObject(keyValues) + ",";
                                }
                                dataSource.Add(new Models.RealTime()
                                {
                                    Id = ++count,
                                    Date = dataReader["time_stamp"].ToString(),
                                    Latitude = dataReader["latitude"].ToString(),
                                    Longitude = dataReader["longitude"].ToString(),
                                    MsgName = (json.Length > 0) ? "{" + json.Remove(json.Length - 1) + "}" : "",
                                    Valid = dataReader["valid"].ToString()
                                });



                            }

                            dataReader.Close();
                        }
                    }
                    mysqlConnection.Close();

                }

                dataSource = dataSource.AsEnumerable().OrderByDescending(x => x.Id).ToList();

                dt_f = DateTime.UtcNow;
                this.dataGridView1.DataSource = dataSource;
                this.toolStripProgressBar1.Visible = false;






                t = new System.Windows.Forms.Timer();
                t.Tick += new EventHandler(t_Elapsed);
                t.Interval = 15000;
                t.Start();
                //this.t_Elapsed(new object(), new EventArgs());

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                   ts.Hours, ts.Minutes, ts.Seconds,
                                                   ts.Milliseconds / 10);
                toolStripStatusLabel1.Text = "Runtime: " + elapsedTime;


            }
        }

        void t_Elapsed(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {

                this.toolStripProgressBar1.Visible = true;
                backgroundWorker1.RunWorkerAsync();

            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            dt_t = DateTime.UtcNow.AddMinutes(1);

            string json = "";
            List<string[]> arrayList = new List<string[]>();
            SqlConnection conn = new SqlConnection();
            using (MySqlConnection mysqlConnection = conn.Get())
            {
                string query = @"  SELECT 
                                        msg.type,
                                        msg.name,
                                        msg.unit_name,
                                        grl.data_bool,
                                        grl.data_double,
                                        grl.data_text
                                    FROM gate_record_latest_v2 grl
                                    JOIN device d 
                                        ON d.owner_id = grl.user_id 
                                    JOIN msg_field_dictionary_entry mfde 
                                        ON mfde.msg_field_dict_id = d.msg_field_dict_id AND mfde.out_msg_field_id = grl.msg_field_id 
                                    JOIN msg_field msg
                                        ON grl.msg_field_id = msg.msg_field_id
                                    WHERE grl.user_id = " + vehicle_id + ";";
                mysqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, mysqlConnection))
                {
                    //Create a data reader and Execute the command
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            string[] variables = { 
                                        dataReader["name"].ToString(), 
                                        dataReader["data_bool"].ToString() + dataReader["data_double"].ToString() + dataReader["data_text"].ToString(),
                                        dataReader["unit_name"].ToString()};
                           
                            arrayList.Add(variables);

                           


                        }

                        foreach (var al in arrayList)
                        {
                            var keyValues = new Dictionary<string, string>
                                   {
                                       { "name", al[0] },
                                       { "data", al[1] },
                                       { "unit", al[2] }
                                   };
                            json += JsonConvert.SerializeObject(keyValues) + ",";
                        }

                        dataReader.Close();
                    }
                }
                mysqlConnection.Close();
            }









            SqlConnection conn2 = new SqlConnection();

            using (MySqlConnection mysqlConnection2 = conn2.Get())
            {
                string query2 = @"  SELECT 
                                        td.track_info_id, 
                                        td.sequence_num, 
                                        td.latitude_mod AS latitude, 
                                        td.longitude_mod AS longitude, 
                                        td.altitude, 
                                        td.heading_mod AS heading, 
                                        td.ground_speed_mod AS ground_speed, 
                                        td.time_stamp, 
                                        td.milliseconds, 
                                        td.valid_mod AS valid,
	                                    ti.owner_id
                                    FROM track_data_v6 td 
	                                     INNER JOIN track_info ti 
	                                       ON ti.track_info_id = td.track_info_id
	                                    
	                                WHERE td.deleted_mod != 1
	                                     AND ti.owner_id = " + vehicle_id + @"
	                                     AND td.time_stamp BETWEEN '" + dt_f.ToString("yyyy-MM-dd HH:mm") + "' AND '" + dt_t.ToString("yyyy-MM-dd HH:mm") + @"'
                                    ORDER BY td.time_stamp,
		                                    td.milliseconds,
		                                    td.sequence_num;";
                mysqlConnection2.Open();

                using (MySqlCommand cmd = new MySqlCommand(query2, mysqlConnection2))
                {
                    //Create a data reader and Execute the command
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            
                            dataSource.Add(new Models.RealTime()
                            {
                                Id = ++count,
                                Date = dataReader["time_stamp"].ToString(),
                                Latitude = dataReader["latitude"].ToString(),
                                Longitude = dataReader["longitude"].ToString(),
                                MsgName = (json.Length > 0) ? "{" + json.Remove(json.Length - 1) + "}" : "",
                                Valid = dataReader["valid"].ToString()
                            });



                        }

                        dataReader.Close();
                    }
                }
                mysqlConnection2.Close();

            }

            System.Threading.Thread.Sleep(1000);

        }



        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error");
            
            }
            dataSource = dataSource.AsEnumerable().OrderByDescending(x => x.Id).ToList();
            
            dt_f = DateTime.UtcNow;
            this.dataGridView1.DataSource = dataSource;
            this.toolStripProgressBar1.Visible = false;



        }

        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            do
            {
                t.Stop();
                System.Threading.Thread.Sleep(100);
            } while (backgroundWorker1.IsBusy == true);
        }
    }
}

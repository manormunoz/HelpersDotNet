using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NLog;
using MySql.Data.MySqlClient;
using System.Diagnostics;



namespace SqlGpsGate
{
    public partial class Form1 : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
        }

        string app_id = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gpsgateserverDataSet.track_data_v6' table. You can move, or remove it, as needed.

            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Value = DateTime.UtcNow.AddDays(-1);
            this.dateTimePicker2.Value = DateTime.UtcNow;

            SqlConnection conn = new SqlConnection();
            using (MySqlConnection mysqlConnection = conn.Get())
            {
                string query = @"SELECT application_id, name FROM applications where deleted = false AND bo_type = 'GpsGate.VehicleTracker.VehicleTrackerApplication' ORDER BY name;";

                mysqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, mysqlConnection))
                {
                    //Create a data reader and Execute the command
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        var dataSourceApp = new List<Models.Combo>();
                        while (dataReader.Read())
                        {
                            dataSourceApp.Add(new Models.Combo() { Value = dataReader["application_id"].ToString(), Name = dataReader["name"].ToString() });

                        }
                        this.comboBox1.DataSource = dataSourceApp;
                        this.comboBox1.DisplayMember = "Name";
                        this.comboBox1.ValueMember = "Value";
                        this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                        dataReader.Close();
                    }
                }
                mysqlConnection.Close();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            try
            {
                app_id = ((Models.Combo)this.comboBox1.SelectedValue).Value;
            }
            catch(Exception){
                if (this.comboBox1.SelectedValue != null)
                app_id = this.comboBox1.SelectedValue.ToString();
            }
            if (string.IsNullOrWhiteSpace(app_id)) return;
            string query = @"   SELECT user_id, name 
                                FROM users 
                                INNER JOIN device
                                    ON users.user_id = device.owner_id
                                WHERE 
                                    active = true AND 
                                    original_application_id = " + app_id + @" AND 
                                    IMEI IS NOT NULL 
                                ORDER BY name ;";

            SqlConnection conn = new SqlConnection();

            using (MySqlConnection mysqlConnection = conn.Get())
            {
                mysqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, mysqlConnection))
                {
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        var dataSourceApp = new List<Models.Combo>();
                        dataSourceApp.Add(new Models.Combo() { Value = "-1", Name= "TODOS" });
                        while (dataReader.Read())
                        {
                            dataSourceApp.Add(new Models.Combo() { Value = dataReader["user_id"].ToString(), Name = dataReader["name"].ToString() });

                        }
                        this.comboBox2.DataSource = dataSourceApp;
                        this.comboBox2.DisplayMember = "Name";
                        this.comboBox2.ValueMember = "Value";
                        this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                        dataReader.Close();
                    }

                    
                }
                mysqlConnection.Close();
                //Create a data reader and Execute the command
                
            }





            query = @"   SELECT * FROM gate_event_expression_evaluator
                                WHERE 
                                    disabled = false AND 
                                    application_id = " + app_id + @"  
                                ORDER BY name ;";

            conn = new SqlConnection();

            using (MySqlConnection mysqlConnection = conn.Get())
            {
                mysqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, mysqlConnection))
                {
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        var dataSourceApp = new List<Models.Combo>();
                        while (dataReader.Read())
                        {
                            dataSourceApp.Add(new Models.Combo() { Value = dataReader["gate_event_expression_evaluator_id"].ToString(), Name = dataReader["name"].ToString() });

                        }
                        this.listBox1.DataSource = dataSourceApp;
                        this.listBox1.DisplayMember = "Name";
                        this.listBox1.ValueMember = "Value";
                        dataReader.Close();
                    }


                }
                mysqlConnection.Close();
                //Create a data reader and Execute the command

            }





        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string vehicle_id = "";
            try
            {
                vehicle_id = ((Models.Combo)this.comboBox1.SelectedValue).Value;
            }
            catch (Exception)
            {
                if (this.comboBox2.SelectedValue != null)
                    vehicle_id = this.comboBox2.SelectedValue.ToString();
            }
            if (string.IsNullOrWhiteSpace(vehicle_id))
            {
                MessageBox.Show("Selecciona un vehículo", "Error");
                return;
            }

            

            List<string> selectedList = new List<string>();
                foreach (var item in this.listBox1.SelectedItems) {
                   selectedList.Add(((Models.Combo)item).Value);
                }

                


            SqlConnection conn = new SqlConnection();
            using (MySqlConnection mysqlConnection = conn.Get())
            {
                string query = @"SELECT 
	                                ev.application_id AS 'client',
                                    ev.server_time_stamp AS 'date',
                                    ex.name AS 'event',
                                    us.name AS 'vehicle',
                                    de.IMEI,
                                    de.phone_number AS 'vehicle_phone_number',
                                    dr.name AS 'default_driver_name',
                                    dr.email AS 'default_driver_email',
                                    drde.phone_number AS 'default_driver_phone_number',
                                    idr.name AS 'current_driver_name',
                                    idr.email AS 'current_driver_email',
                                    idrde.phone_number AS 'current_driver_phone_number',
                                    (SELECT 
	                                     td.latitude_mod
                                    FROM track_info ti
                                    INNER JOIN track_data_v6 td
	                                    ON ti.track_info_id = td.track_info_id
                                   
                                    WHERE td.deleted_mod != 1
	                                     AND td.valid = TRUE
	                                     AND td.time_stamp <= ev.server_time_stamp
                                         AND ti.owner_id = ev.user_id
                                    ORDER BY td.time_stamp DESC LIMIT 1) AS 'latitude',
                                    (SELECT 
	                                     td.longitude_mod
                                    FROM track_info ti
                                    INNER JOIN track_data_v6 td
	                                    ON ti.track_info_id = td.track_info_id
                                    WHERE td.deleted_mod != 1
	                                     AND td.valid = TRUE
	                                     AND td.time_stamp <= ev.server_time_stamp
                                         AND ti.owner_id = ev.user_id
                                    ORDER BY td.time_stamp DESC LIMIT 1) AS 'longitude'
                                FROM event ev
                                LEFT JOIN gate_event_expression_evaluator ex
	                                ON ev.expression_evaluator_id = ex.gate_event_expression_evaluator_id
                                LEFT JOIN users us
	                                ON ev.user_id = us.user_id
                                LEFT JOIN users idr
                                ON idr.driver_id = (
                                    SELECT data_text
	                                FROM track_info ti
	                                INNER JOIN track_data_v6 td
		                                ON ti.track_info_id = td.track_info_id
	                                INNER JOIN gate_message_record_v6 gmr
		                                ON gmr.track_info_id = ti.track_info_id AND gmr.sequence_num = td.sequence_num
	                                INNER JOIN msg_field msg
		                                ON msg.msg_field_id = gmr.msg_field_id
	                                WHERE td.deleted_mod != 1
		                                 AND ti.deleted != 1
                                         AND ti.owner_id = ev.user_id
		                                 AND msg.msg_field_id IN (33)
		                                 AND gmr.data_text IS NOT NULL
                                         AND td.time_stamp <= ev.server_time_stamp
	                                ORDER BY td.time_stamp DESC LIMIT 1
                                    )
                                LEFT JOIN device idrde
	                                ON idrde.owner_id = idr.user_id
                                LEFT JOIN device de
	                                ON de.owner_id = us.user_id
                                LEFT JOIN  users dr
	                                ON us.default_driver_user_id = dr.user_id
                                LEFT JOIN device drde
	                                ON drde.owner_id = dr.user_id
                                WHERE
	                                de.IMEI IS NOT NULL
                                    AND ex.gate_event_expression_evaluator_id IN (" + String.Join(",", selectedList) + @")
                                    AND ev.application_id = " + app_id + @"
                                    AND ev.server_time_stamp BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm") + "' ";
                query += (vehicle_id != "-1") ? "AND us.user_id = " + vehicle_id + ";" : "";

                mysqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, mysqlConnection))
                {
                    cmd.CommandTimeout = (int)TimeSpan.FromHours(1).TotalMilliseconds;
                    //Create a data reader and Execute the command
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        var dataSourceApp = new List<Models.Events>();
                        while (dataReader.Read())
                        {
                            dataSourceApp.Add(new Models.Events()
                            {
                                client = dataReader["client"].ToString(),
                                date = dataReader["date"].ToString(),
                                event_rule = dataReader["event"].ToString(),
                                latitude = dataReader["latitude"].ToString(),
                                longitude = dataReader["longitude"].ToString(),
                                vehicle = dataReader["vehicle"].ToString(),
                                IMEI = dataReader["IMEI"].ToString(),
                                vehicle_phone_number = dataReader["vehicle_phone_number"].ToString(),
                                default_driver_name = dataReader["default_driver_name"].ToString(),
                                default_driver_email = dataReader["default_driver_email"].ToString(),
                                default_driver_phone_number = dataReader["default_driver_phone_number"].ToString(),
                                current_driver_name = dataReader["current_driver_name"].ToString(),
                                current_driver_email = dataReader["current_driver_email"].ToString(),
                                current_driver_phone_number = dataReader["current_driver_phone_number"].ToString() 
                            });

                        }
                        this.dataGridView1.DataSource = dataSourceApp;
                        dataReader.Close();
                    }
                }
                mysqlConnection.Close();
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                               ts.Hours, ts.Minutes, ts.Seconds,
                                               ts.Milliseconds / 10);
            toolStripStatusLabel1.Text = "Runtime: " + elapsedTime;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string vehicle_id = "";
            try
            {
                vehicle_id = ((Models.Combo)this.comboBox1.SelectedValue).Value;
            }
            catch (Exception)
            {
                if (this.comboBox2.SelectedValue != null)
                    vehicle_id = this.comboBox2.SelectedValue.ToString();
            }
            if (string.IsNullOrWhiteSpace(vehicle_id) || vehicle_id == "-1")
            {
                MessageBox.Show("Selecciona un vehículo", "Error");
                return;
            }

            RealTime frm = new RealTime(vehicle_id);
            frm.Show();
            
        }
        
    }
}

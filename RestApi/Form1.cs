using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace RestApi
{
    // Initialize Interface IMethods
    public interface IMethods
    {
        void FiveDaysForecast();
        void CurrentWeather(); 
    }

    // Implement IMethods on Form1
    public partial class Form1 : Form,IMethods
    {
        private WeatherAPI.RootObject cache;

        public Form1()
        {
            InitializeComponent();

            // Click event Handler for Button btnGo 
            btnGo.Click += (s, e) =>
            {
                CurrentWeather();
                FiveDaysForecast();
                timer1.Start();
                timer1.Enabled = true;
            };

            timer1.Tick += (s, e) =>timerTick();
            
        }

        // Returns the five days forecast.
        public void FiveDaysForecast()
        {
            FiveDaysForecast.RootObject forecast = new FiveDaysForecast.RootObject();
            forecast = RestClient.fiveDaysForecast("id=" + txtCityID.Text);
            List<FiveDaysForecast.List> fdfl = forecast.list;
            
            lblCountry2.Text = forecast.city.country;
            fdfl.ForEach(delegate (FiveDaysForecast.List fff)
            {
                listBox1.Items.Add("Temperature" + fff.main.temp.ToString() +
                    "\nTemperature Min=" + fff.main.temp_min +
                    "\nTemperature Max=" +
                    "\nPressure:" + fff.main.pressure +
                    "\nSea Level:" + fff.main.sea_level +
                    "\nGround Level:" + fff.main.grnd_level +
                    "\nHumidity:" + fff.main.humidity +
                    "\nTemp kf:" + fff.main.temp_kf);
            });
        }

        //Displays the current weather forecast.
        public void CurrentWeather()
        {
            WeatherAPI.RootObject rClient = new WeatherAPI.RootObject();
            rClient = RestClient.makeRequest(txtCityID.Text);
            cache = rClient;
            
            lblLon.Text = rClient.coord.lon.ToString();
            lblLat.Text = rClient.coord.lat.ToString();
            lblBase.Text = rClient.@base;
            lblTemperature.Text = rClient.main.temp.ToString();
            lblPressure.Text = rClient.main.pressure.ToString();
            lblHumidity.Text = rClient.main.humidity.ToString();
            lblTempmin.Text = rClient.main.temp_min.ToString();
            lblTempmax.Text = rClient.main.temp_max.ToString();
            lblSealevel.Text = rClient.main.sea_level.ToString();
            lblGroundlevel.Text = rClient.main.grnd_level.ToString();
            lblSpeed.Text = rClient.wind.speed.ToString();
            lblDeg.Text = rClient.wind.deg.ToString();
            lblAll.Text = rClient.clouds.all.ToString();
            lblDt.Text = rClient.dt.ToString();
            lblMessage.Text = rClient.sys.message.ToString();
            lblCountry.Text = rClient.sys.country.ToString();
            lblSunrise.Text = rClient.sys.sunrise.ToString();
            lblSunset.Text = rClient.sys.sunset.ToString();
            lblId.Text = rClient.id.ToString();
            lblName.Text = rClient.name;
            lblCod.Text = rClient.cod.ToString();
            
            List<WeatherAPI.Weather> waw = rClient.weather;
            waw.ForEach(delegate (WeatherAPI.Weather weathw)
           {
               lblMain.Text = weathw.main;
               lblDescription.Text = weathw.description;
               lblIcon.Text = weathw.icon;
           });

        }

        //Updates the WEather Forecast Every 5mins.
        private void timerTick()
        {
            try
            {
                if (txtCityID.Text != "")
                {
                    CurrentWeather();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }
        }
    }
}

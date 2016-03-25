using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    class JsonResult
    {
        JObject originalJson;
        JArray jsonArray;
        StringBuilder strBuilder;
        private string info;
        public string castDate { set; get; }
        public string updateTime { set; get; }
        public string minTemp { set; get; } = "0";
        public string maxTemp { set; get; } = "0";
        public string airQuality { set; get; }
        public string hum { set; get; } = "0";
        public string cond { set; get; }
        public string condDay { set; get; }
        public string condNight { set; get; }
        public JsonResult(string json)
        {
            
            originalJson = (JObject)JsonConvert.DeserializeObject(json);
            jsonArray = TransJObjectToJArray(originalJson["HeWeather data service 3.0"][0]["daily_forecast"].ToString());
            setSevenDayForecast();
        }

        public string getAllInfo()
        {
            return info;
        }


        private void setSevenDayForecast()
        {
            updateTime = originalJson["HeWeather data service 3.0"][0]["basic"]["update"]["loc"].ToString();
            airQuality = originalJson["HeWeather data service 3.0"][0]["aqi"]["city"]["qlty"].ToString();
            info += "\n\n数据更新时间：" + updateTime +"\n今日空气质量："+airQuality+"\n";
            for (int i = 0; i < jsonArray.Count; i++)
            {
                JObject jObj = (JObject)jsonArray[i];

                castDate = originalJson["HeWeather data service 3.0"][0]["daily_forecast"][i]["date"].ToString();
                minTemp = originalJson["HeWeather data service 3.0"][0]["daily_forecast"][i]["tmp"]["min"].ToString();
                maxTemp = originalJson["HeWeather data service 3.0"][0]["daily_forecast"][i]["tmp"]["max"].ToString();
                hum = originalJson["HeWeather data service 3.0"][0]["daily_forecast"][i]["hum"].ToString();
                condDay = originalJson["HeWeather data service 3.0"][0]["daily_forecast"][i]["cond"]["txt_d"].ToString();
                condNight = originalJson["HeWeather data service 3.0"][0]["daily_forecast"][i]["cond"]["txt_n"].ToString();
                //Debug.WriteLine(hum);
                //Debug.WriteLine(condDay);
                //Debug.WriteLine(condNight);
                if (condDay.Equals(condNight))
                {
                    cond = condDay;
                }
                else
                {
                    cond = condDay + "  转  " + condNight;
                }
                info += "\n日期：" + castDate + "\n最低温度：" + minTemp + "℃\n最高温度：" + maxTemp + "℃\n" + cond + "\n相对湿度：" + hum+"%\n\n";

            }
        }
        public static JArray TransJObjectToJArray(string json)
        {
            JArray jarr = (JArray)JsonConvert.DeserializeObject(json);
            return jarr;
        }

    }


  }







    


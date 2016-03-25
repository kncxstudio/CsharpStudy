using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace WeatherApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static string cityName;
        private static string url = "http://apis.baidu.com/heweather/weather/free";
        private static string apiKey = "7d34a227739e836b3656e4c759c152f6";
        private static string content;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void commitButton_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("123123123123");
            request(cityNameTextbox.Text);
            //resultTB.Text = result;
           
        }
        public void request(string city)
        {
            cityName = "city=" + city;
            string strURL = url + '?' + cityName;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
            request.Method = "GET";
            request.Headers["apikey"] = apiKey;
            request.BeginGetResponse(callBack, request);
        }

        private async void callBack(IAsyncResult result)
        {
            
           
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
                WebResponse webResponse = httpWebRequest.EndGetResponse(result);
                using (Stream stream = webResponse.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                    JsonResult resultObj = new JsonResult(content);
                     Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                     {
                         resultTB.Text = resultObj.getAllInfo();
                    }
                    );
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            

        }

    }
}

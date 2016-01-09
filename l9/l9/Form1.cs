using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using l9.ConvTemp;
using l9.CurConv;
using l9.ServiceReference1;
using l9.ServiceReferenceQuotes;
using l9.ServiceResolve;

namespace l9
{
    /// <remarks />
    public sealed class Service : GroupBox
    {
        private static readonly ConcurrentStack<Service> Svc = new ConcurrentStack<Service>();
        private readonly Button _goBtn;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public Service(string name, string def, Func<string, Task<string>> svcCall)
        {
            Text = name;
            Padding = new Padding(2);
            Dock = DockStyle.Bottom;
            Size = new Size(200, 39);
            var v = new Label
            {
                Dock = DockStyle.Left,
                AutoSize = true
            };
            Controls.Add(v);
            var n = new TextBox
            {
                Text = def,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            Controls.Add(n);
            var btn = new Button
            {
                Text = "Go",
                Dock = DockStyle.Right
            };
            Controls.Add(btn);
            btn.AutoSize = true;
            _goBtn = btn;
            btn.Click += async (s, ev) =>
            {
                await _semaphore.WaitAsync();
                try
                {
                    if (!btn.Enabled)
                        return;
                    btn.Enabled = false;
                    var res = "";
                    try
                    {
                        res = await svcCall(n.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    v.Text = res;
                    btn.Enabled = true;
                }
                finally
                {
                    _semaphore.Release();
                }
            };
            Svc.Push(this);
        }

        public static void GoAll()
        {
            var svcs = Svc.ToArray();
            foreach (var svc in svcs)
                svc.Go();
        }

        public void Go()
        {
            _goBtn.PerformClick();
        }
    }

    public class Form1 : Form
    {
        public Form1()
        {
            var delayedStock = new DelayedStockQuoteSoapClient();
            Controls.Add(new Service("DelayedStockQuoteSoapClient/QuickQuote", "GOOG",
                async s => (await delayedStock.GetQuickQuoteAsync(s, "0")).ToString(CultureInfo.CurrentCulture)));
            Controls.Add(new Service("DelayedStockQuoteSoapClient/Quote", "GOOG",
                async s => (await delayedStock.GetQuoteAsync(s, "0")).ChangePercent));
            var ip2GeoSoap = new P2GeoSoapClient();
            Controls.Add(new Service("IP2GeoSoap/IP", "213.180.141.140",
                async s => (await ip2GeoSoap.ResolveIPAsync(s, "0")).City));
            //-> http://www.webservicex.net/ConvertTemperature.asmx?WSDL
            var temperature = new ConvertTemperatureSoapClient();
            Controls.Add(new Service("ConvTemp", "0",
                async s =>
                    (await
                        temperature.ConvertTempAsync(double.Parse(s), TemperatureUnit.degreeCelsius,
                            TemperatureUnit.kelvin)).ToString(CultureInfo.CurrentCulture)));
            //-> WSDL
            var curControl = new CurrencyConvertorSoapClient();
            Controls.Add(new Service("CurrConv", "5",
                async s => (double.Parse(s) * await curControl.ConversionRateAsync(Currency.USD, Currency.EUR)).ToString(CultureInfo.InvariantCulture)));

            var weather = new GlobalWeatherSoapClient();
            Controls.Add(new Service("Weather", "Szczecin", async s =>
            {
                var xml = await weather.GetWeatherAsync(s, "Poland");
                return CurrentWeather.ConvertXml(xml).Temperature;
            }));

            var runall = new Button { Text = "Run all", Dock = DockStyle.Bottom };
            runall.Click += (s, ev) => { Service.GoAll(); };
            Controls.Add(runall);
        }
    }
}
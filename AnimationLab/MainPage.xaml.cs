using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimationLab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage() {
            this.InitializeComponent();
        }
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;

        int timesTicked = 1;

        int positionX = 100;
        int positionY = 100;
        int speedX = 10;
        int speedY = 20;
        int radius = 20;

        public void DispatcherTimerSetup() {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 2);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, object e) {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;

            timesTicked++;


            var path1 = new Windows.UI.Xaml.Shapes.Path();
            path1.Fill = new SolidColorBrush(Windows.UI.Colors.DarkSalmon);


            var geometryGroup1 = new GeometryGroup();


            var ellipseGeometry1 = new EllipseGeometry();
            ellipseGeometry1.Center = new Point(positionX, positionY);
            ellipseGeometry1.RadiusX = radius;
            ellipseGeometry1.RadiusY = radius;
            geometryGroup1.Children.Add(ellipseGeometry1);

            var pathGeometry1 = new PathGeometry();


            geometryGroup1.Children.Add(pathGeometry1);
            path1.Data = geometryGroup1;

            layoutRoot.Children.Clear();
            layoutRoot.Children.Add(path1);

            positionX += speedX;
            positionY += speedY;

            if (positionY + radius > layoutRoot.ActualHeight) {
                speedY *= -1;
            }

            if (positionX + radius > layoutRoot.ActualWidth) {
                speedX *= -1;
            }
            if (positionY - radius < 0) {
                speedY *= -1;
            }

            if (positionX - radius < 0) {
                speedX *= -1;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            DispatcherTimerSetup();
        }
    }
}

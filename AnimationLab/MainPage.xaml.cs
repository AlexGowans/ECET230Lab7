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

        public void DispatcherTimerSetup() {                            //This function sets up a timer so we can simulate physics
            dispatcherTimer = new DispatcherTimer();                    //Create timer
            dispatcherTimer.Tick += dispatcherTimer_Tick;               
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 2);     //How often to tick
            startTime = DateTimeOffset.Now;                             //start time = current date/time
            lastTime = startTime;                                       //when did we last tick? cant be before start
            dispatcherTimer.Start();                                    //Start timer
        }

        void dispatcherTimer_Tick(object sender, object e) {            //what to do each tick
            DateTimeOffset time = DateTimeOffset.Now;    //set a var to now
            TimeSpan span = time - lastTime;            //how long since the last tick?
            lastTime = time;                            //setcurrent time as last tick
            timesTicked++;                              //increment ticks


            var path1 = new Windows.UI.Xaml.Shapes.Path();                  //prepare to draw
            path1.Fill = new SolidColorBrush(Windows.UI.Colors.DarkBlue);   //select a colour


            var geometryGroup1 = new GeometryGroup();                       //new geometry group


            var ellipseGeometry1 = new EllipseGeometry();                   //elipse --> circle
            ellipseGeometry1.Center = new Point(positionX, positionY);      //position of circle
            ellipseGeometry1.RadiusX = radius;                              //circle has single radius all around
            ellipseGeometry1.RadiusY = radius;
            geometryGroup1.Children.Add(ellipseGeometry1);                  //shape stored to draw

            var pathGeometry1 = new PathGeometry();


            geometryGroup1.Children.Add(pathGeometry1);
            path1.Data = geometryGroup1;                                    //add our circle

            layoutRoot.Children.Clear();
            layoutRoot.Children.Add(path1);                                 //draw our circle

            positionX += speedX;        //change position by x for next frame
            positionY += speedY;        //change position by y for next frame


            //Check for boundaries of the screen to and reverse necessarry direction to 'bounce' and stay in screen
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

        private void Page_Loaded(object sender, RoutedEventArgs e) {        //This function starts our timer upon the xaml page loading     
            DispatcherTimerSetup();
        }
    }
}

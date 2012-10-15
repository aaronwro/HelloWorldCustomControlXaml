using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace CustomControls
{
    public sealed class HelloWorld : Control
    {
        public HelloWorld()
        {
            this.DefaultStyleKey = typeof(HelloWorld);
        }

        public bool Blink
        {
            get { return (bool)GetValue(BlinkProperty); }
            set { SetValue(BlinkProperty, value); }
        }

        // Using a DependencyProperty enables animation, styling, binding, etc.
        public static readonly DependencyProperty BlinkProperty =
            DependencyProperty.Register(
                "Blink",                  // The name of the DependencyProperty
                typeof(bool),             // The type of the DependencyProperty
                typeof(HelloWorld),       // The type of the owner of the DependencyProperty
                new PropertyMetadata(     // OnBlinkChanged will be called when Blink changes
                    false,                // The default value of the DependencyProperty
                    new PropertyChangedCallback(OnBlinkChanged)
                )
            );

        private DispatcherTimer __timer = null;
        private DispatcherTimer _timer
        {
            get
            {
                if (__timer == null)
                {
                    __timer = new DispatcherTimer();
                    __timer.Interval = new TimeSpan(0,0,0,0,500); // 500 ms interval
                    __timer.Tick += __timer_Tick;
                }

                return __timer;
            }
        }

        private static void OnBlinkChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var instance = d as HelloWorld;
            if (instance != null)
            {
                if (instance._timer.IsEnabled != instance.Blink)
                {
                    if (instance.Blink)
                    {
                        instance._timer.Start();
                    }
                    else
                    {
                        instance._timer.Stop();
                    }
                }
            }
        }

        private void __timer_Tick(object sender, object e)
        {
            DoBlink();
        }

        public void DoBlink()
        {
            this.Opacity = (this.Opacity + 1) % 2;
            OnBlinked();
        }

        public event EventHandler Blinked;

        private void OnBlinked()
        {
            EventHandler eh = Blinked;
            if (eh != null)
            {
                eh(this, new EventArgs());
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace DNS_Switcher
{
    class Animations
    {
        public void hideAnimation(FrameworkElement element, double duration)
        {
            Storyboard sbHide = new Storyboard();
            //visibility change
            ObjectAnimationUsingKeyFrames myanim = new ObjectAnimationUsingKeyFrames();
            myanim.BeginTime = TimeSpan.FromMilliseconds(duration);
            Storyboard.SetTargetProperty(myanim, new PropertyPath(UIElement.VisibilityProperty));
            var d1 = new DiscreteObjectKeyFrame
            {
                KeyTime = new TimeSpan(0, 0, 0, 0, 0),
                Value = Visibility.Visible
            };
            myanim.KeyFrames.Add(d1);
            d1.KeyTime = new TimeSpan(0, 0, 0, 0, 0);
            d1.Value = Visibility.Collapsed;
            myanim.KeyFrames.Add(d1);


            //opacity changing
            DoubleAnimationUsingKeyFrames myDubAnim = new DoubleAnimationUsingKeyFrames();
            myDubAnim.BeginTime = TimeSpan.FromMilliseconds(0);
            Storyboard.SetTargetProperty(myDubAnim, new PropertyPath(UIElement.OpacityProperty));
            var d2 = new EasingDoubleKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = 1
            };
            myDubAnim.KeyFrames.Add(d2);
            d2.KeyTime = TimeSpan.FromMilliseconds(duration);
            d2.Value = 0;
            myDubAnim.KeyFrames.Add(d2);



            sbHide.Children.Add(myanim);
            sbHide.Children.Add(myDubAnim);
            //start animation
            sbHide.Begin(element);
        }
        public void showAnimation(FrameworkElement element, double duration, double delay)
        {
            Storyboard sbshow = new Storyboard();

            //visibility change
            ObjectAnimationUsingKeyFrames myObjAnim = new ObjectAnimationUsingKeyFrames();
            myObjAnim.BeginTime = TimeSpan.FromMilliseconds(delay);
            Storyboard.SetTargetProperty(myObjAnim, new PropertyPath(UIElement.VisibilityProperty));
            var d1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = Visibility.Collapsed
            };
            myObjAnim.KeyFrames.Add(d1);
            d1.KeyTime = TimeSpan.FromMilliseconds(0);
            d1.Value = Visibility.Visible;
            myObjAnim.KeyFrames.Add(d1);


            //opacity changing
            DoubleAnimationUsingKeyFrames myDubAnim = new DoubleAnimationUsingKeyFrames();
            myDubAnim.BeginTime = TimeSpan.FromMilliseconds(duration + delay);
            Storyboard.SetTargetProperty(myDubAnim, new PropertyPath(UIElement.OpacityProperty));
            var d2 = new EasingDoubleKeyFrame
            {
                KeyTime = new TimeSpan(0),
                Value = 0
            };
            myDubAnim.KeyFrames.Add(d2);
            d2.KeyTime = TimeSpan.FromMilliseconds(duration);
            d2.Value = 1;
            myDubAnim.KeyFrames.Add(d2);
            sbshow.Children.Add(myObjAnim);
            sbshow.Children.Add(myDubAnim);

            //start animation
            sbshow.Begin(element);
        }

        public void ShowNotifcation(FrameworkElement element, double duration,double delay)
        {
            Storyboard sbshow = new Storyboard();

            var animation = new DoubleAnimation()
            {
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(0.4),

                From = element.ActualHeight * -1,

                To = 0
            };
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.Y"));

            sbshow.Children.Add(animation);

            //visibility change
            ObjectAnimationUsingKeyFrames myObjAnim = new ObjectAnimationUsingKeyFrames();
            myObjAnim.BeginTime = TimeSpan.FromMilliseconds(0);
            Storyboard.SetTargetProperty(myObjAnim, new PropertyPath(UIElement.VisibilityProperty));
            var d1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = Visibility.Collapsed
            };
            myObjAnim.KeyFrames.Add(d1);
            d1.KeyTime = TimeSpan.FromMilliseconds(0);
            d1.Value = Visibility.Visible;
            myObjAnim.KeyFrames.Add(d1);


            //opacity changing
            DoubleAnimationUsingKeyFrames myDubAnim = new DoubleAnimationUsingKeyFrames();
            myDubAnim.BeginTime = TimeSpan.FromMilliseconds(100);
            Storyboard.SetTargetProperty(myDubAnim, new PropertyPath(UIElement.OpacityProperty));
            var d2 = new EasingDoubleKeyFrame
            {
                KeyTime = new TimeSpan(0),
                Value = 0
            };
            myDubAnim.KeyFrames.Add(d2);
            d2.KeyTime = TimeSpan.FromMilliseconds(duration);
            d2.Value = 1;
            myDubAnim.KeyFrames.Add(d2);

            //hide
            DoubleAnimationUsingKeyFrames hidedubanim = new DoubleAnimationUsingKeyFrames();
            hidedubanim.BeginTime = TimeSpan.FromMilliseconds(delay);
            Storyboard.SetTargetProperty(hidedubanim, new PropertyPath(UIElement.OpacityProperty));
            var d3 = new EasingDoubleKeyFrame
            {
                KeyTime = new TimeSpan(0),
                Value = 1
            };
            hidedubanim.KeyFrames.Add(d3);
            d3.KeyTime = TimeSpan.FromMilliseconds(duration);
            d3.Value = 0;
            hidedubanim.KeyFrames.Add(d3);


            
            sbshow.Children.Add(myObjAnim);
            sbshow.Children.Add(myDubAnim);
            sbshow.Children.Add(hidedubanim);
            //start animation
            sbshow.Begin(element);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomControls.Pages
{
    class BoundedQueue<T>
    {

        private readonly LinkedList<T> _internalList = new LinkedList<T>();
        private readonly int _maxQueueSize;

        public BoundedQueue(int queueSize)
        {
            if (queueSize < 0) throw new ArgumentException("queueSize");
            _maxQueueSize = queueSize;
        }

        public void Enqueue(T elem)
        {
            if (_internalList.Count == _maxQueueSize)
                throw new Exception("Full");
            _internalList.AddLast(elem);
        }

        public T Dequeue()
        {
            if (_internalList.Count == 0)
                throw new Exception("Empty");

            T elem = _internalList.First.Value;
            _internalList.RemoveFirst();
            return elem;
        }

        public T Peek()
        {
            if (_internalList.Count == 0)
                throw new Exception("Empty");

            return _internalList.First.Value;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CounterAnimationsPage : ContentPage
    {
        private double counterHeight = 0.0;

        private BoundedQueue<View> viewQueue;

        private bool animationStarted = false;
        public CounterAnimationsPage()
        {
            InitializeComponent();

            //for (int i = 0; i < 10; i++)
            //{
            //    //viewQueue.Enqueue(getNumberView(i, 25));
            //    counterGrid.Children.Add(getNumberView(getNumber(i), 25, 25));
            //}

            //viewQueue = new Queue<View>();
            //viewQueue.Enqueue(getNumberView(getNumber(0), 25));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //var animation = new Animation(c => counter.t)
        }

        private async void OnStartAnimateClicked(object sender, EventArgs e)
        {
            animationStarted = !animationStarted;

            var height = 20;
            uint speed = 250;

            counterGrid.Children.Add(getNumberView(0, height));
            counterGrid.Children.Add(getNumberView(1, height, height));

            int i = 1;

            var aniTasks = new List<Task<bool>>();

            while (animationStarted)
            {

                aniTasks.Add(counterGrid.Children[0].TranslateTo(0, (height * -1), speed));
                aniTasks.Add(counterGrid.Children[1].TranslateTo(0, 0, speed));

                await Task.WhenAll(aniTasks);

                if (i >= 9)
                    i = 0;
                else i++;

                counterGrid.BatchBegin();
                counterGrid.Children.RemoveAt(0);
                counterGrid.Children.Add(getNumberView(i, height, height));
                counterGrid.BatchCommit();
            }

            if (!animationStarted)
            {
            }
        }


        //Stacklayout
        //private async void OnStartAnimateClicked(object sender, EventArgs e)
        //{
        //    var animation = new Animation(callback: c => counter.TranslationX = c,
        //        start: counter.TranslationX,
        //        end: counterHeight * -1);

        //    animationStarted = !animationStarted;

        //    counterHeight = counter.Height;

        //    View view = viewQueue.Peek();
        //    int i = 0;

        //    var animationTasks = new List<Task<bool>>();

        //    counter.Children.Add(getNumberView(getNumber(i), 25));


        //    while (animationStarted)
        //    {
        //        //counter.TranslationY = 0;

        //        //var task = counter.TranslateTo(0, -25, 500);

        //        //counter.Children.Add(getNumberView(getNumber(i++), 25));
        //        //await task;
        //        //counter.Children.RemoveAt(0);


        //        //counter.Children.Add(getNumberView(getNumber(i++), 25));

        //        animationTasks.Add(counter.Children[i].TranslateTo(0, -25, 500));
        //        animationTasks.Add(counter.Children[i++].TranslateTo(0, -25, 500));


        //        await Task.WhenAll(animationTasks);
        //        //i++;

        //        //counter.Children.Add(getNumberView(getNumber(i), 25));
        //        //animationTasks.Add(counter.Children[1].TranslateTo(0, -25, 500));

        //        //await Task.WhenAll(animationTasks);

        //        animationTasks.Clear();

        //        //counter.Children.RemoveAt(0);
        //        counter.Children[i].TranslationY = 0;

        //        //viewQueue.Dequeue();

        //        //view = getNumberView(getNumber(i), 25);
        //        //viewQueue.Enqueue(view);
        //        //i++;


        //        //await Task.Run(() => view.TranslateTo(0, -25, 500)
        //        //    .ContinueWith((x) =>
        //        //    {
        //        //        Device.BeginInvokeOnMainThread(() =>
        //        //        {
        //        //            counter.Children.Remove(viewQueue.Peek());
        //        //            viewQueue.Dequeue();
        //        //        });

        //        //    }));

        //        //view = getNumberView(getNumber(i), 25);
        //        //viewQueue.Enqueue(view);
        //        //counter.Children.Add(view);
        //        //i++;
        //    }
        //}

        private int getNumber(int previousNumber)
        {
            return previousNumber++;
        }

        private View getNumberView(int number, double height, double translationY = 0)
        {
            return new ContentView
            {
                HeightRequest = height,
                TranslationY = translationY,
                Content = new Label { Text = number.ToString() }
            };
        }
    }
}
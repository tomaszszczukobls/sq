using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleQueue
{
    internal class SimpleQueueTest
    {
        private static int _index = 0;
        private static SimpleQueue _queue = new SimpleQueue();
        private static Task[] _tasks = new Task[500];

        private static void Main(string[] args)
        {
            for (int i = 0; i < _tasks.Length; ++i)
            {
                _tasks[i] = Task.Run(() => CreateAction());
            }
            Task.WaitAll(_tasks);

            Console.ReadKey();
        }

        private static void CreateAction()
        {
            void action() => Console.WriteLine(++_index);
            _queue.Enqueue(action);
        }
    }

    internal class SimpleQueue
    {
        private readonly object _queueLock = new object();
        private Queue<Action> _queue = new Queue<Action>();
        private bool _idle = true;

        internal void Enqueue(Action action)
        {
            lock (_queueLock)
            {
                _queue.Enqueue(action);
            }

            Process();
        }

        private bool TryDequeue(out Action action)
        {
            action = default(Action);
            bool success = false;

            lock (_queueLock)
            {
                if (_queue.Count > 0)
                {
                    action = _queue.Dequeue();
                    success = true;
                }
            }

            return success;
        }

        private void Process()
        {
            if (_idle)
            {
                _idle = false;

                while (TryDequeue(out Action action))
                {
                    action.Invoke();
                }

                _idle = true;
            }
        }
    }
}

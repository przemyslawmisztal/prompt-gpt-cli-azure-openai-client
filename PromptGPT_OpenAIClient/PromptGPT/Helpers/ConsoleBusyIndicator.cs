using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Helpers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ConsoleBusyIndicator : IDisposable
    {
        private readonly char[] _sequence = { '/', '-', '\\', '|' };
        private int _index = 0;
        private bool _active;
        private readonly Thread _thread;

        public ConsoleBusyIndicator()
        {
            _thread = new Thread(Spin);
        }

        public void Start()
        {
            _active = true;
            if (!_thread.IsAlive)
                _thread.Start();
        }

        public void Stop()
        {
            _active = false;
            Draw(' '); // Clean up the last spinner character
        }

        private void Spin()
        {
            while (_active)
            {
                Draw(_sequence[_index++ % _sequence.Length]);
                Thread.Sleep(100);
            }
        }

        private void Draw(char c)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(c);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}

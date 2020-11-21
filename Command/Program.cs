using System;
using System.Collections;
using System.Collections.Generic;

namespace Command
{

    public interface ICommand
    {
        public void execute();
    }

    public class MacroCommand : ICommand
    {
        private List<ICommand> _stack;

        public MacroCommand()
        {
            _stack = new List<ICommand>();
        }
        public void execute()
        {
            foreach(var c in _stack)
            {
                c.execute();
            }
        }

        public void Append(ICommand cmd)
        {
            if (cmd != this)
            {
                _stack.Add(cmd);
            }
        }

        public void undo()
        {
            if(_stack.Count!=0)
            {
                _stack.RemoveAt(_stack.Count - 1);
            }
        }

        public void clear()
        {
            _stack.Clear();
        }
    }

    public class DrawCommand : ICommand
    {
        public DrawCommand() { }

        public void execute()
        {
            Console.WriteLine("draw");
        }
    }

    public class DrawCanvas
    {
        private MacroCommand _history;
        public DrawCanvas(MacroCommand history)
        {
            _history = history;
        }

        public void Print()
        {
            _history.execute();
        }

        public void Draw()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpriter
{

    public abstract class Node
    {
        public abstract void parse(Context context);
    }

    /// <summary>
    /// <program> ::= program<command list>
    /// </summary>
    public class ProgramNode : Node
    {
        private Node _commandListNode;
        public override void parse(Context context)
        {
            context.SkipToken("program");
            _commandListNode = new CommandLineNode();
            _commandListNode.parse(context);
        }
        public override string ToString()
        {
            return $"[program {_commandListNode}]";
        }
    }

    public class CommandLineNode : Node
    {
        private List<Node> _list = new List<Node>();
        public override void parse(Context context)
        {
            while (true)
            {
                var nowToken = context.CurrentToken();
                if (nowToken == null)
                {
                    throw new ParseException("Missing 'end'");
                }
                else if (nowToken.Equals("end"))
                {
                    context.SkipToken("end");
                    break;
                }
                else
                {
                    Node commandNode = new CommandNode();
                    commandNode.parse(context);
                    _list.Add(commandNode);
                }
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (var np in _list)
            {
                s += $" {np}";
            }
            return $"[{s}]";
        }
    }

    public class CommandNode : Node
    {
        private Node _node;
        public override void parse(Context context)
        {
            if (context.CurrentToken().Equals("repeat"))
            {
                _node = new RepeatCommandNode();
                _node.parse(context);
            }
            else
            {
                _node = new PrimitiveCommandNode();
                _node.parse(context);
            }
        }

        public override string ToString()
        {
            return _node.ToString();
        }
    }

    public class RepeatCommandNode : Node
    {
        private int _number;
        private Node _commandLineNode;
        public override void parse(Context context)
        {
            context.SkipToken("repeat");
            _number = context.CurrentNumver();
            context.NextToken();
            _commandLineNode = new CommandLineNode();
            _commandLineNode.parse(context);
        }
        public override string ToString()
        {
            return $"[repeat {_number} {_commandLineNode}]";
        }
    }

    public class PrimitiveCommandNode : Node
    {
        private string _name;
        private List<string> _validCommands = new List<string>()
        {
            "go","right","left"
        };

        public override void parse(Context context)
        {
            _name = context.CurrentToken();
            if (!_validCommands.Contains(_name))
            {
                throw new ParseException($"{_name} is undefined command");
            }
            context.SkipToken(_name);
        }

        public override string ToString()
        {
            return _name;
        }
    }

    public class ParseException : Exception
    {
        public ParseException() : base()
        {
        }
        public ParseException(string msg) : base(msg)
        {

        }
        public ParseException(string msg, Exception inExp) : base(msg, inExp)
        {
        }
    }


    public class Context
    {
        private List<string> _tokens;
        int _pos;
        public Context(string text)
        {
            char[] delim = { ' ', '\n', '\t' };
            var bt = text.Replace(@"\r", " ");//split時には\nだけに正規化
            bt = bt.Replace(@"\s*", " ");
            _tokens = bt.Split(' ').ToList();
            _pos = 0;
        }

        public void SkipToken(string token)
        {
            if (token != _tokens[_pos])
            {
                throw new ParseException($"skip token not match {token}:{_tokens[_pos]}");
            }
            NextToken();
        }
        public string CurrentToken()
        {
            return _tokens[_pos];
        }
        public string NextToken()
        {
            if (_pos == _tokens.Count())
            {
                return null;
            }
            return _tokens[_pos++];
        }
        public int CurrentNumver()
        {
            var res = int.TryParse(_tokens[_pos], out var nm);
            if (!res)
            {
                throw new ParseException(_tokens[_pos]);
            }
            return nm;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            string text = "program repeat 4 go right end end";
            var n = new ProgramNode();
            n.parse(new Context(text));
            Console.WriteLine(n);
        }
    }
}
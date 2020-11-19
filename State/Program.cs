using System;

namespace State
{

    public interface State
    {
        public abstract void DoClock(Context context, int hour);
        public abstract void DoUse(Context context);
        public abstract void DoAlerm(Context context);
        public abstract void doPhone(Context context);
    }

    public interface Context
    {
        public abstract void SetClock(int hour);
        public abstract void ChangeState(State state);
        public abstract void CallSecurityCenter(string msg);
        public abstract void RecordLog(string msg);
    }

    public class DayState : State
    {
        public static DayState _singleton = new DayState();

        private DayState() { }

        public static State GetInstance()
        {
            return _singleton;
        }

        public void DoAlerm(Context context)
        {
            Console.WriteLine("非常ベル（昼間）");
        }

        public void DoClock(Context context, int hour)
        {
            if(hour<9||17<=hour)
            {
                context.ChangeState(NightState.GetInstance());
            }
        }

        public void doPhone(Context context)
        {
            Console.WriteLine("通常の会話（昼間）");
        }

        public void DoUse(Context context)
        {
            Console.WriteLine("金庫使用（昼間）");
        }
    }

    public class NightState : State
    {
        public static NightState _singleton = new NightState();

        private NightState() { }

        public static State GetInstance()
        {
            return _singleton;
        }

        public void DoAlerm(Context context)
        {
            Console.WriteLine("非常ベル（夜間）");
        }

        public void DoClock(Context context, int hour)
        {
            if (!(hour < 9 || 17 <= hour))
            {
                context.ChangeState(DayState.GetInstance());
            }
        }

        public void doPhone(Context context)
        {
            Console.WriteLine("録音通話（夜間）");
        }

        public void DoUse(Context context)
        {
            Console.WriteLine("非常ベル（夜間）");
        }
    }

    public class SafeFrame : Context
    {

        private State _state = DayState.GetInstance();

        public void CallSecurityCenter(string msg)
        {
            Console.WriteLine(msg);
        }

        public void ChangeState(State state)
        {
            _state = state;
        }

        public void RecordLog(string msg)
        {
            Console.WriteLine($"record {msg}");
        }

        public void SetClock(int hour)
        {
            _state.DoClock(this, hour);
        }

        public void TestExec()
        {
            _state.DoUse(this);
            _state.DoAlerm(this);
            _state.doPhone(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var safe = new SafeFrame();
            
            for(int i=0;i<24;i++)
            {
                Console.WriteLine($"====時刻 {i}====");
                safe.SetClock(i);
                safe.TestExec();
            }
        }
    }
}

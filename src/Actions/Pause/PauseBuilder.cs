using System.Collections.Generic;
using WindowsInput;

namespace R3peat
{
    using System.Threading;

    public class PauseBuilder
    {
        private int PauseDuration;
        private Pause pause;
        private string Name;
        private NameIncrementer NameIncrementer;

        public PauseBuilder BuildPause(string name = null, int? pauseDuration = null)
        {
            Name = name ?? NameIncrementer.Next();
            PauseDuration = pauseDuration ?? 100;
            pause = new Pause(PauseDuration, Name);
            return this;
        }

        public PauseBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public PauseBuilder SetPauseDuration(int pauseDuration)
        {
            PauseDuration = pauseDuration;
            return this;
        }

        public Pause GetPause()
        {
            return pause;
        }

        public PauseBuilder()
        {
            NameIncrementer = new NameIncrementer("Pause");
        }
    }


}

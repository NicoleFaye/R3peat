using System.Collections.Generic;
using WindowsInput;

namespace R3peat
{
    public class PauseBuilder
    {
        private int PauseDuration;
        private Pause pause;
        private string Name;
        private NameIncrementer NameIncrementer;

        public Pause Build(string name = null, int? pauseDuration = null)
        {
            Name = name ?? NameIncrementer.Next();
            PauseDuration = pauseDuration ?? 100;
            pause = new Pause(PauseDuration, Name);
            return this.pause;
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
        public PauseBuilder()
        {
            NameIncrementer = new NameIncrementer("Pause");
        }
    }


}

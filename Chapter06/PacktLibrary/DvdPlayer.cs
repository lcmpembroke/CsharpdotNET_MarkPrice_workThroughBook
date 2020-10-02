using static System.Console;

namespace Packt.Shared
{
    public class DvdPlayer : IPlayable
    {
        public void Pause()
        {
            WriteLine("DVD playing is pausing");
        }     

        public void Play()
        {
            WriteLine("DVD playing is playing");
        }               
    }
}
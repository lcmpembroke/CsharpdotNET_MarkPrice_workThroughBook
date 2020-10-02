namespace Packt.Shared
{
    public class Thing
    {
        public object Data = default(object);

        public string Process(object input)
        {
            string response;
            if (Data == input)
            {
                response = $"Data and input are the same...{Data.ToString()} and {input.ToString()}";
                return response;
            }
            else
            {
                response = $"Data and input NOT the same...{Data.ToString()} and {input.ToString()}";
                return response;
            }
            
        }
    }
}
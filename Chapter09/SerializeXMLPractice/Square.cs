namespace Pembroke.Shared
{
    public class Square : Rectangle
    {
        public override double Height
        {
            set
            { 
                base.Height = value;
                base.Width = value;
            }
        }

        public override double Width
        {
            set
            {   
                base.Height = value;
                base.Width = value;
            }
        }
    }
}
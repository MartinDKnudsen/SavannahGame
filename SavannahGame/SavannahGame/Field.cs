namespace SavannahGame
{
    public class Field
    {

        public Animal animal { get; set; }

        public bool GreenField { get; set; }

        public Field(bool greenField)
        {
            GreenField = greenField;

        }
        //  public Animal animal { get; set; } = new Lion();
       


    }
}

namespace SavannahGame
{
    public class Field
    {

        public Animal animal { get; set; }

        public bool GreenField { get; set; }

        public int Id { get; set; }

        public static int Counter { get; set; }

        public Field(bool greenField)
        {
            GreenField = greenField;
            this.Id = Counter;
            Counter++;

        }
        //  public Animal animal { get; set; } = new Lion();
       


    }
}

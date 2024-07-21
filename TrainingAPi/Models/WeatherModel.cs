namespace TrainingAPi.Models
{
    public class WeatherModel
    {
        public double Temp
        {
            get
            {
                return Temp;
            }
            set
            {
                if (Temp < 0)
                    Temp = value * -1;
            }
        }

        public string Region { get; set; }
    }
}

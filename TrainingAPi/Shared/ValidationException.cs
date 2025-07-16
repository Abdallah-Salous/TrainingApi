namespace TrainingAPi.Shared
{
    public class TrainingValidationException : Exception
    {
        private string[] messages { get; set; }
        public TrainingValidationException(string message) : base(message)
        {

        }

        public TrainingValidationException(string[] messages)
        {
            messages = messages;
        }

        public string[] GetMessages()
        {
            return messages;
        }
    }
}

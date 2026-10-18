namespace ScrabbleScoreboad.Consumer
{
    public static class Compensation
    {
        public static async Task Demo1()
        {
            await StartValidationService();
            await StartProcessingService();
            await StartCompensationService();

            Console.WriteLine("Compensation demo running...");
            Console.ReadLine();
        }

        private static async Task StartValidationService()
        {
        }

        private static async Task StartProcessingService()
        {
        }

        private static async Task StartCompensationService()
        {
        }
    }
}

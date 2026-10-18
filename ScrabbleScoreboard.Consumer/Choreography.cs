namespace ScrabbleScoreboad.Consumer
{
    public static class Choreography
    {
        public static async Task Demo1() {
            await StartValidationService();
            await StartProcessingService();
            await StartCompletionService();

            Console.WriteLine("Choreography running...");
            Console.ReadLine();
        }

        private static async Task StartValidationService() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Validation Service Started");
            Console.ResetColor();

            await Task.CompletedTask;
        }

        private static async Task StartProcessingService() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Processing Service Started");
            Console.ResetColor();

            await Task.CompletedTask;
        }

        private static async Task StartCompletionService() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Completion Service Started");
            Console.ResetColor();

            await Task.CompletedTask;
        }

    }
}

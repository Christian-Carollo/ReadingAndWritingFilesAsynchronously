namespace ReadingAndWritingFilesAsynchronously;
public static class Program // un alert mi diceva che la classe program doveva essere dichiarata static
{
    public static async Task Main(string[] args)
    {
        var filePath = "file.txt";

        /* Alert CA2007 : Provare a chiamare ConfigureAwait sull'attività attesa
         * Ho utilizzato il ConfigureAwait(false) cosi da evitare possibili problemi 
         * nel contesto di sincronizzazione, come prblemi di prestazioni o di deadlock
         */
        await WriteToFile(filePath).ConfigureAwait(false);
        await ReadFromFile(filePath).ConfigureAwait(false);

        static async Task WriteToFile(string filePath)
        {
            Console.WriteLine("Insert text:\n");
            var input = Console.ReadLine();

            while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Error in writing. Insert new text:\n");
                input = Console.ReadLine();
            }
            
            try
            {
                /* Alert CA2007 : Provare a chiamare ConfigureAwait sull'attività attesa
                 * Ho utilizzato il ConfigureAwait(false) cosi da evitare possibili problemi 
                 * nel contesto di sincronizzazione, come prblemi di prestazioni o di deadlock
                 */
                await File.WriteAllTextAsync(filePath, input).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in writing {ex.Message}");
            }
            
        }

        static async Task ReadFromFile(string filePath)
        {
            try
            {
                /* Alert CA2007 : Provare a chiamare ConfigureAwait sull'attività attesa
                 * Ho utilizzato il ConfigureAwait(false) cosi da evitare possibili problemi 
                 * nel contesto di sincronizzazione, come prblemi di prestazioni o di deadlock
                 */
                var content = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
                Console.WriteLine($"Content file:\n {content}");
            }
            /* Alert CA1031 : Indica che il blocco catch cattura un eccezione generica "Exception"
             * il che potrebbe rendere difficile individuare e gestire specificamente le eccezioni 
             * che si verificano nel tuo codice 
             */
            catch (Exception ex)
            {
                Console.WriteLine($"Error in reading {ex.Message}");
            }
            
        }
    }
}
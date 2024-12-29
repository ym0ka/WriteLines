namespace WriteLines
{
    class Program
    {
        private static List<string> readKeywords = new List<string> {"r","R","read","READ"};
        private static List<string> writeKeywords = new List<string> {"w","W","write","WRITE"};
        private static List<string> deleteKeywords = new List<string> {"d","D","delete","DELETE"};
        private static string documentPath = "./document.txt";

        static void Main(string[] args)
        {
            Console.WriteLine(
                "Welcome in WriteLines!\nWhat do you want to do?\nRead (R) / Write (W) / Delete line (D):");

            string userDocumentAction = null;
            while (string.IsNullOrWhiteSpace(userDocumentAction) || !UserInputIsInLists(userDocumentAction))
            {
                userDocumentAction = Console.ReadLine();
                if (string.IsNullOrEmpty(userDocumentAction) || !UserInputIsInLists(userDocumentAction))
                {
                    Console.WriteLine("Choice can't be empty and needs to be R, W or D, please enter a valid option.");
                }
            }

            if (UserInputIsRead(userDocumentAction))
            {
                Console.WriteLine(ReadDocument());
            }

            if (UserInputIsWrite(userDocumentAction))
            {
                Console.WriteLine("Enter the value you want to be written in the document:");
                string userInputValue = Console.ReadLine();
                WriteDocumentLine(userInputValue);
                Console.WriteLine("The entry has been written in the document.");
            }

            if (UserInputIsDelete(userDocumentAction))
            {
                Console.WriteLine("Entre the line number you want to delete:");
                int documentLines = File.ReadLines(documentPath).Count();
                string userInputValue = null;
                bool isNotValidInput = true;
                while (isNotValidInput)
                {
                    userInputValue = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(userInputValue) && userInputValue.All(char.IsDigit) &&
                        !(int.Parse(userInputValue) > documentLines && int.Parse(userInputValue) < 1))
                    {
                        isNotValidInput = false;
                        continue;
                    }
                    Console.WriteLine("Value must be a number between 1 and the maximum of document lines.");
                }
                
                DeleteDocumentLine(int.Parse(userInputValue));
                Console.WriteLine($"Line {userInputValue} was successfully deleted.");
            }
        }

        static void WriteDocumentLine(string line)
        {
            if (!File.Exists(documentPath))
            {
                throw new FileNotFoundException("The file could not be found.", documentPath);
            }
            File.AppendAllText(documentPath, line + Environment.NewLine);
        }

        static string ReadDocument()
        {
            if (!File.Exists(documentPath))
            {
                throw new FileNotFoundException("The file could not be found.", documentPath);
            }
            return File.ReadAllText(documentPath);
        }

        static void DeleteDocumentLine(int lineNumber)
        {
            if (!File.Exists(documentPath))
            {
                throw new FileNotFoundException("The file could not be found.", documentPath);
            }
            List<string> documentLines = File.ReadAllLines(documentPath).ToList();
            documentLines.RemoveAt(lineNumber - 1);
            File.WriteAllLines(documentPath, documentLines);
        }

        static bool UserInputIsInLists(string userInput)
        {
            return readKeywords.Contains(userInput) || deleteKeywords.Contains(userInput) ||
                   writeKeywords.Contains(userInput);

        }

        static bool UserInputIsRead(string userInput)
        {
            return readKeywords.Contains(userInput);
        }

        static bool UserInputIsDelete(string userInput)
        {
            return deleteKeywords.Contains(userInput);
        }

        static bool UserInputIsWrite(string userInput)
        {
            return writeKeywords.Contains(userInput);
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using TranslationTools;

public class CreateFileOrFolder
{
    private readonly string OutputFolder = "Output";
    private readonly string InputFolder = "Input";
    private readonly string RootFolderPath = @"C:\Code\Personal\Translation Tools";

    public async Task Create(string fileName, string title, bool ocrActive)
    {
        string outputFilePath = Path.Combine(RootFolderPath, OutputFolder);
        Directory.CreateDirectory(outputFilePath);
        outputFilePath = Path.Combine(outputFilePath, fileName);

        string inputFolderPath = Path.Combine(RootFolderPath, $"{InputFolder}\\{title}");
        //var romaji = WanaKana.ToRomaji(projectName);

        var mainContext = $"[CHANGE TO JAPANESE]{title}\n" +
            $"{title}\n" +
            $"[LINK TO RAW]\n" +
            "\n" +
            "Translator: belinda\n" +
            "\n" +
            "Cleaners: \n" +
            "\n" +
            "Typesetters: \n" +
            "\n" +
            "Speech Font: \n" +
            "\n";

        try
        {
            
            var fileEntries = Directory.GetFiles(inputFolderPath);

            mainContext = ListAllPages(mainContext, fileEntries);
            mainContext = SetupPages(mainContext, fileEntries, ocrActive);

            await File.WriteAllTextAsync(outputFilePath, mainContext);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }

        // Keep the console window open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private string SetupPages(string mainContext, string[] allFiles, bool ocrActive)
    {
        var ocrService = new ExtractJapaneseOcrService();
        foreach (string file in allFiles)
        {
            var fullFileName = Path.GetFileName(file);
            var fileName = fullFileName.Substring(0, fullFileName.IndexOf("."));

            if (ocrActive)
            {
                Console.WriteLine($"Processing {fileName}");
                var extractedText = ocrService.GetText(file);
                mainContext += $"PAGE {fileName}\n" +
                    $"{extractedText}\n";
            } else
            {
                mainContext += $"PAGE {fileName}\n";
            }
        }

        return mainContext;
    }

    private string ListAllPages(string mainContext, string[] allFiles)
    {
        var allPages = "Pages: ";

        foreach (string file in allFiles)
        {
            var name = Path.GetFileName(file);
            var input = name.Substring(0, name.IndexOf("."));
            allPages += $"{input} ";
        }
        mainContext += allPages + "\n\n";
        return mainContext;
    }
}
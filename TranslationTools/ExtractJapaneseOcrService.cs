using Tesseract;

namespace TranslationTools
{
    public class ExtractJapaneseOcrService
    {
        public string GetText(string filePath)
        {
            using var engine = new TesseractEngine(@"C:\Code\Personal\Translation Tools\TesseractData",
                "jpn_vert", EngineMode.Default);
            using (var img = Pix.LoadFromFile(filePath))
            {
                using (var page = engine.Process(img))
                {
                    var text = page.GetText();
                    return text;
                }
            }
        }
    }
}

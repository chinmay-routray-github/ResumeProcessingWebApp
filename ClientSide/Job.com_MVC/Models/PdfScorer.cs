using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Reflection.PortableExecutable;
using System.Text;

namespace JobSearchApp_MVC.Models
{
    public class PdfScorer
    {
        public static List<string> GetText(string path)
        {
            //string path = "C:\\Users\\chinmay.routray\\Downloads\\010033985224.pdf";
            List<string> allwords = new List<string>();
            List<string> allLines = new List<string>();
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();
                ITextExtractionStrategy Strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string page = "";

                    page = PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
                    string[] lines = page.Split('\n');
                    foreach (string line in lines)
                    {
                        allLines.Add(line);
                        string[] w = line.Split(' ');
                        foreach (var word in w)
                        {
                            allwords.Add(word);
                        }
                    }
                }
            }
            /*foreach (var word in allwords)
            {
                Console.WriteLine(word);
            }*/
            return allLines;
        }

        public static float score(string[] keySkills, List<string> textResume)
        {
            float n = 0;
            foreach (var skill in keySkills)
            {
                foreach (var line in textResume)
                {
                    if (line.Contains(skill))
                    {
                        n += 1;
                        break;
                    }
                }
            }
            float intermitten_score = n / keySkills.Length;
            if (intermitten_score == 1)
            {
                List<float> occurence = new List<float>();
                foreach (var skill in keySkills)
                {
                    float m = 0;
                    foreach (var line in textResume)
                    {
                        if (line.Contains(skill))
                        {
                            m += 1;
                        }
                    }
                    occurence.Add(m);
                }
                float a = occurence.Max();
                float b = occurence.Min();
                float c = 0.5f;
                float wellRoundednessScore = 1 / (a - b + c);
                for (int i = 0; i < occurence.Count; i++)
                {
                    occurence[i] = occurence[i] / occurence.Count;
                }
                float expertiseScore = occurence.Sum();
                return intermitten_score + wellRoundednessScore + expertiseScore;    //total Score for a resume 

            }
            return intermitten_score;
        }
    }
}

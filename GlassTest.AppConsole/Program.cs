using System.Diagnostics;
using GlassTest.Documents.Models;
using GlassTest.Documents.Repositories;

public class Program
{
    private static void Main(string[] args)
    {
        // Here add your connection string
        var repository = DocumentRepository.Create("Server=localhost,1433;Database=MyDocuments;User Id=sa;Password=Gl@ssdbp@ss1;TrustServerCertificate=true");
        
        //Here add query words for matchAll in true execution
        var matchAllQuery = "dolor egestas rhoncus";
        
        List<Document> matchAllResult;
        double timeInSeconds = 0;
        Execute(() => repository.SearchDocuments(matchAllQuery, true), out matchAllResult, out timeInSeconds);

        Console.WriteLine("Count of matches with \"matchAll\" in true {0}. Time: {1} sec", matchAllResult.Count, timeInSeconds);

        //Here add query words for matchAll in false execution
        var notMatchAllQuery = "Donec tempus, lorem fringilla ornare placerat, orci lacus vestibulum lorem, sit amet ultricies";

        List<Document> notMatchAllResult;
        Execute(() => repository.SearchDocuments(notMatchAllQuery, false), out notMatchAllResult, out timeInSeconds);
        
        Console.WriteLine("Count of matches with \"matchAll\" in false {0}. Time: {1}", notMatchAllResult.Count, timeInSeconds);
    }

    private static void Execute(Func<List<Document>> func, out List<Document> result, out double timeInSeconds)
    {
        Stopwatch stopwatch = createStopWatch();

        stopwatch.Start();
        try
        {
            result = func.Invoke();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Oh oh, somethig bad happens :-( -> {0}", ex.Message);
        }
        finally
        {
            result = new();
        }
        stopwatch.Stop();

        timeInSeconds = stopwatch.Elapsed.TotalSeconds;
    }

    private static Stopwatch createStopWatch()
    {
        return new Stopwatch();
    }
}
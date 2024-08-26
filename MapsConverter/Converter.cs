namespace MapsConverter;

public class Converter
{
    private const string SourceFile = "D:\\travel\\mapconverter\\palanga.kml";
    private const string ResultFile = "D:\\travel\\mapconverter\\palanga-converted.kml";

    public static void Convert()
    {
        string googleKml = File.ReadAllText(SourceFile);

        // хорошая достопримечательность
        googleKml = googleKml.Replace("<styleUrl>#icon-1502-7CB342</styleUrl>", "<styleUrl>#placemark-red</styleUrl>");

        // обычная достопримечательность
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-7CB342</styleUrl>", "<styleUrl>#placemark-pink</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-7CB342-nodesc</styleUrl>", "<styleUrl>#placemark-pink</styleUrl>");

        // транспорт, парковки
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-9C27B0-nodesc</styleUrl>", "<styleUrl>#placemark-green</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-9C27B0</styleUrl>", "<styleUrl>#placemark-green</styleUrl>");

        // отели
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-0288D1-nodesc</styleUrl>", "<styleUrl>#placemark-bluegray</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-0288D1</styleUrl>", "<styleUrl>#placemark-bluegray</styleUrl>");

        // ресторан
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-757575-nodesc</styleUrl>", "<styleUrl>#placemark-cyan</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-757575</styleUrl>", "<styleUrl>#placemark-cyan</styleUrl>");

        // хороший ресторан
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-000000-nodesc</styleUrl>", "<styleUrl>#placemark-blue</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-000000</styleUrl>", "<styleUrl>#placemark-blue</styleUrl>");

        // магазин
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-FF5252-nodesc</styleUrl>", "<styleUrl>#placemark-purple</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-FF5252</styleUrl>", "<styleUrl>#placemark-purple</styleUrl>");

        // отличный пляж
        googleKml = googleKml.Replace("<styleUrl>#icon-1502-F57C00-nodesc</styleUrl>", "<styleUrl>#placemark-deeporange</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1502-F57C00</styleUrl>", "<styleUrl>#placemark-deeporange</styleUrl>");

        // хороший пляж
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-F57C00-nodesc</styleUrl>", "<styleUrl>#placemark-orange</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-F57C00</styleUrl>", "<styleUrl>#placemark-orange</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-FBC02-nodesc</styleUrl>", "<styleUrl>#placemark-orange</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-FBC02</styleUrl>", "<styleUrl>#placemark-orange</styleUrl>");

        // пляж
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-FBC02D-nodesc</styleUrl>", "<styleUrl>#placemark-yellow</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-FBC02D</styleUrl>", "<styleUrl>#placemark-yellow</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-BDBDBD-nodesc</styleUrl>", "<styleUrl>#placemark-yellow</styleUrl>");
        googleKml = googleKml.Replace("<styleUrl>#icon-1899-BDBDBD</styleUrl>", "<styleUrl>#placemark-yellow</styleUrl>");


        googleKml = googleKml.Replace("<br><br>", "\n");
        googleKml = googleKml.Replace("<br>", "\n");

        Save(ResultFile, googleKml);
    }

    private static void Save(string filePath, string text)
    {
        try
        {
            File.Delete(filePath);
        }
        catch(Exception)
        {
        }
        File.WriteAllText(filePath, text);
    }
}
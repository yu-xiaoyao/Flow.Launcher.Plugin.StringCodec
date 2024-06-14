using System;
using System.Web;

namespace Flow.Launcher.Plugin.StringCodec;

public class Main_Test
{
    public static void Main()
    {
        string decodeString =
            @"http://19.15.2.42:8000/YZQ/geoscene/rest/services/TZZ/yjjbnt%E6%B0%B8%E4%B9%85%E5%9F%BA%E6%9C%AC%E5%86%9C%E7%94%B0_TZZ202312214/MapServer/0/query?geometryType=esriGeometryPoint&spatialRel=esriSpatialRelIntersects&geometry=%7Bx:115.456944,y:23.788186%7D&distance=2000&units=esriSRUnit_Meter&outFields=BSM&returnGeometry=true&f=json";

        var urlDecode = HttpUtility.UrlDecode(decodeString);
        Console.WriteLine(urlDecode);
        try
        {
            Console.WriteLine(HttpUtility.HtmlEncode("爱你"));
        }
        catch (FormatException e)
        {
        }
    }
}
using System;
using System.Text;
using System.Web;

namespace Flow.Launcher.Plugin.StringCodec;

public class Main_Test
{
    public static void Main()
    {
        try
        {
            Console.WriteLine(HttpUtility.HtmlEncode("爱你")
            );
        }
        catch (FormatException e)
        {
        }
    }
}
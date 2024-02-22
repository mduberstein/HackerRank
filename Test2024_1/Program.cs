// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text;

Console.WriteLine("Hello, World!");

Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");

string[] words = "The quick brown  fox".Split(' ');
foreach (var word in words)
{
    Console.WriteLine(word);
}

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var encoding = Encoding.GetEncoding("UTF-8");

Console.ReadLine();
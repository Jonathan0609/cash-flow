using System.Reflection;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts;

public class ExpensesReportFontResolver : IFontResolver
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelper.DEFAULT_FONT);
        
        var length = (int)stream!.Length;
        var data = new byte[length];

        stream.ReadExactly(buffer: data, offset: 0, count: length);
        
        return data;
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts.{faceName}.ttf");
    }
}
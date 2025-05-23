﻿using CashFlow.Application.UseCases.Reports.Expenses.Pdf.Colors;
using CashFlow.Application.UseCases.Reports.Expenses.Pdf.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf;

public class ReportExpensesPdfUseCase : IReportExpensesPdfUseCase
{
    private const string CURRENCY_SYMBOL = "$";
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
    
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;

    public ReportExpensesPdfUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository)
    {
        _expensesReadOnlyRepository = expensesReadOnlyRepository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(ReportPdfExpenseRequest request)
    {
        var expenses = await _expensesReadOnlyRepository.GetExpensesByMonth(request.Month);

        if (expenses.Count == 0)
            return [];
        
        var document = CreatePdfDocument(request.Month);
        
        var page = CreatePage(document);

        CreateHeaderWithTitle(page);

        CreateTotalSpentSection(request, page, expenses);

        foreach (var expense in expenses)
        {
            var table = CreateExpenseTable(page);
            
            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;
            
            AddExpenseTitle(row.Cells[0], expense.Title);

            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            
            row.Cells[0].AddParagraph(expense.Date.ToString("D"));
            SetStyleBaseForExpenseInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[1].AddParagraph(expense.Date.ToString("t"));
            SetStyleBaseForExpenseInformation(row.Cells[1]);
            
            row.Cells[2].AddParagraph(expense.PaymentType.PaymentTypeToString());
            SetStyleBaseForExpenseInformation(row.Cells[2]);
            
            AddValueForExpense(row.Cells[3], expense.Value);

            if (!string.IsNullOrWhiteSpace(expense.Description))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;
                
                descriptionRow.Cells[0].AddParagraph(expense.Description);
                descriptionRow.Cells[0].Format.Font = new Font
                    { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private static void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private static void AddValueForExpense(Cell cell, decimal value)
    {
        cell.AddParagraph($"-{value} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font
            { Name = FontHelper.RALEWAY_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private static void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font
            { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private static void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font
            { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private static void AddExpenseTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font
            { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private static void CreateTotalSpentSection(ReportPdfExpenseRequest request, Section page, List<Expense> expenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = 40;
        
        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, request.Month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        
        paragraph.AddLineBreak();

        var total = expenses.Sum(expense => expense.Value);
        
        paragraph.AddFormattedText($"{total} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        
        return table;
    }
    private static void CreateHeaderWithTitle(Section page)
    {
        var table = page.AddTable();
        table.AddColumn(300);

        var row = table.AddRow();
        
        row.Cells[0].AddParagraph("Hey, Jonathan Cristian!");
        row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
    }

    private Document CreatePdfDocument(DateOnly month)
    {
        var pdfDocument = new Document();

        pdfDocument.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y}";
        pdfDocument.Info.Author = "Jonathan Cristian";
        
        var style = pdfDocument.Styles["Normal"];

        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        
        return pdfDocument;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        
        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };
        
        renderer.RenderDocument();
        
        using var file = new MemoryStream();
        
        renderer.PdfDocument.Save(file);
        
        return file.ToArray();
    }
}
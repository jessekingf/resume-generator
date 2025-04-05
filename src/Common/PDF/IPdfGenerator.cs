namespace Common.PDF;

/// <summary>
/// Provides functionality for generating PDF documents.
/// </summary>
public interface IPdfGenerator
{
    /// <summary>
    /// Generates a PDF from an HTML file.
    /// </summary>
    /// <param name="htmlPath">The path to the HTML file to generator the PDF from.</param>
    /// <param name="pdfPath">The path to save the generated PDF file.</param>
    void FromHtml(string htmlPath, string pdfPath);
}

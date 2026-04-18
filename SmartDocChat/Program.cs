using OllamaSharp;
using OllamaSharp.Models.Chat;
using UglyToad.PdfPig;
using System.Text;

// 1. Setup paths
string folderPath = @"D:\Interview Prepration\AI Image Tagging and Organizer\SmartDocChat\Data\";

Console.WriteLine("Initializing Local AI (Native Ollama Mode)...");

// 2. Initialize the AI Client directly
var ollama = new OllamaApiClient(new Uri("http://localhost:11434"));
ollama.SelectedModel = "phi3";

// 3. Extract Text from ALL PDFs
StringBuilder combinedContext = new StringBuilder();
try
{
    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
    string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

    if (pdfFiles.Length == 0)
    {
        Console.WriteLine($"No PDFs found. Put a file in: {folderPath}");
        return;
    }

    foreach (string file in pdfFiles)
    {
        using var pdf = PdfDocument.Open(file);
        foreach (var page in pdf.GetPages()) combinedContext.Append(page.Text);
        Console.WriteLine($"Loaded: {Path.GetFileName(file)}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    return;
}

Console.WriteLine("\nAI Ready! Ask me anything:");

while (true)
{
    Console.Write("\nYou: ");
    string? query = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(query) || query.ToLower() == "exit") break;

    var chatRequest = new ChatRequest
    {
        Messages = new List<Message>
        {
            new Message(ChatRole.System, $"Context: {combinedContext}"),
            new Message(ChatRole.User, query)
        }
    };

    Console.Write("AI: ");

    // --- LOADER LOGIC START ---
    bool isFirstToken = true;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Thinking...");
    Console.ResetColor();
    // --- LOADER LOGIC END ---

    try
    {
        await foreach (var stream in ollama.ChatAsync(chatRequest))
        {
            // When the first bit of text arrives, clear the "Thinking..." line
            if (isFirstToken)
            {
                // Move cursor back to overwrite "Thinking..."
                Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                Console.Write("AI: ");
                isFirstToken = false;
            }

            Console.Write(stream?.Message?.Content);
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nAI Error: {ex.Message}");
    }
}
## 🤖 SmartDocChat: Private Local AI Document Assistant
SmartDocChat is a Privacy-First RAG (Retrieval-Augmented Generation) application built with .NET 8/9 and Ollama. It allows you to chat with multiple PDF documents locally on your machine. Your data never leaves your computer, making it ideal for sensitive enterprise documentation.

## 🌟 Key Features
**100% Local:** Powered by Ollama (Phi-3 model) for complete data privacy.
**Multi-PDF Support:** Reads and indexes all PDF files within a specific directory.
**Streaming Responses:** AI answers appear in real-time with a "typing" effect.
**Native Implementation:** Uses OllamaSharp for stable, high-performance communication with the AI.

## 🛠️ Prerequisites
Before running the application, ensure you have the following installed:
**.NET 8.0 SDK or 9.0 SDK**
**Ollama (The engine that runs the AI models).**
**Phi-3 Model: Open your terminal and run:**

Bash
**ollama pull phi3**

## 🚀 Installation & Setup
**1. Clone the Project**
Create a folder on your drive (e.g., D:\Projects\SmartDocChat) and initialize your console app.

**2. Required NuGet Packages**
Install the following dependencies via the Package Manager Console in Visual Studio:
**PowerShell**
## Core library to communicate with Ollama
**dotnet add package OllamaSharp --version 5.0.3**

## Library to extract text from PDF files
**dotnet add package UglyToad.PdfPig --prerelease**

**3. Prepare your Data Folder**
Create a folder named **Data** inside your project directory (or any path of your choice).

Place the PDFs you want to chat with inside this folder.

## 💻 Execution Guide
**Start Ollama:** Ensure the Ollama application is running in your system tray.
**Configure Path:** Open Program.cs and update the folderPath variable to point to your PDF folder:

**C#**
**string folderPath = @"D:\YourPath\SmartDocChat\Data\";**
**Run the App: Press F5 or use the command line:**

**Bash
dotnet run**

📖 How to Use
Initialization: The app will scan your folder and load all text from the PDFs into memory.

The Interface:

**👤 You:** Type your question (e.g., "What are the system modules?").

Thinking... 🤔: The app shows a loader while the AI processes your context.

**🤖 AI**: The response will stream word-by-word based strictly on your document's data.

**Exit:** Type exit to close the application.

## 🏗️ Technical Architecture (RAG)
The application follows the Retrieval-Augmented Generation pattern:

**Extraction:** PdfPig extracts raw text from local files.

**Contextualization:** The extracted text is injected into the AI's System Prompt.

**Inference:** The Phi-3 model processes the user query specifically using the provided context.

**Streaming:** The OllamaApiClient utilizes IAsyncEnumerable to stream the response back to the console.

 ## To Start the AI Engine
Run this to wake up the Phi-3 brain and start the local server:

**PowerShell**

**ollama run phi3**

Note: This command checks if the server is running, starts it if it isn't, and immediately opens the chat interface.

## To Force Stop Everything
If you need to clear your RAM/GPU and shut down all Ollama processes instantly:

**PowerShell**

Stop-Process -Name "ollama*" -Force



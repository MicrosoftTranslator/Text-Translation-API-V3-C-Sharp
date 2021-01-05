---
page_type: sample
name: Microsoft Translator C# samples (v3)
description: This repository includes C# code samples for Microsoft Translator. 
urlFragment: translator-c-sharp-v3
languages:
- csharp
products:
- azure
- azure-cognitive-services
- azure-translator
---

# Translator API V3 - .NET Core Samples, C#

This repository includes .NET Core samples for Microsoft Translator. Each sample corresponds to a **Quickstart** activity on [doc.microsoft.com](https://docs.microsoft.com/azure/cognitive-services/translator/), including:

* Translating text
* Transliterating text
* Identifying the language of source text
* Getting alternate translations
* Getting a complete list of supported languages
* Determining sentence length

[Get started with the Translator quickstart](https://docs.microsoft.com/azure/cognitive-services/translator/quickstart-translator).

## Prerequisites

Here's what you'll need before you use these samples:

* [.NET SDK](https://www.microsoft.com/net/learn/dotnet/hello-world-tutorial)
* [Json.NET NuGet Package](https://www.nuget.org/packages/Newtonsoft.Json/)
* [Visual Studio](https://visualstudio.microsoft.com/downloads/), [Visual Studio Code](https://code.visualstudio.com/download), or your favorite text editor
* An Azure subscription - [Sign-up for a free account](https://docs.microsoft.com/azure/cognitive-services/translator/translator-text-how-to-signup)!
* A Translator resource - [Create a Translator resource](https://ms.portal.azure.com/#create/Microsoft.CognitiveServicesTextTranslation)

## Code samples

This repository includes a sample for each of the methods made available by the Microsoft Translator API v3. To use each of the samples, follow these instructions:

* Create a new project: `dotnet new console -o your_project_name`
* Copy the code from one of the samples into `Program.cs`.
* Set your subscription key.
* Run the program from the project directory: `dotnet run`.

## Resources

* [What is Translator?](https://docs.microsoft.com/azure/cognitive-services/translator/translator-info-overview)
* [v3 Translator API Reference](https://docs.microsoft.com/azure/cognitive-services/translator/)
* [Supported languages](https://docs.microsoft.com/azure/cognitive-services/translator/language-support)

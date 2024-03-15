using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Введите локалхост или IP-адрес веб-сервиса:");
        string serviceUrl = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(serviceUrl))
        {
            Console.WriteLine("Ошибка: Введите корректный локалхост или IP-адрес.");
            return;
        }

        string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "log.txt");

        await TestService(serviceUrl, logFilePath);
    }

    static async Task TestService(string serviceUrl, string logFilePath)
    {
        using var client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(serviceUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                string logMessage = $"OK(#1, {serviceUrl}, name server or service, {responseContent})";

                Console.WriteLine(logMessage);

                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            else
            {
                string errorMessage = $"Ошибка при подключении к сервису {serviceUrl}: {response.StatusCode}";
                Console.WriteLine(errorMessage);
                File.AppendAllText(logFilePath, errorMessage + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            string errorMessage = $"Ошибка при подключении к сервису {serviceUrl}: {ex.Message}";
            Console.WriteLine(errorMessage);
            File.AppendAllText(logFilePath, errorMessage + Environment.NewLine);
        }
    }
}

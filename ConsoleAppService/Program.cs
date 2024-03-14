using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Отправка запроса к веб-сервису
        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("http://localhost:5000/WeatherForecast");

        // Обработка ответа
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Ошибка: {response.StatusCode}");
        }

        // Запись в лог-файл
        string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "log.txt");
        string logMessage = "Запись в лог-файл: " + DateTime.Now.ToString();
        File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
    }
}

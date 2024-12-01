using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string result;

        if (args.Length > 0)
        {
            result = string.Join(" ", args);
        }
        else
        {
            result = Environment.CurrentDirectory;
        }

        var rslt = "/c dir " + result;
        var result_cmd = ExecuteCommand("powershell.exe", rslt);
        Console.WriteLine(result_cmd.Output);
        Console.WriteLine(result_cmd.Error);
    }
    public static (string Output, string Error) ExecuteCommand(string command, string arguments)
    {
        // Создание процесса
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true, // Перенаправляем стандартный вывод
            RedirectStandardError = true,  // Перенаправляем стандартный вывод ошибок
            UseShellExecute = false,       // Не использовать оболочку Windows
            CreateNoWindow = true          // Не создавать окно консоли
        };

        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;

            try
            {
                process.Start(); // Запуск процесса

                // Чтение стандартного вывода и ошибок
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit(); // Ожидание завершения процесса

                return (output, error); // Возвращаем кортеж с выводом и ошибками
            }
            catch (Exception ex)
            {
                return ("", $"{ex.Message}"); // Возвращаем пустой вывод и сообщение об ошибке
            }
        }
    }
}

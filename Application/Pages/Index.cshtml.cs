using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Application.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // place python script path
            var pyFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "py", "exportpdf.py");

            // place your html here
            string htmlContent = "<!DOCTYPE html>\r\n<html dir='rtl'>\r\n<head>\r\n <title>PDF Example</title>\r\n</head>\r\n<body> " +
                                 "<h1>سلام این برنامه به صورت یک نمونه برای شما ساخته شده ! !</h1>\r\n</body>\r\n</html>";

            // place your target pdf path and name here
            var pdfOutPutPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "pypdf.pdf");

            // place your wkhtmltopdf path on your system
            var wkhtmltopdfPath = "C://Program Files/wkhtmltopdf/bin/wkhtmltopdf.exe";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{pyFilePath}\" \"{htmlContent}\" \"{pdfOutPutPath}\" \"{wkhtmltopdfPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine("Output: " + output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
            }
        }
    }
}

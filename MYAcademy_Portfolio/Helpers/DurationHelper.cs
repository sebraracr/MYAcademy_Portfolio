using System.Text.RegularExpressions;

namespace MYAcademy_Portfolio.Helpers
{
    // "Duration" yazısından (örn: "Ocak 2023 - Ocak 2024", "2021 - 2026", "2026 - Halen")
    // yıl bilgisini çıkarıp sıralamada kullanmak için.
    public static class DurationHelper
    {
        private static readonly Regex YearRegex = new Regex(@"\d{4}");

        // Yazıdaki ilk yıl: başlangıç yılı
        public static int StartYear(string text)
        {
            var years = YearRegex.Matches(text ?? "");
            return years.Count > 0 ? int.Parse(years[0].Value) : 0;
        }

        // Yazıdaki son yıl: bitiş yılı. "Halen" / "Devam" yazıyorsa en üstte çıksın diye çok büyük değer döner.
        public static int EndYear(string text)
        {
            text = text ?? "";

            if (text.Contains("halen", StringComparison.OrdinalIgnoreCase) ||
                text.Contains("devam", StringComparison.OrdinalIgnoreCase))
            {
                return 9999;
            }

            var years = YearRegex.Matches(text);
            return years.Count > 0 ? int.Parse(years[years.Count - 1].Value) : 0;
        }
    }
}

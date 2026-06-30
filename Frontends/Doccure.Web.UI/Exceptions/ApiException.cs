namespace Doccure.Web.UI.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(int statusCode)
            : base(GetMessage(statusCode))
        {
            StatusCode = statusCode;
        }

        private static string GetMessage(int code) => code switch
        {
            401 => "İcazəsiz giriş (401). Zəhmət olmasa yenidən daxil olun.",
            403 => "Bu əməliyyat üçün icazəniz yoxdur (403).",
            404 => "Məlumat tapılmadı (404).",
            500 => "Server xətası baş verdi (500). Zəhmət olmasa bir az sonra yenidən cəhd edin.",
            502 => "Xidmət əlçatmaz (502 Bad Gateway). Servisləri yoxlayın.",
            503 => "Xidmət müvəqqəti olaraq işləmir (503).",
            _   => $"API xətası: {code}."
        };
    }
}

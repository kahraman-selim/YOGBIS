namespace YOGBIS.Common.Const
{
    public static class ResultConstant
    {
        public const string LoginUserInfo = "LoginUserInfo";
        public const string RecordedSuccessfully = "Kayıt başarıyla eklendi.";
        public const string RecordedError = "Kayıt eklenirken bir hata oluştu!";
        public const string UpdatedSuccessfully = "Kayıt başarıyla güncellendi.";
        public const string UpdatedError = "Kayıt güncellenirken bir hata oluştu!";
        public const string DeletedSuccessfully = "Kayıt başarıyla silindi.";
        public const string DeletedError = "Kayıt silinirken bir hata oluştu!";
        public const string NotFound = "Kayıt bulunamadı!";
        public const string InvalidOperation = "Geçersiz işlem!";
        public const string UnauthorizedAccess = "Yetkisiz erişim!";
        public const string SessionExpired = "Oturum süresi doldu!";
        public const string InvalidFileFormat = "Geçersiz dosya formatı!";
        public const string FileUploadError = "Dosya yüklenirken bir hata oluştu!";
        public const string FileUploadSuccess = "Dosya başarıyla yüklendi.";
        public const string InvalidData = "Geçersiz veri!";
        public const string ValidationError = "Doğrulama hatası!";
        public const string DatabaseError = "Veritabanı hatası!";
        public const string SystemError = "Sistem hatası!";

        public static string RecordFound { get; set; }
        public static string RecordNotFound { get; set; }
    }
}

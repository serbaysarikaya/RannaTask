namespace RannaTask.Services.Utilities
{
    public static class Messages
    {
        public static class MessagesProduct
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }
            public static string Add(string productName)
            {
                return $"{productName} adlı kategori başarıyla eklenmiştir.";
            }

            public static string Update(string productName)
            {
                return $"{productName} başlıklı makale başarıyla güncellenmiştir.";
            }

            public static string Delete(string productName)
            {
                return $"{productName} başlıklı makale başarıyla silinmiştir.";
            }
            public static string HardDelete(string productName)
            {
                return $"{productName} başlıklı makale başarıyla veri tabanından silinmiştir.";
            }

        }
    }
}

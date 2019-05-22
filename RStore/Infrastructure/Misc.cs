using System;

namespace RStore.Infrastructure {
    public static class Misc {
        public static string EncodeBase64(string text) {
            if (!string.IsNullOrEmpty(text)) {
                byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(encodedBytes);
            } else {
                return string.Empty;
            }
        }

        public static string DecodeBase64(string text) {
            if (!string.IsNullOrEmpty(text)) {
                byte[] encodedBytes = Convert.FromBase64String(text);
                return System.Text.Encoding.UTF8.GetString(encodedBytes);
            } else {
                return string.Empty;
            }
        }
    }
}

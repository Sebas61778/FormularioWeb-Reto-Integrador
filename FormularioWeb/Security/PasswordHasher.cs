using System;
using System.Security.Cryptography;

namespace FormularioWeb.Security
{
    public static class PasswordHasher
    {
        public static byte[] NuevaSal(){var salt=new byte[16];using(var rng=RandomNumberGenerator.Create())rng.GetBytes(salt);return salt;}
        public static byte[] Hash(string password,byte[] salt){using(var derive=new Rfc2898DeriveBytes(password,salt,150000,HashAlgorithmName.SHA256))return derive.GetBytes(32);}
        public static bool Verificar(string password,byte[] salt,byte[] expected){var actual=Hash(password,salt);int diff=actual.Length^expected.Length;for(int i=0;i<actual.Length&&i<expected.Length;i++)diff|=actual[i]^expected[i];return diff==0;}
    }
}

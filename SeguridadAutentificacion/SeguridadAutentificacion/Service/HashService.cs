using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SeguridadAutentificacion.Models;
using System;
using System.Security.Cryptography;

namespace SeguridadAutentificacion.Service
{
    public class HashService
    {
        public HashResult Hash( string input)
        {
            // Generarl sal aleatoria
            byte[] salt =new byte[128 / 8];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Hash(input, salt);
        }

        private HashResult Hash(string input, byte[] salt)
        {
            // Deriva de una subllave de 256 bits (usa HMACSHA1 con 10000 iteraciones)
            string hashead = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256/8
            ));

            return new HashResult()
            {
                Has = hashead,
                Salt = salt
            };
        }
    }
}

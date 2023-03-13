using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Options
{
    public class JwtOptions
    {
        public string SigningSecretKey { get; init; }
        public string EncryptionSecretKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }

    }
}

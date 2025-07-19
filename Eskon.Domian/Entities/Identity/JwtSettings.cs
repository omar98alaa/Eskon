﻿
namespace Eskon.Domian.Entities.Identity
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int RefreshTokenExpireDatePeriod { get; set; }

        public bool ValidateIssuer { get; set; }

        public bool ValidateAudience { get; set; }

        public bool ValidateLifeTime { get; set; }

        public bool ValidateIssuerSigningKey { get; set; }
    }
}

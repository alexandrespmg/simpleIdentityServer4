﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            //services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers())
                .AddSigningCredential(new RsaSecurityKey(
                    new RSAParameters
                    {
                            D = Base64UrlEncoder.DecodeBytes("C6EGZYf9U6RI5Z0BBoSlwy_gKumVqRx-dBMuAfPM6KVbwIUuSJKT3ExeL5P0Ky1b4p-j2S3u7Afnvrrj4HgVLnC1ks6rEOc2ne5DYQq8szST9FMutyulcsNUKLOM5cVromALPz3PAqE2OCLChTiQZ5XZ0AiH-KcG-3hKMa-g1MVnGW-SSmm27XQwRtUtFQFfxDuL0E0fyA9O9ZFBV5201ledBaLdDcPBF8cHC53Gm5G6FRX3QVpoewm3yGk28Wze_YvNl8U3hvbxei2Koc_b9wMbFxvHseLQrxvFg_2byE2em8FrxJstxgN7qhMsYcAyw1qGJY-cYX-Ab_1bBCpdcQ"),
                            DP = Base64UrlEncoder.DecodeBytes("ErP3OpudePAY3uGFSoF16Sde69PnOra62jDEZGnPx_v3nPNpA5sr-tNc8bQP074yQl5kzSFRjRlstyW0TpBVMP0ocbD8RsN4EKsgJ1jvaSIEoP87OxduGkim49wFA0Qxf_NyrcYUnz6XSidY3lC_pF4JDJXg5bP_x0MUkQCTtQE"),
                            DQ = Base64UrlEncoder.DecodeBytes("YbBsthPt15Pshb8rN8omyfy9D7-m4AGcKzqPERWuX8bORNyhQ5M8JtdXcu8UmTez0j188cNMJgkiN07nYLIzNT3Wg822nhtJaoKVwZWnS2ipoFlgrBgmQiKcGU43lfB5e3qVVYUebYY0zRGBM1Fzetd6Yertl5Ae2g2CakQAcPs"),
                            Exponent = Base64UrlEncoder.DecodeBytes("AQAB"),
                            InverseQ = Base64UrlEncoder.DecodeBytes("lbljWyVY-DD_Zuii2ifAz0jrHTMvN-YS9l_zyYyA_Scnalw23fQf5WIcZibxJJll5H0kNTIk8SCxyPzNShKGKjgpyZHsJBKgL3iAgmnwk6k8zrb_lqa0sd1QWSB-Rqiw7AqVqvNUdnIqhm-v3R8tYrxzAqkUsGcFbQYj4M5_F_4"),
                            Modulus = Base64UrlEncoder.DecodeBytes("6-FrFkt_TByQ_L5d7or-9PVAowpswxUe3dJeYFTY0Lgq7zKI5OQ5RnSrI0T9yrfnRzE9oOdd4zmVj9txVLI-yySvinAu3yQDQou2Ga42ML_-K4Jrd5clMUPRGMbXdV5Rl9zzB0s2JoZJedua5dwoQw0GkS5Z8YAXBEzULrup06fnB5n6x5r2y1C_8Ebp5cyE4Bjs7W68rUlyIlx1lzYvakxSnhUxSsjx7u_mIdywyGfgiT3tw0FsWvki_KYurAPR1BSMXhCzzZTkMWKE8IaLkhauw5MdxojxyBVuNY-J_elq-HgJ_dZK6g7vMNvXz2_vT-SykIkzwiD9eSI9UWfsjw"),
                            P = Base64UrlEncoder.DecodeBytes("_avCCyuo7hHlqu9Ec6R47ub_Ul_zNiS-xvkkuYwW-4lNnI66A5zMm_BOQVMnaCkBua1OmOgx7e63-jHFvG5lyrhyYEmkA2CS3kMCrI-dx0fvNMLEXInPxd4np_7GUd1_XzPZEkPxBhqf09kqryHMj_uf7UtPcrJNvFY-GNrzlJk"),
                            Q = Base64UrlEncoder.DecodeBytes("7gvYRkpqM-SC883KImmy66eLiUrGE6G6_7Y8BS9oD4HhXcZ4rW6JJKuBzm7FlnsVhVGro9M-QQ_GSLaDoxOPQfHQq62ERt-y_lCzSsMeWHbqOMci_pbtvJknpMv4ifsQXKJ4Lnk_AlGr-5r5JR5rUHgPFzCk9dJt69ff3QhzG2c"),
                    }
                ));

            services.AddLocalApiAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to support static files
            //app.UseStaticFiles();

            app.UseIdentityServer();

            // uncomment, if you want to add an MVC-based UI
            //app.UseMvcWithDefaultRoute();
        }
    }
}

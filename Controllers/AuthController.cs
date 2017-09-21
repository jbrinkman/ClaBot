using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ClaBot.Models.Github;
using Jose;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace ClaBot.Controllers
{
    public class AuthController: GitHubControllerBase
    {
        string AppId => ConfigurationManager.AppSettings["AppId"];

        public async Task<AccessToken> GetAccessToken(string owner, string reponame, string InstallationId)
        {
            var repository = $"{owner}/{reponame}";
            AccessToken tokenDocument = CacheController.Instance().GetCacheItem(repository) as AccessToken;


            if (tokenDocument == null)
            {
                string issuerClaim = GetIssuerClaimJWT();

                AccessToken token = await GitHubApi.GetAccessToken(InstallationId, $"Bearer {issuerClaim}");
                if (token != null)
                {
                    tokenDocument = token;

                    await CacheController.Instance().AddCacheItemAsync(repository, tokenDocument);
                }
            }

            return tokenDocument;
        }

        private string GetIssuerClaimJWT()
        {
            string token;
            AsymmetricCipherKeyPair keyPair;
            RSACryptoServiceProvider key;

            long now = DateTimeOffset.Now.ToUnixTimeSeconds();
            var payload = new
            {
                iat = now,
                exp = now + (10 * 60),
                iss = AppId
            };

            JWT.DefaultSettings.JsonMapper = new NewtonsoftMapper();

            using (var reader = File.OpenText($"private-key.pem")) // file containing RSA PKCS1 private key
                keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

            var dnRSA = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);


            using (key = new RSACryptoServiceProvider())
            {
                key.ImportParameters(dnRSA);
                token = JWT.Encode(payload, key, JwsAlgorithm.RS256);
            }


            return token;

        }

        public static AuthController Instance()
        {
            return new AuthController();
        }

    }
}

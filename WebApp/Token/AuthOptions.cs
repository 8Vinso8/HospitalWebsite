using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Token;

public class AuthOptions
{
  public const string ISSUER = "HospitalWebsite";
  public const string AUDIENCE = "Hospital";
  const string KEY = "megasecretkeybutwaybigger";
  public const int LIFETIME = 10;

  public static SymmetricSecurityKey GetSymmetricSecurityKey()
  {
    return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
  }
}
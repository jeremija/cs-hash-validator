using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System;

public class Hash {

  public static string REGEX_SIGNATURE_END = @"&signature=.*$";

  public static string EncodeURL(string secret, string url) {
     // Encode the url and Convert the string to a byte array.
    string encodedURL = HttpUtility.UrlEncode(url.ToUpper());
    byte[] encodedUrlBytes = Encoding.UTF8.GetBytes(encodedURL.ToUpper());
    // Get the shared secret as a byte array.
    byte[] sharedSecretBytes = Encoding.UTF8.GetBytes(secret);
    // Calculate the hash.
    byte[] hash = new HMACSHA1(sharedSecretBytes).ComputeHash(encodedUrlBytes);
    // Return the hexadecimal string.
    string localHash = BitConverter.ToString(hash).Replace("-", "");
    url = String.Format(@"{0}&signature={1}", url, localHash);
    return url;
  }

  public static int Main(string[] args) {
    if (args.Length != 2) {
      System.Console.Error.WriteLine(@"Usage: Hash.exe <secret> <url>");
      return 1;
    }

    string secret = args[0].Trim();
    string url = args[1].Trim();

    string urlWithoutSignature = Regex.Replace(url, REGEX_SIGNATURE_END, "");
    string encodedUrl = EncodeURL(secret, urlWithoutSignature);

    System.Console.Error.WriteLine(@"");
    System.Console.Error.WriteLine(@"url:          " + url);
    System.Console.Error.WriteLine(@"");
    System.Console.Error.WriteLine(@"encoded:      " + encodedUrl);
    System.Console.Error.WriteLine(@"");

    bool matches = url.Equals(encodedUrl);

    if (!matches) {
      System.Console.Error.WriteLine(@"ERROR: Hash does not match");
      return 2;
    }

    System.Console.Error.WriteLine(@"hash matches: " + url.Equals(encodedUrl));
    return 0;
  }
}

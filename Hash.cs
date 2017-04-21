using System.Security.Cryptography;
using System.Text;
using System.Web;
using System;

public class Hash {

  private static string EncodeURL(string secret, string url) {
     // Encode the url and Convert the string to a byte array.
    string encodedURL = HttpUtility.UrlEncode(url.ToUpper());
    byte[] encodedUrlBytes = Encoding.UTF8.GetBytes(encodedURL.ToUpper());
    // Get the shared secret as a byte array.
    byte[] sharedSecretBytes = Encoding.UTF8.GetBytes(secret);
    // Calculate the hash.
    byte[] hash = new HMACSHA1(sharedSecretBytes).ComputeHash(encodedUrlBytes);
    // Return the hexadecimal string.
    string localHash = BitConverter.ToString(hash).Replace("-", "");
    url = String.Format("{0}&hash={1}", url, localHash);
    return url;
  }

  public static void Main(string[] args) {
    if (args.Length != 2) {
      System.Console.Error.WriteLine("Usage: Hash.exe <secret> <url>");
      return;
    }

    string secret = args[0];
    string url = args[1];


   https://www.new-innov.com/WebServices/NYUServices/NIIntegration.svc/UpdateEmail?kerberos=aao350&nyuemail=Arshed.Al-Obeidi@nyumc.org&consumerkey=HjEnOiWN7u8TEBTBVlV36wPtHpLt3SOI7pcPD2ljUW7BmNYzzXjhnHh5ezjI3Bw&timestamp=1490288604&signature=4C4B6054780B2CA821612C6716235BE0BD76C21B
  }
}

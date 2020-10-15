using System;
using static System.Console;
using System.Collections.Generic;

class MainClass {
  public static void Main (string[] args) {
    // Defines the key that will be used (can be changed here to change the cipher)
    string key = "01001110";
    Write("\n\nEnter the message to encrypt: ");
    string message = ReadLine();
    // Calls encryption method
    string encrypted = XOREncrypt(message, key);
    WriteLine("\nEncrypted message: {0}\n\n", encrypted);
  }

  public static string XOREncrypt (string message, string key) {
    // States the current key being used (for informational purposes)
    WriteLine("\nXOR Key: {0}", key);
    // Initializes list for ascii codes of the chars in the input string
    List<string> asciis = new List<string>();
    // Adds all ascii codes to the list as strings
    foreach (char c in message) {
      int ascii = (int) c;
      asciis.Add(Convert.ToString(ascii));
    }
    // Defines the variable that will be returned at the end
    string encrypted = "";
    // Loops through every ascii code in the list...
    foreach (string ascii in asciis) {
      // Defines a string that will hold the new binary  
      // ascii code after being compared with the key
      string encryptedBin = "";
      // Calls method to convert the ascii code to binary
      string bin = Convert.ToString(ToBin(Convert.ToInt32(ascii), 1));
      // Ensures that the number is an 8 bit binary number
      if (bin.Length < 8) {
        List<char> chars = new List<char> (bin.ToCharArray());
        for (int t = 0; t < (8 - bin.Length); t++) {
          // Adds '0's to the far left of the number if it is less than 8 bits
          chars.Insert (0, '0');
        }
        // Replaces the short binary number with the 8 bit version
        bin = new string(chars.ToArray());
      }
      // For every bit in the number...
      for (int i = 0; i < 8; i++) {
        // XOR gate to make the encrypted 8 bit number
        if ((bin[i] == '1' || key[i] == '1') && bin[i] != key[i]) {
          encryptedBin += "1";
        }
        else {
          encryptedBin += "0";
        }
      }
      // Takes the ascii code (denary) version of the new number
      int newAscii = ToDen(Convert.ToInt32(encryptedBin), 1);
      // Converts the ascii code into a char
      char c = (char) newAscii;
      // Adds the now encrypted char to the output string
      encrypted += c;
    }
    return encrypted;
  }
  // Uses recursion to convert denary numbers to binary
  public static int ToBin (int num, int den) {
    if (num / 2 > 0) {
      int y = (num % 2) * den;
      return (y + ToBin(num/2, den*10));
    }
    else {
      return (num % 2) * den;
    }
  }
  // Uses recursion to convert binary numbers to denary
  public static int ToDen (int num, int bin) {
    if (num / 10 > 0) {
      int y = (num % 10) * bin;
      return (y + ToDen(num/10, bin*2));
    }
    else {
      return (num % 10) * bin;
    }
  }
}
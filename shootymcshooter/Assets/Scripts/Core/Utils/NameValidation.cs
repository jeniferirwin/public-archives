using System.Text.RegularExpressions;
using UnityEngine;

public class NameValidation : MonoBehaviour
{
    private const string REG_NAME = @"^[A-Za-z][A-Za-z0-9 ]*$";

    public static bool IsValidAsPlayerName(string input)
    {
        if (input == "") return false;
        if (input.Length > 16) return false;
        Regex rx = new Regex(REG_NAME);
        if (rx.Match(input).Success) return true;
        return false;
    }
}

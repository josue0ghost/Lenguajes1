using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Scanner
{
	class Automata
	{
		static Queue<char> Entrada = new Queue<char>();
		static string token = "";
		static char[] LETRAarray = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '_' };
		static List<char> LETRA = new List<char>(LETRAarray);
		static char[] DIGITOarray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		static List<char> DIGITO = new List<char>(DIGITOarray);
		static char[] CHARSETarray = new char[] { ' ', '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', '', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?' };
		static List<char> CHARSET = new List<char>(CHARSETarray);
		static void Main()
		{
			Console.WriteLine("Entrada: ");
			string input = Console.ReadLine();
			char[] cinput = input.ToCharArray();
			bool aceptacion = false;
			Entrada = new Queue<char>(cinput);
			int Estado = 0;
			while (Entrada.Count > 0)
			{
				char ToConsume = Entrada.Peek();
				if (ToConsume != ' ' && ToConsume != '\t')
				{
					switch (Estado)
					{
						case 0:
							Estado = Trans0(ToConsume);
							aceptacion = false;
							break;
						case 1:
							Estado = Trans1(ToConsume);
							aceptacion = true;
							break;
						case 2:
							Estado = Trans2(ToConsume);
							aceptacion = false;
							break;
						case 3:
							Estado = Trans3(ToConsume);
							aceptacion = false;
							break;
						case 4:
							Estado = Trans4(ToConsume);
							aceptacion = true;
							break;
						case 5:
							Estado = Trans5(ToConsume);
							aceptacion = true;
							break;
						case 6:
							Estado = Trans6(ToConsume);
							aceptacion = true;
							break;
						case 7:
							Estado = Trans7(ToConsume);
							aceptacion = false;
							break;
						case 8:
							Estado = Trans8(ToConsume);
							aceptacion = true;
							break;
						case 9:
							Estado = Trans9(ToConsume);
							aceptacion = false;
							break;
						case 10:
							Estado = Trans10(ToConsume);
							aceptacion = false;
							break;
						case 11:
							Estado = Trans11(ToConsume);
							aceptacion = false;
							break;
						case 12:
							Estado = Trans12(ToConsume);
							aceptacion = false;
							break;
						case 13:
							Estado = Trans13(ToConsume);
							aceptacion = true;
							break;
						case 14:
							Estado = Trans14(ToConsume);
							aceptacion = true;
							break;
						case 15:
							Estado = Trans15(ToConsume);
							aceptacion = true;
							break;
						case 16:
							Estado = Trans16(ToConsume);
							aceptacion = true;
							break;
						case 17:
							Estado = Trans17(ToConsume);
							aceptacion = false;
							break;
						case 18:
							Estado = Trans18(ToConsume);
							aceptacion = false;
							break;
						case 19:
							Estado = Trans19(ToConsume);
							aceptacion = false;
							break;
						case 20:
							Estado = Trans20(ToConsume);
							aceptacion = false;
							break;
						case 21:
							Estado = Trans21(ToConsume);
							aceptacion = false;
							break;
						case 22:
							Estado = Trans22(ToConsume);
							aceptacion = false;
							break;
						default:
							break;
					}
					if (Estado == -1 && aceptacion == true)
					{
						string tkn = obtenerToken(token);
						Console.WriteLine(token + " = " + tkn);
						token = "";
						Estado = 0;
						aceptacion = false;
					}
					if (Estado == -1 && aceptacion == false)
					{
						string tkn = obtenerToken(token);
						Console.WriteLine(token + " = " + tkn);
						token = "";
						Estado = 0;
					}
				}
				else
				{
					Entrada.Dequeue();
				}
			}
			if (Estado == 1 || Estado == 4 || Estado == 5 || Estado == 6 || Estado == 8 || Estado == 13 || Estado == 14 || Estado == 15 || Estado == 16)
			{
				string tkn = obtenerToken(token);
				Console.WriteLine(token + " = " + tkn);
				token = "";
			}
			Console.WriteLine("TERMINADO");
			Console.ReadLine();
		}
		static int Trans0(char input)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 2;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '\'':
					ret = 3;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '=':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '<':
					ret = 5;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '>':
					ret = 6;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '+':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '-':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case 'O':
					ret = 7;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '*':
					ret = 8;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case 'A':
					ret = 9;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case 'N':
					ret = 10;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case 'D':
					ret = 11;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case 'M':
					ret = 12;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '(':
					ret = 13;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case ')':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case ';':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '.':
					ret = 14;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '{':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '}':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '[':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case ']':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case ':':
					ret = 15;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case ',':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 1;
				token += input.ToString();
				Entrada.Dequeue();
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 16;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans1(char input)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 1;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans2(char input)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 17;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans3(char input)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 18;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans4(char input)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans5(char input)
		{
			int ret;
			switch (input)
			{
				case '=':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '>':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans6(char input)
		{
			int ret;
			switch (input)
			{
				case '=':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans7(char input)
		{
			int ret;
			switch (input)
			{
				case 'R':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans8(char input)
		{
			int ret;
			switch (input)
			{
				case ')':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans9(char input)
		{
			int ret;
			switch (input)
			{
				case 'N':
					ret = 19;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans10(char input)
		{
			int ret;
			switch (input)
			{
				case 'O':
					ret = 20;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans11(char input)
		{
			int ret;
			switch (input)
			{
				case 'I':
					ret = 21;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans12(char input)
		{
			int ret;
			switch (input)
			{
				case 'O':
					ret = 22;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans13(char input)
		{
			int ret;
			switch (input)
			{
				case '*':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans14(char input)
		{
			int ret;
			switch (input)
			{
				case '.':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans15(char input)
		{
			int ret;
			switch (input)
			{
				case '=':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans16(char input)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 16;
				token += input.ToString();
				Entrada.Dequeue();
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 16;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans17(char input)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans18(char input)
		{
			int ret;
			switch (input)
			{
				case '\'':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans19(char input)
		{
			int ret;
			switch (input)
			{
				case 'D':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans20(char input)
		{
			int ret;
			switch (input)
			{
				case 'T':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans21(char input)
		{
			int ret;
			switch (input)
			{
				case 'V':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans22(char input)
		{
			int ret;
			switch (input)
			{
				case 'D':
					ret = 4;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static string obtenerToken(string cadena)
		{
			string tkn = "";
			List<string> tokensVal = new List<string> { "", "", "=", "<>", "<", ">", ">=", "<=", "+", "-", "OR", "*", "AND", "MOD", "DIV", "NOT", "(*", "*)", ";", ".", "{", "}", "(", ")", "[", "]", "..", ":", ",", ":=", "" };
			List<string> tokensID = new List<string> { "1", "2", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "3" };
			Dictionary<int, string> actions = new Dictionary<int, string>() { { 18, "PROGRAM" }, { 19, "INCLUDE" }, { 20, "CONST" }, { 21, "TYPE" }, { 22, "VAR" }, { 23, "RECORD" }, { 24, "ARRAY" }, { 25, "OF" }, { 26, "PROCEDURE" }, { 27, "FUNCTION" }, { 28, "IF" }, { 29, "THEN" }, { 30, "ELSE" }, { 31, "FOR" }, { 32, "TO" }, { 33, "WHILE" }, { 34, "DO" }, { 35, "EXIT" }, { 36, "END" }, { 37, "CASE" }, { 38, "BREAK" }, { 39, "DOWNTO" }, };
			if (actions.FirstOrDefault(x => x.Value.Equals(cadena, StringComparison.OrdinalIgnoreCase)).Value != null)
			{
				tkn = actions.FirstOrDefault(x => x.Value.Equals(cadena, StringComparison.OrdinalIgnoreCase)).Key.ToString();
			}
			else if (tokensVal.Contains(cadena))
			{
				int index = tokensVal.IndexOf(cadena);
				tkn = tokensID[index];
			}
			else if (EvalTOKEN1(cadena) == true)
			{
				tkn = "1";
			}
			else if (EvalTOKEN2(cadena) == true)
			{
				tkn = "2";
			}
			else if (EvalTOKEN3(cadena) == true)
			{
				tkn = "3";
			}
			return tkn;
		}
		static bool EvalTOKEN1(string input)
		{
			char[] cinput = input.ToCharArray();
			bool aceptacion = false;
			Queue<char> AuxEntrada = new Queue<char>(cinput);
			int Estado = 0;
			while (AuxEntrada.Count > 0)
			{
				char ToConsume = AuxEntrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans23(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 1:
						Estado = Trans24(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 2:
						Estado = Trans25(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 3:
						Estado = Trans26(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 4:
						Estado = Trans27(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 5:
						Estado = Trans28(ToConsume, ref AuxEntrada);
						aceptacion = true;
						break;
					default:
						break;
				}
				if (Estado == -1)
				{
					return false;
				}
			}
			if (Estado == 5)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		static int Trans23(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans24(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 1;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans25(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 3;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans26(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 3;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans27(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans28(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			return ret;
		}
		static bool EvalTOKEN2(string input)
		{
			char[] cinput = input.ToCharArray();
			bool aceptacion = false;
			Queue<char> AuxEntrada = new Queue<char>(cinput);
			int Estado = 0;
			while (AuxEntrada.Count > 0)
			{
				char ToConsume = AuxEntrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans29(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 1:
						Estado = Trans30(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 2:
						Estado = Trans31(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 3:
						Estado = Trans32(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 4:
						Estado = Trans33(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 5:
						Estado = Trans34(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 6:
						Estado = Trans35(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 7:
						Estado = Trans36(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 8:
						Estado = Trans37(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 9:
						Estado = Trans38(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 10:
						Estado = Trans39(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 11:
						Estado = Trans40(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 12:
						Estado = Trans41(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 13:
						Estado = Trans42(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 14:
						Estado = Trans43(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 15:
						Estado = Trans44(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 16:
						Estado = Trans45(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 17:
						Estado = Trans46(ToConsume, ref AuxEntrada);
						aceptacion = true;
						break;
					default:
						break;
				}
				if (Estado == -1)
				{
					return false;
				}
			}
			if (Estado == 17)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		static int Trans29(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 13;
					Entrada.Dequeue();
					break;
				case '\'':
					ret = 14;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans30(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 3;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans31(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 4;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans32(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 5;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans33(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '\'':
					ret = 5;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans34(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans35(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 7;
					Entrada.Dequeue();
					break;
				case '\'':
					ret = 8;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans36(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 9;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans37(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 10;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans38(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 11;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans39(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '\'':
					ret = 11;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans40(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans41(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 13;
					Entrada.Dequeue();
					break;
				case '\'':
					ret = 14;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans42(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 15;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans43(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (CHARSET.IndexOf(input) >= 0)
			{
				ret = 16;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans44(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 17;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans45(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '\'':
					ret = 17;
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans46(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static bool EvalTOKEN3(string input)
		{
			char[] cinput = input.ToCharArray();
			bool aceptacion = false;
			Queue<char> AuxEntrada = new Queue<char>(cinput);
			int Estado = 0;
			while (AuxEntrada.Count > 0)
			{
				char ToConsume = AuxEntrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans47(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 1:
						Estado = Trans48(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 2:
						Estado = Trans49(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 3:
						Estado = Trans50(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 4:
						Estado = Trans51(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 5:
						Estado = Trans52(ToConsume, ref AuxEntrada);
						aceptacion = true;
						break;
					default:
						break;
				}
				if (Estado == -1)
				{
					return false;
				}
			}
			if (Estado == 5)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		static int Trans47(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans48(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 1;
				Entrada.Dequeue();
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 1;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans49(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 3;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans50(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 3;
				Entrada.Dequeue();
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 3;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans51(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans52(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				default:
					ret = -1;
					break;
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 5;
				Entrada.Dequeue();
			}
			return ret;
		}
	}
}
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
		static char[] LETRAarray = new char[] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','_'};
		static List<char> LETRA = new List<char>(LETRAarray);
		static char[] DIGITOarray = new char[] {'0','1','2','3','4','5','6','7','8','9'};
		static List<char> DIGITO = new List<char>(DIGITOarray);
		static char[] CHARSETarray = new char[] {' ','!','\"','#','$','%','&','\'','(',')','*','+',',','-','.','/','0','1','2','3','4','5','6','7','8','9',':',';','<','=','>','?','@','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','[','\\',']','^','_','`','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','{','|','}','~','','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?','?'};
		static List<char> CHARSET = new List<char>(CHARSETarray);
		static void Main()
		{
			Console.WriteLine("Entrada: ");
			string input = Console.ReadLine();
			char[] cinput = input.ToCharArray();
			bool aceptacion = false;
			Entrada = new Queue<char>(cinput);
			int Estado = 0;
			while(Entrada.Count > 0)
			{
				char ToConsume = Entrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans0(ToConsume);
						aceptacion = true;
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
						aceptacion = false;
						break;
					case 7:
						Estado = Trans7(ToConsume);
						aceptacion = false;
						break;
					default:
						break;
				}
				if (Estado == -1 && aceptacion == true)
				{
					string tkn = obtenerToken(token);
					Console.WriteLine(token + " = " + tkn);
					Entrada.Dequeue();
					token = "";
					Estado = 0;
					aceptacion = false;
				}
				if (Estado == -1 && aceptacion == false)
				{
					string tkn = obtenerToken(token);
					Console.WriteLine(token + " = " + tkn);
					Entrada.Dequeue();
					token = "";
					Estado = 0;
					Entrada.Dequeue();
				}
			}
			if (Estado == 0 || Estado == 1 || Estado == 4 || Estado == 5)
			{
				string tkn = obtenerToken(token);
				Console.WriteLine(token + " = " + tkn);
				token = "";
			}
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
				ret = 5;
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
				ret = 6;
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
				ret = 7;
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
				default:
					ret = -1;
					break;
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 5;
				token += input.ToString();
				Entrada.Dequeue();
			}
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 5;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans6(char input)
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
		static int Trans7(char input)
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
		static string obtenerToken(string cadena)
		{
			string tkn = "";
			List<string> tokensVal = new List<string> {"","","=",""};
			List<string> tokensID = new List<string> {"1","2","4","3"};
			Dictionary<int, string> actions = new Dictionary<int, string>(){{18 , "PROGRAM"},{19 , "INCLUDE"},{20 , "CONST"},{21 , "TYPE"},{22 , "VAR"},{23 , "RECORD"},{24 , "ARRAY"},{25 , "OF"},{26 , "PROCEDURE"},{27 , "FUNCTION"},{28 , "IF"},{29 , "THEN"},{30 , "ELSE"},{31 , "FOR"},{32 , "TO"},{33 , "WHILE"},{34 , "DO"},{35 , "EXIT"},{36 , "END"},{37 , "CASE"},{38 , "BREAK"},{39 , "DOWNTO"},};
			if (actions.FirstOrDefault(x => x.Value.Equals(cadena, StringComparison.OrdinalIgnoreCase)).Value != null)
			{
				tkn = actions.FirstOrDefault(x => x.Value.Equals(cadena, StringComparison.OrdinalIgnoreCase)).Key.ToString();
			}
			else if (tokensVal.Contains(cadena))
			{
				int index = tokensVal.IndexOf(cadena);
				tkn = tokensID[index];
			}
			return tkn;
		}
	}
}
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
			else if (EvalTOKEN1(cadena) == true){
				tkn = "1";
			}
			else if (EvalTOKEN2(cadena) == true){
				tkn = "2";
			}
			else if (EvalTOKEN3(cadena) == true){
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
			while(AuxEntrada.Count > 0)
			{
				char ToConsume = AuxEntrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans8(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 1:
						Estado = Trans9(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 2:
						Estado = Trans10(ToConsume, ref AuxEntrada);
						aceptacion = true;
						break;
					case 3:
						Estado = Trans11(ToConsume, ref AuxEntrada);
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
			if (Estado == 2 || Estado == 3)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		static int Trans8(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans9(char input, ref Queue<char> Entrada)
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
		static int Trans10(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans11(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
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
			while(AuxEntrada.Count > 0)
			{
				char ToConsume = AuxEntrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans12(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 1:
						Estado = Trans13(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 2:
						Estado = Trans14(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 3:
						Estado = Trans15(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 4:
						Estado = Trans16(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 5:
						Estado = Trans17(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 6:
						Estado = Trans18(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 7:
						Estado = Trans19(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 8:
						Estado = Trans20(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 9:
						Estado = Trans21(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 10:
						Estado = Trans22(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 11:
						Estado = Trans23(ToConsume, ref AuxEntrada);
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
			if (Estado == 11)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		static int Trans12(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 7;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '\'':
					ret = 8;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans13(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans14(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans15(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 5;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans16(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '\'':
					ret = 5;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans17(char input, ref Queue<char> Entrada)
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
		static int Trans18(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 7;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				case '\'':
					ret = 8;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans19(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans20(char input, ref Queue<char> Entrada)
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
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
		static int Trans21(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '"':
					ret = 11;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
		}
		static int Trans22(char input, ref Queue<char> Entrada)
		{
			int ret;
			switch (input)
			{
				case '\'':
					ret = 11;
					token += input.ToString();
					Entrada.Dequeue();
					break;
				default:
					ret = -1;
					break;
			}
			return ret;
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
			return ret;
		}
		static bool EvalTOKEN3(string input)
		{
			char[] cinput = input.ToCharArray();
			bool aceptacion = false;
			Queue<char> AuxEntrada = new Queue<char>(cinput);
			int Estado = 0;
			while(AuxEntrada.Count > 0)
			{
				char ToConsume = AuxEntrada.Peek();
				switch (Estado)
				{
					case 0:
						Estado = Trans24(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 1:
						Estado = Trans25(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 2:
						Estado = Trans26(ToConsume, ref AuxEntrada);
						aceptacion = false;
						break;
					case 3:
						Estado = Trans27(ToConsume, ref AuxEntrada);
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
			if (Estado == 3)
			{
				return true;
			}
			else
			{
				return false;
			}
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
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 3;
				token += input.ToString();
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
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 1;
				token += input.ToString();
				Entrada.Dequeue();
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 1;
				token += input.ToString();
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
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 3;
				token += input.ToString();
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
			if (LETRA.IndexOf(input) >= 0)
			{
				ret = 3;
				token += input.ToString();
				Entrada.Dequeue();
			}
			if (DIGITO.IndexOf(input) >= 0)
			{
				ret = 3;
				token += input.ToString();
				Entrada.Dequeue();
			}
			return ret;
		}
	}
}
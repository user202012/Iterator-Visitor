using System;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.IO;


	internal interface IVisitor
	{
		void Visit(SHA256Hash str);
		void Visit(MD5Hash str);
	}

	internal abstract class Hash
	{
		public abstract void Accept(IVisitor visitor);
	}

	internal class SHA256Hash : Hash
	{
		public SHA256Hash(string input)
		{
			static string SHA256Hasher(string input)
			{
				SHA256 SHA256 = SHA256.Create();
				byte[] data = SHA256.ComputeHash(Encoding.Default.GetBytes(input));
				StringBuilder sBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
			Content = SHA256Hasher(input);
		}

		public string Content { get; }

		public override void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	internal class MD5Hash : Hash
	{
		public MD5Hash(string input)
		{
			static string MD5Hasher(string input)
			{
				MD5 md5Hasher = MD5.Create();
				byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
				StringBuilder sBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
			Content = MD5Hasher(input);
		}

		public string Content { get; }

		public override void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}





	internal class Writer : IVisitor
	{
		public void Visit(SHA256Hash str)
		{
			Console.WriteLine(str.Content);
		}

		public void Visit(MD5Hash str)
		{
			Console.WriteLine(str.Content);
		}
	}

	internal class Saver : IVisitor
	{
		void saver(string text)
		{
			string writePath = @"C:\Users\User\source\repos\test_public-master\lab2\lab2\key.txt";
			using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
			{
				sw.WriteLine(text);
			}
		}
		public void Visit(SHA256Hash str)
		{
			saver(str.Content);
		}

		public void Visit(MD5Hash str)
		{
			saver(str.Content);
		}
	}

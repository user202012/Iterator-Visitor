using System;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace lab2
{
	class Program1
	{
		static void Main(string[] args)
		{
			Library library = new Library(@"C:\Users\User\source\repos\test_public-master\lab2\lab2\Weaky.txt");
			Reader reader = new Reader();
			reader.GetMethod(library);
			Hash str_sha256 = new SHA256Hash("Hello world");
			Hash str_md5 = new MD5Hash("Hello world");
			IVisitor visitor_w = new Writer();
			IVisitor visitor_s = new Saver();
			str_sha256.Accept(visitor_w);
			str_sha256.Accept(visitor_s);
			str_md5.Accept(visitor_s);
			str_md5.Accept(visitor_w);
		}
	}
}

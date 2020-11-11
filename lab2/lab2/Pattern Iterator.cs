using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;


    class Reader
    {
        public void GetMethod(Library library)
        {
            IDictionaryIterator iterator = library.CreateNumerator();
            while (iterator.Terminate())
            {
                Method algorithm = iterator.Next();
                Console.WriteLine(algorithm.Name);
            }
        }
    }

    interface IDictionaryIterator
    {
        bool Terminate();
        Method Next();
    }
    interface IDictionaryNumerable
    {
        IDictionaryIterator CreateNumerator();
        int Lenght { get; }
        Method this[int index] { get; }
    }
    class Method
    {
        public string Name { get; set; }
    }

    class Library : IDictionaryNumerable
    {
        private static string fileContent;
        private static string WeakCode(string str)
        {
            static string TopSecret(char character, ushort secretKey = 0x0088)
            {
                character = (char)(character ^ secretKey); //Производим XOR операцию
                return Convert.ToString(character);
            }
            string newStr = "";
            for (int i = 0; i < str.Length-2; i++)
            {
                newStr += TopSecret(str[i]);
            }
            return newStr;
        }
        
        private static string getString(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                return fileContent = sr.ReadToEnd();
            }
        }

        private Method[] algorithms;
        public Library(string str)
        {
            algorithms = new Method[]
            {
            new Method {Name = getString(str)},
            new Method {Name = WeakCode(fileContent)},
            };
        }
        public int Lenght
        {
            get { return algorithms.Length; }
        }

        public Method this[int index]
        {
            get { return algorithms[index]; }
        }
        public IDictionaryIterator CreateNumerator()
        {
            return new LibraryNumerator(this);
        }
    }
    class LibraryNumerator : IDictionaryIterator
    {
        IDictionaryNumerable aggregate;
        int index = 0;
        public LibraryNumerator(IDictionaryNumerable a)
        {
            aggregate = a;
        }
        public bool Terminate()
        {
            return index < aggregate.Lenght;
        }

        public Method Next()
        {
            return aggregate[index++];
        }
    }
using System.Text;
using System.Text.RegularExpressions;

namespace WalletUtils
{
    public class History
    {
        public String Description { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
        public History(int value,String description)
        {
            this.Description = description;
            this.Value = value;
        }

        override
        public String ToString()
        {
            return this.Description + " | " + this.Value + " | " + this.Date.ToLongTimeString();
        }
    }

    public class Budget
    {
        public String Description { get; set; }
        public int Value { get; set; }
        public String Name { get; set; }
        public Budget(int value,String description,String name)
        {
            this.Description = description;
            this.Name = name;
            this.Value = value;
        }
        override
         public String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("name: ")
                .Append(this.Name)
                .Append("\ndescription: ")
                .Append(this.Description)
                .Append("\nvalue: ")
                .Append(this.Value);

            return builder.ToString();
        }
    }
    public class CodeUtils
    {
        public CodeUtils()
        {

        }
        public static void WriteIntoTerminal(params String[] args)
        {
            foreach (String item in args)
            {
                Console.WriteLine(item);
            }
        }
        public static int ReadLineInt()
        {
            String input = Console.ReadLine();
            if (input == null || new Regex("[a-b] | [A-b]").Match(input).Success || input.Equals(""))
            {
                return 0;
            }
            return int.Parse(input);
        }
    }
}
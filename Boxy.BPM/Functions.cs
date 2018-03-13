using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxy.Domain.Entities;

namespace Boxy.BPM
{
    public class Functions
    {
        private static Dictionary<string, string> translate = new Dictionary<string, string>();
        private static void PrepareTranslate()
        {
            translate.Add("а", "a");
            translate.Add("б", "b");
            translate.Add("в", "v");
            translate.Add("г", "g");
            translate.Add("д", "d");
            translate.Add("е", "e");
            translate.Add("ё", "yo");
            translate.Add("ж", "zh");
            translate.Add("з", "z");
            translate.Add("и", "i");
            translate.Add("й", "j");
            translate.Add("к", "k");
            translate.Add("л", "l");
            translate.Add("м", "m");
            translate.Add("н", "n");
            translate.Add("о", "o");
            translate.Add("п", "p");
            translate.Add("р", "r");
            translate.Add("с", "s");
            translate.Add("т", "t");
            translate.Add("у", "u");
            translate.Add("ф", "f");
            translate.Add("х", "h");
            translate.Add("ц", "c");
            translate.Add("ч", "ch");
            translate.Add("ш", "sh");
            translate.Add("щ", "sch");
            translate.Add("ъ", "j");
            translate.Add("ы", "i");
            translate.Add("ь", "j");
            translate.Add("э", "e");
            translate.Add("ю", "yu");
            translate.Add("я", "ya");
            translate.Add("А", "A");
            translate.Add("Б", "B");
            translate.Add("В", "V");
            translate.Add("Г", "G");
            translate.Add("Д", "D");
            translate.Add("Е", "E");
            translate.Add("Ё", "Yo");
            translate.Add("Ж", "Zh");
            translate.Add("З", "Z");
            translate.Add("И", "I");
            translate.Add("Й", "J");
            translate.Add("К", "K");
            translate.Add("Л", "L");
            translate.Add("М", "M");
            translate.Add("Н", "N");
            translate.Add("О", "O");
            translate.Add("П", "P");
            translate.Add("Р", "R");
            translate.Add("С", "S");
            translate.Add("Т", "T");
            translate.Add("У", "U");
            translate.Add("Ф", "F");
            translate.Add("Х", "H");
            translate.Add("Ц", "C");
            translate.Add("Ч", "Ch");
            translate.Add("Ш", "Sh");
            translate.Add("Щ", "Sch");
            translate.Add("Ъ", "J");
            translate.Add("Ы", "I");
            translate.Add("Ь", "J");
            translate.Add("Э", "E");
            translate.Add("Ю", "Yu");
            translate.Add("Я", "Ya");
            translate.Add(" ", "_");
            translate.Add(",", "-");
            translate.Add(".", "-");
        }

        //Генерируем новый alias
        public static String AliasGenerator(String text, IEnumerable<object> obj)
        {
            int rep = 1;
            PrepareTranslate();
            StringBuilder buf = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (translate.ContainsKey(text[i].ToString()))
                {
                    buf.Append(translate[text[i].ToString()]);
                }
                else
                {
                    buf.Append(text[i].ToString());
                }
            }
            translate.Clear();

            String buf2 = buf.ToString();
            //проверяем есть ли такой же alias
            while (obj.Contains(buf2))
            {
                buf2 = buf + "_" + rep.ToString();
                rep++;
            }

            return buf2;
        }


        public static string GetParent(int? idParent)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                var ParentName = db.Categories.Where(a => a.Id == idParent).Select(a => a.Name).FirstOrDefault();
                return ParentName;
            }
        }

        public static string GetCategoryName(int? idCategory)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                var categoryName = db.Categories.Where(a => a.Id == idCategory).Select(a => a.Name).FirstOrDefault();
                return categoryName;
            }
            
        }
    }

}

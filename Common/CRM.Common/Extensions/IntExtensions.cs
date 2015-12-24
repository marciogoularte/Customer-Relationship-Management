namespace CRM.Common.Extensions
{
    public static class IntExtensions
    {
        public static string NumberAsWord(this int number)
        {
            return number > 9 ? Tens(number) : DigitAsWord(number);
        }

        private static string Tens(int number)
        {
            var tens = "";
            var num = number.ToString();
            if (num[num.Length - 2] == '1')
            {
                tens += DigitAsWord(int.Parse(num[num.Length - 2] + "" + num[num.Length - 1]));
            }
            else
            {
                tens += DigitAsWord(int.Parse(num[num.Length - 2] + "") * 10);
                if (number % 10 != 0)
                {
                    tens += "-" + DigitAsWord(int.Parse(num[num.Length - 1] + ""));
                }
            }
            return tens;
        }

        private static string DigitAsWord(int p)
        {
            switch (p)
            {
                case 0:
                    return "нула";
                case 1:
                    return "едно";
                case 2:
                    return "две";
                case 3:
                    return "три";
                case 4:
                    return "четири";
                case 5:
                    return "пет";
                case 6:
                    return "шест";
                case 7:
                    return "седем";
                case 8:
                    return "осем";
                case 9:
                    return "девет";
                case 10:
                    return "десет";
                case 11:
                    return "единайсет";
                case 12:
                    return "дванайсет";
                case 13:
                    return "тринайсет";
                case 14:
                    return "четиринайсет";
                case 15:
                    return "петнайсет";
                case 16:
                    return "шестнайсет";
                case 17:
                    return "седемнайсет";
                case 18:
                    return "осемнайсет";
                case 19:
                    return "деветнайсет";
                case 20:
                    return "двайсет";
                case 30:
                    return "трийсет";
                case 40:
                    return "четирийсет";
                case 50:
                    return "петдесет";
                case 60:
                    return "шейсет";
                case 70:
                    return "седемдесет";
                case 80:
                    return "осемдесет";
                case 90:
                    return "деветдесет";
            }

            return "";
        }
    }

}

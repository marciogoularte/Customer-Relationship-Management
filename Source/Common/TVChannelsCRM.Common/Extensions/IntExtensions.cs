namespace TVChannelsCRM.Common.Extensions
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
                case 0: return "нула";
                    break;
                case 1: return "едно";
                    break;
                case 2: return "две";
                    break;
                case 3: return "три";
                    break;
                case 4: return "четири";
                    break;
                case 5: return "пет";
                    break;
                case 6: return "шест";
                    break;
                case 7: return "седем";
                    break;
                case 8: return "осем";
                    break;
                case 9: return "девет";
                    break;
                case 10: return "десет";
                    break;
                case 11: return "единайсет";
                    break;
                case 12: return "дванайсет";
                    break;
                case 13: return "тринайсет";
                    break;
                case 14: return "четиринайсет";
                    break;
                case 15: return "петнайсет";
                    break;
                case 16: return "шестнайсет";
                    break;
                case 17: return "седемнайсет";
                    break;
                case 18: return "осемнайсет";
                    break;
                case 19: return "деветнайсет";
                    break;
                case 20: return "двайсет";
                    break;
                case 30: return "трийсет";
                    break;
                case 40: return "четирийсет";
                    break;
                case 50: return "петдесет";
                    break;
                case 60: return "шейсет";
                    break;
                case 70: return "седемдесет";
                    break;
                case 80: return "осемдесет";
                    break;
                case 90: return "деветдесет";
                    break;
            }

            return "";
        }
    }
}
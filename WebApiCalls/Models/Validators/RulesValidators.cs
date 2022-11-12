namespace WebApiCalls.Models.Validators
{
    public class RulesValidators
    {
        public static bool RulePhone(string phone)
        {
            string[] phoneArray = phone.Split('-');
            if (phoneArray.Length != 5 && phoneArray[0][0] != '+') return false;
            try
            {
                if (!int.TryParse(phoneArray[0][1].ToString(), out _)) return false;
                for (int i = 0; i < phoneArray.Length; i++)
                {
                    if (!int.TryParse(phoneArray[i], out _)) return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

namespace CRM.Services.Data.ViewModels.Finance.Reports
{
    public class SearchedItemDropDown
    {
        public SearchedItemDropDown()
        {
            
        }

        public SearchedItemDropDown(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}

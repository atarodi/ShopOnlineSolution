using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        public List<int> GetRandomData(int count)
        {
            Random myObject = new Random();
            var randomList=new List<int>();
            for (int i = 1; i <= count; i++)
            {
               randomList.Add(myObject.Next());
            }
            return randomList;
        }
    }
}

using Caching.Core.Interceptor;
using EasyCaching.Demo.Interceptors.dao;

namespace EasyCaching.Demo.Interceptors.Services
{
    using System.Threading.Tasks;



    public interface IAspectCoreService //: EasyCaching.Core.Internal.IEasyCaching
    {
        [CachingAble(Expiration = 10),]
        string GetCurrentUtcTime();

        [CachingPut(CacheKeyPrefix = "AspectCore")]
        string PutSomething(string str);

        [CachingEvict(IsBefore = true)]
        void DeleteSomething(int id);

        [CachingAble(Expiration = 10)]
        Task<string> GetUtcTimeAsync();

        [CachingAble(Expiration = 10)]
        Task<Demo> GetDemoAsync(int id);

        [CachingAble(Expiration = 10)]
        Task<System.Collections.Generic.List<Demo>> GetDemoListAsync(int id);


        [CachingAble(Expiration = 10)]
        Demo GetDemo(int id);
    }

    public class AspectCoreService : IAspectCoreService
    {
        private readonly IDatas _datas;

        public AspectCoreService(IDatas datas)
        {
            _datas = datas;
        }
        public void DeleteSomething(int id)
        {
            _datas.DeleteSomething(id);
        }

        public string GetCurrentUtcTime()
        {
            return _datas.GetCurrentUtcTime();
        }

        public Demo GetDemo(int id)
        {
            return _datas.GetDemo(id);
        }

        public Task<Demo> GetDemoAsync(int id)
        {
            //return Task.FromResult(new Demo { Id = id, CreateTime = System.DateTime.Now, Name = "catcher" });

            return _datas.GetDemoAsync(id);
        }

        public Task<System.Collections.Generic.List<Demo>> GetDemoListAsync(int id)
        {
            //return Task.FromResult(new System.Collections.Generic.List<Demo>() { new Demo { Id = id, CreateTime = System.DateTime.Now, Name = "catcher" } });

            return _datas.GetDemoListAsync(id);
        }

        public async Task<string> GetUtcTimeAsync()
        {
            //throw new Not


            return await Task.FromResult(System.DateTime.Now.ToString());
        }

        public async Task DeleteSomethingAsync(int id)
        {
            //await Task.Run(() => System.Console.WriteLine("Handle delete something.."));

            await _datas.GetDemoListAsync(id);
        }

        public string PutSomething(string str)
        {

            return _datas.PutSomething(str);
            //return str;
        }

    }


    public class Demo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public System.DateTime CreateTime { get; set; }
    }
}

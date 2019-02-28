using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caching.Core.Interceptor;


namespace EasyCaching.Demo.Interceptors.dao
{
    public interface IDatas
    {
        [CachingAble(Expiration = 100)]
        string GetCurrentUtcTime();

        [CachingPut(CacheKeyPrefix = "AspectCore")]
        string PutSomething(string str);

        [CachingEvict(IsBefore = true)]
        void DeleteSomething(int id);

        [CachingAble(Expiration = 100)]
        Task<string> GetUtcTimnc();

        [CachingAble(Expiration = 100)]
        Task<Services.Demo> GetDemoAsync(int id);

        [CachingAble(Expiration = 100)]
        Task<System.Collections.Generic.List<Services.Demo>> GetDemoListAsync(int id);


        [CachingAble(Expiration = 100)]
        Services.Demo GetDemo(int id);
    }

    public class Datas : IDatas
    {

        public void DeleteSomething(int id)
        {
            System.Console.WriteLine("Handle delete something..");
        }

        public Task<string> GetUtcTimnc()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUtcTime()
        {
            return System.DateTimeOffset.UtcNow.ToString();
        }

        public Services.Demo GetDemo(int id)
        {
            return new Services.Demo { Id = id, CreateTime = System.DateTime.Now, Name = "catcher" };
        }

        public Task<Services.Demo> GetDemoAsync(int id)
        {
            return Task.FromResult(new Services.Demo { Id = id, CreateTime = System.DateTime.Now, Name = "catcher" });
        }

        public Task<System.Collections.Generic.List<Services.Demo>> GetDemoListAsync(int id)
        {
            return Task.FromResult(new System.Collections.Generic.List<Services.Demo>() { new Services.Demo { Id = id, CreateTime = System.DateTime.Now, Name = "catcher" } });
        }

        public async Task<string> GetUtcTimeAsync()
        {
            return await Task.FromResult<string>(System.DateTimeOffset.UtcNow.ToString());
        }

        public async Task DeleteSomethingAsync(int id)
        {
            await Task.Run(() => System.Console.WriteLine("Handle delete something.."));
        }

        public string PutSomething(string str)
        {
            return str;
        }
    }
}

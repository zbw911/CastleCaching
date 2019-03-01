"# CastleCaching" 


#代码中主体部分Copy自 EasyCaching 项目，对其进行相应的改造。

  以支持类型于 java spring 中的 @cache 注解




#未来
  扩展 自定义策策略形式的缓存方法
  
  
  `
    [CachingAble(Expiration = 1000000, Key = "(a+2) + b + person.FirstName ", Condition = "a>1")]
        string keyGetCurrentUtcTime(int a, string b, Person person);

        [CachingPut(CacheKeyPrefix = "AspectCore", Key = "(a+2) + b + person.FirstName ")]
        string keyPutSomething(int a, string b, Person person);

        [CachingEvict(IsBefore = true, Key = "(a+2) + b + person.FirstName ")]
        void keyDeleteSomething(int a, string b, Person person);
  `
